import { useContext } from "react";
import { Navigate, Outlet } from "react-router-dom";
import { UserContext } from "../App";

export default function Auth () {
    const { user_data, setUserData } = useContext(UserContext);

    return (
        (user_data)? <Navigate to='/'/> : <Outlet/>
    );
}