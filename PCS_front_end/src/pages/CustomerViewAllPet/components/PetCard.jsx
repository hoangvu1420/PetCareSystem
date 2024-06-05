import {
    Card,
    CardHeader,
    CardBody,
    CardFooter,
    Typography,
    Button,
  } from "@material-tailwind/react";
import EditPetDialog from "./EditPetDialog";
import { useContext } from "react";
import { UserContext } from "../../../App";
import DeletePetButton from "./DeletePetButton";
import { useNavigate } from "react-router-dom";


   
export default function PetCard(props) {
  const { user_data, setUserData } = useContext(UserContext);
  const navigate = useNavigate();
  return (
    <Card className="mt-6 w-80">
      <CardHeader color="blue-gray" className="relative h-48">
        <img
          className="object-cover w-full h-full"
          src={props.imageUrl}
          alt="card-image"
        />
      </CardHeader>
      <CardBody className="pt-4 pb-3">
        <Typography variant="h5" color="blue-gray" className="mb-1">
          {props.name}
        </Typography>
        <div className="mb-0 pb-0">
          <div className="flex flex-wrap justify-between">
            <div className="w-1/2"><span className="font-semibold">Tuổi:</span> {props.age}</div>
            <div className="w-1/2"><span className="font-semibold">Giới tính:</span> {props.gender}</div>
            <div className="w-1/2"><span className="font-semibold">Loài:</span> {props.species}</div>
            <div className="w-1/2"><span className="font-semibold">Giống:</span> {props.breed}</div>
            <div className="w-1/2"><span className="font-semibold">Màu lông:</span> {props.hairColor}</div>
          </div>
        </div>
      </CardBody>
      <CardFooter className="pt-0 mt-0">
        <Button className="mr-2" onClick={() => navigate(`/protected/medical-records/${props.id}`)}>Bệnh án</Button>
        <EditPetDialog className="mr-2" 
                            id={props.id} name={props.name}
                            age={props.age}
                            gender={props.gender}
                            hairColor={props.hairColor}
                            species={props.species}
                            breed={props.breed}
                            imageUrl={props.imageUrl}
                            ownerId={props.ownerId}
                            getPetByCurrentId={props.getPetByCurrentId}/>
        <DeletePetButton className="mr-2" id={props.id}
            getPetByCurrentId={props.getPetByCurrentId}/>
      </CardFooter>
    </Card>
  );
}