// IMPORTANTE: Reemplaza 'TU_IP_O_DOMINIO_EC2' con la IP pública de tu instancia EC2 o tu dominio si tienes uno configurado.
// Basado en tu deploy.yml, tu API Dockerizada escucha en el puerto 80 dentro de la red 'awsdeploy'.
// Si accedes directamente desde internet, será el puerto 80 de tu EC2 (asumiendo que lo tienes mapeado).
const API_BASE_URL = 'http://44.201.83.128/api/accounts';
const MAX_OVERDRAFT_FRONTEND = 1000000; // Debe coincidir con el MAX_OVERDRAFT del backend

// Elementos del DOM
const loadAccountsBtn = document.getElementById('load-accounts-btn');
const accountsTableBody = document.getElementById('accounts-table-body');
const accountsLoadingDiv = document.getElementById('accounts-loading');

const accountForm = document.getElementById('account-form');
const accountIdInput = document.getElementById('accountId');
const accountNumberInput = document.getElementById('accountNumber');
const ownerNameInput = document.getElementById('ownerName');
const accountTypeInput = document.getElementById('accountType');
const balanceAmountInput = document.getElementById('balanceAmount'); // Este es el Saldo Inicial (Aporte Propio) en el form
const formTitle = document.getElementById('form-title');
const formFeedback = document.getElementById('form-feedback');
const clearFormBtn = document.getElementById('clear-form-btn');
const formSection = document.getElementById('form-section'); // Asegúrate que esta línea exista (la agregamos antes)

const transactionForm = document.getElementById('transaction-form');
const transactionAccountIdInput = document.getElementById('transactionAccountId');
const transactionAccountNumberInput = document.getElementById('transactionAccountNumber');
const transactionValueInput = document.getElementById('transactionValue');
const depositBtn = document.getElementById('deposit-btn');
const withdrawBtn = document.getElementById('withdraw-btn');
const transactionFeedback = document.getElementById('transaction-feedback');


// --- Funciones de Feedback ---
function showFeedback(element, message, isError = false) {
    element.textContent = message;
    element.className = isError ? 'error' : 'success';
    element.style.display = 'block';
    setTimeout(() => {
        element.style.display = 'none';
        element.textContent = '';
    }, 5000); // Ocultar después de 5 segundos
}

function showLoading(show) {
    accountsLoadingDiv.style.display = show ? 'block' : 'none';
}

// --- Lógica para Cuentas ---

async function fetchAccounts() {
    showLoading(true);
    accountsTableBody.innerHTML = ''; // Limpiar antes de cargar
    try {
        const response = await fetch(API_BASE_URL);
        if (!response.ok) {
            const errorData = await response.json().catch(() => ({ Errors: ["Error desconocido del servidor."] }));
            throw new Error(`Error ${response.status}: ${(errorData.Errors || [response.statusText]).join(', ')}`);
        }
        const accounts = await response.json();
        
        if (accounts.length === 0) {
            accountsTableBody.innerHTML = '<tr><td colspan="9" style="text-align:center;">No hay cuentas para mostrar.</td></tr>'; // colspan actualizado a 9
        } else {
            accounts.forEach(account => {
                const row = accountsTableBody.insertRow();
                // Formatear fecha
                const creationDate = new Date(account.creationDate).toLocaleDateString('es-ES', {
                    year: 'numeric', month: '2-digit', day: '2-digit'
                });

                let saldoPropio = 0;
                let sobregiroUtilizado = parseFloat(account.overdraftAmount);
                let cupoSobregiroDisp = 0;
                let saldoActualNeto = parseFloat(account.balanceAmount); // Este es el balanceAmount de la BD

                if (account.accountType === 'C') {
                    // El balanceAmount de la BD para cuentas 'C' incluye el MAX_OVERDRAFT
                    // Saldo Propio = Balance BD - MAX_OVERDRAFT_FRONTEND
                    saldoPropio = saldoActualNeto - MAX_OVERDRAFT_FRONTEND;
                    cupoSobregiroDisp = MAX_OVERDRAFT_FRONTEND - sobregiroUtilizado;
                } else { // Para cuentas 'A' (Ahorros)
                    saldoPropio = saldoActualNeto; // El balance de la BD es el saldo propio
                    sobregiroUtilizado = 0; // No tienen sobregiro
                    cupoSobregiroDisp = 0; // No tienen sobregiro
                }
                
                // Añadir clase si está en sobregiro para estilos
                if (account.accountType === 'C' && sobregiroUtilizado > 0) {
                    row.classList.add('overdraft-active');
                }

                row.innerHTML = `
                    <td>${account.id}</td>
                    <td>${account.accountNumber}</td>
                    <td>${account.ownerName}</td>
                    <td>${account.accountType === 'A' ? 'Ahorros' : 'Corriente'}</td>
                    <td>${parseFloat(saldoPropio).toFixed(2)}</td>
                    <td>${parseFloat(sobregiroUtilizado).toFixed(2)}</td>
                    <td>${parseFloat(cupoSobregiroDisp).toFixed(2)}</td>
                    <td>${creationDate}</td>
                    <td>
                        <button onclick="loadAccountForEdit(${account.id})">Editar</button>
                        <button class="delete-btn" onclick="deleteAccount(${account.id})">Eliminar</button>
                    </td>
                `;
            });
        }
    } catch (error) {
        console.error('Error al obtener cuentas:', error);
        showFeedback(formFeedback, `Error al cargar cuentas: ${error.message}`, true);
        accountsTableBody.innerHTML = `<tr><td colspan="9" style="text-align:center;">Error al cargar cuentas: ${error.message}</td></tr>`; // colspan actualizado a 9
    } finally {
        showLoading(false);
    }
}

function clearAccountForm() {
    accountForm.reset();
    accountIdInput.value = '';
    formTitle.textContent = 'Crear Nueva Cuenta';
    formFeedback.style.display = 'none';
    balanceAmountInput.readOnly = false; 
    // No es necesario resetear un campo de sobregiro en el formulario ya que no lo tenemos para edición directa.
}

async function handleAccountFormSubmit(event) {
    event.preventDefault();
    formFeedback.style.display = 'none';

    const accountId = accountIdInput.value;
    // El valor de balanceAmountInput es el "Saldo Inicial (Aporte Propio)"
    const aportePropio = parseFloat(balanceAmountInput.value);


    const accountData = {
        accountNumber: accountNumberInput.value,
        ownerName: ownerNameInput.value,
        accountType: accountTypeInput.value,
        balanceAmount: aportePropio // Para POST, este es el saldo inicial propio. El backend ajustará.
                                  
    };

    let url = API_BASE_URL;
    let method = 'POST';

    if (accountId) { // Actualización
        accountData.id = parseInt(accountId);
        url = `${API_BASE_URL}/${accountId}`;
        method = 'PUT';
        
    
        const currentAccountResponse = await fetch(`${API_BASE_URL}/${accountId}`);
        if (!currentAccountResponse.ok) throw new Error('No se pudo obtener los datos actuales de la cuenta para actualizar.');
        const currentAccountData = await currentAccountResponse.json();

        accountData.balanceAmount = parseFloat(currentAccountData.balanceAmount); // Enviar el balanceAmount de la BD
        accountData.overdraftAmount = parseFloat(currentAccountData.overdraftAmount); // Enviar el overdraftAmount de la BD
        
        // Si se quisiera permitir cambiar el tipo de cuenta y esto afecta el sobregiro:
        if (accountData.accountType === 'A' && currentAccountData.accountType === 'C') {
             // Si cambia de Corriente a Ahorros, el sobregiro debe eliminarse.
             // El `balanceAmount` debería ser el saldo propio (balance BD - MAX_OVERDRAFT)
            accountData.balanceAmount = parseFloat(currentAccountData.balanceAmount) - MAX_OVERDRAFT_FRONTEND;
            accountData.overdraftAmount = 0;
        } else if (accountData.accountType === 'C' && currentAccountData.accountType === 'A') {
            // Si cambia de Ahorros a Corriente, se añade el cupo de sobregiro al balance
            accountData.balanceAmount = parseFloat(currentAccountData.balanceAmount) + MAX_OVERDRAFT_FRONTEND;
            accountData.overdraftAmount = 0; // Inicialmente no hay sobregiro utilizado
        }


    }


    try {
        const response = await fetch(url, {
            method: method,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(accountData),
        });

        if (!response.ok) {
            const errorData = await response.json().catch(() => ({ Errors: ["Error al procesar la solicitud."] }));
            throw new Error(`Error ${response.status}: ${(errorData.Errors || [response.statusText]).join(', ')}`);
        }
        
        const resultMessage = `Cuenta ${accountId ? 'actualizada' : 'creada'} exitosamente!`;
        showFeedback(formFeedback, resultMessage, false);
        clearAccountForm();
        fetchAccounts();
    } catch (error) {
        console.error('Error al guardar cuenta:', error);
        showFeedback(formFeedback, `Error al guardar cuenta: ${error.message}`, true);
    }
}

async function loadAccountForEdit(id) {
    clearAccountForm(); 
    formFeedback.style.display = 'none';
    try {
        const response = await fetch(`${API_BASE_URL}/${id}`);
        if (!response.ok) {
            const errorData = await response.json().catch(() => ({ Errors: ["Error al obtener detalles de la cuenta."] }));
            throw new Error(`Error ${response.status}: ${(errorData.Errors || [response.statusText]).join(', ')}`);
        }
        const account = await response.json(); // Este account tiene balanceAmount y overdraftAmount de la BD
        
        formTitle.textContent = `Editar Cuenta (ID: ${account.id})`;
        accountIdInput.value = account.id;
        accountNumberInput.value = account.accountNumber;
        ownerNameInput.value = account.ownerName;
        accountTypeInput.value = account.accountType;

        // El campo 'balanceAmountInput' en el form es para "Saldo Inicial (Aporte Propio)"
        // Necesitamos calcular el aporte propio original para mostrarlo
        let aportePropioEditable = parseFloat(account.balanceAmount);
        if (account.accountType === 'C') {
            aportePropioEditable = parseFloat(account.balanceAmount) - MAX_OVERDRAFT_FRONTEND;
        }
        balanceAmountInput.value = aportePropioEditable.toFixed(2);
        
        // El balance y sobregiro no son directamente editables en este formulario simplificado.
        // La edición del tipo de cuenta (si cambia de/hacia Corriente) se manejará en handleAccountFormSubmit.
        balanceAmountInput.readOnly = false; // O true si decides no permitir editar el aporte propio una vez creado.

        window.scrollTo({ top: formSection.offsetTop, behavior: 'smooth' });

    } catch (error) {
        console.error('Error al cargar cuenta para editar:', error);
        showFeedback(formFeedback, `Error al cargar cuenta: ${error.message}`, true);
    }
}

async function deleteAccount(id) {
    if (!confirm(`¿Estás seguro de que quieres eliminar la cuenta con ID ${id}?`)) {
        return;
    }
    formFeedback.style.display = 'none';
    try {
        const response = await fetch(`${API_BASE_URL}/${id}`, { method: 'DELETE' });
        if (!response.ok && response.status !== 204) { // 204 No Content es éxito para DELETE
            // Intenta leer el cuerpo del error solo si no es 204
            let errorData = { Errors: ["Error al eliminar cuenta."] };
            if (response.headers.get("content-type")?.includes("application/json")) {
                 errorData = await response.json().catch(() => errorData);
            }
            throw new Error(`Error ${response.status}: ${(errorData.Errors || [response.statusText]).join(', ')}`);
        }
        showFeedback(formFeedback, 'Cuenta eliminada exitosamente!', false);
        fetchAccounts();
    } catch (error) {
        console.error('Error al eliminar cuenta:', error);
        showFeedback(formFeedback, `Error al eliminar cuenta: ${error.message}`, true);
    }
}


// --- Lógica para Transacciones ---

async function handleTransaction(type) { // type será 'Deposit' o 'Withdrawal'
    transactionFeedback.style.display = 'none';
    const accountId = transactionAccountIdInput.value;
    const accountNumber = transactionAccountNumberInput.value;
    const valueAmount = parseFloat(transactionValueInput.value);

    if (!accountId || !accountNumber || isNaN(valueAmount) || valueAmount <= 0) {
        showFeedback(transactionFeedback, 'Por favor, completa todos los campos de transacción correctamente y con un monto válido.', true);
        return;
    }

    const transactionData = { 
        id: parseInt(accountId), 
        accountNumber: accountNumber,
        valueAmount: valueAmount
    };

    const url = `${API_BASE_URL}/${accountId}/${type}`;

    try {
        const response = await fetch(url, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(transactionData),
        });

        if (!response.ok && response.status !== 204) { 
            let errorData = { Errors: [`Error al procesar ${type.toLowerCase()}.`] };
             if (response.headers.get("content-type")?.includes("application/json")) {
                 errorData = await response.json().catch(() => errorData);
            }
            throw new Error(`Error ${response.status}: ${(errorData.Errors || [response.statusText]).join(', ')}`);
        }
        
        showFeedback(transactionFeedback, `¡${type === 'Deposit' ? 'Depósito' : 'Retiro'} realizado exitosamente!`, false);
        transactionForm.reset();
        fetchAccounts(); 
    } catch (error) {
        console.error(`Error al realizar ${type.toLowerCase()}:`, error);
        showFeedback(transactionFeedback, `Error al realizar ${type.toLowerCase()}: ${error.message}`, true);
    }
}


// --- Event Listeners ---
loadAccountsBtn.addEventListener('click', fetchAccounts);
accountForm.addEventListener('submit', handleAccountFormSubmit);
clearFormBtn.addEventListener('click', clearAccountForm);

depositBtn.addEventListener('click', () => handleTransaction('Deposit'));
withdrawBtn.addEventListener('click', () => handleTransaction('Withdrawal'));


// Cargar cuentas al iniciar
window.addEventListener('DOMContentLoaded', () => {
    clearAccountForm(); 
    fetchAccounts();
});