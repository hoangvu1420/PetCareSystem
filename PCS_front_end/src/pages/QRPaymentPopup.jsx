import React, { useState, useEffect, useMemo } from 'react';
import { Dialog, Button } from "@material-tailwind/react";
import { toast } from "react-toastify";
import axios from "axios";
import QR from "../assets/QRPayment.jpg";

const QRPaymentPopup = ({ isOpen, onClose, onPaymentSuccess }) => {
    const [countdown, setCountdown] = useState(600); // 10 minutes countdown

    // Generate random 15-digit order number, but only once
    const orderNumber = useMemo(() => Math.random().toString().substr(2, 15), []);

    useEffect(() => {
        if (!isOpen) return;
        const timer = setInterval(() => {
            setCountdown(prev => prev > 0 ? prev - 1 : 0);
        }, 1000);
        return () => clearInterval(timer);
    }, [isOpen]);

    const handleCancel = () => {
        onClose();
    };

    const handleCheckPayment = async () => {
        toast.success("Thanh toán thành công", { autoClose: 2000 });
        onClose();
        onPaymentSuccess(); 
    };

    const formatTime = (seconds) => {
        const minutes = Math.floor(seconds / 60);
        const secs = seconds % 60;
        return `${minutes}:${secs < 10 ? '0' : ''}${secs}`;
    };

    return (
        <Dialog open={isOpen} handler={onClose}>
            <div className="flex p-6">
                <div className="w-1/2 pr-6 border-r flex flex-col">
                    <h2 className="text-xl font-semibold mb-4">Thanh Toán</h2>
                    <p>Đơn hàng hết hạn sau: <span className="font-bold">{formatTime(countdown)}</span></p>
                    <p>Nhà cung cấp: Paw Mingle</p>
                    <p>Thông tin: Đặt dịch vụ</p>
                    <p>Đơn hàng: {orderNumber}</p>
                    <div className="flex justify-center mt-auto">
                        <Button color="red" onClick={handleCancel}>Hủy đơn hàng</Button>
                    </div>
                </div>
                <div className="w-1/2 pl-6 flex flex-col justify-between">
                    <img src="https://upload.wikimedia.org/wikipedia/vi/f/fe/MoMo_Logo.png" alt="Momo Logo" className="absolute top-0 right-0 mt-4 mr-4 w-16" />
                    <p className="text-center mb-4 mt-16">Quét mã để thanh toán</p>
                    <img src={QR} alt="QR Code" className="mb-4 mx-auto w-48" />
                    <div className="flex justify-center items-center mb-4">
                        <img src="https://i.gifer.com/ZKZg.gif" alt="Loading" className="w-4" />
                        <p className="ml-2">Đang chờ bạn quét...</p>
                    </div>
                    <Button color="green" onClick={handleCheckPayment}>Kiểm tra thanh toán</Button>
                </div>
            </div>
        </Dialog>
    );
};

export default QRPaymentPopup;
