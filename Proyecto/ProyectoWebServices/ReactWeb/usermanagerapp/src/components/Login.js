import React, { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUser, faLock } from "@fortawesome/free-solid-svg-icons";
import axios from "axios";



export const Login = ({ setLogoutUser }) => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const login = (e) => {
    e.preventDefault();
    axios
      .post("http://localhost:4000/api/auth/login", {
        email,
        password,
      })
      .then((response) => {
        console.log("response", response);
        localStorage.setItem(
          "login",
          JSON.stringify({
            userLogin: true,
            token: response.data.access_token,
          })
        );
        setError("");
        setEmail("");
        setPassword("");
        navigate("/Home");
      })
      .catch((error) => setError(error.response.data.message));
  };

  return (
    <div className="login-wrapper">
      <div className="login-container">
        <h2 className="text-center">Sura Login</h2>
        {error && <p className="error-message">{error}</p>}
        <Form onSubmit={login}>
          <FormGroup className="mb-3">
            <Label for="password" className="label-with-icon">
              <FontAwesomeIcon icon={faLock} className="icon" />
              Password
            </Label>
            <Input
              id="password"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </FormGroup>
          <FormGroup className="mb-3">
            <Label for="username" className="label-with-icon">
              <FontAwesomeIcon icon={faUser} className="icon" />
              Username
            </Label>
            <Input
              id="username"
              type="text"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
          </FormGroup>
          <Button color="danger" type="submit" className="login-button">
            Login
          </Button>
        </Form>
        <p className="text-center mt-3">
          Don't have an account? <Link to="/register">Register</Link> yourself
        </p>
      </div>
    </div>
  );
};
