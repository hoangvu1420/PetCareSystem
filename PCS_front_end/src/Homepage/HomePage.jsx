import React from 'react';
import {
    Card,
    CardHeader,
    CardBody,
    CardFooter,
    Typography,
    Button,
} from "@material-tailwind/react";
import { useNavigate } from 'react-router-dom';

const HomePage = () => {
    const navigate = useNavigate();

    const handleButtonClick = () => {
        navigate('/protected/pets');
    };

    return (
        <div className={'border border-gray-200 shadow-2xl flex flex-col gap-6 p-4 rounded-xl'}>
            <img className={'col-span-6 rounded-lg w-full h-auto'} width={1128} height={600}
                     src="https://images.unsplash.com/photo-1546377791-2e01b4449bf0?q=80&w=2533&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                     alt="pet-image"
                />
            <div className={'col-span-6 col-start-7'}>
                <Typography variant="h5" color="blue-gray" className="mb-2">
                    Chào mừng đến với Pet Care System - Hệ thống quản lý thú cưng của bạn
                </Typography>
                <Typography>
                    Pet Care System là một hệ thống quản lý thú cưng dành cho các cửa hàng thú cưng, bệnh viện thú cưng và các cá nhân yêu thú cưng. Hệ thống giúp bạn quản lý thông tin của thú cưng, lịch sử khám bệnh, lịch sử tiêm phòng và nhiều tính năng khác.
                </Typography>
                <Button className={'mt-2'} onClick={handleButtonClick}>Xem pet của bạn</Button>
            </div>
        </div>
    );
};

export default HomePage;
