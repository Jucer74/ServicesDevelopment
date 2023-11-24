import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';
import { Button, Container, Table, Form } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import axios from 'axios';

export function PizzeriaList() {
  const baseUrl = "https://localhost:5001/api/Pizzerias";
  
  

  const navigate = useNavigate();
  const [validationError, setValidationError] = useState('');
  const [data, setData] = useState([]);
  const [currentPizzeria, setCurrentPizzeria] = useState({
    id: '',
    categoriaa: '',
  });

  const [showModalCreate, setShowModalCreate] = useState(false);
  const [showModalUpdate, setShowModalUpdate] = useState(false);
  const [showModalDetails, setShowModalDetails] = useState(false);
  const [showModalDelete, setShowModalDelete] = useState(false);

  const openCloseModalCreate = () => {
    setShowModalCreate(!showModalCreate);
    setValidationError('');
  }

  const openCloseModalUpdate = () => {
    setShowModalUpdate(!showModalUpdate);
    setValidationError('');
  }

  const openCloseModalDetails = () => {
    setShowModalDetails(!showModalDetails);
  }

  const openCloseModalDelete = () => {
    setShowModalDelete(!showModalDelete);
  }

  const handleChange = (e) => {
    const { name, value } = e.target;
    setCurrentPizzeria({
      ...currentPizzeria,
      [name]: value
    });
    setValidationError('');
  }

  const getPizzerias = async () => {
    await axios.get(baseUrl)
      .then(response => {
        setData(response.data);
      }).catch(error => {
        console.log(error);
      })
  }

  useEffect(() => {
    getPizzerias();
  }, []);

  const postPizzeria = async () => {
    if (!currentPizzeria.categoriaa) {
      setValidationError('Please provide a Categoriaa.');
      return;
    }
  
    const newPizzeria = {
      propertyType: currentPizzeria.categoriaa,
    };
  
    await axios.post(baseUrl, newPizzeria)
      .then(response => {
        getPizzerias();
        openCloseModalCreate();
        setCurrentPizzeria({}); 
      }).catch(error => {
        console.log(error);
      });
  };
  
  const putPizzeria = async () => {
    if (!currentPizzeria.categoriaa) {
      setValidationError('Please provide a .Categoriaa');
      return;
    }
  
    await axios.put(baseUrl + "/" + currentPizzeria.id, currentPizzeria)
      .then(response => {
        var result = response.data;
        var updatedData = data.map(pizzeria => pizzeria.id === currentPizzeria.id ? result : pizzeria);
        setData(updatedData);
        getPizzerias();
        openCloseModalUpdate();
        setCurrentPizzeria({}); 
      }).catch(error => {
        console.log(error);
      });
  };

  const deletePizzeria = async (id) => {
    await axios.delete(baseUrl + "/" + id)
      .then(() => {
        setData(data.filter(pizzeria => pizzeria.id !== id));
        openCloseModalDelete();
      }).catch(error => {
        console.log(error);
      })
  }

  const selectCurrentPizzeria = (pizzeria, action) => {
    setCurrentPizzeria(pizzeria);
    switch (action) {
      case "Edit":
        openCloseModalUpdate();
        break;
      case "Details":
        openCloseModalDetails();
        break;
      case "Delete":
        openCloseModalDelete();
        break;
        case "Product":
          navigate(`/PizzeriaList/${pizzeria.id}`);
          break;
        default:
          break;
    }
  }

  return (
      <Container className="bg-white pt-5 pb-4 text-center text-md-left" fluid>
        <div style={{ marginTop: '20px' }}>
          <h1>Pizzeria Categoria List</h1>
        </div>
        <p>
          <Button className="left" variant="success btn-sm" onClick={() => openCloseModalCreate()}>
            <FontAwesomeIcon icon={faPlus} /> New
          </Button>
        </p>
        <Table id="PizzeriasTable">
          <thead>
            <tr>
              <th>Id</th>
              <th>Categoria</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {data.map(pizzeria => (
              <tr key={pizzeria.id}>
                <td>{pizzeria.id}</td>
                <td>{pizzeria.categoriaa}</td>
                <td>
                  <Button variant="outline-primary btn-sm" onClick={() => selectCurrentPizzeria(pizzeria, "Edit")}>Edit</Button>
                  <Button variant="outline-warning btn-sm" onClick={() => selectCurrentPizzeria(pizzeria, "Details")}>Details</Button>
                  <Button variant="outline-danger btn-sm" onClick={() => selectCurrentPizzeria(pizzeria, "Delete")}>Delete</Button>
                  <Button variant="outline-info btn-sm" onClick={() => selectCurrentPizzeria(pizzeria, "Product")}>Product</Button>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
  
      {/* Create */}
      <Modal isOpen={showModalCreate}>
        <ModalHeader>Create Pizzeria Categoriaa Type</ModalHeader>
        <ModalBody>
          <Form>
            <Form.Group>
              <Form.Label>Categoriaa:</Form.Label>
              <Form.Control type="text" id="txtcategoriaaa" name="categoriaa" required isInvalid={!currentPizzeria.categoriaa && validationError !== ''} onChange={handleChange} />
              <Form.Control.Feedback type="invalid">{validationError}</Form.Control.Feedback>
            </Form.Group>
          </Form>
        </ModalBody>
        <ModalFooter>
          <Button variant="primary" onClick={() => postPizzeria()}>Create</Button>
          <Button variant="outline-info" onClick={() => openCloseModalCreate()}>Back</Button>
        </ModalFooter>
      </Modal>

        {/* Update */}
        <Modal isOpen={showModalUpdate}>
      <ModalHeader>Edit Pizzeria Categoriaa </ModalHeader>
      <ModalBody>
        <Form>
          <Form.Group>
            <Form.Label>Id:</Form.Label>
            <Form.Control type="text" id="txtid" name="Id" readOnly value={currentPizzeria.id} />
          </Form.Group>
          <Form.Group>
            <Form.Label>Categoriaa:</Form.Label>
            <Form.Control type="text" id="txtcategoriaaa" name="categoriaa" required isInvalid={!currentPizzeria.categoriaa && validationError !== ''} onChange={handleChange} value={currentPizzeria.categoriaa} />
            <Form.Control.Feedback type="invalid">{validationError}</Form.Control.Feedback>
          </Form.Group>
        </Form>
      </ModalBody>
      <ModalFooter>
        <Button variant="primary" onClick={() => putPizzeria()}>Save</Button>
        <Button variant="outline-info" onClick={() => openCloseModalUpdate()}>Back</Button>
      </ModalFooter>
    </Modal>

    {/* Details */}
    <Modal isOpen={showModalDetails}>
      <ModalHeader>Details Pizzeria Categoriaa</ModalHeader>
      <ModalBody>
        <Form>
          <Form.Group>
            <Form.Label>Id:</Form.Label>
            <Form.Control type="text" id="txtid" name="Id" readOnly value={currentPizzeria.id} />
          </Form.Group>
          <Form.Group>
            <Form.Label>Categoriaa:</Form.Label>
            <Form.Control type="text" id="txtcategoriaaa" name="categoriaa" readOnly value={currentPizzeria.categoriaa} />
          </Form.Group>
        </Form>
      </ModalBody>
      <ModalFooter>
        <Button variant="outline-info" onClick={() => openCloseModalDetails()}>Back</Button>
      </ModalFooter>
    </Modal>

    {/* Delete */}
    <Modal isOpen={showModalDelete}>
      <ModalHeader>Are you sure to delete this Pizzeria Categoriaa?</ModalHeader>
      <ModalBody>
        <Form>
          <Form.Group>
            <Form.Label><b>Id:</b></Form.Label>
            <Form.Label>{currentPizzeria.id}</Form.Label><br />
            <Form.Label><b>categoriaa:</b></Form.Label>
            <Form.Label>{currentPizzeria.categoriaa}</Form.Label><br />
          </Form.Group>
        </Form>
      </ModalBody>
      <ModalFooter>
        <Button variant="danger" onClick={() => deletePizzeria(currentPizzeria.id)}>Delete</Button>
        <Button variant="outline-info" onClick={() => openCloseModalDelete()}>Back</Button>
      </ModalFooter>
    </Modal>
  </Container>
);
}