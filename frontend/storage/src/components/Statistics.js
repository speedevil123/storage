import React from 'react';
import { Select, Button, Space, Typography } from 'antd';
import { LineChart, Line, CartesianGrid, XAxis, YAxis, Tooltip, ResponsiveContainer } from 'recharts';
import "../App.css";

const { Title } = Typography;
const { Option } = Select;

const data = [
    { date: '2025-01-01', toolsTaken: 4 },
    { date: '2025-01-02', toolsTaken: 5 },
    { date: '2025-01-03', toolsTaken: 9 },
    { date: '2025-01-04', toolsTaken: 7 },
    { date: '2025-01-05', toolsTaken: 9 },
    { date: '2025-01-06', toolsTaken: 4 },
    { date: '2025-01-07', toolsTaken: 7 },
    { date: '2025-01-08', toolsTaken: 9 },
    { date: '2025-01-09', toolsTaken: 10 },
    { date: '2025-01-10', toolsTaken: 6 },
    { date: '2025-01-11', toolsTaken: 2 },
    { date: '2025-01-12', toolsTaken: 8 },
    { date: '2025-01-13', toolsTaken: 4 },
    { date: '2025-01-14', toolsTaken: 5 },
    { date: '2025-01-15', toolsTaken: 10 },
    { date: '2025-01-16', toolsTaken: 8 },
    { date: '2025-01-17', toolsTaken: 6 },
    { date: '2025-01-18', toolsTaken: 8 },
    { date: '2025-01-19', toolsTaken: 7 },
    { date: '2025-01-20', toolsTaken: 8 },
    { date: '2025-01-21', toolsTaken: 1 },
    { date: '2025-01-22', toolsTaken: 3 },
    { date: '2025-01-23', toolsTaken: 5 },
    { date: '2025-01-24', toolsTaken: 6 },
    { date: '2025-01-25', toolsTaken: 4 },
    { date: '2025-01-26', toolsTaken: 10 },
    { date: '2025-01-27', toolsTaken: 8 },
    { date: '2025-01-28', toolsTaken: 5 },
    { date: '2025-01-29', toolsTaken: 6 },
    { date: '2025-01-30', toolsTaken: 2 },
    { date: '2025-01-31', toolsTaken: 7 },
    { date: '2025-02-01', toolsTaken: 4 },
    { date: '2025-02-02', toolsTaken: 10 },
    { date: '2025-02-03', toolsTaken: 4 },
    { date: '2025-02-04', toolsTaken: 8 },
    { date: '2025-02-05', toolsTaken: 8 },
    { date: '2025-02-06', toolsTaken: 8 },
    { date: '2025-02-07', toolsTaken: 4 },
    { date: '2025-02-08', toolsTaken: 5 },
    { date: '2025-02-09', toolsTaken: 8 }
];
;

const Statistics = () => {
    const handleGenerateReport = () => {
        console.log('Формируем отчет...');
    };

    return (
        <div style={{ margin: '0 70px 0 70px', display: 'flex', padding: '20px', height: '100vh'}}>
            {/* Левая часть - Формирование отчетов */}
            <div style={{ flex: 1, padding: '20px', borderRight: '1px solid #eaeaea' }}>
                <Title level={2} style={{ marginBottom: '40px' }}>Формирование отчетов</Title>
                <Space direction="vertical" size="large" style={{ width: '100%' }}>
                    <Space style={{ width: '100%', justifyContent: 'space-between' }}>
                        <Select
                            placeholder="Выберите тип отчета по аренде"
                            style={{ width: '300px' }}
                        >
                            <Option value="Active">Активные</Option>
                            <Option value="Penaltied">Просроченные</Option>
                            <Option value="Completed">Завершенные в срок</Option>
                            <Option value="CompletedPenaltied">Погашенные</Option>
                        </Select>
                        <Button type="primary" onClick={handleGenerateReport}>
                            Создать отчет
                        </Button>
                    </Space>
                    <Space style={{ width: '100%', justifyContent: 'space-between' }}>
                        <Select
                            placeholder="Выберите отчет по таблицам"
                            style={{ width: '300px' }}
                        >
                            <Option value="tools">Отчет по инструментам</Option>
                            <Option value="workers">Отчет по работникам</Option>
                        </Select>
                        <Button type="primary" onClick={handleGenerateReport}>
                            Создать отчет
                        </Button>
                    </Space>
                    <Space style={{ width: '100%', justifyContent: 'space-between' }}>
                        <Select
                            placeholder="Другие отчеты"
                            style={{ width: '300px' }}
                        >
                            <Option value="stockRemaining">Остаток инструментов</Option>
                            <Option value="stockInWork">Инструменты находящиеся в работе</Option>
                        </Select>
                        <Button type="primary" onClick={handleGenerateReport}>
                            Создать отчет
                        </Button>
                    </Space>
                </Space>
            </div>

            {/* Правая часть - График */}
            <div style={{ flex: 1, padding: '20px' }}>
                <Title level={2} style={{ textAlign: 'center', marginBottom: '40px' }}>Анализ аренды за последний месяц</Title>
                <ResponsiveContainer width="100%" height="70%">
                    <LineChart data={data}>
                        <Line type="monotone" dataKey="toolsTaken" stroke="#1890ff" strokeWidth={2} />
                        <CartesianGrid stroke="#ccc" />
                        <XAxis dataKey="date" />
                        <YAxis />
                        <Tooltip />
                    </LineChart>
                </ResponsiveContainer>
            </div>
        </div>
    );
};

export default Statistics;
