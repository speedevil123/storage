import React, { useState } from 'react';
import { Button, Modal, Table, Input } from 'antd';

const PenaltyDetailsModal = ({ visible, onClose, penaltyDetails }) => {
    const columns = [
        { title: 'Работник', dataIndex: 'workerName', key: 'workerName' },
        { title: 'Инструмент', dataIndex: 'toolName', key: 'toolName' },
        { title: 'Сумма штрафа', dataIndex: 'fine', key: 'fine' },
        { title: 'Дата штрафа', dataIndex: 'penaltyDate', key: 'penaltyDate' },
    ];

    return (
        <Modal
            title="Детали просрочки"
            visible={visible}
            onCancel={onClose}
            footer={[
                <Button key="close" onClick={onClose}>Закрыть</Button>,
                <Button key="pay" type="primary" onClick={() => alert('Погашение просрочки')}>
                    Погасить просрочку
                </Button>,
            ]}
        >
            <Table
                dataSource={penaltyDetails}
                columns={columns}
                pagination={false}
                rowKey="id"
            />
        </Modal>
    );
};

export default PenaltyDetailsModal;
