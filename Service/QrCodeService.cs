using System;
using System.Security.Authentication;
using AutoMapper;
using NuciAPI.Requests;
using NuciDAL.Repositories;
using NuciLog.Core;
using NuciSecurity.HMAC;
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
        public GetQrCodeResponse GetRandom(GetQrCodeRequest request)
        {
            logger.Info(
                MyOperation.GetQrCode,
                OperationStatus.Started,
                new LogInfo(MyLogInfoKey.UserId, request.UserPhoneNumber));

            ValidateRequest(request?.UserPhoneNumber, request);

            User user = mapper.Map<User>(userRepository.Get(request.UserPhoneNumber));
            QrCode qrCode = mapper.Map<QrCode>(qrCodeRepository.GetRandom());

            GetQrCodeResponse response = new()
            {
                Id = qrCode.Id,
                Url = qrCode.Url,
                Description = qrCode.Description
            };
            response.HmacToken = HmacEncoder.GenerateToken(response, user.Password);

            logger.Debug(
                MyOperation.GetQrCode,
                OperationStatus.Success,
                new LogInfo(MyLogInfoKey.UserId, request.UserPhoneNumber),
                new LogInfo(MyLogInfoKey.QrCodeId, qrCode.Id));

            return response;
        }

        void ValidateRequest<TRequest>(string userId, TRequest request) where TRequest : Request
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
                HmacValidator.Validate(request.HmacToken, request, user.Password);
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
