import React from 'react';
import {Table} from 'antd';
import { useEffect, useState } from 'react';
import {GETRequest} from '../request'


//Response - ответ
//Request - запрос

function WorkerTable() {

    const[workers, setWorkers] = useState([]);

    const getWorkers = async () => {
        try {
            const workersData = await GETRequest('/Workers');
            setTools(workersData);
        }
        catch (error) {
            console.error('Error fetching tools:', error);
        }
    };

    //[] - пустые зависимости
    //значит только при открытии будет рендер
    useEffect(() => {
        getWorkers();
    }, [])

    const dataSource = workers.map(worker => ({
        key: worker.id,
        name: worker.name,
        pos: worker.type,
        manufacturer: worker.manufacturer,
        quantity: worker.quantity,
        isTaken: worker.isTaken
    }));
      
    const columns = [
        {
            title: 'Model',
            dataIndex: 'name',
            key: 'name',
            showSorterTooltip: {
                target: 'full-header',
            },
        },
        {
            title: 'Type',
            dataIndex: 'type',
            key: 'type'
        },
        {
            title: 'Manufacturer',
            dataIndex: 'manufacturer',
            key: 'manufacturer'
        },
        {
            title: 'Quantity',
            dataIndex: 'quantity',
            key: 'quantity'
        },
        {
            title: 'Is Taken',
            dataIndex: 'isTaken',
            key: 'isTaken',
            render: (text) => text ? 'Yes' : 'No',
        },
    ];

      return (
        <div>
            <Table 
            dataSource={dataSource} 
            columns={columns}
            pagination={{pageSize: 10}}
            showSorterTooltip={{
                target: 'sorter-icon',
              }}
            />
        </div>
      );
}

export default ToolTable;