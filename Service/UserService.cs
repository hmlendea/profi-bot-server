using System;
using System.Security.Authentication;
using AutoMapper;
using NuciAPI.Requests;
using NuciDAL.Repositories;
using NuciLog.Core;
using ProfiBotServer.Api.Requests;
using ProfiBotServer.Api.Responses;
using ProfiBotServer.DataAccess.DataObjects;
using ProfiBotServer.Logging;
using ProfiBotServer.Service.Models;

namespace ProfiBotServer.Service
{
    public class UserService(
        IFileRepository<UserEntity> userRepository,
        IMapper mapper,
        ILogger logger) : IUserService
    {
        public GetUserBalanceResponse GetBalance(GetUserBalanceRequest request)
        {
            logger.Info(
                MyOperation.GetBalance,
                OperationStatus.Started,
                new LogInfo(MyLogInfoKey.UserId, request.UserPhoneNumber));

            ValidateRequest(MyOperation.GetBalance, request?.UserPhoneNumber, request);

            User user = mapper.Map<User>(userRepository.Get(request.UserPhoneNumber));

            GetUserBalanceResponse response = new()
            {
                UserPhoneNumber = user.Id,
                Balance = user.Balance
            };
            response.SignHMAC(user.Password);

            logger.Debug(
                MyOperation.GetBalance,
                OperationStatus.Success,
                new LogInfo(MyLogInfoKey.UserId, request.UserPhoneNumber),
                new LogInfo(MyLogInfoKey.Balance, response.Balance));

            return response;
        }

        public void UpdateBalance(UpdateUserBalanceRequest request)
        {
            logger.Info(
                MyOperation.UpdateBalance,
                OperationStatus.Started,
                new LogInfo(MyLogInfoKey.UserId, request.UserPhoneNumber),
                new LogInfo(MyLogInfoKey.Balance, request.Balance));

            ValidateRequest(MyOperation.UpdateBalance, request?.UserPhoneNumber, request);

            UserEntity user = userRepository.Get(request.UserPhoneNumber);
            user.Balance = request.Balance;

            userRepository.Update(user);
            userRepository.ApplyChanges();

            logger.Debug(
                MyOperation.UpdateBalance,
                OperationStatus.Success,
                new LogInfo(MyLogInfoKey.UserId, request.UserPhoneNumber),
                new LogInfo(MyLogInfoKey.Balance, request.Balance));
        }

        void ValidateRequest<TRequest>(Operation operation, string userId, TRequest request) where TRequest : NuciApiRequest
        {
            UserEntity userEntity = userRepository.TryGet(userId);

            if (userEntity is null)
            {
                AuthenticationException ex = new($"User with phone number {userId} not found.");

                logger.Error(
                    operation,
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
                    operation,
                    OperationStatus.Failure,
                    ex,
                    new LogInfo(MyLogInfoKey.UserId, userId));

                throw;
            }
        }
    }
}
