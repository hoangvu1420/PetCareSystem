import { useState, createContext, useContext } from 'react'
import Navbar from './components/Navbar';
import { Router, Routes, Route, Link, NavLink, BrowserRouter, Navigate } from "react-router-dom";
import CustomerLogin from './components/CustomerLogin';
import EmployeeLogin from './components/EmployeeLogin'
import Layout from './components/Layout';
import RouteGuard from './components/RouteGuard'

export const UserContext = createContext();

// Note: users now are able to login, and will be redirected to
// root path '/', but the session ends if refresh or close tab
// (will work on this later)

function App() {
  const [token, setToken] = useState(null);

  return (
    <UserContext.Provider value={{token, setToken}}>
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<Layout/>}>
            <Route index element={<RouteGuard/>}/>
            <Route path='login' element={<CustomerLogin/>}/>
          </Route>
        </Routes>
      </BrowserRouter>
    </UserContext.Provider>
  );

}

export default App
