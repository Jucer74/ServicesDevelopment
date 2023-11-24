import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { Layout } from './Layout';
import { NoMatch } from './components/NoMatch';
import { Home } from './components/Home';
import { PizzeriaList } from './components/Users';
import { CategoriaList } from './components/PizzeriaList';
import { NavigationBar } from './components/NavigationBar';
import { Footer } from './components/Footer';
import { Contactus } from './components/Contact';
import { Todas } from './components/Todas';
import Homes from './components/Homes';
import Details from './components/Details';
import Login from './components/Login';




function App() {
  return (
    <div className="App">
      <React.Fragment>
        <NavigationBar />
        <Layout>
          <Router>
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/Users" element={<PizzeriaList />} />
              <Route path="/PizzeriaList" element={<CategoriaList />} />
              <Route path="/PizzeriaList/:pizzaId" element={<CategoriaList />} />
              <Route path="/Login" element={<Login />} />
              <Route path="/Homes" element={<Homes />} />
              <Route path='/details' element={<Details />} />
              <Route path="/Contactanos" element={<Contactus />} />
              <Route path="/Todas" element={<Todas />} />
              <Route element={<NoMatch />} />
            </Routes>
          </Router>
        </Layout>
      </React.Fragment>
      <Footer/>
    </div>
  );
}

export default App;
