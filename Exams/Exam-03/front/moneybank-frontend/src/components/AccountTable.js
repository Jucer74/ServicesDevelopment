import React from 'react';

function AccountTable({ accounts, onEdit, onDelete, onDeposit, onWithdraw }) {
  return (
    <table border="1" cellPadding="10" style={{ width: "100%", background: "#e8e8e8" }}>
      <thead style={{ background: "#222", color: "#fff" }}>
        <tr>
          <th>Id</th>
          <th>Número de Cuenta</th>
          <th>Propietario</th>
          <th>Tipo</th>
          <th>Balance</th>
          <th>Sobregiro</th>
          <th>Acciones</th>
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
            <td>{account.overdraftAmount}</td>
            <td>
              <button onClick={() => onEdit(account)}>Editar</button>
              <button onClick={() => onDelete(account.id)} style={{ marginLeft: 8 }}>Eliminar</button>
              <button onClick={() => onDeposit(account)} style={{ marginLeft: 8 }}>Depositar</button>
              <button onClick={() => onWithdraw(account)} style={{ marginLeft: 8 }}>Retirar</button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}

export default AccountTable;
