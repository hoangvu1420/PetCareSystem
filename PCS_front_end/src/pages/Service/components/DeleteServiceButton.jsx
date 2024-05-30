import { FaTrash } from "react-icons/fa";
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
        axios.delete(api_url + '/api/grooming-services/' + props.id)
            .then(
                (res) => {
                    console.log(res);
                    if (res.data.isSucceed) {
                        toast.success("Xoá thành công", { autoClose: 2000 });
                        props.getGroomingServices();
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
        <Button color="red" className="px-3" variant="text"
            onClick={onDelete}>
            <div className="flex items-center">
                <FaTrash />
            </div>
        </Button>
    );
};