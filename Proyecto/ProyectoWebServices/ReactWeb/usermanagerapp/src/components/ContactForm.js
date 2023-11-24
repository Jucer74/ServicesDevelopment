import React, { useState } from 'react';
import { Form, Button, Col, Row, Modal } from 'react-bootstrap';

const ContactForm = () => {
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [message, setMessage] = useState('');
  const [show, setShow] = useState(false);

  const handleSubmit = (e) => {
    e.preventDefault();
    setShow(true);
    console.log({ name, email, message });
  };

  const handleClose = () => {
    setShow(false);
  };

  return (
    <div >
      <div style={{ textAlign: 'center', color: '#800', display: 'flex', justifyContent: 'center'  }}>
      <Form onSubmit={handleSubmit}>
        <Row className="mb-3">
          <Form.Group as={Col} controlId="name">
            <Form.Label>Nombre:</Form.Label>
            <Form.Control
              type="text"
              value={name}
              onChange={(e) => setName(e.target.value)}
              style={{ width: '100%', maxWidth: '300px', color: '#800' }}
            />
          </Form.Group>
        </Row>

        <Row className="mb-3">
          <Form.Group as={Col} controlId="email">
            <Form.Label>Correo electrónico:</Form.Label>
            <Form.Control
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              style={{ width: '100%', maxWidth: '300px', color: '#800' }}
            />
          </Form.Group>
        </Row>

        <Row className="mb-3">
          <Form.Group as={Col} controlId="message">
            <Form.Label>Mensaje:</Form.Label>
            <Form.Control
              as="textarea"
              rows={3}
              value={message}
              onChange={(e) => setMessage(e.target.value)}
              style={{ width: '100%', maxWidth: '300px', color: '#800' }}
            />
          </Form.Group>
        </Row>

        <Button type="submit" className="btn btn-danger">
          Enviar
        </Button>
      </Form>
      </div>
      <br />
      <iframe
        src="https://www.google.com/maps/embed?pb=!1m14!1m12!1m3!1d63734.17793477185!2d-76.5394944!3d3.2538623999999996!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!5e0!3m2!1ses!2sco!4v1700613295070!5m2!1ses!2sco"
        width="100%"
        height="300"
        style={{ border: '0' }}
        allowfullscreen=""
        loading="lazy"
        referrerio="no-referrer-when-downgrade"
      />

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title style={{ color: '#800' }}>Mensaje enviado</Modal.Title>
        </Modal.Header>
        <Modal.Body style={{ color: '#800' }}>Su mensaje ha sido enviado con éxito.</Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose} style={{ color: '#800' }}>
            Cerrar
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
};

export default ContactForm;
