import { FaUser, FaLock } from "react-icons/fa";
import { IoMail } from "react-icons/io5";
import { FaLocationDot } from "react-icons/fa6";
import { useState } from "react";
import axios from "axios";

import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function CustomerRegister() {
    const api_url = 'https://petcaresystem20240514113535.azurewebsites.net'

    const [user_info, updateUserInfo] = useState({
        "firstName": "Valenka",
        "lastName": "Girardy",
        "districs": "Dong Da",
        "profilePictureUrl": "null",
        "email": "vgirardy2@homestead.com",
        "password": "user1111",
    });

    const handleChange = (e) => {
        updateUserInfo({
            ...user_info,
            [e.target.name]: e.target.value
        })
    };

    const onRegister = () => {

        if (user_info.password != document.getElementById('confirm_password').value) {
            (() => toast.error("Mật khẩu xác nhận không trùng khớp"))();
            return;
        }

        axios.post(api_url + '/api/Auth/register', user_info)
            .then((res) => {
                console.log(res.data);
                if (res.data.isSucceed === true) {
                    (() => toast.success("Đăng ký thành công"))();
                }
            })
            .catch(err => {
                (() => toast.error(err.response.data.errorMessages[0]))();
            });
    };

    return (
        <div className="flex justify-center items-center h-screen">
            <div className="w-96 p-6 shadow-lg rounded-md">
                <h1 className="text-center mb-4 text-2xl font-bold text-[#35b8be]">Đăng ký</h1>
                <hr className="mb-4"/>

                <div className="flex justify-between">
                    <div className="w-1/2 flex items-center mb-4">
                        <FaUser />
                        <input name="firstName" value={user_info.firstName} onChange={handleChange} className="ml-2 focus:border-[#35b8be] border-transparent border-b duration-300 outline-none h-10 p-2 w-full" type="text" placeholder="Tên" required/>
                    </div>
                    <div className="w-1/2 flex items-center mb-4">
                        <FaUser />
                        <input name="lastName" value={user_info.lastName} onChange={handleChange} className="ml-2 focus:border-[#35b8be] border-transparent border-b duration-300 outline-none h-10 p-2 w-full" type="text" placeholder="Họ" required/>
                    </div>
                </div>

                <div className="flex items-center mb-4">
                        <FaLocationDot />
                        <input name="districs" value={user_info.districs} onChange={handleChange} className="ml-2 focus:border-[#35b8be] border-transparent border-b duration-300 outline-none h-10 p-2 w-full" type="text" placeholder="Quận/ Huyện" required/>
                </div>

                <div className="flex items-center mb-4">
                    <IoMail className="w-5 h-5 -ml-0.5"/>
                    <input name="email" value={user_info.email} onChange={handleChange} className="ml-2 focus:border-[#35b8be] border-transparent border-b duration-300 outline-none h-10 p-2 w-full" type="text" placeholder="Email" required/>
                </div>
                <div className="flex items-center mb-4">
                    <FaLock />
                    <input name="password" value={user_info.password} onChange={handleChange} className="ml-2 focus:border-[#35b8be] border-transparent border-b duration-300 outline-none h-10 p-2 w-full" type="password" placeholder="Mật khẩu" required/>
                </div>
                <div className="flex items-center mb-4">
                    <FaLock />
                    <input id="confirm_password" className="ml-2 focus:border-[#35b8be] border-transparent border-b duration-300 outline-none h-10 p-2 w-full" type="password" placeholder="Xác nhận mật khẩu" required/>
                </div>
                
                <div className="flex justify-center mb-4">
                    <button onClick={onRegister} className="bg-[#35b8be] duration-300 hover:bg-[#35c9cf] w-full py-2 text-white rounded-xl">Đăng ký</button>
                </div>
                
                <div className="flex justify-center mb-3">
                    <p className="mr-1">Đã có tài khoản?</p>
                    <button className="text-[#35b8be] hover:underline">Đăng nhập</button>
                </div>
                <div className="flex justify-center items-center mb-3">
                    <hr className="w-1/3"/>
                    <p className="mx-2 text-gray-400">or</p>
                    <hr className="w-1/3"/>
                </div>
                <div className="flex justify-center">
                    <p className="mr-1">Not an employee?</p>
                    <button className="text-[#35b8be] hover:underline">Go to customer page</button>
                </div>
            </div>
            <ToastContainer position="bottom-right"/> 
        </div>
    );
}

export default CustomerRegister;