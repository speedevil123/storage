import React from 'react';
import { AutoComplete, Button, Table, Input } from 'antd';
import { useEffect, useState } from 'react';
import { DELETERequest, GETRequest, POSTRequest, PUTRequest } from '../request';
import { CheckOutlined, CheckCircleOutlined, DeleteOutlined, SearchOutlined } from '@ant-design/icons';
import ConfirmDeleteModal from './ConfirmDeleteModal';
import { message } from 'antd';

message.config({
    duration: 3, 
    maxCount: 1, 
});

const RentalTable = () => {
    const [workers, setWorkers] = useState([]);
    const [tools, setTools] = useState([]);
    const [dataSource, setDataSource] = useState([]);
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [selectedRowKey, setSelectedRowKey] = useState(null);
    const [pagination, setPagination] = useState({
        current: 1,
        pageSize: 10,
    });
    const [searchText, setSearchText] = useState(''); // Для поиска

    const filteredDataSource = dataSource.filter((item) =>
        Object.values(item).some(
            (value) =>
                value &&
                value.toString().toLowerCase().includes(searchText.toLowerCase())
        )
    );

    const validateRow = (record) => {   
        const errors = []; 
        if (
            !record.workerName || record.workerName.trim() === '' ||
            !record.toolName || record.toolName.trim() === '' ||
            !record.toolQuantity) 
        {
            console.log(record.toolQuantity);
            errors.push('Пожалуйста заполните все поля');
        }
        if(record.toolQuantity <= 0)
        {
            errors.push('Количество взятого инструмента не может быть отрицательным');
        }
        // if(!record.endDate || new Date(record.endDate) < new Date())
        // {
        //     errors.push('Дата возврата должна быть позже даты взятия.')
        // }
        return errors;
    };

    const formatDate = (date) => {
        const d = new Date(date);
        const day = String(d.getDate()).padStart(2, '0');
        const month = String(d.getMonth() + 1).padStart(2, '0');
        const year = d.getFullYear();
        const hour = d.getHours();
        const minute = d.getMinutes();
        const seconds = d.getSeconds();
        return `${day}.${month}.${year} ${hour}:${minute}:${seconds}`;
    };
    
    const handleActivate = (key) => {
        const record = dataSource.find((item) => item.key === key);
        if (!record) return;
    
        const errors = validateRow(record);
        if (record.toolQuantity > record.availableQuantity) {
            errors.push('Количество инструментов превышает доступное на складе.');
        }

        if (errors.length > 0) {
            message.error(errors.map((error) => <p style={{margin: '0', textAlign: 'left'}}>{error}</p>));
            return;
        }

        const newRental = {
            workerId: record.workerId,
            toolId: record.toolId,
            startDate: record.startDate,
            returnDate: record.returnDate,
            endDate: formatDate(record.endDate),
            status: 'Активен',
            toolQuantity: record.toolQuantity
        };

        const rentalId = POSTRequest('/Rentals', newRental);
    
        if(rentalId)
        {
            setDataSource((prevData) =>
                prevData.map((item) =>
                    item.key === key ? {...item, ...newRental, key: item.key} : item
                )
            );
            message.success("Аренда успешно добавлена")
        }
        else
        {
            message.error("Ошибка добавления аренды")
        }
    };

    const handleComplete = (key) => {
        const record = dataSource.find((item) => item.key === key);
        if (!record) return;
    
        const errors = validateRow(record);
    
        if (errors.length > 0) {
            message.error(`Ошибка: ${errors.join(' ')}`);
            return;
        }
    
        const today = formatDate(new Date());
        const isOverdue = compareFormattedDates(today, record.endDate);
        
        const rentalToUpdate = {
            startDate: record.startDate,
            returnDate: today,
            endDate: record.endDate,
            status: isOverdue === 1 ? 'Просрочено' : 'Завершено',
            toolQuantity: record.toolQuantity
        }

        const rentalId = PUTRequest(`/Rentals/${record.workerId}/${record.toolId}`, rentalToUpdate);

        if(rentalId)
        {
            setDataSource((prevData) =>
                prevData.map((item) =>
                    item.key === key ? {...item, ...rentalToUpdate, key: item.key}: item
                )
            );
            isOverdue === 1 ? message.warning("Аренда просрочена!") : message.success("Аренда успешно завершена");
        }
        else
        {
            message.error("Ошибка обновления аренды")
        }
    };
    
    

    const updateField = (key, field, value) => {
        setDataSource((prevData) =>
            prevData.map((item) =>
                item.key === key ? { ...item, [field]: value } : item
            )
        );
    };

    const deleteRow = (key) => {
        const record = dataSource.find((item) => item.key === key);

        if (record.status === 'Неопределен') {
            setDataSource((prevData) => prevData.filter((item) => item.key !== key));
            setIsModalVisible(false);
            return;
        }

        const rentalId = DELETERequest(`/Rentals/${record.workerId}/${record.toolId}`);
        if(rentalId)
        {
            setDataSource((prevData) => prevData.filter((item) => item.key !== key));
            message.info("Аренда успешно удалена");
        }
        else
        {
            message.error("Ошибка удаления аренды")
        }
        setIsModalVisible(false);

    };

    const showDeleteModal = (key) => {
        console.log(key);
        setSelectedRowKey(key);
        setIsModalVisible(true);
    };

    const handleCancelModal = () => {
        setIsModalVisible(false);
        setSelectedRowKey(null);
    };

    const handleConfirmDelete = () => {
        if (selectedRowKey) {
            deleteRow(selectedRowKey);
        }
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
            console.log(toolsData);
        } catch (error) {
            console.error('Error fetching tools:', error);
        }
    };

    const getRentals = async () => {
        try {
            const rentalsData = await GETRequest('/Rentals');
            const rentalRows = rentalsData.map((rental) => ({
                key: `${rental.workerId} ${rental.toolId}`,
                workerName: rental.workerName,
                workerId: rental.workerId,
                toolName: rental.toolName,
                toolId: rental.toolId,
                startDate: rental.startDate,
                returnDate: rental.returnDate.toString() == '01.01.0001 12:00:00' ? 'Ожидает возврата' : rental.returnDate,
                endDate: rental.endDate,
                status: rental.status,
                toolQuantity: rental.toolQuantity,
            }));
            setDataSource(rentalRows);
        } catch (error) {
            console.error('Error fetching rentals:', error);
        }
    };

    const parseFormattedDate = (formattedDate) => {
        const [datePart, timePart] = formattedDate.split(' ');
        const [day, month, year] = datePart.split('.').map(Number);
        const [hour, minute, second] = timePart.split(':').map(Number);
        return new Date(year, month - 1, day, hour, minute, second); 
    };
    
    const compareFormattedDates = (formattedDate1, formattedDate2) => {
        const date1 = parseFormattedDate(formattedDate1);
        const date2 = parseFormattedDate(formattedDate2);
    
        if (date1 > date2) return 1;
        if (date1 < date2) return -1; 
        return 0; 
    };

    const handleAddCustomRow = () => {
        const hasUndefinedRow = dataSource.some((row) => row.status === 'Неопределен');
        if (hasUndefinedRow) {
            message.error('Вы можете добавить только одну строку в режиме добавления.');
            return;
        }

        const newRow = {
            key: `new-${dataSource.length + 1}`,
            workerName: '',
            workerId: null,
            toolName: '',
            toolId: null,
            startDate: formatDate(new Date()),
            returnDate: '',
            endDate: '',
            status: 'Неопределен',
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
            render: (text, record, index) => {
                const currentPage = pagination.current || 1; 
                const pageSize = pagination.pageSize || 10;  
                return (currentPage - 1) * pageSize + index + 1; 
            },
        },
        {
            title: 'Имя Работника',
            dataIndex: 'workerName',
            key: 'workerName',
            width: 200,
            sorter: (a, b) => a.workerName.localeCompare(b.workerName),
            filters: [
                ...new Set(dataSource.map((item) => item.workerName)),
            ].map((worker) => ({ text: worker, value: worker })),
            onFilter: (value, record) => record.workerName.includes(value), 
            render: (text, record) => (
                record.status === 'Неопределен' ? (
                    <AutoComplete
                        value={record.workerName || ''}
                        onSearch={(searchText) => {
                            const filteredWorkers = workers.filter(worker =>
                                worker.name.toLowerCase().includes(searchText.toLowerCase())
                            );
                            updateField(record.key, 'filteredWorkers', filteredWorkers);
                            updateField(record.key, 'workerName', searchText);
                        }}
                        options={(record.filteredWorkers || workers).map(worker => ({
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
                        style={{ width: '100%' }}
                        placeholder="Выберите работника"
                    />
                ) : (
                    <Input type="text" value={record.workerName || ''} readOnly style={readOnlyStyle} />
                )
            ),
        },
        {
            title: 'Название Инструмента',
            dataIndex: 'toolName',
            key: 'toolName',
            width: 250,
            sorter: (a, b) => a.toolName.localeCompare(b.toolName),
            filters: [
                ...new Set(dataSource.map((item) => item.toolName)),
            ].map((tool) => ({ text: tool, value: tool })),
            onFilter: (value, record) => record.toolName.includes(value), 
            render: (text, record) => (
                record.status === 'Неопределен' ? (
                <AutoComplete
                    value={record.toolName || ''}
                    onSearch={(searchText) => {
                        const filteredTools = tools.filter(tool =>
                            `${tool.categoryName} ${tool.modelName} - ${tool.manufacturerName}`
                                .toLowerCase()
                                .includes(searchText.toLowerCase())
                        );
                        updateField(record.key, 'filteredTools', filteredTools);
                        updateField(record.key, 'toolName', searchText);
                    }}
                    options={(record.filteredTools || tools).map(tool => {
                        const takenQuantity = dataSource
                            .filter((rental) => rental.toolId === tool.id && rental.status === 'Активен')
                            .reduce((sum, rental) => sum + rental.toolQuantity, 0);
                        const availableQuantity = tool.quantity - takenQuantity;
        
                        return {
                            value: tool.id,
                            label: `(${availableQuantity} шт.) ${tool.categoryName} ${tool.modelName} - ${tool.manufacturerName}`,
                        };
                    })}
                    onSelect={(value) => {
                        const selectedTool = tools.find(tool => tool.id === value);
                        if (selectedTool) {
                            const takenQuantity = dataSource
                                .filter((rental) => rental.toolId === selectedTool.id && rental.status === 'Активен')
                                .reduce((sum, rental) => sum + rental.toolQuantity, 0);
                            const availableQuantity = selectedTool.quantity - takenQuantity;
        
                            updateField(record.key, 'toolId', selectedTool.id);
                            updateField(record.key, 'toolName', `${selectedTool.categoryName} ${selectedTool.modelName} - ${selectedTool.manufacturerName}`);
                            updateField(record.key, 'availableQuantity', availableQuantity);
                        }
                    }}
                    dropdownStyle={{ minWidth: '350px' }} // Установите ширину выпадающего списка
                    style={{ width: '100%' }}
                    placeholder="Введите название инструмента"
                />
                ) : (
                    <Input type="text" value={record.toolName || ''} readOnly style={readOnlyStyle} />
                )
            ),
        },        
        {
            title: 'Дата Взят',
            dataIndex: 'startDate',
            key: 'startDate',
            width: 165,
            sorter: (a, b) => a.startDate.localeCompare(b.startDate), 
            render: (text) => (
                <Input type="text" value={text} readOnly style={{ ...readOnlyStyle, width: '100%' }} />
            ),
        },
        {
            title: 'Планируемый Возврат',
            dataIndex: 'endDate',
            key: 'endDate',
            width: 135, 
            sorter: (a, b) => a.endDate.localeCompare(b.endDate), 
            render: (text, record) => (
                record.status === 'Неопределен' ? (
                <Input
                    type="date"
                    value={text || ''}
                    onChange={(e) => updateField(record.key, 'endDate', e.target.value)}
                    style={{ width: '100%' }}
                />
            ) : (
                <Input type="text" value={record.endDate || ''} readOnly style={readOnlyStyle} />
            )),
        },
        {
            title: 'Фактический Возврат',
            dataIndex: 'returnDate',
            key: 'returnDate',
            width: 165, 
            sorter: (a, b) => a.returnDate.localeCompare(b.returnDate), 
            render: (text) => (
                <Input type="text" value={text || 'Ожидает возврата'} readOnly style={{ ...readOnlyStyle, width: '100%' }} />
            ),
        },
        {
            title: 'Статус',
            dataIndex: 'status',
            key: 'status',
            width: 100, 
            sorter: (a, b) => a.status.localeCompare(b.status), 
            filters: [
                { text: 'Неопределен', value: 'Неопределен' },
                { text: 'Активен', value: 'Активен' },
                { text: 'Завершено', value: 'Завершено' },
                { text: 'Просрочено', value: 'Просрочено' },
            ],
            onFilter: (value, record) => record.status === value,
            render: (text) => (
                <div
                    style={{
                        width: '100%',
                        backgroundColor:
                            text === 'Активен'
                                ? '#d4edda' 
                                : text === 'Завершено'
                                ? '#f8d7da' 
                                : text === 'Просрочено'
                                ? '#FFE4B5'
                                : 'transparent', 
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
            width: 100, 
            sorter: (a, b) => a.toolQuantity - b.toolQuantity, 
            render: (text, record) => (
                record.status === 'Неопределен' ? (
                    <Input
                        type="number"
                        value={text || ''}
                        onChange={(e) => updateField(record.key, 'toolQuantity', e.target.value)}
                        style={{ width: '100%' }}
                    />
                ) : (
                    <Input type="number" value={text || ''} readOnly style={readOnlyStyle} />
                )
            )
        },
        {
            title: 'Действия',
            key: 'actions',
            width: 120, 
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
                        onClick={() => showDeleteModal(record.key)}
                    />
                </div>
            ),
        },
    ];

    return (
        <div>
            <div style={{ marginBottom: '5px', textAlign: 'left' }}>
                <Button onClick={handleAddCustomRow}>Добавить строку</Button>
                <Input
                    prefix={<SearchOutlined />}
                    placeholder="Поиск"
                    value={searchText}
                    onChange={(e) => setSearchText(e.target.value)}
                    style={{ marginBottom: '5px',marginLeft: '10px', width: '220px' }}
                />
            </div>
            <Table
                dataSource={filteredDataSource}
                columns={columns}
                pagination={{
                    ...pagination,
                    onChange: (page, pageSize) => setPagination({ current: page, pageSize }),
                }}
                rowKey="key"
            />
            <ConfirmDeleteModal
                visible={isModalVisible}
                onConfirm={handleConfirmDelete}
                onCancel={handleCancelModal}
            />
        </div>
    );
};

export default RentalTable;
