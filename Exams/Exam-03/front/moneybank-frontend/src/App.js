import React, { useEffect, useState } from 'react';
import axios from 'axios';

// Cambia la URL por la de tu API en AWS App Runner
const API_URL = "https://wizyub7ngd.us-east-2.awsapprunner.com/api/Accounts";

function App() {
  const [accounts, setAccounts] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    axios.get(API_URL)
      .then(res => {
        console.log("Datos recibidos de la API:", res.data);
        setAccounts(res.data);
        setLoading(false);
      })
      .catch(err => {
        console.error(err);
        setLoading(false);
      });
  }, []);

  return (
    <div style={{ padding: "2rem" }}>
      <h1>MoneyBank - Cuentas</h1>
      {loading ? (
        <p>Cargando cuentas...</p>
      ) : (
        <table border="1" cellPadding="10">
          <thead>
            <tr>
              <th>Id</th>
              <th>Número de Cuenta</th>
              <th>Propietario</th>
              <th>Tipo</th>
              <th>Balance</th>
            </tr>
          </thead>
          <tbody>
            {accounts.map(account => (
              <tr key={account.id}>
                <td>{account.id}</td>
                <td>{account.accountNumber}</td>
                <td>{account.ownerName}</td>
                <td>{account.accountType}</td>
                <td>{account.balanceAmount}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}

export default App;
