/* This file contains functions that 
make auth request to API endpoint */

import axios from "axios"

const baseUrl = 'https://petcaresystem20240514113535.azurewebsites.net'

const signIn = credentials => {
    return axios.post(`${baseUrl}/api/Auth/login`, credentials)
}

const signUp = credentials => {
    return axios.post(`${baseUrl}/api/Auth/register`, credentials)
}

export default { signIn, signUp }