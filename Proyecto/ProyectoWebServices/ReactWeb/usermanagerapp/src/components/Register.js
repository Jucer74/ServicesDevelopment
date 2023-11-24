import React, { useState } from 'react';
import axios from 'axios';

const Register = () => {
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    confirmedPassword: '',
  });

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    // Realizar validaciones aquí si es necesario

    try {
      const response = await axios.post('http://localhost:4000/api/auth/register', formData);
      console.log(response.data); // Mensaje de registro exitoso
      // Puedes redirigir al usuario o mostrar un mensaje de éxito aquí
    } catch (error) {
      console.error('Error al registrar:', error.response.data.message);
      // Puedes mostrar un mensaje de error al usuario
    }
  };

  return (
    <div style={{ textAlign: 'center', margin: 'auto', maxWidth: '400px' }}>
      <h2>Register</h2>
      <form onSubmit={handleSubmit}>
        <label style={{ display: 'block', margin: '10px 0' }}>
          First Name:
          <input
            type="text"
            name="firstName"
            onChange={handleChange}
            style={{ width: '100%', padding: '10px', border: '1px solid #ccc' }}
          />
        </label>
        <label style={{ display: 'block', margin: '10px 0' }}>
          Last Name:
          <input
            type="text"
            name="lastName"
            onChange={handleChange}
            style={{ width: '100%', padding: '10px', border: '1px solid #ccc' }}
          />
        </label>
        <label style={{ display: 'block', margin: '10px 0' }}>
          Email:
          <input
            type="email"
            name="email"
            onChange={handleChange}
            style={{ width: '100%', padding: '10px', border: '1px solid #ccc' }}
          />
        </label>
        <label style={{ display: 'block', margin: '10px 0' }}>
          Password:
          <input
            type="password"
            name="password"
            onChange={handleChange}
            style={{ width: '100%', padding: '10px', border: '1px solid #ccc' }}
          />
        </label>
        <label style={{ display: 'block', margin: '10px 0' }}>
          Confirm Password:
          <input
            type="password"
            name="confirmedPassword"
            onChange={handleChange}
            style={{ width: '100%', padding: '10px', border: '1px solid #ccc' }}
          />
        </label>
        <button
          type="submit"
          style={{
            backgroundColor: '#990000',
            color: '#fff',
            padding: '10px 20px',
            borderRadius: '5px',
            border: 'none',
            cursor: 'pointer',
          }}
        >
          Register
        </button>
      </form>
    </div>
  );
};

export default Register;
