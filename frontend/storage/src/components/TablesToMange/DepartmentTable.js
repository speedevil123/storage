import React from 'react';
import { Table } from 'antd';

const DepartmentTable = () => {
  // Данные для таблицы
  const data = [
    { key: 1, name: 'Alice', age: 24 },
    { key: 2, name: 'Bob', age: 30 },
    { key: 3, name: 'Charlie', age: 28 },
    { key: 4, name: 'David', age: 35 },
    { key: 5, name: 'Eve', age: 22 },
  ];

  // Определяем колонки для таблицы
  const columns = [
    {
      title: 'ID',
      dataIndex: 'key', // поле, которое будет отображаться в этой колонке
      key: 'key',
    },
    {
      title: 'Name',
      dataIndex: 'name',
      key: 'name',
    },
    {
      title: 'Age',
      dataIndex: 'age',
      key: 'age',
    },
  ];

  return (
    <div>
      <h1>Ant Design Table</h1>
      <Table dataSource={data} columns={columns} />
    </div>
  );
};

export default DepartmentTable;
