import { Outlet, Link, Navigate } from "react-router-dom"
import NavbarDefault from "./NavbarDefault";
import { UserContext } from "../App";
import { useContext } from "react";

function Layout() {
    const { user_data } = useContext(UserContext);
    return (
        <div>
            <NavbarDefault/>
            {(user_data)? <Outlet/> : <Navigate to='login'/>}
        </div>
    );
}

export default Layout;