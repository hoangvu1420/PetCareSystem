import { FaUser, FaLock } from "react-icons/fa";
import React, { useContext, useEffect, useState } from "react";
import axios from 'axios'
import { Link, Navigate, useNavigate } from "react-router-dom";
import { UserContext } from "../App";

// For displaying toasts
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
//

function CustomerLogin() {
    console.log("render customer login");

    const navigate = useNavigate();
    const { user_data, setUserData } = useContext(UserContext);
    const api_url = 'https://petcaresystem20240514113535.azurewebsites.net'

    const [user_info, updateUserInfo] = useState({
        "email": "john.doe@example.com",
        "password": "user1111"
    });

    const [loading, setLoading] = useState(false); // Add loading state

    const handleChange = (e) => {
        updateUserInfo({
            ...user_info,
            [e.target.name]: e.target.value
        })
    };

    const onLogin = (e) => {
        e.preventDefault();

        setLoading(true); // Set loading to true when login starts
        axios.post(api_url + '/api/Auth/login', user_info)
            .then((res) => {
                console.log(res);
                if (res.data.isSucceed === true) {
                    toast.success("Success! Redirect in 2 seconds...");
                    setTimeout(() => {
                        setUserData(JSON.stringify(res.data));
                        sessionStorage.setItem("user_data", JSON.stringify(res.data));
                        setLoading(false); // Reset loading state after response
                        navigate('/');
                    }, 2000);
                } else {
                    setLoading(false); // Reset loading state if login fails
                }
            })
            .catch(err => {
                toast.error(err.response.data.errorMessages[0]);
                setLoading(false); // Reset loading state if there is an error
            });
    };

    useEffect(() => {
        console.log("UseEffect was called");
        if (user_data != null) {
            navigate('/');
        }
    }, [user_data]);

    return (
        <div className="flex justify-center items-center h-screen">
            <div className="w-96 p-6 shadow-lg rounded-md">
                <h1 className="text-center mb-4 text-2xl font-bold text-[#212121]">Đăng nhập</h1>
                <hr className="mb-4" />

                <form onSubmit={onLogin}>
                    <div className="flex items-center mb-4">
                        <FaUser />
                        <input onChange={handleChange} value={user_info.email} name="email" className="ml-2 focus:border-[#212121] border-transparent border-b duration-300 outline-none h-10 p-2 w-full" type="email" placeholder="Email" required />
                    </div>
                    <div className="flex items-center mb-4">
                        <FaLock />
                        <input onChange={handleChange} value={user_info.password} name="password" className="ml-2 focus:border-[#212121] border-transparent border-b duration-300 outline-none h-10 p-2 w-full" type="password" placeholder="Mật khẩu" required />
                    </div>
                    <div className="flex justify-center mb-4">
                        <button type="submit" className="bg-[#212121] duration-300 hover:bg-[#35c9cf] w-full py-2 text-white rounded-xl" disabled={loading}>
                            {loading ? 'Đang đăng nhập...' : 'Đăng nhập'} {/* Change button text based on loading state */}
                        </button>
                    </div>
                </form>

                <div className="flex justify-center mb-3">
                    <p className="mr-1">Chưa có tài khoản?</p>
                    <Link to='/register' relative="path">
                        <button className="text-[#35b8be] hover:underline">Đăng ký</button>
                    </Link>
                </div>
                <div className="flex justify-center items-center mb-3">
                    <hr className="w-1/3" />
                    <p className="mx-2 text-gray-400">hoặc</p>
                    <hr className="w-1/3" />
                </div>
                <div className="flex justify-center">
                    <p className="mr-1">Not a customer?</p>
                    <button className="text-[#35b8be] hover:underline">Đến trang nhân viên</button>
                </div>
            </div>
            {/* To let Toastify able to display a toast */}
            <ToastContainer position="bottom-right" />
        </div>
    );
}

export default CustomerLogin;