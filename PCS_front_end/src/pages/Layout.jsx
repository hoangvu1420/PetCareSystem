import { Outlet, Navigate } from "react-router-dom"
import NavbarDefault from "./NavbarDefault";
import { UserContext } from "../App";
import { useContext, useEffect } from "react";

function Layout() {
    const { user_data, setUserData } = useContext(UserContext);

    useEffect(() => {
        if (user_data != null)
            if (new Date(JSON.parse(user_data).expirationDate) < new Date()) {
                localStorage.removeItem("user_data");
                setUserData(null);
            }
    }, []);

    return (
        <div>
            <NavbarDefault/>
            {(user_data)? <Outlet/> : <Navigate to='login'/>}
        </div>
    );
}

export default Layout;