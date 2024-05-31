import axios from "axios"

const baseUrl = 'https://petcaresystem20240514113535.azurewebsites.net'

const signIn = async credentials => {
    const response = await axios.post(`${baseUrl}/api/Auth/login`, credentials)
    return response
}

export default { signIn }