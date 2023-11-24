import React, { useState, useEffect } from 'react';
import { Carousel, Container, Card, Button } from 'react-bootstrap';

const baseUrl = "http://127.0.0.1:4000/api/Libros";

export function Home() {
  const [data, setData] = useState([]);
  const [currentMedico, setCurrentMedico] = useState({
    id: 0,
    titulo: '',
    imagen: '',
    fecha: '',
    categoria: '',
  });
  const [currentSlide, setCurrentSlide] = useState(1);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(baseUrl);
        const medicosData = await response.json();
        setData(medicosData);
        if (medicosData.length > 0) {
          setCurrentMedico(medicosData[0]);
        }
      } catch (error) {
        console.error('Error al cargar datos:', error);
      }
    };

    fetchData();
  }, []);

  const handlePrevSlide = () => {
    if (currentSlide > 1) {
      setCurrentSlide(currentSlide - 1);
    }
  };

  const handleNextSlide = () => {
    if (currentSlide < Math.ceil(data.length / 3)) {
      setCurrentSlide(currentSlide + 1);
    }
  };

  return (
    <Container>
      <h1 className="text-center" style={{ color: 'white' }}>Médicos Carousel</h1>

      <Carousel
        interval={null} // Desactiva la transición automática
        nextLabel=""
        prevLabel=""
        indicators={false}
        controls={true}
      >
        {data.map((medico, index) => (
          <Carousel.Item key={index}>
            <div className="d-flex justify-content-around">
              {data
                .slice(index * 3, (index + 1) * 3)
                .map((medico, subIndex) => (
                  <Card key={subIndex} className="medico-card">
                    <Card.Img
                      variant="top"
                      src={medico.imagen}
                      alt={medico.titulo}
                      style={{ width: '150px', height: '150px' }} // Set image size
                    />
                    <Card.Body>
                      <Card.Title>{medico.titulo}</Card.Title>
                      <Card.Text>Fecha: {medico.fecha}</Card.Text>
                      <Card.Text>Categoría: {medico.categoria}</Card.Text>
                      <Card.Text>Categoría: {medico.categoria}</Card.Text>
                      <Button
                        variant="danger" // Change the variant to "danger" for dark red color
                        onClick={() => setCurrentMedico(medico)}
                      >
                        Detalles
                      </Button>

                    </Card.Body>
                  </Card>
                ))}
            </div>
          </Carousel.Item>
        ))}
      </Carousel>
    </Container>
  );
}
