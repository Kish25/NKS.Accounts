using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NKS.Accounts.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using NKS.Accounts.Api.Mappers;
using NKS.Accounts.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NKS.Accounts.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PhoneNumbersController : ControllerBase
    {
        private readonly IPhoneService _phoneService;
        private readonly IAccountService _accountService;
        private readonly IPhoneNumberMapper _phoneNumberMapper;

        public PhoneNumbersController(IPhoneService phoneService, IPhoneNumberMapper phoneNumberMapper, IAccountService accountService)
        {
            _phoneService = phoneService;
            _phoneNumberMapper = phoneNumberMapper;
            _accountService = accountService;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Account))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create(string number)
        {
            if (string.IsNullOrEmpty(number) || string.IsNullOrWhiteSpace(number))
                return BadRequest("Phone number is required"); // as single to use REQUIRED data annotations with message on name property in request model, so for complex request, dont have to this manually.

            var phoneNumber = _phoneNumberMapper.Map(number);
            var isNumberCreated = await _phoneService.CreateAsync(phoneNumber);
            var url = HttpContext.Request.Host.Value + $"/{phoneNumber.Id}";

            if (isNumberCreated)
                return Created(url, phoneNumber);

            return BadRequest("Unable to add phone number");
        }

        [HttpPost("/AssignToAccount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AssignToAccount(Guid phoneId, Guid accountId)
        {
            var phoneNumber = await _phoneService.GetByIdAsync(phoneId);
            if (phoneNumber == null)
                return BadRequest("can't find given number");
            
            var isSetToAccount = phoneNumber.SetToAccount(accountId);

            if (!isSetToAccount)
                return BadRequest("number is already assign to either same or different account.");

            var isSucess =await _phoneService.AssignToAccountAsync(accountId, phoneId);
            if (!isSucess)
                return BadRequest("Unable to assign phone to account, please try agian. ");

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(PhoneNumber))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var phoneNumber = await _phoneService.GetByIdAsync(id);
            if (phoneNumber == null)
                return NotFound("can't find given number");

            return Ok(phoneNumber);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PhoneNumber))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("/account/{accountId}")]
        public async Task<ActionResult> GetByAccount(Guid accountId)
        {
            var account = await _accountService.GetByIdAsync(accountId);
            if (account is null )
                return NotFound("can't find account");

            var phoneNumber = await _phoneService.GetByAccountAsync(accountId);
            if (phoneNumber is null || phoneNumber.Count==0)
                return NotFound("can't find any numbers assigned to account");

            var result = _phoneNumberMapper.Map(account, phoneNumber);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PhoneNumber))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result  = await _phoneService.DeleteAsync(id);

            if (!result)
                return NotFound("Number not found or unable to delete");

            return Ok();
        }
    }
}
