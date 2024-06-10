import axios from "axios"

const baseUrl = 'https://petcaresystem20240514113535.azurewebsites.net'

const getAllPets = (token) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.get(`${baseUrl}/api/pets`)
}

const createGroomingService = (token, service_data) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.post(`${baseUrl}/api/grooming-services`, service_data)
}

const editGroomingService = (token, service_data) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.put(`${baseUrl}/api/grooming-services/${service_data.id}`, service_data)
}

const deleteGroomingService = (token, service_id) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.delete(`${baseUrl}/api/grooming-services/${service_id}`)
}

const createRoom = (token, room_data) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.post(`${baseUrl}/api/rooms`, room_data)
}

const editRoom = (token, room_data) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.put(`${baseUrl}/api/rooms/${room_data.id}`, room_data)
}

const deleteRoom = (token, room_id) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.delete(`${baseUrl}/api/rooms/${room_id}`)
}

export default { 
    getAllPets, 
    createGroomingService, 
    editGroomingService, 
    deleteGroomingService,
    createRoom,
    editRoom,
    deleteRoom
}