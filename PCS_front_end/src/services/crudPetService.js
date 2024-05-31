/* This file contains functions that 
make CRUD Pet request to API endpoint */

import axios from "axios"

const baseUrl = 'https://petcaresystem20240514113535.azurewebsites.net'

const getOwnedPet = (token, user_id) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.get(`${baseUrl}/api/pets?userId=${user_id}`)
}

const createPet = (token, pet_data) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.post(`${baseUrl}/api/pets`, pet_data)
}

const editPet = (token, pet_data) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.put(`${baseUrl}/api/pets/${pet_data.id}`, pet_data)
}

const deletePet = (token, pet_id) => {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    return axios.delete(`${baseUrl}/api/pets/${pet_id}`)
}

export default { getOwnedPet, createPet, deletePet, editPet }