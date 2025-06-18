using System;
using System.Linq;
using System.Security.Authentication;
using AutoMapper;
using NuciDAL.Repositories;
using NuciSecurity.HMAC;
using ProfiBotServer.Api.Requests;
using ProfiBotServer.DataAccess.DataObjects;
using ProfiBotServer.Service.Models;

namespace ProfiBotServer.Service
{
    public class PrizeService(
        IRepository<PrizeEntity> prizeRepository,
        IRepository<UserEntity> userRepository,
        IMapper mapper) : IPrizeService
    {
        public void RecordPrize(RecordPrizeRequest request)
        {
            ValidateRequest(request);

            Prize prize = mapper.Map<Prize>(request);
            prize.Id = Guid.NewGuid().ToString();

            prizeRepository.Add(mapper.Map<PrizeEntity>(prize));
            prizeRepository.ApplyChanges();
        }

        void ValidateRequest(RecordPrizeRequest request)
        {
            Console.WriteLine(userRepository.GetAll().Count());
            UserEntity userEntity = userRepository.TryGet(request.UserPhoneNumber) ?? throw new AuthenticationException($"User with phone number {request.UserPhoneNumber} not found.");
            User user = mapper.Map<User>(userEntity);
            HmacValidator.Validate(request.HmacToken, request, user.Password);
        }
    }
}
