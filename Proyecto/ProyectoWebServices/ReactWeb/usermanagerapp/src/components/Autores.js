import React, { Component } from 'react';
import axios from 'axios';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBookReader, faEdit, faTrashAlt } from '@fortawesome/free-solid-svg-icons';
import { Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';

import 'bootstrap/dist/css/bootstrap.min.css';

const url = 'https://localhost:5001/api/Autores';
const autores = "http://localhost:3000/Autores/"

class Autores extends Component {
  state = {
    data: [],
    modalInsertar: false,
    modalEliminar: false,
    form: {
      id: '',
      libroId: 0,
      nombre: '',
      apellido: '',
      pais: '',
      tipoModal: '',
    },
  };

  peticionGet = () => {
    axios
      .get(url)
      .then((response) => {
        this.setState({ data: response.data });
      })
      .catch((error) => {
        console.log(error.message);
      });
  };

  peticionPost = async () => {
    try {
      const response = await axios.post(url, this.state.form);
      this.modalInsertar();
      this.peticionGet();
    } catch (error) {
      console.log(error.message);
    }
  };

  peticionPut = async () => {
    try {
      await axios.put(`${url}/${this.state.form.libroId}`, this.state.form);
      this.modalInsertar();
      this.peticionGet();
    } catch (error) {
      console.log(error.message);
    }
  };

  peticionDelete = async () => {
    try {
      await axios.delete(`${url}/${this.state.form.libroId}`);
      this.setState({ modalEliminar: false });
      this.peticionGet();
    } catch (error) {
      console.log(error.message);
    }
  };

  modalInsertar = () => {
    this.setState({ modalInsertar: !this.state.modalInsertar });
  };

  seleccionarLibro = (libro) => {
    this.setState({
      tipoModal: 'actualizar',
      form: {
        id: libro.id,
        libroId: libro.libroId,
        nombre: libro.nombre,
        apellido: libro.apellido,
        pais: libro.pais,
      },
    });
  };

  handleChange = (e) => {
    e.persist();
    this.setState((prevState) => ({
      form: {
        ...prevState.form,
        [e.target.name]: e.target.value,
      },
    }));
  };

  componentDidMount() {
    this.peticionGet();
  }

  render() {
    const { form } = this.state;
    return (
      <div className="App">
        <br />
        <br />
        <br />
        <button
          className="btn btn-success"
          onClick={() => {
            this.setState({ form: null, tipoModal: 'insertar' });
            this.modalInsertar();
          }}
        >
          Agregar Libro
        </button>
        <br />
        <br />
        <table className="table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Libro ID</th>
              <th>Nombre</th>
              <th>Apellido</th>
              <th>País</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {this.state.data.map((libro) => {
              return (
                <tr key={libro.id}>
                  <td>{libro.id}</td>
                  <td>{libro.libroId}</td>
                  <td>{libro.nombre}</td>
                  <td>{libro.apellido}</td>
                  <td>{libro.pais}</td>
                  <td>
                    <button
                      className="btn btn-primary"
                      onClick={() => {
                        this.seleccionarLibro(libro);
                        this.modalInsertar();
                      }}
                    >
                      <FontAwesomeIcon icon={faBookReader} />
                    </button>
                    {'   '}
                    <button
                      className="btn btn-danger"
                      onClick={() => {
                        this.seleccionarLibro(libro);
                        this.setState({ modalEliminar: true });
                      }}
                    >
                      <FontAwesomeIcon icon={faTrashAlt} />
                    </button>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </table>

        <Modal isOpen={this.state.modalInsertar}>
          <ModalHeader style={{ display: 'block' }}>
            <span style={{ float: 'right' }} onClick={() => this.modalInsertar()}>
              x
            </span>
          </ModalHeader>
          <ModalBody>
            <div className="form-group">
              <label htmlFor="nombre">Nombre</label>
              <input
                className="form-control"
                type="text"
                name="nombre"
                id="nombre"
                onChange={this.handleChange}
                value={form ? form.nombre : ''}
              />
              <br />
              <label htmlFor="apellido">Apellido</label>
              <input
                className="form-control"
                type="text"
                name="apellido"
                id="apellido"
                onChange={this.handleChange}
                value={form ? form.apellido : ''}
              />
              <br />
              <label htmlFor="pais">País</label>
              <input
                className="form-control"
                type="text"
                name="pais"
                id="pais"
                onChange={this.handleChange}
                value={form ? form.pais : ''}
              />
            </div>
          </ModalBody>
          <ModalFooter>
            {this.state.tipoModal === 'insertar' ? (
              <button className="btn btn-success" onClick={() => this.peticionPost()}>
                Insertar
              </button>
            ) : (
              <button className="btn btn-primary" onClick={() => this.peticionPut()}>
                Actualizar
              </button>
            )}
            <button className="btn btn-danger" onClick={() => this.modalInsertar()}>
              Cancelar
            </button>
          </ModalFooter>
        </Modal>

        <Modal isOpen={this.state.modalEliminar}>
          <ModalBody>Estás seguro que deseas eliminar el libro {form && form.nombre}</ModalBody>
          <ModalFooter>
            <button className="btn btn-danger" onClick={() => this.peticionDelete()}>
              Sí
            </button>
            <button
              className="btn btn-secondary"
              onClick={() => this.setState({ modalEliminar: false })}
            >
              No
            </button>
          </ModalFooter>
        </Modal>
      </div>
    );
  }
}

export default Autores;
