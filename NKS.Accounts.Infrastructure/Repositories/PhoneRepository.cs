using System;
using System.Collections.Generic;
using NKS.Accounts.Core.Interfaces;
using NKS.Accounts.Domain.Models;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Numerics;

namespace NKS.Accounts.Infrastructure.Repositories
{
    public class PhoneRepository: IPhoneRepository
    {
        private readonly IDbConnection _connection;

        public PhoneRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> CreateAsync(PhoneNumber phone)
        {
            string query = "INSERT INTO PhoneNumber (Id,AccountId,Number) VALUES(@Id,@AccountId,@Number)";
            var rowsAffected = await _connection.ExecuteAsync(query, phone);
            return rowsAffected == 1;

        }

        public async Task<bool> AssignToAccountAsync(Guid phoneId, Guid accountId)
        {
            string query = "UPDATE PhoneNumber Set AccountId=@AccountId WHERE Id=@Id";
            var rowsAffected = await _connection.ExecuteAsync(query, new
            {
                Id = phoneId,
                AccountId=accountId 
            });
            return rowsAffected == 1;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            string query = "DELETE FROM PhoneNumber WHERE Id=@Id";
            var rowsAffected = await _connection.ExecuteAsync(query, new
            {
                Id = id
            });
            return rowsAffected == 1;
        }

        public async Task<PhoneNumber> GetAsync(Guid id)
        {
            string query = "Select Id,AccountId,Number FROM PhoneNumber WHERE Id=@Id";
            var phoneNumber= await _connection.QueryFirstOrDefaultAsync<PhoneNumber>(query, new
            {
                Id = id
            });

            return phoneNumber;
        }

        public async Task<List<PhoneNumber>> GetByAccountAsync(Guid accountId)
        {
            string query = "Select Id,AccountId,Number FROM PhoneNumber WHERE Accountid=@Accountid";
            var phoneNumber = await _connection.QueryAsync<PhoneNumber>(query, new
            {
                Accountid=accountId 
            });

            return phoneNumber.ToList();
        }
    }
}