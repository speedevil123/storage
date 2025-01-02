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

const CategoriesTable = () => {
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

    const getCategories = async () => {
        try {
            const categoriesData = await GETRequest('/Categories');
            const categoryRows = categoriesData.map((category) => ({
                key: category.id,
                name: category.name,
            }))
            setDataSource(categoryRows);
        } catch (error) {
            message.error('Ошибка загрузки категорий.');
        }
    };

    useEffect(() => {
        setAddingKey(false);
        getCategories();
    }, []);

    const validateRow = (record) => {   
        const errors = []; 
        if (!record.name || record.name.trim() === ''
            )
        {
            errors.push('Пожалуйста заполните все поля');
        }
        if (filteredDataSource.some(category => 
            category.name === record.name &&
            category.key !== record.key
            ))
        {
            errors.push('Данная категория уже существует');
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

    const handleAddCategory = async (key) => {
        const record = dataSource.find((item) => item.key === key)
        if(!record) return;

        const errors = validateRow(record);

        if (errors.length > 0) {
            message.error(errors.map((error) => <p style={{margin: '0', textAlign: 'left'}}>{error}</p>));
            return;
        }

        const newCategory = {
            id: record.key,
            name: record.name
        }

        const categoryId = await POSTRequest('/Categories', newCategory);

        if (categoryId) {
            setDataSource((prevData) =>
                prevData.map((item) =>
                    item.key === key ? {...item, ...newCategory, key: item.key} : item
                )
            );            
            message.success('Категория добавлена.');
            setAddingKey(null);
        } 
        else {
            message.error('Ошибка добавления категории.');
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
            handleDeleteCategory(selectedRowKey);
        }
    };

    const handleEditCategory = async (key) => {
        const record = dataSource.find((item) => item.key === key);
        if (!record) return;
    
        const errors = validateRow(record);
        if (errors.length > 0) {
            message.error(`Ошибка: ${errors.join(' ')}`);
            return;
        }

        const categoryToUpdate = { 
            id: record.key,
            name: record.name 
        };
        const categoryId = await PUTRequest(`/Categories/${record.key}`, categoryToUpdate);

        if (categoryId) 
        {
            setDataSource((prevData) =>
                prevData.map((item) =>
                    item.key === key ? {...item, ...categoryToUpdate, key: item.key}: item
                )
            );
            message.success('Категория обновлена.');
            setEditingKey(null);
        } 
        else 
        {
            message.error('Ошибка обновления категории.');
        }
    };

    const handleDeleteCategory = async (key) => {
        const record = dataSource.find((item) => item.key === key);

        if (addingKey === key) {
            console.log("ВНУТРИ ИФА НА УДАЛЕНИЕ СТРОКИ НОВОЙ")
            setDataSource((prevData) => prevData.filter((item) => item.key !== key));
            setIsModalVisible(false);
            setAddingKey(null);
            message.info('Категория удалена.');
            return;
        }

        const categoryId = await DELETERequest(`/Categories/${record.key}`);
        if (categoryId) 
        {
            setDataSource((prev) => prev.filter((item) => item.key !== key));
            message.info('Категория удалена.');
        } 
        else 
        {
            message.error('Ошибка удаления категории.');
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
            name: ''
        };
        setDataSource((prevData) => [newRow, ...prevData]);
        setAddingKey(newRow.key);

    }

    const readOnlyStyle = {
        backgroundColor: '#f5f5f5',
        cursor: 'not-allowed',
        border: '1px solid #d9d9d9',
        width: '300px' 
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
            title: 'Название категории',
            dataIndex: 'name',
            key: 'name',
            sorter: (a, b) => a.name.localeCompare(b.name),
            render: (text, record) =>
                editingKey === record.key || addingKey === record.key ? (
                    <Input
                        value = {record.name || ''}
                        onChange={(e) => {
                            updateField(record.key, 'name', e.target.value);
                        }}
                        // value={record.name || ''}
                        style={{ width: '300px' }}
                        placeholder="Введите категорию"
                    />
                ) : (
                    <Input type="text" value={record.name || ''} readOnly style={readOnlyStyle} />
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
                        onClick={() => handleAddCategory(record.key)}
                    />
                    ) : editingKey && editingKey === record.key ? (
                        <Button
                        type="text"
                        icon={<CheckOutlined style={{ color: 'green', fontSize: '18px' }} />}
                        onClick={() => handleEditCategory(record.key)}
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
            <h2>Категории инструментов</h2>
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

export default CategoriesTable;
