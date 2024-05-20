import { useState, createContext, useContext } from 'react'
import Navbar from './pages/NavbarDefault';
import { Router, Routes, Route, Link, NavLink, BrowserRouter, Navigate } from "react-router-dom";
import CustomerLogin from './pages/CustomerLogin';
import CustomerRegister from './pages/CustomerRegister';
import EmployeeLogin from './pages/EmployeeLogin'
import Layout from './pages/Layout';
import RouteGuard from './components/RouteGuard'
import CustomerViewAllPet from './pages/CustomerViewAllPet/CustomerViewAllPet';
import TableWithStripedRows from './pages/TableWithStripedRows';

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
            <Route path='test' element={<TableWithStripedRows/>}/>
          </Route>
        </Routes>
      </BrowserRouter>
    </UserContext.Provider>
  );

}

export default App
