using Microsoft.AspNetCore.Mvc;
using MoneyBankWeb_davhergar.Models;
using MoneyBankWeb_davhergar.Services;

namespace MoneyBankWeb_davhergar.Controllers
{
    public class AccountsController : Controller
    {
        private readonly MoneyBankApiService _apiService;

        public AccountsController(MoneyBankApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index(string? accountNumber)
        {
            var accounts = await _apiService.GetAccountsAsync(accountNumber);
            return View(accounts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var account = await _apiService.GetAccountAsync(id);
            if (account == null) return NotFound();
            return View(account);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AccountDto account)
        {
            if (!ModelState.IsValid) return View(account);
            var created = await _apiService.CreateAccountAsync(account);
            if (created == null) return View(account);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var account = await _apiService.GetAccountAsync(id);
            if (account == null) return NotFound();
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AccountDto account)
        {
            if (!ModelState.IsValid) return View(account);
            var success = await _apiService.UpdateAccountAsync(account);
            if (!success) return View(account);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var account = await _apiService.GetAccountAsync(id);
            if (account == null) return NotFound();
            return View(account);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiService.DeleteAccountAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Deposit(int id)
        {
            var account = await _apiService.GetAccountAsync(id);
            if (account == null) return NotFound();
            return View(new TransactionDto { AccountNumber = account.AccountNumber });
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(int id, TransactionDto transaction)
        {
            if (!ModelState.IsValid) return View(transaction);
            var success = await _apiService.DepositAsync(id, transaction);
            if (!success) return View(transaction);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Withdrawal(int id)
        {
            var account = await _apiService.GetAccountAsync(id);
            if (account == null) return NotFound();
            return View(new TransactionDto { AccountNumber = account.AccountNumber });
        }

        [HttpPost]
        public async Task<IActionResult> Withdrawal(int id, TransactionDto transaction)
        {
            if (!ModelState.IsValid) return View(transaction);
            var success = await _apiService.WithdrawalAsync(id, transaction);
            if (!success) return View(transaction);
            return RedirectToAction(nameof(Index));
        }
    }
}
