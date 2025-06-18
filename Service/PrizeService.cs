using System;
using System.Linq;
using System.Security.Authentication;
using AutoMapper;
using NuciAPI.Requests;
using NuciDAL.Repositories;
using NuciLog.Core;
using NuciSecurity.HMAC;
using ProfiBotServer.Api.Requests;
using ProfiBotServer.DataAccess.DataObjects;
using ProfiBotServer.Logging;
using ProfiBotServer.Service.Models;

namespace ProfiBotServer.Service
{
    public class PrizeService(
        IRepository<PrizeEntity> prizeRepository,
        IRepository<UserEntity> userRepository,
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

            logger.Debug(
                MyOperation.RecordPrize,
                OperationStatus.Success,
                new LogInfo(MyLogInfoKey.UserId, request.UserPhoneNumber),
                new LogInfo(MyLogInfoKey.PrizeId, prize.Id));
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
