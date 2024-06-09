/* This file contains functions that 
make CRUD Room request to API endpoint */

import axios from "axios"

const baseUrl = 'https://petcaresystem20240514113535.azurewebsites.net'

const getBookings = (token) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.get(`${baseUrl}/api/room-bookings`)
}

const addRoom = (token, room_data) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.post(`${baseUrl}/api/rooms`, room_data)
}

const deleteRoom = (token, room_id) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.delete(`${baseUrl}/api/rooms/${room_id}`)
}

const editBooking = (token, booking_id, booking_data) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.put(`${baseUrl}/api/room-bookings/${booking_id}`, booking_data)
}

export default { getBookings, addRoom, deleteRoom, editBooking }