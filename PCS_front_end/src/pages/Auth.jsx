import { useContext } from "react";
import { Navigate, Outlet } from "react-router-dom";
import { UserContext } from "../App";
import 'react-toastify/dist/ReactToastify.css';
import { ToastContainer } from "react-toastify";

export default function Auth () {
    const { user_data, setUserData } = useContext(UserContext);

    return (
        <div>
            {(user_data)? <Navigate to='/'/> : <Outlet/>}
            <ToastContainer position="bottom-right"/>
        </div>
    );
}