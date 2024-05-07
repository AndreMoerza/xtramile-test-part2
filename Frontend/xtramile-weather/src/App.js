import './App.css';
import Weather from './Weather';
import {Container} from 'react-bootstrap';
import "bootstrap/dist/css/bootstrap.min.css"; 

function App() {
  return (
    <Container className="App">
      <Weather />
    </Container>
  );
}

export default App;
