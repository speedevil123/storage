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

const DepartmentTable = () => {
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

    const getDepartments = async () => {
        try {
            const departmentsData = await GETRequest('/Departments');
            const departmentRows = departmentsData.map((department) => ({
                key: department.id,
                name: department.name,
                phoneNumber: department.phoneNumber,
                email: department.email,
                address: department.address
            }))
            setDataSource(departmentRows);
        } catch (error) {
            message.error('Ошибка загрузки отделов.');
        }
    };

    useEffect(() => {
        setAddingKey(false);
        getDepartments();
    }, []);


    const validateRow = (record) => {   
        const errors = []; 
        if (!record.name || record.name.trim() === '' ||
            !record.phoneNumber || record.phoneNumber.trim() === '' ||
            !record.email || record.email.trim() === '' ||
            !record.address || record.address.trim() === ''
            )
        {
            errors.push('Пожалуйста заполните все поля');
        }
        if (filteredDataSource.some(department => 
            department.name === record.name &&
            department.phoneNumber === record.phoneNumber &&
            department.email === record.email &&
            department.address === record.address &&
            department.key !== record.key
            ))
        {
            errors.push('Данный отдел уже существует');
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

    const handleAddDepartment = async (key) => {
        const record = dataSource.find((item) => item.key === key)
        if(!record) return;

        const errors = validateRow(record);

        if (errors.length > 0) {
            message.error(errors.map((error) => <p style={{margin: '0', textAlign: 'left'}}>{error}</p>));
            return;
        }

        const newDepartment = {
            id: record.key,
            name: record.name,
            phoneNumber: record.phoneNumber,
            email: record.email,
            address: record.address
        }

        const departmentId = await POSTRequest('/Departments', newDepartment);

        if (departmentId) {
            setDataSource((prevData) =>
                prevData.map((item) =>
                    item.key === key ? {...item, ...newDepartment, key: item.key} : item
                )
            );            
            message.success('Отдел добавлен.');
            setAddingKey(null);
        } 
        else {
            message.error('Ошибка добавления отдела.');
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
            handleDeleteDepartment(selectedRowKey);
        }
    };

    const handleEditDepartment = async (key) => {
      console.log(key);
        const record = dataSource.find((item) => item.key === key);
        if (!record) return;
    
        const errors = validateRow(record);
        if (errors.length > 0) {
            message.error(`Ошибка: ${errors.join(' ')}`);
            return;
        }

        const departmentToUpdate = {
            id: record.key,
            name: record.name,
            phoneNumber: record.phoneNumber,
            email: record.email,
            address: record.address
        }

        const departmentId = await PUTRequest(`/Departments/${record.key}`, departmentToUpdate);

        if (departmentId) 
        {
            setDataSource((prevData) =>
                prevData.map((item) =>
                    item.key === key ? {...item, ...departmentToUpdate, key: item.key}: item
                )
            );
            message.success('Отдел обновлен.');
            setEditingKey(null);
        } 
        else 
        {
            message.error('Ошибка обновления отдела.');
        }
    };

    const handleDeleteDepartment = async (key) => {
        const record = dataSource.find((item) => item.key === key);

        if (addingKey === key) {
            setDataSource((prevData) => prevData.filter((item) => item.key !== key));
            setIsModalVisible(false);
            setAddingKey(null);
            message.info('Отдел удален.');
            return;
        }

        const departmentId = await DELETERequest(`/Departments/${record.key}`);
        if (departmentId) 
        {
            setDataSource((prev) => prev.filter((item) => item.key !== key));
            message.info('Отдел удален.');
        } 
        else 
        {
            message.error('Ошибка удаления отдела.');
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
            address: ''
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
            title: 'Отдел',
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
        title: 'Номер отдела',
        dataIndex: 'address',
        key: 'address',
        sorter: (a, b) => a.address.localeCompare(b.address),
        render: (text, record) =>
            editingKey === record.key || addingKey === record.key ? (
                <Input
                    onChange={(e) => {
                        updateField(record.key, 'address', e.target.value);
                    }}
                    value={record.address || ''}
                    style={{ width: '200px' }}
                    placeholder="Введите номер отдела"
                />
            ) : (
                <Input type="text" value={record.address || ''} readOnly style={readOnlyStyle} />
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
                        onClick={() => handleAddDepartment(record.key)}
                    />
                    ) : editingKey && editingKey === record.key ? (
                        <Button
                        type="text"
                        icon={<CheckOutlined style={{ color: 'green', fontSize: '18px' }} />}
                        onClick={() => handleEditDepartment(record.key)}
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
          <h2>Отделы производства</h2>
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

export default DepartmentTable;
