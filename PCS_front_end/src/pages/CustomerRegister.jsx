import { FaUser, FaLock } from "react-icons/fa";
import { IoMail } from "react-icons/io5";
import { FaLocationDot } from "react-icons/fa6";
import { useState } from "react";
import { toast } from 'react-toastify';
import { Link } from "react-router-dom";

import authService from "../services/authService";
import { Button } from "@material-tailwind/react";

function CustomerRegister() {
    const init_info = {
        "firstName": "",
        "lastName": "",
        "districs": "",
        "profilePictureUrl": "null",
        "email": "",
        "password": "",
        "confirm_password": ""
    }

    const [user_info, updateUserInfo] = useState(init_info);

    const handleChange = (e) => {
        updateUserInfo({
            ...user_info,
            [e.target.name]: e.target.value
        })
    };

    const onRegister = async (e) => {
        e.preventDefault();
        if (user_info.confirm_password != user_info.password) {
            toast.error("Mật khẩu xác nhận không khớp")
            return
        }
        try {
            const {confirm_password, ...credentials} = user_info
            const response = await authService.signUp(credentials)
            updateUserInfo(init_info);
            if (response.data.isSucceed) {
                toast.success("Đăng ký thành công")
            }
            console.log(response.data)
        }
        catch (error) {
            toast.error(error.response.data.errorMessages[0])
        }
    };

    return (
        <div className="flex justify-center items-center h-screen">
            <div className="w-96 p-6 shadow-lg rounded-md">
                <h1 className="text-center mb-4 text-2xl font-bold text-[#212121]">Đăng ký</h1>
                <hr className="mb-4"/>
                <form onSubmit={onRegister}>
                    <div className="flex justify-between">
                        <div className="w-1/2 flex items-center mb-4">
                            <FaUser />
                            <input name="firstName" value={user_info.firstName} onChange={handleChange} className="ml-2 focus:border-[#212121] border-transparent border-b duration-300 outline-none h-10 p-2 w-full" type="text" placeholder="Tên" required/>
                        </div>
                        <div className="w-1/2 flex items-center mb-4">
                            <FaUser />
                            <input name="lastName" value={user_info.lastName} onChange={handleChange} className="ml-2 focus:border-[#212121] border-transparent border-b duration-300 outline-none h-10 p-2 w-full" type="text" placeholder="Họ" required/>
                        </div>
                    </div>

                    <div className="flex items-center mb-4">
                            <FaLocationDot />
                            <input name="districs" value={user_info.districs} onChange={handleChange} className="ml-2 focus:border-[#212121] border-transparent border-b duration-300 outline-none h-10 p-2 w-full" type="text" placeholder="Quận/ Huyện" required/>
                    </div>

                    <div className="flex items-center mb-4">
                        <IoMail className="w-5 h-5 -ml-0.5"/>
                        <input name="email" value={user_info.email} onChange={handleChange} className="ml-2 focus:border-[#v] border-transparent border-b duration-300 outline-none h-10 p-2 w-full" type="email" placeholder="Email" required/>
                    </div>
                    <div className="flex items-center mb-4">
                        <FaLock />
                        <input name="password" value={user_info.password} onChange={handleChange} className="ml-2 focus:border-[#212121] border-transparent border-b duration-300 outline-none h-10 p-2 w-full" type="password" placeholder="Mật khẩu" required/>
                    </div>
                    <div className="flex items-center mb-4">
                        <FaLock />
                        <input name="confirm_password" value={user_info.confirm_password} onChange={handleChange} className="ml-2 focus:border-[#212121] border-transparent border-b duration-300 outline-none h-10 p-2 w-full" type="password" placeholder="Xác nhận mật khẩu" required/>
                    </div>
                    
                    <div className="flex justify-center mb-4">
                        <Button type="submit" fullWidth>
                            Đăng ký
                        </Button>
                    </div>

                </form>
                <div className="flex justify-center mb-3">
                    <p className="mr-1 text-gray-700">Đã có tài khoản?</p>
                    <Link to='/auth/login' relative='path'>
                        <button className="hover:underline">Đăng nhập</button>
                    </Link>
                </div>
                <div className="flex justify-center items-center mb-3">
                    <hr className="w-1/3"/>
                    <p className="mx-2 text-gray-400">hoặc</p>
                    <hr className="w-1/3"/>
                </div>
                <div className="flex justify-center">
                    <Link to='/'>
                        <button className="hover:underline">Về trang chủ</button>
                    </Link>
                </div>
            </div>
        </div>
    );
}

export default CustomerRegister;