import axios from "axios";
import { useEffect, useState, useContext } from "react";
import ServiceCard from './components/ServiceCard.jsx'
import CreateNewServiceButton from "./components/CreateNewServiceButton.jsx";
import { UserContext } from "../../App.jsx";
import { toast } from "react-toastify";

export default function ViewServices() {
    const [data, updateData] = useState([]);
    const api_url = 'https://petcaresystem20240514113535.azurewebsites.net'
    const { user_data } = useContext(UserContext);

    const getGroomingServices = () => {
        axios.defaults.headers.common['Authorization'] = "Bearer " + JSON.parse(user_data).token;
        axios.get(api_url + '/api/grooming-services')
        .then((res) => {
            console.log(res);
            if (res.data.isSucceed === true) {
                updateData(res.data.data);
            };
        })
        .catch((e) => {
            console.log(e);
            if (e.response.status !== 404)
                toast.error(e.response.data.errorMessages[0]);
            updateData([]);
        });
    };

    useEffect(() => {
        getGroomingServices();
    }, [])

    return (
        <div className="pt-4">
            <ul className="flex-wrap flex justify-center">
                {data.sort((a, b) => (a.id > b.id)? -1 : 1).map((service) => (
                    <li className="p-2" key={service.id}>
                        <ServiceCard 
                            id={service.id} 
                            name={service.name}
                            price={service.price}
                            description={service.description}
                            bookedCount={service.bookedCount}
                            getGroomingServices={getGroomingServices}
                        />
                    </li>
                ))}
                {JSON.parse(user_data).userInfo.roles.includes("Admin") ?
                <li className="p-2">
                    <CreateNewServiceButton getGroomingServices={getGroomingServices}/>
                </li>
                : null}
            </ul>
        </div>
        
    );
}