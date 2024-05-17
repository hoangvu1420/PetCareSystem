import { Outlet, Link, Navigate } from "react-router-dom"
import Navbar from "./Navbar";
import { UserContext } from "../App";
import { useContext } from "react";

function Layout() {
    const { token } = useContext(UserContext);
    return (
        <div>
            <Navbar/>
            {(token)? <Outlet/> : <Navigate to='login'/>}
        </div>
    );
}

export default Layout;