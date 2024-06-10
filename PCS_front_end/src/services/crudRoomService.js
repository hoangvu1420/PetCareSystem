/* This file contains functions that 
make CRUD Room request to API endpoint */

import axios from "axios"

const baseUrl = 'https://petcaresystem20240514113535.azurewebsites.net'

const getRooms = (token) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.get(`${baseUrl}/api/rooms`)
}

const addRoom = (token, room_data) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.post(`${baseUrl}/api/rooms`, room_data)
}

const deleteRoom = (token, room_id) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.delete(`${baseUrl}/api/rooms/${room_id}`)
}

const editRoom = (token, room_id, room_data) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.put(`${baseUrl}/api/rooms/${room_id}`, room_data)
}

export default { getRooms, addRoom, deleteRoom, editRoom }