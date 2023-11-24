import React, { Component } from 'react';
import axios from 'axios';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBookReader, faEdit, faTrashAlt } from '@fortawesome/free-solid-svg-icons';
import { Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';

import 'bootstrap/dist/css/bootstrap.min.css';

const url = 'http://127.0.0.1:4000/api/Libros';

class Libros extends Component {
  state = {
    data: [],
    autores: [],
    modalInsertar: false,
    modalEliminar: false,
    modalConsultar: false,
    form: {
      titulo: '',
      id: 0,
      imagen: '',
      fecha: '',
      categoria: '',
      tipoModal: '',
    },
    form2: {
      id: '',
      libroId: 0,
      nombre: '',
      apellido: '',
      pais: '',
      tipoModal: '',
    },
  };

  peticionAutores = (id) => {
    axios
      .get(`https://localhost:5001/api/Autores/team/${id}`)
      .then((response) => {
        this.setState({
          autores: response.data,
        });
        this.modalConsultar();
      })
      .catch((error) => {
        console.log(error.message);
      });
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
      const response = await axios.put(`${url}/${this.state.form.id}`, this.state.form);
      this.modalInsertar();
      this.peticionGet();
    } catch (error) {
      console.log(error.message);
    }
  };

  peticionDelete = async () => {
    try {
      const response = await axios.delete(`${url}/${this.state.form.id}`);
      this.setState({ modalEliminar: false });
      this.peticionGet();
    } catch (error) {
      console.log(error.message);
    }
  };

  modalInsertar = () => {
    this.setState({ modalInsertar: !this.state.modalInsertar });
  };

  modalConsultar = () => {
    this.setState({ modalConsultar: !this.state.modalConsultar });
  };

  seleccionarLibro = (libro) => {
    this.setState({
      tipoModal: 'actualizar',
      form: {
        titulo: libro.titulo,
        id: libro.id,
        imagen: libro.imagen,
        fecha: libro.fecha,
        categoria: libro.categoria,
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
              <th>Título</th>
              <th>ID</th>
              <th>Imagen</th>
              <th>Fecha</th>
              <th>Categoría</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {this.state.data.map((libro) => {
              return (
                <tr key={libro.id}>
                  <td>{libro.titulo}</td>
                  <td>{libro.id}</td>
                  <td>
                    <img src={libro.imagen} alt={libro.titulo} width="100px" height="100px" />
                  </td>
                  <td>{libro.fecha}</td>
                  <td>{libro.categoria}</td>
                  <td>
                    <button
                      className="btn btn-primary"
                      onClick={() => {
                        this.seleccionarLibro(libro);
                        this.modalInsertar();
                      }}
                    >
                      <FontAwesomeIcon icon={faEdit} />
                    </button>
                    {'   '}
                    <button
                      className="btn btn-primary"
                      onClick={() => {
                        this.seleccionarLibro(libro);
                        this.peticionAutores(libro.id);
               
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
              <label htmlFor="titulo">Título</label>
              <input
                className="form-control"
                type="text"
                name="titulo"
                id="titulo"
                onChange={this.handleChange}
                value={form ? form.titulo : ''}
              />
              <br />
              <label htmlFor="imagen">Imagen</label>
              <input
                className="form-control"
                type="text"
                name="imagen"
                id="imagen"
                onChange={this.handleChange}
                value={form ? form.imagen : ''}
              />
              <br />
              <label htmlFor="fecha">Fecha</label>
              <input
                className="form-control"
                type="date"
                name="fecha"
                id="fecha"
                onChange={this.handleChange}
                value={form ? form.fecha : ''}
              />
              <br />
              <label htmlFor="categoria">Categoría</label>
              <input
                className="form-control"
                type="text"
                name="categoria"
                id="categoria"
                onChange={this.handleChange}
                value={form ? form.categoria : ''}
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

        <Modal isOpen={this.state.modalConsultar}>
          <ModalHeader style={{ display: 'block' }}>
            <span style={{ float: 'right' }} onClick={() => this.modalConsultar()}>
              x
            </span>
          </ModalHeader>
          <ModalBody>
            <table className="table">
              <thead>
                <tr>
                  <th>ID</th>
                  <th>Libro ID</th>
                  <th>Nombre</th>
                  <th>Apellido</th>
                  <th>País</th>
                </tr>
              </thead>
              <tbody>
                {this.state.autores.map((autor) => (
                  <tr key={autor.id}>
                    <td>{autor.libroId}</td>
                    <td>{autor.nombre}</td>
                    <td>{autor.apellido}</td>
                    <td>{autor.pais}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </ModalBody>
          <ModalFooter>
            <button className="btn btn-secondary" onClick={() => this.modalConsultar()}>
              Cerrar
            </button>
          </ModalFooter>
        </Modal>

        <Modal isOpen={this.state.modalEliminar}>
          <ModalBody>Estás seguro que deseas eliminar el libro {form && form.titulo}</ModalBody>
          <ModalFooter>
            <button className="btn btn-danger" onClick={() => this.peticionDelete()}>
              Sí
            </button>
            <button className="btn btn-secondary" onClick={() => this.setState({ modalEliminar: false })}>
              No
            </button>
          </ModalFooter>
        </Modal>
      </div>
    );
  }
}

export default Libros;
