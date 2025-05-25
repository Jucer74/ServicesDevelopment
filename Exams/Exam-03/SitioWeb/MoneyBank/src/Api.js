const API_BASE_URL = "http://34.219.147.164/api"; // Cambia esto si tu API está en otro lugar

// Obtener todas las cuentas
export const getAllAccounts = async () => {
  const response = await fetch(`${API_BASE_URL}/Accounts`);
  return response.json();
};

// Obtener cuenta por número de cuenta
export const getAccountByNumber = async (accountNumber) => {
  const response = await fetch(`${API_BASE_URL}/Accounts?accountNumber=${accountNumber}`);
  return response.json();
};

// Obtener cuenta por ID
export const getAccountById = async (id) => {
  const response = await fetch(`${API_BASE_URL}/Accounts/${id}`);
  return response.json();
};

// Crear una cuenta
export const createAccount = async (account) => {
  const response = await fetch(`${API_BASE_URL}/Accounts`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(account),
  });
  return response.json();
};

// Actualizar una cuenta
export const updateAccount = async (id, account) => {
  const response = await fetch(`${API_BASE_URL}/Accounts/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(account),
  });
  return response.json();
};

// Eliminar una cuenta
export const deleteAccount = async (id) => {
  const response = await fetch(`${API_BASE_URL}/Accounts/${id}`, {
    method: "DELETE",
  });
  return response.ok;
};

// Hacer un depósito
export const deposit = async (id, deposit) => {
  const response = await fetch(`${API_BASE_URL}/Accounts/${id}/Deposit`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(deposit),
  });
  
  // Si la respuesta es 204 No Content, devolvemos un indicador de éxito
  if (response.status === 204) {
    return { success: true };
  }

  // Si la respuesta tiene contenido JSON, lo procesamos
  if (response.ok && response.headers.get("Content-Type")?.includes("application/json")) {
    return response.json();
  }

  // Si no es 204 ni JSON válido, lanzamos un error
  throw new Error("Error al realizar el depósito.");
};

// Hacer un retiro
export const withdraw = async (id, withdraw) => {
  const response = await fetch(`${API_BASE_URL}/Accounts/${id}/Withdrawal`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(withdraw),
  });
  // Si la respuesta es 204 No Content, devolvemos un indicador de éxito
  if (response.status === 204) {
    return { success: true };
  }

  // Si la respuesta tiene contenido JSON, lo procesamos
  if (response.ok && response.headers.get("Content-Type")?.includes("application/json")) {
    return response.json();
  }

  // Si no es 204 ni JSON válido, lanzamos un error
  throw new Error("Error al realizar el retiro.");
};