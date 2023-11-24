import React, { useState } from 'react';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import { useNavigate } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

export const Homes = () => {
    const history = useNavigate();

    const [inpval, setInpval] = useState({
        email: '',
        password: '',
    });

    const [isLoginSuccess, setIsLoginSuccess] = useState(false);

    const getdata = (e) => {
        const { value, name } = e.target;

        setInpval(() => {
            return {
                ...inpval,
                [name]: value,
            };
        });
    };

    const addData = (e) => {
        e.preventDefault();

        const getuserArr = localStorage.getItem('useryoutube');

        const { email, password } = inpval;
        if (email === '') {
            toast.error('El campo de correo electrónico es obligatorio', {
                position: 'top-center',
            });
        } else if (!email.includes('@')) {
            toast.error('Ingrese una dirección de correo electrónico válida', {
                position: 'top-center',
            });
        } else if (password === '') {
            toast.error('El campo de contraseña es obligatorio', {
                position: 'top-center',
            });
        } else if (password.length < 5) {
            toast.error('La longitud de la contraseña debe ser mayor a cinco', {
                position: 'top-center',
            });
        } else {
            if (getuserArr && getuserArr.length) {
                const userdata = JSON.parse(getuserArr);
                const userlogin = userdata.filter((el) => {
                    return el.email === email && el.password === password;
                });

                if (userlogin.length === 0) {
                    alert('Detalles inválidos');
                } else {
                    console.log('Inicio de sesión exitoso');
                    setIsLoginSuccess(true);

                    // Limpiar campos de entrada
                    setInpval({
                        email: '',
                        password: '',
                    });

                    localStorage.setItem('user_login', JSON.stringify(userlogin));

                    history('/details');
                }
            }
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
                <section className="d-flex justify-content-between">
                    <div className="left_data mt-3 p-3" style={{ width: '100%' }}>
                        <h3 className="text-center col-lg-6">Inicia Sesion</h3>
                        {isLoginSuccess && (
                            <div className="alert alert-success" role="alert">
                                ¡Inicio de sesión exitoso!
                            </div>
                        )}
                        <Form>
                            <Form.Group className="mb-3 col-lg-6" controlId="formBasicEmail">
                                <Form.Control
                                    type="email"
                                    name="email"
                                    onChange={getdata}
                                    placeholder="Ingrese su correo electrónico"
                                    value={inpval.email}
                                />
                            </Form.Group>

                            <Form.Group className="mb-3 col-lg-6" controlId="formBasicPassword">
                                <Form.Control
                                    type="password"
                                    name="password"
                                    onChange={getdata}
                                    placeholder="Contraseña"
                                    value={inpval.password}
                                />
                            </Form.Group>
                            <Button
                                variant="primary"
                                className="col-lg-6"
                                onClick={addData}
                                style={{ background: 'rgb(13, 110, 235)' }}
                                type="submit"
                            >
                                Enviar
                            </Button>
                        </Form>
                        <p className="mt-3">
                            Pizzeria Linguini 
                        </p>
                    </div>
                </section>
                <ToastContainer />
            </div>
        </>
    );
};

export default Homes;
