import {Outlet, Navigate, useLocation} from "react-router-dom"
import NavbarDefault from "./NavbarDefault";
import {UserContext} from "../App";
import {useContext, useEffect} from "react";
import 'react-toastify/dist/ReactToastify.css';
import {ToastContainer} from "react-toastify";
import BottomBar from "./BottomBar";

function Layout() {
    const {user_data, setUserData} = useContext(UserContext);

    useEffect(() => {
        if (user_data != null)
            if (new Date(JSON.parse(user_data).expirationDate) < new Date()) {
                setUserData(null);
            }
    }, []);

    return (
        <div className='flex flex-col min-h-screen overflow-hidden'>
            <NavbarDefault/>
            <main className='flex-grow sm:max-[540px] md:max-w-[768px] lg:max-w-[1348px] mx-auto pt-6 px-4 md:px-8 mb-8 overflow-hidden'>
                <Outlet/>
            </main>
            <BottomBar/>
            <ToastContainer position="bottom-right"/>
        </div>
    );
}

export default Layout;