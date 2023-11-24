import React, { useEffect, useState } from 'react';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import { useNavigate } from 'react-router-dom';

const Details = () => {
    const [logindata, setLoginData] = useState([]);
    const history = useNavigate();
    const [show, setShow] = useState(false);
    const todayDate = new Date().toISOString().slice(0, 10);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const Birthday = () => {
        const getuser = localStorage.getItem('user_login');
        if (getuser && getuser.length) {
            const user = JSON.parse(getuser);
            setLoginData(user);

            const userbirth = logindata.map((el, k) => {
                return el.date === todayDate;
            });

            if (userbirth) {
                setTimeout(() => {
                    console.log('ok');
                    handleShow();
                }, 3000);
            }
        }
    };

    const userlogout = () => {
        localStorage.removeItem('user_login');
        history('/');
    };

    useEffect(() => {
        Birthday();
    }, []);

    return (
        <>
            {logindata.length === 0 ? (
                'error'
            ) : (
                <>
                    <div className="container bg-white mt-3">
                        <h1>Bienvenido</h1>
                        <h1>{logindata[0].name}</h1>
                        <Button onClick={userlogout}>Cerrar Sesión</Button>
                    </div>

                    {logindata[0].date === todayDate ? (
                        <Modal show={show} onHide={handleClose}>
                            <Modal.Header closeButton>
                                <Modal.Title>{logindata[0].name} </Modal.Title>
                            </Modal.Header>
                            <Modal.Body>¡Te deseamos muchos años de felicidad en tu día!</Modal.Body>
                            <Modal.Footer>
                                <Button variant="secondary" onClick={handleClose}>
                                    Cerrar
                                </Button>
                                <Button variant="primary" onClick={handleClose}>
                                    Guardar Cambios
                                </Button>
                            </Modal.Footer>
                        </Modal>
                    ) : (
                        ''
                    )}
                </>
            )}
        </>
    );
};

export default Details;
