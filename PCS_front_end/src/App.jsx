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
  const [user_data, setUserData] = useState(sessionStorage.getItem("user_data"));

  console.log("App rendered", user_data);

  return (
    <UserContext.Provider value={{user_data, setUserData}}>
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
