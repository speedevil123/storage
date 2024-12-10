import './App.css';
import ToolTable from './components/ToolTable';
import WorkerTable from './components/WorkerTable';

export default function App() 
{ 
  return (
    <div className="App">
      <ToolTable/>
      <WorkerTable/>
    </div>
  );
}