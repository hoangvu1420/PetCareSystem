import { useState, createContext, useContext } from 'react'
import Navbar from './components/NavbarDefault';
import { Router, Routes, Route, Link, NavLink, BrowserRouter, Navigate } from "react-router-dom";
import CustomerLogin from './components/CustomerLogin';
import CustomerRegister from './components/CustomerRegister';
import EmployeeLogin from './components/EmployeeLogin'
import Layout from './components/Layout';
import RouteGuard from './components/RouteGuard'
import CustomerViewAllPet from './components/CustomerViewAllPet';

export const UserContext = createContext();


function App() {
  const [token, setToken] = useState(sessionStorage.getItem("token"));

  console.log("App rendered", token);

  return (
    <UserContext.Provider value={{token, setToken}}>
      <BrowserRouter>
        <Routes>
          <Route exact path='/login' element={<CustomerLogin/>}/>
          <Route exact path='/register' element={<CustomerRegister/>}/>
          <Route path='/' element={<Layout/>}>
            <Route index element={<CustomerViewAllPet/>}/>
          </Route>
        </Routes>
      </BrowserRouter>
    </UserContext.Provider>
  );

}

export default App
