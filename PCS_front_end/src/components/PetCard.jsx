import {
    Card,
    CardHeader,
    CardBody,
    CardFooter,
    Typography,
    Button,
  } from "@material-tailwind/react";
import { FaEdit } from "react-icons/fa";
   
export default function PetCard(props) {

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
            <div className="w-1/2"><span className="font-semibold">Loài:</span> {props.species}</div>
            <div className="w-1/2"><span className="font-semibold">Giống:</span> {props.breed}</div>
            <div className="w-1/2"><span className="font-semibold">Màu lông:</span> {props.hairColor}</div>
          </div>
        </div>
      </CardBody>
      <CardFooter className="pt-0 mt-0">
        <Button className="flex items-center justify-center w-5/12">
          <FaEdit className="mr-2"/>
          Sửa
        </Button>
      </CardFooter>
    </Card>
  );
}