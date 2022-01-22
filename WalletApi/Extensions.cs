﻿using System;
using WalletApi.DTOs;
using WalletApi.Entities;

namespace WalletApi
{
    public static class Extensions
    {
        public static UserInfoDto AsDto(this User user, string requestGuid, Status status)
        {
            return new UserInfoDto
            {
                User = user.UserName,
                BirthDate = user.BirthDate.ToString("yyyy-MM-dd"),
                Country = user.Country.ToString(),
                RequestUuid = requestGuid,
                Status = status.ToString(),
                RegistrationDate = user.RegistrationDate.ToString("yyyy-MM-dd")
            };
        }

        public static UserBalanceDto AsBalanceDto(this User user, string requestGuid, Status status)
        {
            return new UserBalanceDto()
            {
                User = user.UserName,
                Status = status.ToString(),
                RequestUuid = requestGuid,
                Currency = user.Currency.ToString(),
                Balance = user.Balance
            };
        }
    }
}