import React, { useState, useEffect } from 'react';
import { Table, Button, Input, message, AutoComplete } from 'antd';
import { EditOutlined, DeleteOutlined, PlusOutlined, CheckOutlined, SearchOutlined } from '@ant-design/icons';
import { GETRequest, POSTRequest, PUTRequest, DELETERequest } from '../request.js';
import ConfirmDeleteModal from './ConfirmDeleteModal';
import { generateGUID } from '../guidGenerator.js';

message.config({
    duration: 3, 
    maxCount: 1, 
});

const ManufacturerTable = () => {
    const [dataSource, setDataSource] = useState([]);
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [selectedRowKey, setSelectedRowKey] = useState(false);
    const [pagination, setPagination] = useState({
        current: 1,
        pageSize: 8,
    });
    const [editingKey, setEditingKey] = useState(false);
    const [addingKey, setAddingKey] = useState(false);

    const [searchText, setSearchText] = useState(''); // Для поиска

    const filteredDataSource = dataSource.filter((item) =>
        Object.values(item).some(
            (value) =>
                value &&
                value.toString().toLowerCase().includes(searchText.toLowerCase())
        )
    );

    const getManufacturers = async () => {
        try {
            const manufacturersData = await GETRequest('/Manufacturers');
            const manufacturerRows = manufacturersData.map((manufacturer) => ({
                key: manufacturer.id,
                name: manufacturer.name,
                phoneNumber: manufacturer.phoneNumber,
                email: manufacturer.email,
                country: manufacturer.country, 
                postIndex: manufacturer.postIndex
            }))
            setDataSource(manufacturerRows);
        } catch (error) {
            message.error('Ошибка загрузки производителей.');
        }
    };

    useEffect(() => {
        setAddingKey(false);
        getManufacturers();
    }, []);

    const validateRow = (record) => {   
        const errors = []; 
        if (!record.name || record.name.trim() === '' ||
            !record.phoneNumber || record.phoneNumber.trim() === '' ||
            !record.email || record.email.trim() === '' ||
            !record.country || record.country.trim() === '' ||
            !record.postIndex || record.postIndex.trim() === ''
            )
        {
            errors.push('Пожалуйста заполните все поля');
        }
        if (filteredDataSource.some(manufacturer => 
            manufacturer.name === record.name &&
            manufacturer.phoneNumber === record.phoneNumber &&
            manufacturer.email === record.email &&
            manufacturer.key !== record.key
            ))
        {
            errors.push('Данный производитель уже существует');
        }
        return errors;
    };

    const updateField = (key, field, value) => {
        setDataSource((prevData) =>
            prevData.map((item) =>
                item.key === key ? { ...item, [field]: value } : item
            )
        );
    };

    const handleAddManufacturer = async (key) => {
        const record = dataSource.find((item) => item.key === key)
        if(!record) return;

        const errors = validateRow(record);

        if (errors.length > 0) {
            message.error(errors.map((error) => <p style={{margin: '0', textAlign: 'left'}}>{error}</p>));
            return;
        }

        const newManufacturer = {
            id: record.key,
            name: record.name,
            phoneNumber: record.phoneNumber,
            email: record.email,
            country: record.country,
            postIndex: record.postIndex
        }

        const manufacturerId = await POSTRequest('/Manufacturers', newManufacturer);

        if (manufacturerId) {
            setDataSource((prevData) =>
                prevData.map((item) =>
                    item.key === key ? {...item, ...newManufacturer, key: item.key} : item
                )
            );            
            message.success('Производитель добавлен.');
            setAddingKey(null);
        } 
        else {
            message.error('Ошибка добавления производителя.');
        }
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
            handleDeleteManufacturer(selectedRowKey);
        }
    };

    const handleEditManufacturer = async (key) => {
        const record = dataSource.find((item) => item.key === key);
        if (!record) return;
    
        const errors = validateRow(record);
        if (errors.length > 0) {
            message.error(`Ошибка: ${errors.join(' ')}`);
            return;
        }

        const manufacturerToUpdate = {
            id: record.key,
            name: record.name,
            phoneNumber: record.phoneNumber,
            email: record.email,
            country: record.country,
            postIndex: record.postIndex
        }

        const manufacturerId = await PUTRequest(`/Manufacturers/${record.key}`, manufacturerToUpdate);

        if (manufacturerId) 
        {
            setDataSource((prevData) =>
                prevData.map((item) =>
                    item.key === key ? {...item, ...manufacturerToUpdate, key: item.key}: item
                )
            );
            message.success('Производитель обновлен.');
            setEditingKey(null);
        } 
        else 
        {
            message.error('Ошибка обновления производителя.');
        }
    };

    const handleDeleteManufacturer = async (key) => {
        const record = dataSource.find((item) => item.key === key);

        if (addingKey === key) {
            setDataSource((prevData) => prevData.filter((item) => item.key !== key));
            setIsModalVisible(false);
            setAddingKey(null);
            message.info('Производитель удален.');
            return;
        }

        const manufacturerId = await DELETERequest(`/Manufacturers/${record.key}`);
        if (manufacturerId) 
        {
            setDataSource((prev) => prev.filter((item) => item.key !== key));
            message.info('Производитель удален.');
        } 
        else 
        {
            message.error('Ошибка удаления производителя.');
        }
        setIsModalVisible(false);
    };

    const handleAddCustomRow = () => {
        if(addingKey)
        {
            message.error('Вы можете добавить только одну строку в режиме добавления.');
            return;
        }

        const newRow = {
            key: generateGUID(),
            name: '',
            phoneNumber: '',
            email: '',
            country: '',
            postIndex: ''
        };
        setDataSource((prevData) => [newRow, ...prevData]);
        setAddingKey(newRow.key);

    }

    const readOnlyStyle = {
        backgroundColor: '#f5f5f5',
        cursor: 'not-allowed',
        border: '1px solid #d9d9d9',
        width: '200px' 
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
            title: 'Компания',
            dataIndex: 'name',
            key: 'name',
            sorter: (a, b) => a.name.localeCompare(b.name),
            render: (text, record) =>
                editingKey === record.key || addingKey === record.key ? (
                    <Input
                        onChange={(e) => {
                            updateField(record.key, 'name', e.target.value);
                        }}
                        value={record.name || ''}
                        style={{ width: '200px' }}
                        placeholder="Введите название"
                    />
                ) : (
                    <Input type="text" value={record.name || ''} readOnly style={readOnlyStyle} />
                )
        },
        {
          title: 'Номер телефона',
          dataIndex: 'phoneNumber',
          key: 'phoneNumber',
          render: (text, record) =>
              editingKey === record.key || addingKey === record.key ? (
                  <Input
                      onChange={(e) => {
                          updateField(record.key, 'phoneNumber', e.target.value);
                      }}
                      value={record.phoneNumber || ''}
                      style={{ width: '200px' }}
                      placeholder="Введите номер телефона"
                  />
              ) : (
                  <Input type="text" value={record.phoneNumber || ''} readOnly style={readOnlyStyle} />
              )
        },
        {
          title: 'Электронная почта',
          dataIndex: 'email',
          key: 'email',
          render: (text, record) =>
              editingKey === record.key || addingKey === record.key ? (
                  <Input
                      onChange={(e) => {
                          updateField(record.key, 'email', e.target.value);
                      }}
                      value={record.email || ''}
                      style={{ width: '200px' }}
                      placeholder="Введите эл. почту"
                  />
              ) : (
                  <Input type="text" value={record.email || ''} readOnly style={readOnlyStyle} />
              )
      },
      {
        title: 'Страна',
        dataIndex: 'country',
        key: 'country',
        sorter: (a, b) => a.country.localeCompare(b.country),
        render: (text, record) =>
            editingKey === record.key || addingKey === record.key ? (
                <Input
                    onChange={(e) => {
                        updateField(record.key, 'country', e.target.value);
                    }}
                    value={record.country || ''}
                    style={{ width: '200px' }}
                    placeholder="Введите страну"
                />
            ) : (
                <Input type="text" value={record.country || ''} readOnly style={readOnlyStyle} />
            )
        },
        {
          title: 'Почтовый индекс',
          dataIndex: 'postIndex',
          key: 'postIndex',
          render: (text, record) =>
            editingKey === record.key || addingKey === record.key ? (
                <Input
                    onChange={(e) => {
                        updateField(record.key, 'postIndex', e.target.value);
                    }}
                    value={record.postIndex || ''}
                    style={{ width: '200px' }}
                    placeholder="Введите почтовый индекс"
                />
            ) : (
                <Input type="text" value={record.postIndex || ''} readOnly style={readOnlyStyle} />
            )
        },
        {
            title: 'Действия',
            key: 'actions',
            width: 150,
            render: (_, record) => (
                <div style={{ display: 'flex', gap: '8px', alignItems: 'center' }}>
                    {addingKey && addingKey === record.key ? (
                        <Button
                        type="text"
                        icon={<CheckOutlined style={{ color: 'green', fontSize: '18px' }} />}
                        onClick={() => handleAddManufacturer(record.key)}
                    />
                    ) : editingKey && editingKey === record.key ? (
                        <Button
                        type="text"
                        icon={<CheckOutlined style={{ color: 'green', fontSize: '18px' }} />}
                        onClick={() => handleEditManufacturer(record.key)}
                    />
                    ) : 
                    <Button
                        type="text"
                        icon={<EditOutlined />}
                        onClick={() => setEditingKey(record.key)}
                    />}
                    
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
            <h2>Производители инструментов</h2>
            <div style={{ marginBottom: '10px', display: 'flex', gap: '10px', alignItems: 'center' }}>
                <Button
                    type="primary"
                    icon={<PlusOutlined />}
                    onClick={handleAddCustomRow}>
                    Добавить
                </Button>
                <Input
                    prefix={<SearchOutlined />}
                    placeholder="Поиск"
                    value={searchText}
                    onChange={(e) => setSearchText(e.target.value)}
                    style={{ marginLeft: '10px', width: '220px' }}
                />
            </div>
            <Table
                dataSource={filteredDataSource}
                columns={columns}
                pagination={{
                    ...pagination,
                    onChange: (page, pageSize) => setPagination({ current: page, pageSize }),
                }}
                onChange={(page) => setPagination(page)}
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

export default ManufacturerTable;
