import './App.css';
import ToolTable from './components/ToolTable';
import WorkerTable from './components/WorkerTable';
import OperationHistoryTable from './components/OperationHistoryTable';

export default function App() 
{ 
  return (
    <div className="App">
      <div>
        <h2>Tool Table</h2>
        <ToolTable/>
      </div>
      <div>
        <h2>Worker Table</h2>
        <WorkerTable/>
      </div>
      <div>
        <h2>Rental Table</h2>
      </div>
      <div>
        <h2>Operation History Table</h2>
        <OperationHistoryTable/>
      </div>
    </div>
  );
}