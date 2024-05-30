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
        navigate('/pets');
    };

    return (
        <Card className="mt-6 w-4/5 h-[85vh] mx-auto my-10">
            <CardHeader color="blue-gray" className="relative">
                <img
                    src="https://images.unsplash.com/photo-1546377791-2e01b4449bf0?q=80&w=2533&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                    alt="pet-image"
                />
            </CardHeader>
            <CardBody>
                <Typography variant="h5" color="blue-gray" className="mb-2">
                    Chào mừng đến với Pet Care System - Hệ thống quản lý thú cưng của bạn
                </Typography>
                <Typography>
                    Pet Care System là một hệ thống quản lý thú cưng dành cho các cửa hàng thú cưng, bệnh viện thú cưng và các cá nhân yêu thú cưng. Hệ thống giúp bạn quản lý thông tin của thú cưng, lịch sử khám bệnh, lịch sử tiêm phòng và nhiều tính năng khác.
                </Typography>
            </CardBody>
            <CardFooter className="pt-0">
                <Button onClick={handleButtonClick}>Xem pet của bạn</Button>
            </CardFooter>
        </Card>
    );
};

export default HomePage;
