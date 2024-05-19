import { FaUser, FaLock } from "react-icons/fa";

function CustomerLogin() {

    return (
        <div className="flex justify-center items-center h-screen">
            <div className="w-96 p-6 shadow-lg rounded-md">
                <h1 className="text-center mb-4 text-2xl font-bold text-[#35b8be]">Employee login</h1>
                <hr className="mb-4"/>
                <div className="flex items-center mb-4">
                    <FaUser />
                    <input className="ml-2 focus:border-[#35b8be] border-transparent border-b duration-300 outline-none h-10 p-2 w-full" type="text" placeholder="Username" required/>
                </div>
                <div className="flex items-center mb-4">
                    <FaLock />
                    <input className="ml-2 focus:border-[#35b8be] border-transparent border-b duration-300 outline-none h-10 p-2 w-full" type="password" placeholder="Password" required/>
                </div>
                <div className="flex justify-center mb-4">
                    <button className="bg-[#35b8be] duration-300 hover:bg-[#35c9cf] w-full py-2 text-white rounded-xl">Login</button>
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
                    <p className="mr-1">Not an employee?</p>
                    <button className="text-[#35b8be] hover:underline">Go to customer page</button>
                </div>
            </div>
        </div>
    );
}

export default CustomerLogin;