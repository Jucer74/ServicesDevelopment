import React, { useState, useEffect } from "react";

const initialState = {
  accountType: "",
  accountNumber: "",
  ownerName: "",
  balanceAmount: "",
};

function AccountForm({ onSubmit, editingAccount, onCancel }) {
  const [form, setForm] = useState(initialState);

  useEffect(() => {
    if (editingAccount) setForm(editingAccount);
    else setForm(initialState);
  }, [editingAccount]);

  const handleChange = (e) => {
    const { name, value } = e.target;

    if (name === "accountNumber") {
      // Quita todo lo que no sean dígitos
      setForm({ ...form, [name]: value.replace(/\D/g, "") });
    } else if (name === "balanceAmount") {
      // Si quieres que balanceAmount acepte solo números (opcional)
      setForm({ ...form, [name]: value.replace(/\D/g, "") });
    } else {
      setForm({ ...form, [name]: value });
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!form.accountType || !form.accountNumber || !form.ownerName)
      return alert("Todos los campos son obligatorios");

    // Asegura que el número de cuenta sea string y tenga 10 caracteres
    const cleanAccountNumber = form.accountNumber.toString().padStart(10, "0");

    const formToSend = {
      ...form,
      accountType: form.accountType, // solo la letra "C" o "A", como string
      accountNumber: cleanAccountNumber, // string
      ownerName: form.ownerName,
      balanceAmount: Number(form.balanceAmount), // número
    };
    onSubmit(formToSend);
    setForm(initialState);
  };

  return (
    <form
      onSubmit={handleSubmit}
      style={{
        marginBottom: "1rem",
        background: "#222",
        color: "#fff",
        padding: 16,
        borderRadius: 8,
      }}
    >
      <h3>{editingAccount ? "Editar Cuenta" : "Crear Nueva Cuenta"}</h3>
      <label>
        Tipo:
        <select
          name="accountType"
          value={form.accountType}
          onChange={handleChange}
          required
        >
          <option value="">Seleccione</option>
          <option value="C">Corriente</option>
          <option value="A">Ahorros</option>
        </select>
      </label>{" "}
      <label>
        Número:
        <input
          name="accountNumber"
          value={form.accountNumber}
          onChange={handleChange}
          required
          maxLength={10}
        />
      </label>{" "}
      <label>
        Propietario:
        <input
          name="ownerName"
          value={form.ownerName}
          onChange={handleChange}
          required
        />
      </label>{" "}
      <label>
        Balance:
        <input
          name="balanceAmount"
          type="number"
          value={form.balanceAmount}
          onChange={handleChange}
          required
          min={0}
        />
      </label>{" "}
      <button type="submit">{editingAccount ? "Actualizar" : "Crear"}</button>
      {editingAccount && (
        <button type="button" onClick={onCancel}>
          Cancelar
        </button>
      )}
    </form>
  );
}

export default AccountForm;
