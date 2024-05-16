import { Outlet, Link } from "react-router-dom"
import Navbar from "./Navbar";

function Layout() {
    const token = localStorage.getItem("token");

    return (
        <div>
            {(token)? <Navbar/> : null}
            <Outlet/>
        </div>
    );
}

export default Layout;