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

export default { getRooms, addRoom }