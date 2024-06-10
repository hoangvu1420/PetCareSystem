import { useEffect, useState, useContext } from "react";
import PetCard from './components/PetCard.jsx'
import { UserContext } from "../../App.jsx";
import { toast } from "react-toastify";
import CreateNewPetButton from "./components/CreateNewPetButton.jsx";
import crudPetService from "../../services/crudPetService.js";

export default function CustomerViewAllPet() {
    const [data, updateData] = useState([]);
    const { user_data, setUserData } = useContext(UserContext);

    const getPetByCurrentId = async () => {
        try {
            const response = await crudPetService.getOwnedPet(
                JSON.parse(user_data).token,
                JSON.parse(user_data).userInfo.id
            )
            console.log(response)
            if (response.data.isSucceed) {
                updateData(response.data.data);
            };
        }
        catch (error) {
            console.log(error);
            if (error.response.status !== 404)
                toast.error(error.response.data.errorMessages[0]);
            updateData([]);
        }
    };

    useEffect(() => {
        getPetByCurrentId();
    }, [])

    return (
        <div className="pt-4">
            <ul className="flex-wrap flex justify-center">
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
        </div>
        
    );
}