import React from 'react';
import { AutoComplete, Button, Table, Input } from 'antd';
import { useEffect, useState } from 'react';
import { GETRequest } from '../request';
import { CheckOutlined, CheckCircleOutlined, DeleteOutlined } from '@ant-design/icons';

const RentalTable = () => {
    const [rentals, setRentals] = useState([]);
    const [workers, setWorkers] = useState([]);
    const [tools, setTools] = useState([]);
    const [dataSource, setDataSource] = useState([]);

    const handleActivate = (key) => {
        setDataSource((prevData) =>
            prevData.map((item) =>
                item.key === key ? { ...item, status: 'Активен' } : item
            )
        );
    };

    const handleComplete = (key) => {
        const today = formatDate(new Date()); // Текущая дата
        setDataSource((prevData) =>
            prevData.map((item) =>
                item.key === key ? { ...item, status: 'Завершено', returnDate: today } : item
            )
        );
    };

    const updateField = (key, field, value) => {
        setDataSource((prevData) =>
            prevData.map((item) =>
                item.key === key ? { ...item, [field]: value } : item
            )
        );
    };

    const deleteRow = (key) => {
        setDataSource((prevData) => prevData.filter((item) => item.key !== key));
    };

    const getWorkers = async () => {
        try {
            const workersData = await GETRequest('/Workers');
            setWorkers(workersData);
        } catch (error) {
            console.error('Error fetching workers:', error);
        }
    };

    const getTools = async () => {
        try {
            const toolsData = await GETRequest('/Tools');
            setTools(toolsData);
        } catch (error) {
            console.error('Error fetching tools:', error);
        }
    };

    const getRentals = async () => {
        try {
            const rentalsData = await GETRequest('/Rentals');
            const rentalRows = rentalsData.map((rental) => ({
                key: rental.workerId + " " + rental.ToolId,
                workerName: rental.workerName,
                workerId: rental.workerId,
                toolName: rental.toolName,
                toolId: rental.toolId,
                startDate: rental.startDate,
                returnDate: rental.returnDate,
                endDate: rental.endDate,
                status: rental.status,
                toolQuantity: rental.toolQuantity,
            }));
            setDataSource(rentalRows);
        } catch (error) {
            console.error('Error fetching rentals:', error);
        }
    };

    const formatDate = (date) => {
        const d = new Date(date);
        const day = String(d.getDate()).padStart(2, '0');
        const month = String(d.getMonth() + 1).padStart(2, '0');
        const year = d.getFullYear();
        return `${day}.${month}.${year}`;
    };
    

    const handleAddCustomRow = () => {
        const newRow = {
            key: `new-${dataSource.length + 1}`,
            workerName: '',
            workerId: null,
            toolName: '',
            toolId: null,
            startDate: formatDate(new Date()),
            returnDate: '',
            endDate: '',
            status: 'Неопределен', // Новый статус
            toolQuantity: '',
        };
        setDataSource((prevData) => [newRow, ...prevData]);
    };

    useEffect(() => {
        getRentals();
        getWorkers();
        getTools();
    }, []);

    const readOnlyStyle = {
        backgroundColor: '#f5f5f5',
        cursor: 'not-allowed',
        border: '1px solid #d9d9d9',
    };

    const columns = [
        {
            title: '№',
            key: 'index',
            width: 50,
            render: (text, record, index) => index + 1,
        },
        {
            title: 'Имя Работника',
            dataIndex: 'workerName',
            key: 'workerName',
            width: 200, 
            render: (text, record) => (
                <AutoComplete
                    value={record.workerName || ''}
                    options={workers.map((worker) => ({
                        value: worker.id,
                        label: worker.name,
                    }))}
                    onSelect={(value) => {
                        const selectedWorker = workers.find((worker) => worker.id === value);
                        if (selectedWorker) {
                            updateField(record.key, 'workerId', selectedWorker.id);
                            updateField(record.key, 'workerName', selectedWorker.name);
                        }
                    }}
                    onChange={(value) => {
                        const Worker = workers.find((worker) => worker.id === value);
                        if(Worker) {
                            updateField(record.key, 'workerId', Worker.id);
                            updateField(record.key, 'workerName', Worker.name);
                        }
                    }}
                    style={{ width: '100%' }}
                    placeholder="Выберите работника"
                />
            ),
        },
        {
            title: 'Название Инструмента',
            dataIndex: 'toolName',
            key: 'toolName',
            width: 300, // Ширина для длинных названий инструментов
            render: (text, record) => (
                <AutoComplete
                    value={record.toolName || ''}
                    options={tools.map((tool) => ({
                        value: tool.id,
                        label: `${tool.categoryName} - ${tool.modelName} (${tool.manufacturerName})`,
                    }))}
                    onSelect={(value) => {
                        const selectedTool = tools.find((tool) => tool.id === value);
                        if (selectedTool) {
                            updateField(record.key, 'toolId', selectedTool.id);
                            updateField(record.key, 'toolName', `${selectedTool.categoryName} - ${selectedTool.modelName} (${selectedTool.manufacturerName})`);
                        }
                    }}
                    style={{ width: '100%' }}
                    placeholder="Выберите инструмент"
                />
            ),
        },
        {
            title: 'Дата Взят',
            dataIndex: 'startDate',
            key: 'startDate',
            width: 150, // Умеренная ширина для даты
            render: (text) => (
                <Input type="text" value={text} readOnly style={{ ...readOnlyStyle, width: '100%' }} />
            ),
        },
        {
            title: 'Планируемый Возврат',
            dataIndex: 'endDate',
            key: 'endDate',
            width: 180, // Ширина для поля выбора даты
            render: (text, record) => (
                <Input
                    type="date"
                    value={text || ''}
                    onChange={(e) => updateField(record.key, 'endDate', e.target.value)}
                    style={{ width: '100%' }}
                />
            ),
        },
        {
            title: 'Фактический Возврат',
            dataIndex: 'returnDate',
            key: 'returnDate',
            width: 180, // Достаточно для текстового отображения даты
            render: (text) => (
                <Input type="text" value={text || 'Автоматически'} readOnly style={{ ...readOnlyStyle, width: '100%' }} />
            ),
        },
        {
            title: 'Статус',
            dataIndex: 'status',
            key: 'status',
            width: 150, // Ширина для статуса
            render: (text) => (
                <div
                    style={{
                        width: '100%',
                        backgroundColor:
                    text === 'Активен'
                        ? '#d4edda' // Светло-зеленый
                        : text === 'Завершено'
                        ? '#f8d7da' // Светло-красный
                        : 'transparent', // Прозрачный для других статусов
                        padding: '4px',
                        borderRadius: '4px',
                        textAlign: 'center',
                    }}
                >
                    {text}
                </div>
            ),
        },
        {
            title: 'Кол-во',
            dataIndex: 'toolQuantity',
            key: 'toolQuantity',
            width: 100, // Небольшая ширина, так как тут всего несколько цифр
            render: (text, record) => (
                <Input
                    type="number"
                    value={text || ''}
                    onChange={(e) => updateField(record.key, 'toolQuantity', e.target.value)}
                    style={{ width: '100%' }}
                />
            ),
        },
        {
            title: 'Действия',
            key: 'actions',
            width: 120, // Ширина для кнопок действий
            render: (_, record) => (
                <div style={{ display: 'flex', gap: '8px', alignItems: 'center' }}>
                    {record.status === 'Неопределен' && (
                        <Button
                            type="text"
                            icon={<CheckOutlined style={{ color: 'green', fontSize: '18px' }} />}
                            onClick={() => handleActivate(record.key)}
                        />
                    )}
                    {record.status === 'Активен' && (
                        <Button
                            type="text"
                            icon={<CheckCircleOutlined style={{ color: 'blue', fontSize: '18px' }} />}
                            onClick={() => handleComplete(record.key)}
                        />
                    )}
                    <Button
                        type="text"
                        danger
                        icon={<DeleteOutlined />}
                        onClick={() => deleteRow(record.key)}
                    />
                </div>
            ),
        },
    ];

    return (
        <div>
            <div style={{ marginBottom: '16px', textAlign: 'left' }}>
                <Button onClick={handleAddCustomRow}>Добавить строку</Button>
            </div>
            <Table
                dataSource={dataSource}
                columns={columns}
                pagination={{ pageSize: 10 }}
                rowKey="key"
            />
        </div>
    );
};

export default RentalTable;
