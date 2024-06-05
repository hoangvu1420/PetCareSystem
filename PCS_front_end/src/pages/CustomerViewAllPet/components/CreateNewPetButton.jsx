import { useContext, useState } from "react";
import {
    Button,
    Dialog,
    Card,
    CardBody,
    CardFooter,
    Typography,
    Input,
    Select,
    Option
} from "@material-tailwind/react";
import { IoIosAdd } from "react-icons/io";
import { UserContext } from "../../../App";
import { toast } from "react-toastify";

import crudPetService from "../../../services/crudPetService";

export default function CreateNewPetButton(props) {
    const [open, setOpen] = useState(false);
    const { user_data, setUserData } = useContext(UserContext);
    const [pet_data, setPetData] = useState({
        "name": "",
        "age": "",
        "hairColor": "",
        "gender": "",
        "species": "",
        "breed": "",
        "imageUrl": "",
        "ownerId": JSON.parse(user_data).userInfo.id
    });

    const handleOpen = () => setOpen((cur) => !cur);

    const handleChange = (e) => {
        setPetData({
            ...pet_data,
            [e.target.name]: e.target.value
        })
    }

    const onCreatingPet = async () => {
        handleOpen();

        try {
            const res = await crudPetService.createPet(
                JSON.parse(user_data).token,
                pet_data
            )
            console.log(res);
            if (res.data.isSucceed === true) {
                props.getPetByCurrentId();
                toast.success("Tạo thành công", { autoClose: 2000 });
            }
        } catch (e) {
            console.log(e)
        }
    }


    return (
        <div className="">
            <button onClick={handleOpen} className="mt-1 flex justify-center items-center rounded-xl hover:bg-gray-200 duration-300 w-80 h-96 shadow-md">
                <div>
                    <IoIosAdd className="fill-gray-600 w-36 h-36" />
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
                            Thêm pet mới
                        </Typography>
                        <Input name="name" value={pet_data.name} onChange={handleChange} label="Tên" size="md" />
                        <Input name="species" value={pet_data.species} onChange={handleChange} label="Loài" size="md" />
                        <Input name="age" value={pet_data.age} onChange={handleChange} label="Tuổi" type="number" size="md" />
                        <Select onChange={(val) => setPetData({...pet_data, ["gender"]: val})} label="Giới tính" size="md">
                            <Option value="Male" onChange={handleChange}>Male</Option>
                            <Option value="Female" onChange={handleChange}>Female</Option>
                        </Select>
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