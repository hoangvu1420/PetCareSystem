import { useContext, useState } from "react";
import {
  Button,
  Dialog,
  Card,
  CardBody,
  CardFooter,
  Typography,
  Input,
} from "@material-tailwind/react";
import { IoIosAdd } from "react-icons/io";
import axios from "axios";
import { UserContext } from "../../../App";
import { toast } from "react-toastify";

export default function CreateNewPetButton(props) {
    const [open, setOpen] = useState(false);
    const { user_data, setUserData } = useContext(UserContext);
    const [pet_data, setPetData] = useState({
        "name": "",
        "age": "",
        "gender": "",
        "hairColor": "",
        "species": "",
        "breed": "",
        "imageUrl": "",
        "ownerId": JSON.parse(user_data).userInfo.id
    });

    const api_url = 'https://petcaresystem20240514113535.azurewebsites.net';

    const handleOpen = () => setOpen((cur) => !cur);
    
    const handleChange = (e) => {
        setPetData({
            ...pet_data,
            [e.target.name]: e.target.value
        })
    }

    const onCreatingPet = () => {
        handleOpen();
        axios.defaults.headers.common['Authorization'] = "Bearer " + JSON.parse(user_data).token;
        axios.post(api_url + '/api/Pet', pet_data)
        .then((res) => {
            console.log(res);
            if (res.data.isSucceed === true) {
                props.getPetByCurrentId();
                toast.success("Tạo thành công", {autoClose: 2000});
            }
        })
        .catch((e) => console.log(e));
    }


    return (
        <div className="">
            <button onClick={handleOpen} className="mt-1 flex justify-center items-center rounded-xl hover:bg-gray-200 duration-300 w-80 h-96 shadow-md">
                <div>
                    <IoIosAdd className="fill-gray-600 w-36 h-36"/>
                    <div className="text-gray-600 text-2xl">Thêm pet mới</div>
                </div>
            </button>
            <Dialog
                size="xs"
                open={open}
                handler={handleOpen}
                className="bg-transparent shadow-none"
            >
                <Card className="mx-auto w-full max-w-[24rem]">
                <CardBody className="flex flex-col gap-4">
                    <Typography variant="h4" color="blue-gray">
                    Chỉnh sửa
                    </Typography>
                    <Input name="name" value={pet_data.name} onChange={handleChange} label="Tên" size="md"/>
                    <Input name="species" value={pet_data.species} onChange={handleChange} label="Loài" size="md" />
                    <Input name="age" value={pet_data.age} onChange={handleChange} label="Tuổi" type="number" size="md" />
                    <Input name="gender" value={pet_data.gender} onChange={handleChange} label="Giới tính" size="md"/>
                    <Input name="breed" value={pet_data.breed} onChange={handleChange} label="Giống" size="md" />
                    <Input name="hairColor" value={pet_data.hairColor} onChange={handleChange} label="Màu lông" size="md" />
                    <Input name="imageUrl" value={pet_data.imageUrl} onChange={handleChange} label="Link ảnh" type="url" size="md" />
                </CardBody>
                <CardFooter className="pt-0">
                    <Button variant="text" color="gray" onClick={handleOpen}>
                        Huỷ
                    </Button>
                    <Button variant="gradient" onClick={onCreatingPet}>
                    Lưu
                    </Button>
                </CardFooter>
                </Card>
            </Dialog>
        </div>
    );
};