import React from 'react';
import {Table} from 'antd';
import { useEffect, useState } from 'react';
import {GETRequest} from '../request'


//Response - ответ
//Request - запрос

function OperationHistoryTable() {

    const[operationHistories, setOperationHistories] = useState([]);

    const getOperationHistories = async () => {
        try {
            const operationHistoriesData = await GETRequest('/OperationHistories');
            setOperationHistories(operationHistoriesData);
        }
        catch (error) {
            console.error('Error fetching operation histories:', error);
        }
    };

    //[] - пустые зависимости
    //значит только при открытии будет рендер
    useEffect(() => {
        getOperationHistories();
    }, [])

    //name-model???
    const dataSource = operationHistories.map(operationHistory => ({
        key: operationHistory.id,
        operationType: operationHistory.operationType,
        toolName: operationHistory.toolName,
        workerName: operationHistory.workerName,
        Date: operationHistory.Date,
        Comment: operationHistory.Comment
    }));
      
    const columns = [
        {
            title: 'Operation Type',
            dataIndex: 'operationType',
            key: 'operationType'
        },
        {
            title: 'Tool Name',
            dataIndex: 'toolName',
            key: 'toolName'
        },
        {
            title: 'Worker Name',
            dataIndex: 'workerName',
            key: 'workerName'
        },
        {
            title: 'Date',
            dataIndex: 'Date',
            key: 'Date'
        },
        {
            title: 'Comment',
            dataIndex: 'Comment',
            key: 'Comment'
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

export default OperationHistoryTable;