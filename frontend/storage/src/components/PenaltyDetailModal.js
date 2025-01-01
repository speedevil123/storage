import React from 'react';
import { Button, Modal, Card, Descriptions, Tag, message } from 'antd';

const formatDate = (date) => {
    const d = new Date(date);
    const day = String(d.getDate()).padStart(2, '0');
    const month = String(d.getMonth() + 1).padStart(2, '0');
    const year = d.getFullYear();
    const hour = String(d.getHours()).padStart(2, '0');
    const minute = String(d.getMinutes()).padStart(2, '0');
    const seconds = String(d.getSeconds()).padStart(2, '0');
    return `${day}.${month}.${year} ${hour}:${minute}:${seconds}`;
};

const PenaltyDetailsModal = ({ visible, onClose, penaltyDetails, onPenaltyPaid }) => {
    const handlePayment = async () => {
        try {
            await onPenaltyPaid(penaltyDetails.id); // Вызываем функцию для погашения штрафа
        } catch (error) {
            console.log(error);
        }
    };

    return (
        <Modal
            title="Детали просрочки"
            visible={visible}
            onCancel={onClose}
            footer={[
                <Button key="close" onClick={onClose}>
                    Закрыть
                </Button>,
                <Button
                    key="pay"
                    type="primary"
                    disabled={penaltyDetails?.isPaidOut}
                    onClick={handlePayment}
                >
                    Погасить просрочку
                </Button>,
            ]}
        >
            {penaltyDetails ? (
                <Card>
                    <Descriptions bordered column={1}>
                        <Descriptions.Item label="Работник">{penaltyDetails.workerName}</Descriptions.Item>
                        <Descriptions.Item label="Инструмент">{penaltyDetails.toolName}</Descriptions.Item>
                        <Descriptions.Item label="Сумма штрафа">{penaltyDetails.fine} руб.</Descriptions.Item>
                        <Descriptions.Item label="Дата штрафа">{formatDate(penaltyDetails.penaltyDate)}</Descriptions.Item>
                        <Descriptions.Item label="Статус">
                        {penaltyDetails.isPaidOut ? (
                            <Tag color="green" style={{ textAlign: "center", width: "120px", fontSize: '16px', padding: '5px 10px' }}>
                                Погашено
                            </Tag>
                        ) : (
                            <Tag color="red" style={{ textAlign: "center", width: "120px", fontSize: '16px', padding: '5px 10px' }}>
                                Просрочено
                            </Tag>
                        )}
                        </Descriptions.Item>
                    </Descriptions>
                </Card>
            ) : (
                <p>Нет данных о штрафах.</p>
            )}
        </Modal>
    );
};

export default PenaltyDetailsModal;
