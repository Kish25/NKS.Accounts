using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NKS.Accounts.Api.Mappers;
using NKS.Accounts.Core.Interfaces;
using NKS.Accounts.Domain.Models;
using System;
using System.Threading.Tasks;

namespace NKS.Accounts.Api.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IPhoneService _phoneService;
        private readonly IAccountRequestMapper _accountRequestMapper;
        public AccountsController(IAccountService accountService, IPhoneService phoneService, IAccountRequestMapper accountRequestMapper)
        {
            _accountService = accountService;
            _phoneService = phoneService;
            _accountRequestMapper = accountRequestMapper;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Account))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create(string accountName)
        {
            if (string.IsNullOrEmpty(accountName) || string.IsNullOrWhiteSpace(accountName))
                return BadRequest("Account name required"); // as single to use REQUIRED data annotations with message on name property in request model, so for complex request, dont have to this manually.

            var account = _accountRequestMapper.Map(accountName);
            var isAccountCreated = await _accountService.CreateAsync(account);
            var url = HttpContext.Request.Host.Value + $"/{account.Id}";

            if (isAccountCreated)
                return Created(url,account);

            return BadRequest("Unable to create account");
        }

        [HttpPost("{id}/activate")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Activate(Guid id)
        {
            var account = _accountService.GetByIdAsync(id).Result;
            if (account == null)
                return NotFound("unknown account");

            account.SetStatus("Active");
            var isUpdated = await _accountService.UpdateAsync(account);
            if(isUpdated)
                return Ok();

            return BadRequest("Cannot activate account at this time, please try again");
        }

        [HttpPost("{id}/suspend")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Suspend(Guid id)
        {
            var account = _accountService.GetByIdAsync(id).Result;
            if (account == null)
                return NotFound("unknown account");

            account.SetStatus("Suspended");
            var isUpdated = await _accountService.UpdateAsync(account);
            if (isUpdated)
                return Ok();

            return BadRequest("Cannot suspend account at this time, please try again");

        }


    }
}
