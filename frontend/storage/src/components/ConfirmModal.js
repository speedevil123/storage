import React, { useState } from 'react';
import { ExclamationCircleOutlined } from '@ant-design/icons';
import { Button, Modal, Space } from 'antd';

const LocalizedModal = () => {
    const [open, setOpen] = useState(false);
  
    const showModal = () => {
      setOpen(true);
    };
  
    const hideModal = () => {
      setOpen(false);
    };
  
    return (
      <>
        <Button type="primary" onClick={showModal}/>
        <Modal
          title="Modal"
          open={open}
          onOk={hideModal}
          onCancel={hideModal}
          okText="Удалить"
          cancelText="Отменить"
        >
          <p>Bla bla ...</p>
          <p>Bla bla ...</p>
          <p>Bla bla ...</p>
        </Modal>
      </>
    );
  };