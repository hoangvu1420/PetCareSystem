import { useState } from 'react'
import Navbar from './components/Navbar';
import { Router, Routes, Route, Link, NavLink, BrowserRouter, Navigate } from "react-router-dom";
import CustomerLogin from './components/CustomerLogin';
import EmployeeLogin from './components/EmployeeLogin'
import Layout from './components/Layout';
import RouteGuard from './components/RouteGuard'


function App() {
  return (
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<Layout/>}>
            <Route index element={<RouteGuard/>}/>
            <Route path='/login' element={<CustomerLogin/>}/>
          </Route>
        </Routes>
      </BrowserRouter>
  );

}

export default App
