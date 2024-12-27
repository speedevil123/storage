import React, { useState, useEffect } from 'react';
import { Table, Button, Input, message, AutoComplete } from 'antd';
import { EditOutlined, DeleteOutlined, PlusOutlined, CheckOutlined, SearchOutlined } from '@ant-design/icons';
import { GETRequest, POSTRequest, PUTRequest, DELETERequest } from '../request.js';
import ConfirmDeleteModal from './ConfirmDeleteModal';
import { generateGUID } from '../guidGenerator.js';


message.config({
    duration: 3, 
    maxCount: 3, 
});

const ToolTable = () => {
    const [models, setModels] = useState([]);
    const [manufacturers, setManufacturers] = useState([])
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

    const getModels = async () => {
      try {
          const modelsData = await GETRequest('/Models');
          setModels(modelsData);
      } catch (error) {
          message.error('Ошибка загрузки моделей.');
      }
    };

    const getManufacturers = async () => {
      try {
          const manufacturersData = await GETRequest('/Manufacturers');
          setManufacturers(manufacturersData);
      } catch(error) {
          message.error('Ошибка загрузки производителей')
      }
    }

    const getTools = async () => {
        try {
            const toolsData = await GETRequest('/Tools');
            const toolsRows = toolsData.map((tool) => ({
                key: tool.id,
                quantity: tool.quantity,
                modelId: tool.modelId,
                manufacturerId: tool.manufacturerId
            }))
            setDataSource(toolsRows);
        } catch (error) {
            message.error('Ошибка загрузки инструментов.');
        }
    };

    useEffect(() => {
        getModels();
        getManufacturers();
        getTools();
    }, []);

    const validateRow = (record) => {   
        const errors = []; 
        // if (!record.name || record.name.trim() === '')
        // {
        //     errors.push('Пожалуйста заполните все поля');
        // }
        // if (filteredDataSource.some(model => model.name === record.name && model.key !== model.key))
        // {
        //     errors.push('Работник с таким названием уже существует');
        // }
        return errors;
    };

    const updateField = (key, field, value) => {
        setDataSource((prevData) =>
            prevData.map((item) =>
                item.key === key ? { ...item, [field]: value } : item
            )
        );
    };

    const handleAddTool = async (key) => {
        const record = dataSource.find((item) => item.key === key)
        if(!record) return;

        const errors = validateRow(record);

        if (errors.length > 0) {
            message.error(errors.map((error) => <p style={{margin: '0', textAlign: 'left'}}>{error}</p>));
            return;
        }

        const newTool = {
            id: record.key,
            quantity: record.quantity,
            modelId: record.modelId,
            manufacturerId: record.manufacturerId
        }

        const toolId = await POSTRequest('/Tools', newTool);

        if (toolId) {
            setDataSource((prevData) =>
                prevData.map((item) =>
                    item.key === key ? {...item, ...newTool, key: item.key} : item
                )
            );            
            message.success('Инструмент добавлен.');
            setAddingKey(null);
        } 
        else {
            message.error('Ошибка добавления инструмента.');
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
            handleDeleteTool(selectedRowKey);
        }
    };

    const handleEditTool = async (key) => {
        const record = dataSource.find((item) => item.key === key);
        if (!record) return;
    
        const errors = validateRow(record);
        if (errors.length > 0) {
            message.error(`Ошибка: ${errors.join(' ')}`);
            return;
        }

        const toolToUpdate = {
          id: record.key,
          quantity: record.quantity,
          modelId: record.modelId,
          manufacturerId: record.manufacturerId
        }
        const toolId = await PUTRequest(`/Tools/${record.key}`, toolToUpdate);

        if (toolId) 
        {
            setDataSource((prevData) =>
                prevData.map((item) =>
                    item.key === key ? {...item, ...toolToUpdate, key: item.key}: item
                )
            );
            message.success('Инструмент обновлен.');
            setEditingKey(null);
        } 
        else 
        {
            message.error('Ошибка обновления инструмента.');
        }
    };

    const handleDeleteTool = async (key) => {
        const record = dataSource.find((item) => item.key === key);

        if (addingKey === key) {
            setDataSource((prevData) => prevData.filter((item) => item.key !== key));
            setIsModalVisible(false);
            setAddingKey(null);
            message.info('Данные об инструменте удалены.');
            return;
        }

        const toolId = await DELETERequest(`/Tools/${record.key}`);
        if (toolId) 
        {
            setDataSource((prev) => prev.filter((item) => item.key !== key));
            message.info('Данные об инструменте удалены.');
        } 
        else 
        {
            message.error('Ошибка удаления данных об инструменте.');
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
            quantity: 0,
            modelId: null,
            manufacturerId: null
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
          dataIndex: 'model',
          key: 'model',
          sorter: (a, b) => a.modelName.localeCompare(b.modelName),
          render: (text, record) =>
              editingKey === record.key || addingKey === record.key ? (
                <AutoComplete
                  value={record.modelName || ''}
                  onSearch={(searchText) => {
                      const filteredModels = models.filter(model =>
                        `${model.name}`
                          .toLowerCase()
                          .includes(searchText.toLowerCase())
                      );
                      updateField(record.key, 'filteredModels', filteredModels);
                      updateField(record.key, 'modelName', searchText);
                  }}
                  options={(record.filteredModels || models).map(model => ({
                      value: model.id,
                      label: `${model.name}`,
                  }))}
                  onSelect={(value) => {
                      const selectedModel = models.find((model) => model.id === value);
                      if (selectedModel) {
                          updateField(record.key, 'modelId', selectedModel.id);
                          updateField(record.key, 'modelName', selectedModel.name);
                      }
                  }}
                  style={{ width: '300px' }}
                  placeholder="Выберите модель"
                />
              ) : (
                  <Input type="text" value={record.modelName || ''} readOnly style={readOnlyStyle} />
              )
        },
        {
          title: 'Производитель',
          dataIndex: 'manufacturer',
          key: 'manufacturer',
          sorter: (a, b) => a.manufacturerName.localeCompare(b.manufacturerName),
          render: (text, record) =>
              editingKey === record.key || addingKey === record.key ? (
                <AutoComplete
                  value={record.manufacturerName || ''}
                  onSearch={(searchText) => {
                      const filteredManufacturers = manufacturers.filter(manufacturer =>
                        `${manufacturer.name}`
                          .toLowerCase()
                          .includes(searchText.toLowerCase())
                      );
                      updateField(record.key, 'filteredManufacturers', filteredManufacturers);
                      updateField(record.key, 'manufacturerName', searchText);
                  }}
                  options={(record.filteredManufacturers || manufacturers).map(manufacturer => ({
                      value: manufacturer.id,
                      label: `${manufacturer.name}`,
                  }))}
                  onSelect={(value) => {
                      const selectedManufacturer = manufacturers.find((model) => model.id === value);
                      if (selectedManufacturer) {
                          updateField(record.key, 'manufacturerId', selectedManufacturer.id);
                          updateField(record.key, 'manufacturerName', selectedManufacturer.name);
                      }
                  }}
                  style={{ width: '300px' }}
                  placeholder="Выберите производителя"
                />
              ) : (
                  <Input type="text" value={record.manufacturerName || ''} readOnly style={readOnlyStyle} />
              )
        },
        {
            title: 'Общее количество',
            dataIndex: 'quantity',
            key: 'quantity',
            sorter: (a, b) => a.quantity.localeCompare(b.quantity),
            render: (text, record) =>
                editingKey === record.key || addingKey === record.key ? (
                    <Input
                        value = {record.quantity}
                        onChange={(e) => {
                            updateField(record.key, 'quantity', e.target.value);
                        }}
                        value={record.quantity || ''}
                        style={{ width: '100%' }}
                        placeholder="(Пример: СюткинНЮ)"
                    />
                ) : (
                    <Input type="text" value={record.quantity || ''} readOnly style={readOnlyStyle} />
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
                        onClick={() => handleAddTool(record.key)}
                    />
                    ) : editingKey && editingKey === record.key ? (
                        <Button
                        type="text"
                        icon={<CheckOutlined style={{ color: 'green', fontSize: '18px' }} />}
                        onClick={() => handleEditTool(record.key)}
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
          <h2>Инструменты</h2>
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

export default ToolTable;
