import React, { useEffect, useState } from 'react';
import axios from 'axios';
import AccountTable from './components/AccountTable';
import AccountForm from './components/AccountForm';
import ATMLayout from './components/ATMLayout';

// Cambia por la URL real de tu API en AWS App Runner
const API_URL = "https://wizyub7ngd.us-east-2.awsapprunner.com/api/Accounts";

function App() {
  const [accounts, setAccounts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [editingAccount, setEditingAccount] = useState(null);

  // Cargar cuentas
  const fetchAccounts = () => {
    setLoading(true);
    axios.get(API_URL)
      .then(res => setAccounts(res.data))
      .catch(() => alert("Error al cargar cuentas"))
      .finally(() => setLoading(false));
  };

  useEffect(fetchAccounts, []);

  // Crear o actualizar
  const handleSubmit = (form) => {
    if (editingAccount) {
      axios.put(`${API_URL}/${form.id}`, form)
        .then(fetchAccounts)
        .catch(() => alert("Error al actualizar"));
      setEditingAccount(null);
    } else {
      axios.post(API_URL, form)
        .then(fetchAccounts)
        .catch(() => alert("Error al crear"));
    }
  };

  // Editar
  const handleEdit = (account) => setEditingAccount(account);

  // Eliminar
  const handleDelete = (id) => {
    if (window.confirm("¿Eliminar cuenta?")) {
      axios.delete(`${API_URL}/${id}`)
        .then(fetchAccounts)
        .catch(() => alert("Error al eliminar"));
    }
  };

  return (
    <ATMLayout>
      <h1 style={{ textAlign: "center" }}>MoneyBank - Cuentas</h1>
      <AccountForm onSubmit={handleSubmit} editingAccount={editingAccount} onCancel={() => setEditingAccount(null)} />
      {loading
        ? <p>Cargando...</p>
        : <AccountTable accounts={accounts} onEdit={handleEdit} onDelete={handleDelete} />
      }
    </ATMLayout>
  );
}

export default App;
