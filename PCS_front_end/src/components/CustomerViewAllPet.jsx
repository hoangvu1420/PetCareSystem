import axios from "axios";
import { useEffect, useState } from "react";
import PetCard from './PetCard.jsx'


export default function CustomerViewAllPet() {
    const [data, updateData] = useState(
        [
            {
                "id": 1,
                "name": "Bob",
                "age": 1,
                "hairColor": "Black",
                "species": "Dog",
                "breed": "Pug",
                "imageUrl": "https://images.unsplash.com/photo-1558788353-f76d92427f16?q=80&w=1638&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                "ownerId": "1bf413dd-9df7-4823-9490-435a71507ee8"
            },
            {
                "id": 2,
                "name": "Kimmi",
                "age": 2,
                "hairColor": "Brown",
                "species": "Cat",
                "breed": "Ragdoll",
                "imageUrl": "https://images.unsplash.com/photo-1584197176155-304c9a3c03ce?q=80&w=1587&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                "ownerId": "1bf413dd-9df7-4823-9490-435a71507ee8"
            },
        ]
    );

    useEffect(() => {
    }, [])

    return (
        <div className="pt-4">
            <ul className="flex-wrap flex">
                {data.map((pet) => (
                    <li className="p-2" key={pet.id}>
                        <PetCard name={pet.name}
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
        </div>
        
    );
}