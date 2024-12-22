import React from 'react';
import { Modal } from 'antd';

const ConfirmDeleteModal = ({ visible, onConfirm, onCancel }) => {
    return (
        <Modal
            title="Подтверждение удаления"
            visible={visible}
            onOk={onConfirm}
            onCancel={onCancel}
            okText="Удалить"
            cancelText="Отмена"
            okButtonProps={{ danger: true }}
        >
            <p>Вы уверены, что хотите удалить эту запись?</p>
        </Modal>
    );
};

export default ConfirmDeleteModal;
