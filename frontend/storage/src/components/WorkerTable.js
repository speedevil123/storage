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

const WorkerTable = () => {
    const [departments, setDepartments] = useState([]);
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
          setDepartments(departmentsData);
      } catch (error) {
          message.error('Ошибка загрузки отделов.');
      }
    };

    const getWorkers = async () => {
        try {
            const workersData = await GETRequest('/Workers');
            const workerRows = workersData.map((worker) => ({
                key: worker.id,
                name: worker.name,
                position: worker.position,
                email: worker.email,
                phoneNumber: worker.phoneNumber,
                registrationDate: worker.registrationDate,
                departmentId: worker.departmentId
            }))
            setDataSource(workerRows);
        } catch (error) {
            message.error('Ошибка загрузки работников.');
        }
    };

    useEffect(() => {
        setAddingKey(false);
        getDepartments();
        getWorkers();
    }, []);

    const validateRow = (record) => {   
        const errors = []; 
        if (!record.name || record.name.trim() === '' ||
            !record.position || record.position.trim() === '' ||
            !record.phoneNumber || record.phoneNumber.trim() === '' ||
            !record.email || record.email.trim() === '' ||
            !record.departmentName || record.departmentName.trim() === ''
            )
        {
            errors.push('Пожалуйста заполните все поля');
        }
        if (filteredDataSource.some(worker => 
            worker.name === record.name &&
            worker.phoneNumber === record.phoneNumber &&
            worker.email === record.email &&
            worker.departmentName === record.departmentName &&
            worker.key !== record.key
            ))
        {
            errors.push('Данный работник уже существует');
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

    const handleAddWorker = async (key) => {
        const record = dataSource.find((item) => item.key === key)
        if(!record) return;

        const errors = validateRow(record);

        if (errors.length > 0) {
            message.error(errors.map((error) => <p style={{margin: '0', textAlign: 'left'}}>{error}</p>));
            return;
        }

        const newWorker = {
            id: record.key,
            name: record.name,
            position: record.position,
            email: record.email,
            phoneNumber: record.phoneNumber,
            registrationDate: record.registrationDate,
            departmentId: record.departmentId
        }

        const workerId = await POSTRequest('/Workers', newWorker);

        if (workerId) {
            setDataSource((prevData) =>
                prevData.map((item) =>
                    item.key === key ? {...item, ...newWorker, key: item.key} : item
                )
            );            
            message.success('Работник добавлен.');
            setAddingKey(null);
        } 
        else {
            message.error('Ошибка добавления работника.');
        }
    };

    const showDeleteModal = (key) => {
        setSelectedRowKey(key);
        setIsModalVisible(true);
    };

    const handleCancelModal = () => {
        setIsModalVisible(false);
        setSelectedRowKey(null);
    };

    const handleConfirmDelete = () => {
        if (selectedRowKey) {
            handleDeleteWorker(selectedRowKey);
        }
    };

    const handleEditWorker = async (key) => {
        const record = dataSource.find((item) => item.key === key);
        if (!record) return;
    
        const errors = validateRow(record);
        if (errors.length > 0) {
            message.error(`Ошибка: ${errors.join(' ')}`);
            return;
        }

        const workerToUpdate = { 
            id: record.key,
            name: record.name,
            position: record.position,
            email: record.email,
            phoneNumber: record.phoneNumber,
            registrationDate: record.registrationDate,
            departmentId: record.departmentId
        };
        const workerId = await PUTRequest(`/Workers/${record.key}`, workerToUpdate);

        if (workerId) 
        {
            setDataSource((prevData) =>
                prevData.map((item) =>
                    item.key === key ? {...item, ...workerToUpdate, key: item.key}: item
                )
            );
            message.success('Работник обновлен.');
            setEditingKey(null);
        } 
        else 
        {
            message.error('Ошибка обновления данных работника.');
        }
    };

    const handleDeleteWorker = async (key) => {
        const record = dataSource.find((item) => item.key === key);

        if (addingKey === key) {
            setDataSource((prevData) => prevData.filter((item) => item.key !== key));
            setIsModalVisible(false);
            setAddingKey(null);
            message.info('Данные о работнике удалены.');
            return;
        }

        const workerId = await DELETERequest(`/Workers/${record.key}`);
        if (workerId) 
        {
            setDataSource((prev) => prev.filter((item) => item.key !== key));
            message.info('Данные о работнике удалены.');
        } 
        else 
        {
            message.error('Ошибка удаления данных о работнике.');
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
            position: '',
            email: '',
            phoneNumber: '',
            registrationDate: '',
            departmentId: null,
            departmentName: ''
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
            title: 'ФамилияИО',
            dataIndex: 'name',
            key: 'name',
            sorter: (a, b) => a.name.localeCompare(b.name),
            render: (text, record) =>
                editingKey === record.key || addingKey === record.key ? (
                    <Input
                        value = {record.name}
                        onChange={(e) => {
                            updateField(record.key, 'name', e.target.value);
                        }}
                        value={record.name || ''}
                        style={{ width: '100%' }}
                        placeholder="(Пример: СюткинНЮ)"
                    />
                ) : (
                    <Input type="text" value={record.name || ''} readOnly style={readOnlyStyle} />
                )
        },
        {
          title: 'Позиция',
          dataIndex: 'position',
          key: 'position',
          sorter: (a, b) => a.position.localeCompare(b.position),
          render: (text, record) =>
              editingKey === record.key || addingKey === record.key ? (
                  <Input
                      value = {record.position}
                      onChange={(e) => {
                          updateField(record.key, 'position', e.target.value);
                      }}
                      value={record.position || ''}
                      style={{ width: '100%' }}
                      placeholder="(Пример: Инженер-программист)"
                  />
              ) : (
                  <Input type="text" value={record.position || ''} readOnly style={readOnlyStyle} />
              )
        },
        {
          title: 'Электронная почта',
          dataIndex: 'email',
          key: 'email',
          sorter: (a, b) => a.email.localeCompare(b.email),
          render: (text, record) =>
              editingKey === record.key || addingKey === record.key ? (
                  <Input
                      value = {record.email}
                      onChange={(e) => {
                          updateField(record.key, 'email', e.target.value);
                      }}
                      value={record.email || ''}
                      style={{ width: '100%' }}
                      placeholder="Введите электронную почту"
                  />
              ) : (
                  <Input type="text" value={record.email || ''} readOnly style={readOnlyStyle} />
              )
        },
        {
          title: 'Номер телефона',
          dataIndex: 'phoneNumber',
          key: 'phoneNumber',
          sorter: (a, b) => a.phoneNumber.localeCompare(b.phoneNumber),
          render: (text, record) =>
              editingKey === record.key || addingKey === record.key ? (
                  <Input
                      value = {record.phoneNumber}
                      onChange={(e) => {
                          updateField(record.key, 'phoneNumber', e.target.value);
                      }}
                      value={record.phoneNumber || ''}
                      style={{ width: '100%' }}
                      placeholder="Введите номер телефона"
                  />
              ) : (
                  <Input type="text" value={record.phoneNumber || ''} readOnly style={readOnlyStyle} />
              )
        },
        {
          title: 'Дата регистрации',
          dataIndex: 'registrationDate',
          key: 'registrationDate',
          sorter: (a, b) => a.registrationDate.localeCompare(b.registrationDate),
          render: (text, record) =>
                  <Input type="text" value={new Date() || ''} readOnly style={readOnlyStyle} />
        },
        {
          title: 'Отдел',
          dataIndex: 'department',
          key: 'department',
          sorter: (a, b) => a.departmentName.localeCompare(b.departmentName),
          render: (text, record) =>
              editingKey === record.key || addingKey === record.key ? (
                <AutoComplete
                  value={record.departmentName || ''}
                  onSearch={(searchText) => {
                      const filteredDepartments = departments.filter(department =>
                        `${department.name} (${department.address})`
                          .toLowerCase()
                          .includes(searchText.toLowerCase())
                      );
                      updateField(record.key, 'filteredDepartments', filteredDepartments);
                      updateField(record.key, 'departmentName', searchText);
                  }}
                  options={(record.filteredDepartments || departments).map(department => ({
                      value: department.id,
                      label: `${department.name} (${department.address})`,
                  }))}
                  onSelect={(value) => {
                      const selectedDepartment = departments.find((department) => department.id === value);
                      if (selectedDepartment) {
                          updateField(record.key, 'departmentId', selectedDepartment.id);
                          updateField(record.key, 'departmentName', selectedDepartment.name);
                      }
                  }}
                  style={{ width: '300px' }}
                  placeholder="Выберите отдел"
                />
              ) : (
                  <Input type="text" value={record.departmentName || ''} readOnly style={readOnlyStyle} />
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
                        onClick={() => handleAddWorker(record.key)}
                    />
                    ) : editingKey && editingKey === record.key ? (
                        <Button
                        type="text"
                        icon={<CheckOutlined style={{ color: 'green', fontSize: '18px' }} />}
                        onClick={() => handleEditWorker(record.key)}
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
          <h2>Работники производства</h2>
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

export default WorkerTable;
