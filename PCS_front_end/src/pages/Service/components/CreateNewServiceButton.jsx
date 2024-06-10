import { useContext, useState } from "react";
import {
    Button,
    Dialog,
    Card,
    CardBody,
    CardFooter,
    Typography,
    Input,
    Textarea
} from "@material-tailwind/react";
import { IoIosAdd } from "react-icons/io";
import axios from "axios";
import { UserContext } from "../../../App";
import { toast } from "react-toastify";

export default function CreateNewPetButton(props) {
    const [open, setOpen] = useState(false);
    const { user_data, setUserData } = useContext(UserContext);
    const [service_data, setServiceData] = useState({
        "name": "",
        "price": "",
        "description": ""
    });

    const api_url = 'https://petcaresystem20240514113535.azurewebsites.net';

    const handleOpen = () => setOpen((cur) => !cur);

    const handleChange = (e) => {
        setServiceData({
            ...service_data,
            [e.target.name]: e.target.value
        })
    }

    const onCreatingPet = () => {
        handleOpen();
        axios.defaults.headers.common['Authorization'] = "Bearer " + JSON.parse(user_data).token;
        axios.post(api_url + '/api/grooming-services', service_data)
            .then((res) => {
                console.log(res);
                if (res.data.isSucceed === true) {
                    props.getGroomingServices();
                    toast.success("Tạo thành công", { autoClose: 2000 });
                }
            })
            .catch((e) => console.log(e));
    }

    return (
        <div className="">
            <button onClick={handleOpen} className="mt-1 flex justify-center items-center rounded-xl hover:bg-gray-200 duration-300 w-80 h-96 shadow-md">
                <div>
                    <IoIosAdd className="fill-gray-600 w-36 h-36" />
                    <div className="text-gray-600 text-2xl">Thêm dịch vụ</div>
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
                            Thêm dịch vụ mới
                        </Typography>
                        <Input name="name" value={service_data.name} onChange={handleChange} label="Tên dịch vụ" size="md" />
                        <Textarea name="description" label="Mô tả" value={service_data.description} onChange={handleChange} size="md"/>
                        <Input name="price" value={service_data.price} onChange={handleChange} label="Giá" type="number" size="md" />
                    </CardBody>
                    <CardFooter className="pt-0">
                        <Button variant="text" color="gray" onClick={handleOpen}>
                            Huỷ
                        </Button>
                        <Button variant="gradient" onClick={onCreatingPet}>
                            Thêm
                        </Button>
                    </CardFooter>
                </Card>
            </Dialog>
        </div>
    );
};