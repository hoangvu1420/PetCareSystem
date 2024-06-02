import { useState, createContext } from 'react'
import { Router, Routes, Route, Link, NavLink, BrowserRouter, Navigate } from "react-router-dom";
import CustomerLogin from './pages/CustomerLogin';
import CustomerRegister from './pages/CustomerRegister';
import EmployeeLogin from './pages/EmployeeLogin'
import Layout from './pages/Layout';
import CustomerViewAllPet from './pages/CustomerViewAllPet/CustomerViewAllPet';
import ViewPetMedicalRecords from './pages/MedicalRecord/ViewPetMedicalRecords';
import HomePage from './Homepage/HomePage';
import ViewServices from './pages/Service/ViewServices';
import Protected from './pages/Protected';
import Auth from './pages/Auth';

export const UserContext = createContext();

function App() {
  const [user_data, _setUserData] = useState(localStorage.getItem("user_data"));
  
  function setUserData(u) {
    if (u === null) {
      localStorage.removeItem("user_data")
    }
    else {
      localStorage.setItem("user_data", u)
    }
    _setUserData(u)
  }

  return (
    <UserContext.Provider value={{user_data, setUserData}}>
      <BrowserRouter>
        <Routes>
            <Route path='/auth' element={<Auth/>}>
              <Route path='login' element={<CustomerLogin/>}/>
              <Route path='register' element={<CustomerRegister/>}/>
            </Route>
          <Route path='/' element={<Layout/>}>
            <Route index element={<HomePage/>}/>
            <Route path='protected' element={<Protected/>}>
              <Route path='services' element={<ViewServices/>}/>
              <Route path='pets' element={<CustomerViewAllPet/>}/>
              <Route path='medical-records/:pet_id' element={<ViewPetMedicalRecords/>}/>
            </Route>
          </Route>
        </Routes>
      </BrowserRouter>
    </UserContext.Provider>
  );

}

export default App
