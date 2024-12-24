import React from 'react';
import { Tabs } from 'antd';
import ToolTable from './TablesToMange/ToolTable';
import WorkerTable from './TablesToMange/WorkerTable';
import DepartmentTable from './TablesToMange/DepartmentTable';
import ModelTable from './TablesToMange/ModelTable';
import CategoryTable from './TablesToMange/CategoryTable';
import ManufacturerTable from './TablesToMange/ManufacturerTable';

const { TabPane } = Tabs; // Деструктурируем TabPane из Tabs

const ManageTables = () => {
    return (
        <div>
            <h1>Управление таблицами</h1>
            <Tabs defaultActiveKey="1">
                <TabPane tab="Инструменты" key="1">
                    <ToolTable />
                </TabPane>
                <TabPane tab="Работники" key="2">
                    <WorkerTable />
                </TabPane>
                <TabPane tab="Отделы" key="3">
                    <DepartmentTable />
                </TabPane>
                <TabPane tab="Модели" key="4">
                    <ModelTable />
                </TabPane>
                <TabPane tab="Категории" key="5">
                    <CategoryTable />
                </TabPane>
                <TabPane tab="Производители" key="6">
                    <ManufacturerTable />
                </TabPane>
            </Tabs>
        </div>
    );
};

export default ManageTables;
