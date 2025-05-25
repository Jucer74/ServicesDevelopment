import React, { useEffect, useState } from "react";
import axios from "axios";
import AccountTable from "./components/AccountTable";
import AccountForm from "./components/AccountForm";
import ATMLayout from "./components/ATMLayout";

// Cambia por la URL real de tu API en AWS App Runner
const API_URL = "https://wizyub7ngd.us-east-2.awsapprunner.com/api/Accounts";

function App() {
  const [accounts, setAccounts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [editingAccount, setEditingAccount] = useState(null);

  const [modal, setModal] = useState({
    open: false,
    action: "", // 'deposit' o 'withdraw'
    account: null,
    value: "",
  });

  const openModal = (action, account) => {
    setModal({ open: true, action, account, value: "" });
  };

  const closeModal = () =>
    setModal({ open: false, action: "", account: null, value: "" });

  const handleModalSubmit = async () => {
    if (!/^\d+$/.test(modal.value) || Number(modal.value) <= 0) {
      alert("Ingresa un valor positivo y entero");
      return;
    }
    const url = `${API_URL}/${modal.account.id}/${
      modal.action === "deposit" ? "Deposit" : "Withdrawal"
    }`;
    const body = {
      id: modal.account.id,
      accountNumber: modal.account.accountNumber,
      valueAmount: Number(modal.value),
    };
    if (
      window.confirm(
        `¿Seguro que deseas ${
          modal.action === "deposit" ? "depositar" : "retirar"
        } $${modal.value} en la cuenta ${modal.account.accountNumber}?`
      )
    ) {
      try {
        await axios.put(url, body);
        closeModal();
        fetchAccounts();
      } catch (e) {
        alert(
          "Error: " +
            (e.response?.data?.errors?.join(", ") ??
              "No se pudo procesar la transacción")
        );
      }
    }
  };

  // Cargar cuentas
  const fetchAccounts = () => {
    setLoading(true);
    axios
      .get(API_URL)
      .then((res) => setAccounts(res.data))
      .catch(() => alert("Error al cargar cuentas"))
      .finally(() => setLoading(false));
  };

  useEffect(fetchAccounts, []);

  // Crear o actualizar
  const handleSubmit = (form) => {
    if (editingAccount) {
      axios
        .put(`${API_URL}/${form.id}`, form)
        .then(fetchAccounts)
        .catch(() => alert("Error al actualizar"));
      setEditingAccount(null);
    } else {
      axios
        .post(API_URL, form)
        .then(fetchAccounts)
        .catch(() => alert("Error al crear"));
    }
  };

  // Editar
  const handleEdit = (account) => setEditingAccount(account);

  // Eliminar
  const handleDelete = (id) => {
    if (window.confirm("¿Eliminar cuenta?")) {
      axios
        .delete(`${API_URL}/${id}`)
        .then(fetchAccounts)
        .catch(() => alert("Error al eliminar"));
    }
  };

  return (
    <ATMLayout>
      <h1 style={{ textAlign: "center" }}>MoneyBank - Cuentas</h1>
      <AccountForm
        onSubmit={handleSubmit}
        editingAccount={editingAccount}
        onCancel={() => setEditingAccount(null)}
      />
      {loading ? (
        <p>Cargando...</p>
      ) : (
        <AccountTable
          accounts={accounts}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onDeposit={(account) => openModal("deposit", account)}
          onWithdraw={(account) => openModal("withdraw", account)}
        />
      )}

      {modal.open && (
        <div
          style={{
            position: "fixed",
            top: 0,
            left: 0,
            width: "100vw",
            height: "100vh",
            background: "rgba(0,0,0,0.4)",
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
            zIndex: 1000,
          }}
        >
          <div
            style={{
              background: "#fff",
              padding: 24,
              borderRadius: 12,
              minWidth: 320,
              boxShadow: "0 8px 32px #2227",
            }}
          >
            <h3 style={{ marginBottom: 16 }}>
              {modal.action === "deposit" ? "Depósito" : "Retiro"} - Cuenta{" "}
              {modal.account.accountNumber}
            </h3>
            <input
              type="number"
              min="1"
              placeholder="Valor a ingresar"
              value={modal.value}
              onChange={(e) => setModal({ ...modal, value: e.target.value })}
              style={{
                width: "100%",
                padding: 8,
                fontSize: 16,
                marginBottom: 16,
              }}
            />
            <div
              style={{ display: "flex", justifyContent: "flex-end", gap: 12 }}
            >
              <button
                onClick={handleModalSubmit}
                style={{
                  background: "#28a745",
                  color: "#fff",
                  padding: "8px 18px",
                  borderRadius: 6,
                }}
              >
                Confirmar
              </button>
              <button
                onClick={closeModal}
                style={{
                  background: "#dc3545",
                  color: "#fff",
                  padding: "8px 18px",
                  borderRadius: 6,
                }}
              >
                Cancelar
              </button>
            </div>
          </div>
        </div>
      )}
    </ATMLayout>
  );
}

export default App;
