import { Outlet, Navigate, useLocation } from "react-router-dom"
import NavbarDefault from "./NavbarDefault";
import { UserContext } from "../App";
import { useContext, useEffect } from "react";

function Layout() {
    const { user_data, setUserData } = useContext(UserContext);

    useEffect(() => {
        if (user_data != null)
            if (new Date(JSON.parse(user_data).expirationDate) < new Date()) {
                setUserData(null);
            }
    }, []);

    return (
        <div>
            <NavbarDefault/>
            <Outlet/>
        </div>
    );
}

export default Layout;