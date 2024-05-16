import { Outlet, Link } from "react-router-dom"
import Navbar from "./Navbar";
import { UserContext } from "../App";
import { useContext } from "react";

function Layout() {
    const { token, setToken } = useContext(UserContext);
    return (
        <div>
            {(token)? <Navbar/> : null}
            {(token)? <div>You has logged in</div>: <div></div>}
            <Outlet/>
        </div>
    );
}

export default Layout;