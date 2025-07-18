using System;
using System.Linq;
using System.Security.Authentication;
using AutoMapper;
using NuciAPI.Requests;
using NuciDAL.Repositories;
using NuciExtensions;
using NuciLog.Core;
using ProfiBotServer.Api.Requests;
using ProfiBotServer.Api.Responses;
using ProfiBotServer.DataAccess.DataObjects;
using ProfiBotServer.Logging;
using ProfiBotServer.Service.Models;

namespace ProfiBotServer.Service
{
    public class QrCodeService(
        IFileRepository<QrCodeEntity> qrCodeRepository,
        IFileRepository<UserEntity> userRepository,
        IMapper mapper,
        ILogger logger) : IQrCodeService
    {
        public void Add(AddQrCodeRequest request)
        {
            logger.Info(
                MyOperation.AddQrCode,
                OperationStatus.Started,
                new LogInfo(MyLogInfoKey.UserId, request.UserPhoneNumber),
                new LogInfo(MyLogInfoKey.QrCodeId, request.Id));

            ValidateRequest(request?.UserPhoneNumber, request);

            QrCode qrCode = new()
            {
                Id = request.Id,
                Description = request.Description,
                StoreName = request.StoreName,
                StoreType = StoreType.FromId(request.StoreType),
                IsEnabled = request.IsEnabled
            };

            qrCodeRepository.Add(mapper.Map<QrCodeEntity>(qrCode));
            qrCodeRepository.ApplyChanges();

            logger.Debug(
                MyOperation.AddQrCode,
                OperationStatus.Success,
                new LogInfo(MyLogInfoKey.UserId, request.UserPhoneNumber),
                new LogInfo(MyLogInfoKey.QrCodeId, qrCode.Id));
        }

        public GetQrCodeResponse GetRandomEnabled(GetQrCodeRequest request)
        {
            logger.Info(
                MyOperation.GetQrCode,
                OperationStatus.Started,
                new LogInfo(MyLogInfoKey.UserId, request.UserPhoneNumber));

            ValidateRequest(request?.UserPhoneNumber, request);

            User user = mapper.Map<User>(userRepository.Get(request.UserPhoneNumber));
            QrCode qrCode = mapper.Map<QrCode>(qrCodeRepository.GetAll().Where(qr => qr.IsEnabled).GetRandomElement());

            GetQrCodeResponse response = new()
            {
                Id = qrCode.Id,
                Url = qrCode.Url,
                Description = qrCode.Description,
                StoreName = qrCode.StoreName,
                StoreType = qrCode.StoreType,
                IsEnabled = qrCode.IsEnabled
            };
            response.SignHMAC(user.Password);

            logger.Debug(
                MyOperation.GetQrCode,
                OperationStatus.Success,
                new LogInfo(MyLogInfoKey.UserId, request.UserPhoneNumber),
                new LogInfo(MyLogInfoKey.QrCodeId, qrCode.Id));

            return response;
        }

        void ValidateRequest<TRequest>(string userId, TRequest request) where TRequest : NuciApiRequest
        {
            UserEntity userEntity = userRepository.TryGet(userId);

            if (userEntity is null)
            {
                AuthenticationException ex = new($"User with phone number {userId} not found.");

                logger.Error(
                    MyOperation.GetQrCode,
                    OperationStatus.Failure,
                    ex,
                    new LogInfo(MyLogInfoKey.UserId, userId));

                throw ex;
            }

            User user = mapper.Map<User>(userEntity);

            try
            {
                request.ValidateHMAC(user.Password);
            }
            catch (Exception ex)
            {
                logger.Error(
                    MyOperation.GetQrCode,
                    OperationStatus.Failure,
                    ex,
                    new LogInfo(MyLogInfoKey.UserId, userId));

                throw;
            }
        }
    }
}
