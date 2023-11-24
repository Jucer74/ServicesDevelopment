import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { Layout } from './Layout';
import { NoMatch } from './components/NoMatch';
import { Home } from './components/Home';
import { Login } from './components/Login';
import Register from './components/Register';
import { NavigationBar } from './components/NavigationBar';
import Medicos from './components/Medicos';
import Libros  from './components/Libros';
import ContactForm from './components/ContactForm';
import Autores from './components/Autores';


function App() {
  return (
    <div className="App">
      
      <React.Fragment>
        <NavigationBar />
        <Layout>
          <Router>
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/Medicos" element={<Medicos />} />
              <Route path="/Home" element={<Home />} />
              <Route path="/Contact" element={<ContactForm />} />
              <Route path="/Login" element={<Login />} />
              <Route path="/Libros" element={<Libros />} />
              <Route path="/Autores" element={<Autores />} />
              <Route path="/Register" element={<Register />} />
              <Route element={<NoMatch />} />
            </Routes>
          </Router>
        </Layout>
      </React.Fragment>
    </div>
  );
}

export default App;
