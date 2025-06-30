using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using AutoMapper;
using NuciAPI.Requests;
using NuciDAL.Repositories;
using NuciLog.Core;
using NuciSecurity.HMAC;
using ProfiBotServer.Api.Requests;
using ProfiBotServer.DataAccess.DataObjects;
using ProfiBotServer.Logging;
using ProfiBotServer.Service.Models;
using ProfiBotServer.Service.Notifications;

namespace ProfiBotServer.Service
{
    public class PrizeService(
        ISmtpNotifier smtpNotifier,
        IFileRepository<PrizeEntity> prizeRepository,
        IFileRepository<UserEntity> userRepository,
        IMapper mapper,
        ILogger logger) : IPrizeService
    {
        public void RecordPrize(RecordPrizeRequest request)
        {
            logger.Info(
                MyOperation.RecordPrize,
                OperationStatus.Started,
                new LogInfo(MyLogInfoKey.UserId, request.UserPhoneNumber));

            ValidateRequest(request?.UserPhoneNumber, request);

            Prize prize = mapper.Map<Prize>(request);
            prize.Id = Guid.NewGuid().ToString();

            prizeRepository.Add(mapper.Map<PrizeEntity>(prize));
            prizeRepository.ApplyChanges();

            User user = mapper.Map<User>(userRepository.TryGet(request.UserPhoneNumber));

            Task.Run(() => NotifyPrize(user, prize));

            logger.Debug(
                MyOperation.RecordPrize,
                OperationStatus.Success,
                new LogInfo(MyLogInfoKey.UserId, request.UserPhoneNumber),
                new LogInfo(MyLogInfoKey.PrizeId, prize.Id));
        }

        void NotifyPrize(User user, Prize prize)
        {
            if (!string.IsNullOrWhiteSpace(user.SmtpNotificationRecipient))
            {
                NotifyPrizeViaSmtp(user.SmtpNotificationRecipient, prize);
            }
        }

        void NotifyPrizeViaSmtp(string recipient, Prize prize)
        {
            logger.Info(
                MyOperation.SmtpNotification,
                OperationStatus.Started,
                new LogInfo(MyLogInfoKey.PrizeId, prize.Id),
                new LogInfo(MyLogInfoKey.Recipient, recipient));

            try
            {
                smtpNotifier.Send(
                    recipient,
                    "Profi Prize Won!",
                    $"User ID: {prize.UserId}\nPrize ID: {prize.Id}\nTimestamp: {prize.Timestamp}");
            }
            catch (Exception ex)
            {
                logger.Error(
                    MyOperation.SmtpNotification,
                    OperationStatus.Failure,
                    ex,
                    new LogInfo(MyLogInfoKey.PrizeId, prize.Id),
                    new LogInfo(MyLogInfoKey.Recipient, recipient));

                throw;
            }

            logger.Debug(
                MyOperation.SmtpNotification,
                OperationStatus.Success,
                new LogInfo(MyLogInfoKey.PrizeId, prize.Id),
                new LogInfo(MyLogInfoKey.Recipient, recipient));
        }

        void ValidateRequest<TRequest>(string userId, TRequest request) where TRequest : Request
        {
            UserEntity userEntity = userRepository.TryGet(userId);

            if (userEntity is null)
            {
                AuthenticationException ex = new($"User with phone number {userId} not found.");

                logger.Error(
                    MyOperation.RecordPrize,
                    OperationStatus.Failure,
                    ex,
                    new LogInfo(MyLogInfoKey.UserId, userId));

                throw ex;
            }

            User user = mapper.Map<User>(userEntity);

            try
            {
                HmacValidator.Validate(request.HmacToken, request, user.Password);
            }
            catch (Exception ex)
            {
                logger.Error(
                    MyOperation.RecordPrize,
                    OperationStatus.Failure,
                    ex,
                    new LogInfo(MyLogInfoKey.UserId, userId));

                throw;
            }
        }
    }
}
