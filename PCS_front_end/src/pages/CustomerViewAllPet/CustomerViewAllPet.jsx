import axios from "axios";
import { useEffect, useState, useContext } from "react";
import PetCard from './components/PetCard.jsx'
import { UserContext } from "../../App.jsx";
import { ToastContainer, toast } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import CreateNewPetButton from "./components/CreateNewPetButton.jsx";

export default function CustomerViewAllPet() {
    const [data, updateData] = useState([]);
    const api_url = 'https://petcaresystem20240514113535.azurewebsites.net'
    const { user_data, setUserData } = useContext(UserContext);

    const getPetByCurrentId = () => {
        axios.defaults.headers.common['Authorization'] = "Bearer " + JSON.parse(user_data).token;
        axios.get(api_url + '/api/pets?userId=' + JSON.parse(user_data).userInfo.id)
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
        getPetByCurrentId();
    }, [])

    return (
        <div className="pt-4">
            <ul className="flex-wrap flex">
                {data.sort((a, b) => (a.id > b.id)? -1 : 1).map((pet) => (
                    <li className="p-2" key={pet.id}>
                        <PetCard id={pet.id} name={pet.name}
                            age={pet.age}
                            gender={pet.gender}
                            hairColor={pet.hairColor}
                            species={pet.species}
                            breed={pet.breed}
                            imageUrl={pet.imageUrl}
                            ownerId={pet.ownerId}
                            getPetByCurrentId={getPetByCurrentId}
                        />
                    </li>
                ))}
                <li className="p-2">
                    <CreateNewPetButton getPetByCurrentId={getPetByCurrentId}/>
                </li>
            </ul>

            <ToastContainer position="bottom-right"/>
        </div>
        
    );
}