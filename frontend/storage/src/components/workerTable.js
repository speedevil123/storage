import React from 'react';
import {Table} from 'antd';
import {useEffect, useState } from 'react';
import {GETRequest} from '../request'


//Response - ответ
//Request - запрос

function WorkerTable() {

    const[workers, setWorkers] = useState([]);

    const getWorkers = async () => {
        try {
            const workersData = await GETRequest('/Workers');
            setWorkers(workersData);
        }
        catch (error) {
            console.error('Error fetching workers:', error);
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
        position: worker.position,
        department: worker.department,
        email: worker.email,
        phone: worker.phone,
        registrationDate: worker.registrationDate
    }));
      
    const columns = [
        {
            title: 'Name',
            dataIndex: 'name',
            key: 'name',
            showSorterTooltip: {
                target: 'full-header',
            },
        },
        {
            title: 'Position',
            dataIndex: 'position',
            key: 'position'
        },
        {
            title: 'Department',
            dataIndex: 'department',
            key: 'department'
        },
        {
            title: 'Email',
            dataIndex: 'email',
            key: 'email'
        },
        {
            title: 'Phone',
            dataIndex: 'phone',
            key: 'phone'
        },
        {
            title: 'Registration Date',
            dataIndex: 'registrationDate',
            key: 'registrationDate'
        }
    ];

      return (
        <div>
            <Table 
            dataSource={dataSource} 
            columns={columns}
            pagination={{pageSize: 10}}
            />
        </div>
      );
}

export default WorkerTable;