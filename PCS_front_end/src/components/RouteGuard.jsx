import { Outlet, Link, Navigate } from "react-router-dom"
import { UserContext } from "../App";
import { useContext } from "react";

function RouteGuard() {
    const { token, setToken } = useContext(UserContext);
    
    return (
        <div>
            {(token)? null : <Navigate to='login'/>}
        </div>
    );
}

export default RouteGuard;