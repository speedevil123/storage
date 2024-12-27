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

const ModelTable = () => {
    const [categories, setCategories] = useState([]);
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
          setCategories(categoriesData);
      } catch (error) {
          message.error('Ошибка загрузки категорий.');
      }
    };

    const getModels = async () => {
        try {
            const modelsData = await GETRequest('/Models');
            const modelRows = modelsData.map((model) => ({
                key: model.id,
                categoryName: model.categoryName,
                categoryId: model.categoryId,
                name: model.name,
            }))
            setDataSource(modelRows);
        } catch (error) {
            message.error('Ошибка загрузки моделей.');
        }
    };

    useEffect(() => {
        getModels();
        getCategories();
    }, []);

    const validateRow = (record) => {   
        const errors = []; 
        if (!record.name || record.name.trim() === '')
        {
            errors.push('Пожалуйста заполните все поля');
        }
        if (filteredDataSource.some(model => model.name === record.name && model.key !== model.key))
        {
            errors.push('Модель с таким названием уже существует');
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

    const handleAddModel = async (key) => {
        const record = dataSource.find((item) => item.key === key)
        if(!record) return;

        const errors = validateRow(record);

        if (errors.length > 0) {
            message.error(errors.map((error) => <p style={{margin: '0', textAlign: 'left'}}>{error}</p>));
            return;
        }

        const newModel = {
            id: record.key,
            name: record.name,
            categoryId: record.categoryId
        }

        const modelId = await POSTRequest('/Models', newModel);

        if (modelId) {
            setDataSource((prevData) =>
                prevData.map((item) =>
                    item.key === key ? {...item, ...newModel, key: item.key} : item
                )
            );            
            message.success('Модель добавлена.');
            setAddingKey(null);
        } 
        else {
            message.error('Ошибка добавления модели.');
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
            handleDeleteModel(selectedRowKey);
        }
    };

    const handleEditModel = async (key) => {
        const record = dataSource.find((item) => item.key === key);
        if (!record) return;
    
        const errors = validateRow(record);
        if (errors.length > 0) {
            message.error(`Ошибка: ${errors.join(' ')}`);
            return;
        }

        const modelToUpdate = { 
            id: record.key,
            name: record.name,
            categoryId: record.categoryId
        };
        const modelId = await PUTRequest(`/Models/${record.key}`, modelToUpdate);

        if (modelId) 
        {
            setDataSource((prevData) =>
                prevData.map((item) =>
                    item.key === key ? {...item, ...modelToUpdate, key: item.key}: item
                )
            );
            message.success('Модель обновлена.');
            setEditingKey(null);
        } 
        else 
        {
            message.error('Ошибка обновления модели.');
        }
    };

    const handleDeleteModel = async (key) => {
        const record = dataSource.find((item) => item.key === key);

        if (addingKey === key) {
            setDataSource((prevData) => prevData.filter((item) => item.key !== key));
            setIsModalVisible(false);
            setAddingKey(null);
            message.info('Модель удалена.');
            return;
        }

        const modelId = await DELETERequest(`/Models/${record.key}`);
        if (modelId) 
        {
            setDataSource((prev) => prev.filter((item) => item.key !== key));
            message.info('Модель удалена.');
        } 
        else 
        {
            message.error('Ошибка удаления модели.');
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
            categoryId: '',
            categoryName: ''
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
            title: 'Модель',
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
                        style={{ width: '300px' }}
                        placeholder="Введите модель"
                    />
                ) : (
                    <Input type="text" value={record.name || ''} readOnly style={readOnlyStyle} />
                )
        },
        {
          title: 'Категория',
          dataIndex: 'categoryName',
          key: 'categoryName',
          sorter: (a, b) => a.categoryName.localeCompare(b.categoryName),
          render: (text, record) =>
              editingKey === record.key || addingKey === record.key ? (
                <AutoComplete
                  value={record.categoryName || ''}
                  onSearch={(searchText) => {
                      const filteredCategories = categories.filter(category =>
                        category.name.toLowerCase().includes(searchText.toLowerCase())
                      );
                      updateField(record.key, 'filteredCategories', filteredCategories);
                      updateField(record.key, 'categoryName', searchText);
                  }}
                  options={(record.filteredCategories || categories).map(category => ({
                      value: category.id,
                      label: category.name,
                  }))}
                  onSelect={(value) => {
                      const selectedCategory = categories.find((category) => category.id === value);
                      if (selectedCategory) {
                          updateField(record.key, 'categoryId', selectedCategory.id);
                          updateField(record.key, 'categoryName', selectedCategory.name);
                      }
                  }}
                  style={{ width: '300px' }}
                  placeholder="Выберите категорию"
                />
              ) : (
                  <Input type="text" value={record.categoryName || ''} readOnly style={readOnlyStyle} />
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
                        onClick={() => handleAddModel(record.key)}
                    />
                    ) : editingKey && editingKey === record.key ? (
                        <Button
                        type="text"
                        icon={<CheckOutlined style={{ color: 'green', fontSize: '18px' }} />}
                        onClick={() => handleEditModel(record.key)}
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
          <h2>Модели инструментов</h2>
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

export default ModelTable;
