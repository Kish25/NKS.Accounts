using Dapper;
using NKS.Accounts.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using NKS.Accounts.Domain.Models;

namespace NKS.Accounts.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {

        private readonly IDbConnection _connection;

        public AccountRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> CreateAsync(Account account)
        {
            string query = "INSERT INTO Accounts(Id,Name,Status) VALUES (@Id,@Name,@Status)";
            var rowsAffected = await _connection.ExecuteAsync(query, account);
            return rowsAffected == 1;
        }

        public async Task<bool> UpdateAsync(Account account)
        {
            string query = "UPDATE Accounts SET Status=@Status WHERE Id = @Id";
            var rowsAffected = await _connection.ExecuteAsync(query, account);
            return rowsAffected > 0;
        }

        public async Task<Account> GetAsync(Guid id)
        {
            string query = "Select id,name,Status from Accounts WHERE Id = @id";
            var account = await _connection.QueryFirstOrDefaultAsync<Account>(query, new
            {
                Id = id
            });
            return account ;
        }
        public async Task<List<Account>> GetAllAsync(Guid id)
        {
            return new List<Account>();
        }

    }
}