import { FaTrash } from "react-icons/fa";
import {
    Button,
} from "@material-tailwind/react";
import { useContext } from "react";
import { UserContext } from "../../../App";
import { toast } from "react-toastify";
import crudPetService from "../../../services/crudPetService";

export default function DeletePetButton(props) {
    const { user_data, setUserData } = useContext(UserContext);

    const onDelete = async () => {
        try {
            const res = await crudPetService.deletePet(
                JSON.parse(user_data).token,
                props.id
            )
            console.log(res);
            if (res.data.isSucceed) {
                toast.success("Xoá thành công", { autoClose: 2000 });
                props.getPetByCurrentId();
            }
        } catch (e) {
            console.log(e)
        }
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