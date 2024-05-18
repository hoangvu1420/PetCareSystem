import { Outlet, Link, Navigate } from "react-router-dom"
import NavbarDefault from "./NavbarDefault";
import { UserContext } from "../App";
import { useContext } from "react";

function Layout() {
    const { token } = useContext(UserContext);
    return (
        <div>
            <NavbarDefault/>
            {(token)? <Outlet/> : <Navigate to='login'/>}
        </div>
    );
}

export default Layout;