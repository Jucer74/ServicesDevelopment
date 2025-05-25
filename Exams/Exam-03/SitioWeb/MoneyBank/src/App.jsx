import React, { useState } from "react";
import {
  getAllAccounts,
  getAccountByNumber,
  getAccountById,
  createAccount,
  updateAccount,
  deleteAccount,
  deposit,
  withdraw,
} from "./Api";
import "./App.css";

const App = () => {
  const [operation, setOperation] = useState(""); // Estado para la operación seleccionada
  const [accounts, setAccounts] = useState([]);
  const [account, setAccount] = useState(null);
  const [message, setMessage] = useState("");

  // Estados para los valores de entrada
  const [accountNumber, setAccountNumber] = useState("");
  const [accountId, setAccountId] = useState("");
  const [ownerName, setOwnerName] = useState("");
  const [accountType, setAccountType] = useState("");
  const [balanceAmount, setBalanceAmount] = useState("");
  const [overdraftAmount, setOverdraftAmount] = useState("");
  const [transactionAmount, setTransactionAmount] = useState("");

  // Funciones para las operaciones
  const handleGetAllAccounts = async () => {
    const data = await getAllAccounts();
    setAccounts(data);
    setMessage("Cuentas obtenidas correctamente.");
  };

  const handleGetAccountByNumber = async () => {
    const data = await getAccountByNumber(accountNumber);
    setAccount(data[0]); // Suponiendo que devuelve un array
    setMessage("Cuenta obtenida por número. Por favor revise la sección de abajo");
  };

  const handleGetAccountById = async () => {
    const data = await getAccountById(accountId);
    setAccount(data);
    setMessage("Cuenta obtenida por ID. Por favor revise la sección de abajo");
  };

  const handleCreateAccount = async () => {
    const newAccount = {
      id: 0, // Siempre en 0 al crear
      accountType,
      accountNumber,
      ownerName,
      balanceAmount: parseFloat(balanceAmount),
      overdraftAmount: parseFloat(overdraftAmount),
    };
    const data = await createAccount(newAccount);
    setMessage(`Cuenta creada con ID: ${data.id}`);
  };

  const handleUpdateAccount = async () => {
    const updatedAccount = {
      id: parseInt(accountId),
      accountType,
      accountNumber,
      ownerName,
      balanceAmount: parseFloat(balanceAmount),
    };
    const data = await updateAccount(accountId, updatedAccount);
    setMessage(`Cuenta actualizada: ${data.id}`);
  };

  const handleDeleteAccount = async () => {
    const success = await deleteAccount(accountId);
    setMessage(success ? "Cuenta eliminada" : "Error al eliminar la cuenta");
  };

  const handleDeposit = async () => {
    console.log("handleDeposit", transactionAmount);
    console.log("handleDeposit ParseFlot", parseFloat(transactionAmount));
    console.log("Valor ingresado en Monto:", transactionAmount);
    const newDeposit = {
      id: parseInt(accountId),
      accountNumber,
      valueAmount: parseFloat(transactionAmount),
    };
    try {
      const data = await deposit(accountId, newDeposit);

      if (data.success) {
        setMessage("Depósito realizado exitosamente.");
      } else {
        setMessage(`Depósito realizado: ${data.valueAmount}`);
      }
    } catch (error) {
      setMessage("Error al realizar el depósito. Por favor, inténtalo de nuevo.");
      console.error(error);
    }

  };

  const handleWithdraw = async () => {
    const newWithdraw = {
      id: parseInt(accountId),
      accountNumber,
      valueAmount: parseFloat(transactionAmount),
    };
    try {
      const data = await withdraw(accountId, newWithdraw);

      if (data.success) {
        setMessage("Retiro realizado exitosamente.");
      } else {
        setMessage(`Retiro realizado: ${data.valueAmount}`);
      }
    } catch (error) {
      setMessage("Error al realizar el Retiro. Por favor, inténtalo de nuevo.");
      console.error(error);
    }
  };

  const clearFields = () => {
    setAccountNumber(""); // Limpia el número de cuenta
    setAccountId(""); // Limpia el ID de la cuenta
    setOwnerName(""); // Limpia el nombre del propietario
    setAccountType(""); // Limpia el tipo de cuenta
    setBalanceAmount(""); // Limpia el balance
    setOverdraftAmount(""); // Limpia el sobregiro
    setTransactionAmount(""); // Limpia el monto de la transacción
    setMessage(""); // Limpia el mensaje
    setAccount(null); // Limpia la cuenta seleccionada
  };

  // Renderizar los campos según la operación seleccionada
  const renderFields = () => {
    switch (operation) {
      case "getByNumber":
        return (
          <label>
            Número de Cuenta:
            <input
              type="text"
              value={accountNumber}
              onChange={(e) => setAccountNumber(e.target.value)}
            />
          </label>
        );
      case "getById":
        return (
          <label>
            ID de la Cuenta:
            <input
              type="text"
              value={accountId}
              onChange={(e) => setAccountId(e.target.value)}
            />
          </label>
        );
      case "create":
        return (
          <>
            <label>
              Tipo de Cuenta:
              <input
                type="text"
                value={accountType}
                onChange={(e) => setAccountType(e.target.value)}
              />
            </label>
            <label>
              Número de Cuenta:
              <input
                type="text"
                value={accountNumber}
                onChange={(e) => setAccountNumber(e.target.value)}
              />
            </label>
            <label>
              Nombre del Propietario:
              <input
                type="text"
                value={ownerName}
                onChange={(e) => setOwnerName(e.target.value)}
              />
            </label>
            <label>
              Balance Inicial:
              <input
                type="number"
                value={balanceAmount}
                onChange={(e) => setBalanceAmount(e.target.value)}
              />
            </label>
            <label>
              Sobregiro:
              <input
                type="number"
                value={overdraftAmount}
                onChange={(e) => setOverdraftAmount(e.target.value)}
              />
            </label>
          </>
        );
      case "update":
        return (
          <>
            <label>
              ID de la Cuenta:
              <input
                type="text"
                value={accountId}
                onChange={(e) => setAccountId(e.target.value)}
              />
            </label>
            <label>
              Tipo de Cuenta:
              <input
                type="text"
                value={accountType}
                onChange={(e) => setAccountType(e.target.value)}
              />
            </label>
            <label>
              Número de Cuenta:
              <input
                type="text"
                value={accountNumber}
                onChange={(e) => setAccountNumber(e.target.value)}
              />
            </label>
            <label>
              Nombre del Propietario:
              <input
                type="text"
                value={ownerName}
                onChange={(e) => setOwnerName(e.target.value)}
              />
            </label>
            <label>
              Balance:
              <input
                type="number"
                value={balanceAmount}
                onChange={(e) => setBalanceAmount(e.target.value)}
              />
            </label>
          </>
        );
      case "delete":
        return (
          <label>
            ID de la Cuenta:
            <input
              type="text"
              value={accountId}
              onChange={(e) => setAccountId(e.target.value)}
            />
          </label>
        );
      case "deposit":
      case "withdraw":
        return (
          <>
            <label>
              ID de la Cuenta:
              <input
                type="text"
                value={accountId}
                onChange={(e) => setAccountId(e.target.value)}
              />
            </label>
            <label>
              Número de Cuenta:
              <input
                type="text"
                value={accountNumber}
                onChange={(e) => setAccountNumber(e.target.value)}
              />
            </label>
            <label>
              Monto:
              <input
                type="number"
                value={transactionAmount}
                onChange={(e) => setTransactionAmount(e.target.value)}
              />
            </label>
          </>
        );
      default:
        return null;
    }
  };

  // Ejecutar la operación seleccionada
  const executeOperation = () => {
    switch (operation) {
      case "getAll":
        handleGetAllAccounts();
        break;
      case "getByNumber":
        handleGetAccountByNumber();
        break;
      case "getById":
        handleGetAccountById();
        break;
      case "create":
        handleCreateAccount();
        break;
      case "update":
        handleUpdateAccount();
        break;
      case "delete":
        handleDeleteAccount();
        break;
      case "deposit":
        handleDeposit();
        break;
      case "withdraw":
        handleWithdraw();
        break;
      default:
        setMessage("Por favor selecciona una operación.");
    }
  };

  return (
    <div className="App">
      <h1>MoneyBankService</h1>

      <div className="opciones-menu">
        <h2>Selecciona una Operación</h2>
        <select
          onChange={(e) => {
            setOperation(e.target.value); // Cambia la operación seleccionada
            clearFields(); // Limpia los campos
          }}
          value={operation}
        >
          <option value="">-- Selecciona una opción --</option>
          <option value="getAll">Obtener Todas las Cuentas</option>
          <option value="getByNumber">Obtener Cuenta por Número</option>
          <option value="getById">Obtener Cuenta por ID</option>
          <option value="create">Crear Cuenta</option>
          <option value="update">Actualizar Cuenta</option>
          <option value="delete">Eliminar Cuenta</option>
          <option value="deposit">Hacer Depósito</option>
          <option value="withdraw">Hacer Retiro</option>
        </select>
      </div>

      <div>
        <h2>Campos</h2>
        {renderFields()}
      </div>

      <div>
        <h2>Acción</h2>
        <button onClick={executeOperation}>Ejecutar</button>
      </div>

      <h2>Mensaje</h2>
      <p>{message}</p>

      <h2>Cuentas</h2>
      {accounts.length > 0 ? (
        <table>
          <thead>
            <tr>
              <th>ID</th>
              <th>Tipo de Cuenta</th>
              <th>Número de Cuenta</th>
              <th>Nombre del Propietario</th>
              <th>Balance</th>
              <th>Sobregiro</th>
            </tr>
          </thead>
          <tbody>
            {accounts.map((acc) => (
              <tr key={acc.id}>
                <td>{acc.id}</td>
                <td>{acc.accountType}</td>
                <td>{acc.accountNumber}</td>
                <td>{acc.ownerName}</td>
                <td>${acc.balanceAmount}</td>
                <td>${acc.overdraftAmount}</td>
              </tr>
            ))}
          </tbody>
        </table>
      ) : (
        <p>No hay cuentas disponibles.</p>
      )}

      {account && (
        <div className="selected-account">
          <h2>Cuenta Seleccionada</h2>
          <p><strong>ID:</strong> {account.id}</p>
          <p><strong>Tipo de Cuenta:</strong> {account.accountType}</p>
          <p><strong>Número de Cuenta:</strong> {account.accountNumber}</p>
          <p><strong>Nombre del Propietario:</strong> {account.ownerName}</p>
          <p><strong>Balance:</strong> ${account.balanceAmount}</p>
          <p><strong>Sobregiro:</strong> ${account.overdraftAmount}</p>
        </div>
      )}
    </div>
  );
};

export default App;