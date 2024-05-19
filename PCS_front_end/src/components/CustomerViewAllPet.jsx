import axios from "axios";
import { useEffect, useState, useContext } from "react";
import PetCard from './PetCard.jsx'
import { UserContext } from "../App.jsx";
import { ToastContainer, toast } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';

export default function CustomerViewAllPet() {
    const [data, updateData] = useState([]);
    const api_url = 'https://petcaresystem20240514113535.azurewebsites.net'
    const { user_data, setUserData } = useContext(UserContext);

    const getPetByCurrentId = () => {
        axios.defaults.headers.common['Authorization'] = "Bearer " + JSON.parse(user_data).token;
        axios.get(api_url + '/api/Pet/user/' + JSON.parse(user_data).userInfo.id)
        .then((res) => {
            console.log(res);
            if (res.data.isSucceed === true) {
                updateData(res.data.data);
            };
        })
        .catch((e) => {
            console.log(e);
            toast.error(e.response.data.errorMessages[0]);
        });
    };

    useEffect(() => {
        getPetByCurrentId();
    }, [])

    return (
        <div className="pt-4">
            <ul className="flex-wrap flex">
                {data.map((pet) => (
                    <li className="p-2" key={pet.id}>
                        <PetCard id={pet.id} name={pet.name}
                            age={pet.age}
                            hairColor={pet.hairColor}
                            species={pet.species}
                            breed={pet.breed}
                            imageUrl={pet.imageUrl}
                            ownerId={pet.ownerId}
                        />
                    </li>
                ))}
            </ul>

            <ToastContainer position="bottom-right"/>
        </div>
        
    );
}