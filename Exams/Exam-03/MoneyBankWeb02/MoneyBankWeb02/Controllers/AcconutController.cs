using Microsoft.AspNetCore.Mvc;
using MoneyBankWeb02.Services;

namespace MoneyBankWeb02.Controllers
{
    public class AccountsController : Controller
    {
        
            private readonly AccountService _accountService;

            public AccountsController(AccountService accountService)
            {
                _accountService = accountService;
            }

            public async Task<IActionResult> Index()
            {
                // Obtiene la lista de cuentas a través del servicio
                var accounts = await _accountService.GetAccountsAsync();
                return View(accounts);
            }
        }
    }

