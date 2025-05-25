import React from 'react';

function ATMLayout({ children }) {
  return (
    <div style={{
      background: "#333",
      minHeight: "100vh",
      display: "flex",
      justifyContent: "center",
      alignItems: "center"
    }}>
      <div style={{
        width: 600,
        minHeight: 400,
        background: "#d6f0ff",
        border: "10px solid #222",
        borderRadius: 24,
        boxShadow: "0 0 25px #111",
        padding: 24
      }}>
        {children}
      </div>
    </div>
  );
}

export default ATMLayout;
