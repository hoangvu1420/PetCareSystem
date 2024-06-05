import { useContext, useState, useEffect } from "react";
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
import { FaEdit, FaTrash } from "react-icons/fa";
import axios from "axios";
import { UserContext } from "../../../App";
import { toast } from "react-toastify";

export default function BookServiceDialog(props) {
    const [open, setOpen] = useState(false);
    const [pets, setPets] = useState([]);
    const [booking_data, setBookingData] = useState({
        "petId": "",
        "groomingServiceId": props.id,
        "bookingDate": "",
        "notes": ""
    });

    const api_url = 'https://petcaresystem20240514113535.azurewebsites.net';
    const { user_data } = useContext(UserContext);

    const handleOpen = () => {
        setOpen((cur) => !cur);
        if (!open) {
            getPetByCurrentId();
        }
    };

    const handleChange = (e) => {
        let value = e.target.value;

        if (e.target.name === 'bookingDate') {
            let date = new Date(value);
            date.setHours(12, 0, 0, 0);
            value = date.toISOString().split('T')[0]; // Only keep the date part
        }

        setBookingData({
            ...booking_data,
            [e.target.name]: value
        });
    };

    const onPlaced = () => {
        handleOpen();

        axios.defaults.headers.common['Authorization'] = "Bearer " + JSON.parse(user_data).token;
        console.log(booking_data);

        // Convert bookingDate back to an ISO string with time set to 12:00 PM
        let bookingDataCopy = { ...booking_data };
        let date = new Date(bookingDataCopy.bookingDate);
        date.setHours(12, 0, 0, 0);
        bookingDataCopy.bookingDate = date.toISOString();

        axios.post(api_url + '/api/grooming-service-bookings', bookingDataCopy)
            .then((res) => {
                console.log(res);
                if (res.data.isSucceed === true) {
                    props.getGroomingServices();
                    toast.success("Đặt thành công", { autoClose: 2000 });
                }
            })
            .catch((e) => console.log(e));
    };

    const getPetByCurrentId = async () => {
        try {
            const response = await axios.get(`${api_url}/api/pets?userId=${JSON.parse(user_data).userInfo.id}`, {
                headers: {
                    Authorization: `Bearer ${JSON.parse(user_data).token}`
                }
            });
            console.log(response);
            if (response.data.isSucceed) {
                setPets(response.data.data);
            }
        } catch (error) {
            console.log(error);
            if (error.response.status !== 404)
                toast.error(error.response.data.errorMessages[0]);
            setPets([]);
        }
    };

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
                        <Select
                            label="Chọn pet"
                            name="petId"
                            value={booking_data.petId}
                            onChange={(e) => setBookingData({ ...booking_data, petId: e })}
                        >
                            {pets.map((pet) => (
                                <Option key={pet.id} value={pet.id.toString()}>
                                    {pet.name} - {pet.breed}
                                </Option>
                            ))}
                        </Select>
                        <Input required name="bookingDate" value={booking_data.bookingDate} onChange={handleChange} label="Ngày" type="date" size="md" />
                        <Input className="w-1/2" name="notes" value={booking_data.notes} onChange={handleChange} label="Ghi chú" type="text" size="md" placeholder="Thêm ghi chú hoặc để trống" />
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
