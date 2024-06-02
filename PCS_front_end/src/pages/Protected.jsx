import { useContext } from "react";
import { Navigate, Outlet } from "react-router-dom";
import { UserContext } from "../App";

export default function Protected () {
    const { user_data, setUserData } = useContext(UserContext);

    return (
        (user_data)? <Outlet/> : <Navigate to='/auth/login'/>
    );
}