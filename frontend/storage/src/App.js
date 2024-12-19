import './App.css';
import Navbar from './components/Navbar'
import RentalTable from './components/RentalTable';
import ToolTable from './components/ToolTable';
import WorkerTable from './components/WorkerTable';

export default function App() 
{ 
  return (
    <div className="App">
      <div>
        <Navbar/>
      </div>
      <div className="Body">
        <h1>Аренда Инструментов</h1>
        <RentalTable/>
      </div>
    </div>
  );
}