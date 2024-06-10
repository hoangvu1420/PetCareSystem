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
import { FaEdit, FaTrash } from "react-icons/fa";
import axios from "axios";
import { UserContext } from "../../../App";
import { toast } from "react-toastify";

export default function EditPetDialog(props) {
    const [open, setOpen] = useState(false);

    const [service_data, setServiceData] = useState({
        "id": props.id,
        "name": props.name,
        "price": props.price,
        "description": props.description
    });

    const api_url = 'https://petcaresystem20240514113535.azurewebsites.net';
    const { user_data, setUserData } = useContext(UserContext);

    const handleOpen = () => setOpen((cur) => !cur);

    const handleChange = (e) => {
        setServiceData({
            ...service_data,
            [e.target.name]: e.target.value
        })
    }

    const onSavingChange = () => {
        handleOpen();
        axios.defaults.headers.common['Authorization'] = "Bearer " + JSON.parse(user_data).token;
        axios.put(api_url + '/api/grooming-services/' + service_data.id, service_data)
            .then((res) => {
                console.log(res);
                if (res.data.isSucceed === true) {
                    props.getGroomingServices();
                    toast.success("Sửa thành công", { autoClose: 2000 });
                }
            })
            .catch((e) => console.log(e));
    }

    return (
        <>
            <Button className="px-3 mr-3" onClick={() => handleOpen()}><FaEdit /></Button>
            <Dialog
                open={open}
                handler={handleOpen}
                className="bg-transparent shadow-none"
            >
                <Card className="mx-auto w-full max-w-[24rem]">
                    <CardBody className="flex flex-col gap-4">
                        <Typography variant="h4" color="blue-gray">
                            Chỉnh sửa
                        </Typography>
                        <Input name="name" value={service_data.name} onChange={handleChange} label="Tên" size="md" />
                        <Textarea name="description" label="Mô tả" value={service_data.description} onChange={handleChange} size="md" />
                        <Input className="w-1/2" name="price" value={service_data.price} onChange={handleChange} label="Giá" type="number" size="md" />
                    </CardBody>
                    <CardFooter className="pt-0">
                        <Button variant="text" color="gray" onClick={handleOpen}>
                            Huỷ
                        </Button>
                        <Button variant="gradient" onClick={onSavingChange}>
                            Lưu
                        </Button>
                    </CardFooter>
                </Card>
            </Dialog>
        </>
    );
}