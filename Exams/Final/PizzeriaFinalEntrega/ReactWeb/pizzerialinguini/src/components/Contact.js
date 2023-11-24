import React, { useState } from 'react';
import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';
import L from 'leaflet';

const redIcon = new L.Icon({
  iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-red.png',
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34],
  shadowSize: [41, 41],
});

export const Contactus = () => {
  const [formData, setFormData] = useState({
    fullName: '',
    phoneNumber: '',
    email: '',
    message: '',
  });

  const [formSubmitted, setFormSubmitted] = useState(false);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log('Formulario enviado:', formData);
    setFormSubmitted(true);

    setFormData({
      fullName: '',
      phoneNumber: '',
      email: '',
      message: '',
    });
  };

  return (
    <div>
      <section className="py-4 bg-primary">
        <div className="container">
          <div className="row">
            <div className="col-md-12 text-center">
              <h2>Contactenos</h2>
            </div>
          </div>
        </div>
      </section>

      <section className="section">
        <div className="container">
          <div className="card shadow">
            <div className="card-body">
              <div className="row">
                <div className="col-md-6">
                  <h2 className="mb-4">Formulario de Contacto</h2>
                  <form onSubmit={handleSubmit}>
                    <div className="mb-3">
                      <label className="form-label">Nombre Completo</label>
                      <input
                        type="text"
                        className="form-control"
                        placeholder="..."
                        name="fullName"
                        value={formData.fullName}
                        onChange={handleChange}
                      />
                    </div>
                    <div className="mb-3">
                      <label className="form-label">Numero Telefonico</label>
                      <input
                        type="text"
                        className="form-control"
                        placeholder="..."
                        name="phoneNumber"
                        value={formData.phoneNumber}
                        onChange={handleChange}
                      />
                    </div>
                    <div className="mb-3">
                      <label className="form-label">Correo Electronico</label>
                      <input
                        type="text"
                        className="form-control"
                        placeholder="..."
                        name="email"
                        value={formData.email}
                        onChange={handleChange}
                      />
                    </div>
                    <div className="mb-3">
                      <label className="form-label">Mensaje</label>
                      <textarea
                        rows="3"
                        className="form-control"
                        placeholder="Escribe tu mensaje..."
                        name="message"
                        value={formData.message}
                        onChange={handleChange}
                      ></textarea>
                    </div>
                    <div className="mb-3">
                      <button type="submit" className="btn btn-primary w-100">
                        Enviar
                      </button>
                    </div>
                  </form>
                </div>
                <div className="col-md-6 border-start">
                  <h5 className="main-heading mb-4">Ejemplo de Datos</h5>
                  <p>Nombre: luiz Martinez Londoño Perez</p>
                  <p>Numero Telefonico: 323-371-89-04</p>
                  <p>Correo Electronico: holacomoestas@gmail.com</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>

      {formSubmitted && (
        <div className="alert alert-success mt-3" role="alert">
          Mensaje enviado con éxito.
        </div>
      )}

      <section className="mt-4">
        <MapContainer
          center={[3.3692, -76.5275]}
          zoom={18}
          style={{ height: '400px', width: '100%', border: '2px solid #ddd' }}
        >
          <TileLayer
            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
          />
          <Marker position={[3.3692, -76.5275]} icon={redIcon}>
            <Popup>
              A pretty CSS3 popup. <br /> Easily customizable.
            </Popup>
          </Marker>
        </MapContainer>
      </section>
    </div>
  );
};






