import { Outlet, Link, Navigate } from "react-router-dom"
import Navbar from "./Navbar";

function RouteGuard() {
    const token = localStorage.getItem("token");

    return (
        <div>
            {(token)? null : <Navigate to='login'/>}
        </div>
    );
}

export default RouteGuard;