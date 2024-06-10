import {Outlet, Navigate, useLocation} from "react-router-dom"
import NavbarDefault from "./NavbarDefault";
import {UserContext} from "../App";
import {useContext, useEffect} from "react";
import 'react-toastify/dist/ReactToastify.css';
import {ToastContainer} from "react-toastify";

function Layout() {
    const {user_data, setUserData} = useContext(UserContext);

    useEffect(() => {
        if (user_data != null)
            if (new Date(JSON.parse(user_data).expirationDate) < new Date()) {
                setUserData(null);
            }
    }, []);

    return (
        <div className={'overflow-hidden'}>
            <NavbarDefault/>
            <main className={'sm:max-[540px] md:max-w-[768px] lg:max-w-[1348px] mx-auto pt-6 px-4 md:px-8  overflow-hidden'}>
                <Outlet/>
            </main>
            <ToastContainer position="bottom-right"/>
        </div>
    );
}

export default Layout;