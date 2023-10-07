using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyBankService.Application.Interfaces;

namespace MoneyBankService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;


        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        
        }

    }
}
