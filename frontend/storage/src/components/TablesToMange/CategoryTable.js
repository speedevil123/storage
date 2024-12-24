import React, { useState, useEffect } from 'react';
import { Table, Button, Input, message } from 'antd';
import { EditOutlined, DeleteOutlined, PlusOutlined } from '@ant-design/icons';
import { GETRequest, POSTRequest, PUTRequest, DELETERequest } from '/storage/frontend/storage/src/request.js';

const CategoriesTable = () => {
    const [categories, setCategories] = useState([]);
    const [editingKey, setEditingKey] = useState(null);
    const [newCategoryName, setNewCategoryName] = useState('');
    const [pagination, setPagination] = useState({
        current: 1,
        pageSize: 10,
    });
    const [searchText, setSearchText] = useState('');

    const getCategories = async () => {
        try {
            const categoriesData = await GETRequest('/Categories');
            const categoryRows = categoriesData.map((category) => ({
                key: category.id,
                name: category.name,
            }))
            setCategories(categoryRows);
        } catch (error) {
            message.error('Ошибка загрузки категорий.');
        }
    };

    useEffect(() => {
        getCategories();
    }, []);

    const handleAddCategory = async () => {
        if (!newCategoryName.trim()) {
            message.warning('Введите название категории.');
            return;л
        }

        const newCategory = { name: newCategoryName.trim() };
        const categoryId = await POSTRequest('/Categories', newCategory);

        const isDuplicateKey = categories.some(category => category.key === newCategory.key);
        if (isDuplicateKey) {
            message.error('Ключ уже существует.');
            return;
        }

        if (categoryId) {
            console.log(categoryId);
            setCategories((prev) => [...prev, { key: categoryId, name: newCategoryName.trim()}]);
            message.success('Категория добавлена.');

            setNewCategoryName('');
        } 
        else {
            message.error('Ошибка добавления категории.');
        }
    };

    const handleEditCategory = async (key, newName) => {
        if (!newName.trim()) 
        {
            message.warning('Введите новое название категории.');
            return;
        }

        const updatedCategory = { name: newName.trim() };
        const result = await PUTRequest(`/Categories/${key}`, updatedCategory);

        if (result) 
        {
            setCategories((prev) =>
                prev.map((category) =>
                    category.key === key ? { ...category, name: newName.trim() } : category
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
        const result = await DELETERequest(`/Categories/${key}`);
        if (result) 
        {
            setCategories((prev) => prev.filter((category) => category.key !== key));
            message.success('Категория удалена.');
        } 
        else 
        {
            message.error('Ошибка удаления категории.');
        }
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
                editingKey === record.key ? (
                    <Input
                        defaultValue={text}
                        onPressEnter={(e) => handleEditCategory(record.key, e.target.value)}
                        onBlur={(e) => handleEditCategory(record.key, e.target.value)}
                        autoFocus
                    />
                ) : (
                    text
                ),
        },
        {
            title: 'Действия',
            key: 'actions',
            width: 150,
            render: (_, record) => (
                <div style={{ display: 'flex', gap: '8px', alignItems: 'center' }}>
                    <Button
                        type="text"
                        icon={<EditOutlined />}
                        onClick={() => setEditingKey(record.key)}
                    />
                    <Button
                        type="text"
                        danger
                        icon={<DeleteOutlined />}
                        onClick={() => handleDeleteCategory(record.key)}
                    />
                </div>
            ),
        },
    ];

    return (
        <div>
            <div style={{ marginBottom: '10px', display: 'flex', gap: '10px', alignItems: 'center' }}>
                <Input
                    placeholder="Введите название новой категории"
                    value={newCategoryName}
                    onChange={(e) => setNewCategoryName(e.target.value)}
                    style={{ width: '300px' }}
                />
                <Button
                    type="primary"
                    icon={<PlusOutlined />}
                    onClick={handleAddCategory}
                >
                    Добавить
                </Button>
            </div>
            <Table
                dataSource={categories}
                columns={columns}
                pagination={pagination}
                onChange={(page) => setPagination(page)}
                rowKey="key"
            />
        </div>
    );
};

export default CategoriesTable;
