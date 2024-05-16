import { FaUser, FaLock } from "react-icons/fa";
import React, { useEffect, useState } from "react";
import axios from 'axios'

// For displaying toasts
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { Navigate, redirect } from "react-router-dom";
//

function CustomerLogin() {
    const notify = () => toast.success("Wow so easy!");
    const api_url = 'https://petcaresystem20240514113535.azurewebsites.net'

    const [user_info, updateUserInfo] = useState({
       "email": "",
       "password": "" 
    });

    const handleChange = (e) => {
        updateUserInfo({
            ...user_info,
            [e.target.name]: e.target.value
        })
    };

    const onLogin = () => {
        axios.post(api_url + '/api/Auth/login', user_info)
            .then((res) => console.log(res));
    };

    useEffect(()=>{
        const token = localStorage.getItem("token");
        console.log(token)
        if (token == null) {
            
        }
            
    }, []);

    return (
        <div className="flex justify-center items-center h-screen">
            <div className="w-96 p-6 shadow-lg rounded-md">
                <h1 className="text-center mb-4 text-2xl font-bold text-[#35b8be]">Customer login</h1>
                <hr className="mb-4"/>
                <div className="flex items-center mb-4">
                    <FaUser />
                    <input onChange={handleChange} value={user_info.email} name="email" className="ml-2 focus:border-[#35b8be] border-transparent border-b duration-300 outline-none h-10 p-2 w-full" type="email" placeholder="Email" required/>
                </div>
                <div className="flex items-center mb-4">
                    <FaLock />
                    <input onChange={handleChange} value={user_info.password} name="password" className="ml-2 focus:border-[#35b8be] border-transparent border-b duration-300 outline-none h-10 p-2 w-full" type="password" placeholder="Password" required/>
                </div>
                <div className="flex justify-center mb-4">
                    <button onClick={onLogin} className="bg-[#35b8be] duration-300 hover:bg-[#35c9cf] w-full py-2 text-white rounded-xl">Login</button>
                </div>
                
                <div className="flex justify-center mb-3">
                    <p className="mr-1">New here?</p>
                    <button className="text-[#35b8be] hover:underline">Sign Up</button>
                </div>
                <div className="flex justify-center items-center mb-3">
                    <hr className="w-1/3"/>
                    <p className="mx-2 text-gray-400">or</p>
                    <hr className="w-1/3"/>
                </div>
                <div className="flex justify-center">
                    <p className="mr-1">Not a customer?</p>
                    <button className="text-[#35b8be] hover:underline">Go to employee page</button>
                </div>
            </div>
            {/* To let Toastify able to display a toast */}
            <ToastContainer position="bottom-right"/> 
        </div>
    );
}

export default CustomerLogin;