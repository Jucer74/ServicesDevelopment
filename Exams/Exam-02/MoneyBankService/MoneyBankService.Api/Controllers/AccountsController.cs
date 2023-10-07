using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyBankService.Api.Dto;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public AccountsController(IAccountService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }

    // Recupera todas las cuentas
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Obtiene todas las cuentas y las mapea a DTOs
        var accounts = await _accountService.GetAllAccounts() as List<Account>;
        return Ok(_mapper.Map<List<Account>, List<AccountDto>>(accounts!));
    }

    // Recupera una cuenta por número de cuenta
    [HttpGet("ByAccountNumber")]
    public async Task<IActionResult> GetByAccountNumber([FromQuery] string accountNumber)
    {
        // Encuentra cuentas por número de cuenta y las mapea a DTOs
        var accounts = await _accountService.FindAccountsByAccountNumber(accountNumber) as List<Account>;
        return Ok(_mapper.Map<List<Account>, List<AccountDto>>(accounts!));
    }

    // Recupera una cuenta por ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        // Obtiene una cuenta por ID y la mapea a un DTO
        var account = await _accountService.GetAccountById(id);
        return Ok(_mapper.Map<Account, AccountDto>(account));
    }

    // Crea una nueva cuenta
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AccountDto accountDto)
    {
        // Mapea el DTO a una entidad de cuenta y luego devuelve el DTO mapeado
        var account = _mapper.Map<AccountDto, Account>(accountDto);
        return Ok(_mapper.Map<Account, AccountDto>(await _accountService.CreateAccount(account)));
    }

    // Actualiza una cuenta existente por ID
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AccountDto accountDto)
    {
        // Mapea el DTO a una entidad de cuenta y luego devuelve el DTO mapeado
        var account = _mapper.Map<AccountDto, Account>(accountDto);
        return Ok(_mapper.Map<Account, AccountDto>(await _accountService.UpdateAccount(id, account)));
    }

    // Elimina una cuenta por ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        // Elimina una cuenta por ID
        await _accountService.DeleteAccount(id);
        return Ok();
    }

    // Realiza un depósito en una cuenta
    [HttpPut("{id}/Deposit")]
    public async Task<IActionResult> Deposit(int id, [FromBody] TransactionDto transactionDto)
    {
        // Realiza un depósito en la cuenta
        await _accountService.Deposit(id, transactionDto);
        return Ok();
    }

    // Realiza un retiro de una cuenta
    [HttpPut("{id}/Withdraw")]
    public async Task<IActionResult> Withdraw(int id, [FromBody] TransactionDto transactionDto)
    {
        // Realiza un retiro de la cuenta
        await _accountService.Withdraw(id, transactionDto);
        return Ok();
    }
}
