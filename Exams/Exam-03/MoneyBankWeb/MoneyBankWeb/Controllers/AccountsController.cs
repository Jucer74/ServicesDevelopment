using Microsoft.AspNetCore.Mvc;
using MoneyBankWeb_josoterad.Models;
using MoneyBankWeb_josoterad.Services;
using System.Threading.Tasks;

namespace MoneyBankWeb_josoterad.Controllers
{
    public class AccountsController : Controller
    {
        private readonly AccountService _accountService;

        public AccountsController()
        {
            _accountService = new AccountService();
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            var accounts = await _accountService.GetAllAsync();
            return View(accounts);
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var account = await _accountService.GetByIdAsync(id);
            if (account == null) return NotFound();
            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountDto account)
        {
            if (!ModelState.IsValid) return View(account);

            var success = await _accountService.CreateAsync(account);
            if (!success) return View(account);

            return RedirectToAction(nameof(Index));
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var account = await _accountService.GetByIdAsync(id);
            if (account == null) return NotFound();
            return View(account);
        }

        // POST: Accounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AccountDto account)
        {
            if (!ModelState.IsValid) return View(account);

            var success = await _accountService.UpdateAsync(account);
            if (!success) return View(account);

            return RedirectToAction(nameof(Index));
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var account = await _accountService.GetByIdAsync(id);
            if (account == null) return NotFound();
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _accountService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
