import React from 'react'
import {AutoComplete, Button, Table} from 'antd'
import { useEffect, useState} from 'react';
import {GETRequest} from '../request'
import Dropdown from 'antd/es/dropdown/dropdown';

const RentalTable = () => {
    const[rentals, setRentals] = useState([]);
    const[workers, setWorkers] = useState([]);
    const[dataSource, setDataSource] = useState([]);

    const updateField = (key, field, value) => {
        setDataSource((prevData) =>
            prevData.map((item) =>
                item.key === key ? { ...item, [field]: value } : item
            )
        );
    };

    const getWorkers = async () => {
        try {
            const workersData = await GETRequest('/Workers');
            setWorkers(workersData);
        }
        catch(error) {
            console.error('Error fetching workers: ', error);
        }
    }

    const getRentals = async () => {
        try {
            const rentalsData = await GETRequest('/Rentals');
            setRentals(rentalsData)
            const aaaa = rentalsData.map(rental => ({
                key: `${Date.now()}-${Math.random()}`,
                workerName: rental.workerName,
                workerId: rental.workerId,
                toolName: rental.toolName,
                toolId: rental.toolId,
                startDate: rental.startDate,
                returnDate: rental.returnDate,
                endDate: rental.endDate,
                status: rental.status,
                toolQuantity: rental.toolQuantity
            }))
            setDataSource(aaaa)
        }
        catch(error) {
            console.error('Error fetching rentals: ', error);
        }
    }

    const handleAddCustomRow = () => {
        const newRow = {
            key: `${Date.now()}-${Math.random()}`,
            workerName: '',
            toolName: 'ашфулауфшаф',
            startDate: '',
            returnDate: '',
            endDate: '',
            status: '',
            toolQuantity: ''
        }
        setRentals((prevData) => [newRow, ...prevData]);
    }

    useEffect(() => {
        getRentals();
        getWorkers();
    }, [])

    const columns = [
        {
            title: "Имя Работника",
            dataIndex: "workerName",
            key: "workerName",
            render: (skip, record) => {
                return (
                    <AutoComplete
                        options={workers.map((worker) => ({
                            value: worker.id,
                            label: worker.name,
                        }))}
                        onSelect={(value) => {
                            const selectedWorker = workers.find(worker => worker.id === value);
                            if (selectedWorker) {
                                console.log(selectedWorker.name)
                                updateField(record.workerId, 'workerName', selectedWorker.name); // Обновляем поле
                            }
                        }}
                        style={{ width: '100%' }}
                        dropdownMatchSelectWidth={false}
                        dropdownStyle={{ minWidth: '150px' }}
                    />
                );
            }
        },
        {
            title: "Название Инструмента",
            dataIndex: "toolName",
            key: "toolName"
        },
        {
            title: "Взят",
            dataIndex: "startDate",
            key: "startDate"
        },
        {
            title: "Фактический Возврат",
            dataIndex: "returnDate",
            key: "returnDate"
        },
        {
            title: "Планируемый Возврат",
            dataIndex: "endDate",
            key: "endDate"
        },
        {
            title: "Статус",
            dataIndex: "status",
            key: "status"
        },
        {
            title: "Кол-во",
            dataIndex: "toolQuantity",
            key: "toolQuantity"
        },

    ];

    return (
        <div>
            <Button onClick={handleAddCustomRow}>Добавить строку</Button>
            <Table
            dataSource={dataSource}
            columns={columns}
            pagination={{pagesize: 10}}
            />
        </div>
    )
}



export default RentalTable;