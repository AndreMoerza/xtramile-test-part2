import React, { useEffect, useState } from 'react';
import axios from 'axios';
import {Row, Col, FormControl, Button, Card, Form, Table} from 'react-bootstrap';
import Moment from 'react-moment';

const Weather = () => {
  const [city, setCity] = useState('');
  const [cities, setCities] = useState([]);
  const [countries, setCountries] = useState([]);
  const [weatherData, setWeatherData] = useState(null);

  const fetchData = async () => {
    try {
      const response = await axios.get(
        `http://localhost:5000/weather-city?city=${city}`
      );
      if (response.data) {
        setWeatherData(response.data);
      }
    } catch (error) {
      console.error(error);
    }
  };

  useEffect(() => {
    fillCountries();
  }, []);

  const fillCountries = async () => {
    try {
      const response = await axios.get(
        '/countries.json'
      );
      setCountries(response.data);
    } catch (error) {
      console.error(error);
    }
  }

  const fillCities = async (e) => {
    countries.forEach((element) => {
      
      if(element.id == e.target.value)
      {
        setCities(element.cities);
      }
    });
  };

  const handleInputChange = (e) => {
    setCity(e.target.value);
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    fetchData();
  };

  return (
    <>
      <Row>
        <Col>
          <h1>Checking Weather App</h1>
        </Col>
      </Row>
      <Row>
        <Col className="text-center">
          <Form.Select onChange={fillCities}>
            <option value="">Select an country...</option>
            {countries.map((item) => (
              <option key={item.id} value={item.id}>
                {item.name}
              </option>
            ))}
          </Form.Select>
        </Col>
      </Row>
      <Row>
        <Col className="text-center">
          <Form.Select onChange={handleInputChange}>
           <option value="">Select an city...</option>
           {cities.map((key) => (
             <option key={key.id} value={key.name}>
               {key.name}
             </option>
           ))}
         </Form.Select>
        </Col>
      </Row>
      <Row>
        <Col>
          <Button onClick={handleSubmit}>Check Weather</Button>
        </Col>
      </Row>
     
      {weatherData ? 
      (
        <Table striped bordered hover variant="info">
          <tbody>
            <tr>
              <td>Location</td>
              <td>{weatherData.name}</td>
            </tr>
            <tr>
              <td>Time</td>
              <td><Moment date={weatherData.time}/></td>
            </tr>
            <tr>
              <td>Wind</td>
              <td>{weatherData.wind?.speed}m/s</td>
            </tr>
            <tr>
              <td>Visibility</td>
              <td>{weatherData.visibility}</td>
            </tr>
            <tr>
              <td>Sky conditions</td>
              <td>{weatherData?.weather[0]?.description}</td>
            </tr>
            <tr>
              <td>Temperature in Celcius</td>
              <td>{weatherData.main?.tempC}°C</td>
            </tr>
            <tr>
              <td>Temperature in Farenheit</td>
              <td>{weatherData.main?.tempF}°F</td>
            </tr>
            <tr>
              <td>Dew Point</td>
              <td>{weatherData.main?.dewPoint}°C</td>
            </tr>
            <tr>
              <td>Relative Humidity</td>
              <td>{weatherData.main?.humidity}%</td>
            </tr>
            <tr>
              <td>Pressure</td>
              <td>{weatherData.main?.pressure}</td>
            </tr>
          </tbody>
        </Table>
      ): null}
    </>
  );
};

export default Weather;