/* This file contains functions that 
make CRUD Room request to API endpoint */

import axios from "axios"

const baseUrl = 'https://petcaresystem20240514113535.azurewebsites.net'

const getBookings = (token) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.get(`${baseUrl}/api/room-bookings`)
}

const createBooking = (token, booking_data) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.post(`${baseUrl}/api/room-bookings`, booking_data)
}

const deleteBooking = (token, booking_id) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.delete(`${baseUrl}/api/room-bookings/${booking_id}`)
}

const editBooking = (token, booking_id, booking_data) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.put(`${baseUrl}/api/room-bookings/${booking_id}`, booking_data)
}

export default { getBookings, createBooking, deleteBooking, editBooking }