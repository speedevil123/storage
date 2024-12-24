import ToolTable from './TablesToMange/ToolTable';
import WorkerTable from './TablesToMange/WorkerTable';
import DepartmentTable from './TablesToMange/DepartmentTable';
import ModelTable from './TablesToMange/ModelTable';
import CategoryTable from './TablesToMange/CategoryTable';
import ManufacturerTable from './TablesToMange/ManufacturerTable';

const ManageTables = () => {
    return (
        <div>
            <ToolTable/>
            <WorkerTable/>
            <DepartmentTable/>
            <ModelTable/>
            <CategoryTable/>
            <ManufacturerTable/>
        </div>
    )
}

export default ManageTables;