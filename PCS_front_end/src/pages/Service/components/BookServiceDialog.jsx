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
import { FaEdit, FaTrash } from "react-icons/fa";
import axios from "axios";
import { UserContext } from "../../../App";
import { toast } from "react-toastify";

export default function BookServiceDialog(props) {
    const [open, setOpen] = useState(false);

    const [booking_data, setBookingData] = useState({
        "petId": 13,
        "groomingServiceId": props.id,
        "bookingDate": null,
        "notes": null
    });

    const api_url = 'https://petcaresystem20240514113535.azurewebsites.net';
    const { user_data, setUserData } = useContext(UserContext);

    const handleOpen = () => setOpen((cur) => !cur);

    const handleChange = (e) => {
        setBookingData({
            ...booking_data,
            [e.target.name]: e.target.value
        })
    }

    const onPlaced = () => {
        handleOpen();

        axios.defaults.headers.common['Authorization'] = "Bearer " + JSON.parse(user_data).token;
        axios.put(api_url + '/api/grooming-service-bookings', booking_data)
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
            <Button className="px-3 mr-3" onClick={() => handleOpen()}>Đặt dịch vụ</Button>
            <Dialog
                open={open}
                handler={handleOpen}
                className="bg-transparent shadow-none"
            >
                <Card className="mx-auto w-full max-w-[24rem]">
                    <CardBody className="flex flex-col gap-4">
                        <Typography variant="h4" color="blue-gray">
                            Đặt dịch vụ
                        </Typography>
                        <Input required name="date" value={booking_data.bookingDate} onChange={handleChange} label="Ngày" type="date" size="md" />
                        <Input className="w-1/2" name="price" value={booking_data.notes} onChange={handleChange} label="Ghi chú" type="text" size="md" placeholder="Thêm ghi chú hoặc để trống" />
                    </CardBody>
                    <CardFooter className="pt-0">
                        <Button variant="text" color="gray" onClick={handleOpen}>
                            Huỷ
                        </Button>
                        <Button variant="gradient" onClick={onPlaced}>
                            Đặt
                        </Button>
                    </CardFooter>
                </Card>
            </Dialog>
        </>
    );
}