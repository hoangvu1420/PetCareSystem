import { MdDeleteForever } from "react-icons/md";
import {
    Button,
  } from "@material-tailwind/react";
import axios from "axios";
import { useContext } from "react";
import { UserContext } from "../../../App";
import { toast } from "react-toastify";

export default function DeletePetButton(props) {
    const api_url = 'https://petcaresystem20240514113535.azurewebsites.net'
    const { user_data, setUserData } = useContext(UserContext);

    const onDelete = () => {
        axios.defaults.headers.common['Authorization'] = "Bearer " + JSON.parse(user_data).token;
        axios.delete(api_url + '/api/pets/' + props.id)
        .then(
            (res) => {
                console.log(res);
                if (res.data.isSucceed) {
                    toast.success("Xoá thành công", {autoClose: 2000});
                    props.getPetByCurrentId();
                }
            }
        )
        .catch(
            (error) => {
                console.log(error);
            }
        )
    };

    return (
        <Button color="red" className="ml-1" variant="text" 
        onClick={onDelete}>
              <div className="flex items-center">
                <MdDeleteForever className=" mr-1 w-5 h-5"/>
                Xoá
              </div>
              
        </Button>
    ); 
};