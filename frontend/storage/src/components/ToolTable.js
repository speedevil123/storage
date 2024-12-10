import React from 'react';
import {Table} from 'antd';
import { useEffect, useState } from 'react';
import {GETRequest} from '../request'


//Response - ответ
//Request - запрос

function ToolTable() {

    const[tools, setTools] = useState([]);

    const getTools = async () => {
        try {
            const toolsData = await GETRequest('/Tools');
            setTools(toolsData);
        }
        catch (error) {
            console.error('Error fetching tools:', error);
        }
    };

    //[] - пустые зависимости
    //значит только при открытии будет рендер
    useEffect(() => {
        getTools();
    }, [])

    //name-model???
    const dataSource = tools.map(tool => ({
        key: tool.id,
        name: tool.model,
        type: tool.type,
        manufacturer: tool.manufacturer,
        quantity: tool.quantity,
        isTaken: tool.isTaken
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