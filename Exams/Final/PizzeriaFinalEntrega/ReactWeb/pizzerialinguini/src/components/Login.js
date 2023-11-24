import React, { useState } from 'react';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import { NavLink, useNavigate } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const Login = () => {
    const history = useNavigate();

    const [inpval, setInpval] = useState({
        name: "",
        email: "",
        date: "",
        password: ""
    });

    const [data, setData] = useState([]);
    console.log(inpval);

    const getdata = (e) => {
        const { value, name } = e.target;
        setInpval((prevInput) => {
            return {
                ...prevInput,
                [name]: value
            };
        });
    };

    const addData = (e) => {
        e.preventDefault();

        const { name, email, date, password } = inpval;

        if (name === "") {
            toast.error('Nombre es requerido!', {
                position: "top-center",
            });
        } else if (email === "") {
            toast.error('correo electronico es requerido', {
                position: "top-center",
            });
        } else if (!email.includes("@")) {
            toast.error('Please enter a valid correo electronico', {
                position: "top-center",
            });
        } else if (date === "") {
            toast.error('Fecha es requerida', {
                position: "top-center",
            });
        } else if (password === "") {
            toast.error('contraseña es requerida', {
                position: "top-center",
            });
        } else if (password.length < 5) {
            toast.error('la contraseña debe ser mayor a 5 letras', {
                position: "top-center",
            });
        } else {
            console.log("Data added successfully");
            localStorage.setItem("useryoutube", JSON.stringify([...data, inpval]));
            history("/Homes"); 
        }
    };

    return (
        <>
            <section className="py-4 bg-primary">
                <div className="container">
                    <div className="row">
                        <div className="col-md-12 text-center">
                            <h2>Inicie Sesion</h2>
                        </div>
                    </div>
                </div>
            </section>

            <div className="container bg-white mt-3">
                <section className='d-flex justify-content-between'>
                    <div className="left_data mt-3 p-3" style={{ width: "100%" }}>
                        <h3 className='text-center col-lg-6'>Registrate</h3>
                        <Form>
                            <Form.Group className="mb-3 col-lg-6" controlId="formBasicEmail">
                                <Form.Control type="text" name='name' onChange={getdata} placeholder="Escribe tu nombre" />
                            </Form.Group>
                            <Form.Group className="mb-3 col-lg-6" controlId="formBasicEmail">
                                <Form.Control type="email" name='email' onChange={getdata} placeholder="Ingrese su correo electronico" />
                            </Form.Group>
                            <Form.Group className="mb-3 col-lg-6" controlId="formBasicEmail">
                                <Form.Control onChange={getdata} name='date' type="date" />
                            </Form.Group>
                            <Form.Group className="mb-3 col-lg-6" controlId="formBasicPassword">
                                <Form.Control type="password" name='password' onChange={getdata} placeholder="Contraseña" />
                            </Form.Group>
                            <Button variant="primary" className='col-lg-6' onClick={addData} style={{ background: "rgb(13, 110, 235)" }} type="submit">
                                Enviar
                            </Button>
                        </Form>
                        <p className='mt-3'>Ya tienes cuenta? <span><NavLink to="/Homes">Inicia Sesion</NavLink></span> </p>
                    </div>
                </section>
                <ToastContainer />
            </div>
        </>
    );
};

export default Login;
