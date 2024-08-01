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
    Option,
    Textarea
} from "@material-tailwind/react";
import { IoIosAdd } from "react-icons/io";
import { UserContext } from "../../App";
import { toast } from "react-toastify";
import axios from "axios";
import crudPetService from "../../services/crudPetService";
import bookingService from "../../services/bookingService";


export default function BookRoomDialog(props) {
    const [open, setOpen] = useState(false);
    const [pets, setPets] = useState([]);
    const { user_data, setUserData } = useContext(UserContext);
    const [booking_data, setPetData] = useState({
        "petId": "",
        "roomId": props.roomId,
        "checkIn": "",
        "checkOut": "",
        "notes": ""
    });

    const changeOpenState = () => {
        setOpen((cur) => !cur);
    }

    const handleOpen = async () => {
        await getPetByCurrentId();
        changeOpenState()
    }

    const handleChange = (e) => {
        setPetData({
            ...booking_data,
            [e.target.name]: e.target.value
        })
    }

    const getPetByCurrentId = async () => {
        try {
            const response = await crudPetService.getOwnedPet(
                JSON.parse(user_data).token,
                JSON.parse(user_data).userInfo.id
            )
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

    const onCreatingPet = async () => {
        changeOpenState();
        console.log(booking_data)
        try {
            const res = await bookingService.createBooking(
                JSON.parse(user_data).token,
                booking_data
            )
            console.log(res);
            if (res.data.isSucceed === true) {
                toast.success("Tạo thành công", { autoClose: 2000 });
            }
        } catch (e) {
            console.log(e)
        }
    }


    return (
        <div className="">
            <Button className="px-3" onClick={handleOpen}>Đặt</Button>
            <Dialog
                size="xs"
                open={open}
                handler={handleOpen}
                className="bg-transparent shadow-none"
            >
                <Card className="mx-auto w-full max-w-[24rem]">
                    <CardBody className="flex flex-col gap-4">
                        <Typography variant="h4" color="blue-gray">
                            Đặt phòng
                        </Typography>
                        <Select required onChange={(val) => setPetData({...booking_data, ["petId"]: val})} label="Chọn pet" size="md">
                            {pets.map((pet) => (
                                <Option key={pet.id} value={pet.id.toString()}>
                                    {pet.name} - {pet.breed}
                                </Option>
                            ))}
                        </Select>
                        <Input required name="checkIn" value={booking_data.checkIn} onChange={handleChange} label="Vào" type="datetime-local" size="md" />
                        <Input required name="checkOut" value={booking_data.checkOut} onChange={handleChange} label="Ra" type="datetime-local" size="md" />
                        <Input readOnly name="payment" value="Thanh toán tại quầy" label="Thanh toán" type="text" size="md" />
                        <Textarea name="notes" value={booking_data.notes} onChange={handleChange} label="Ghi chú" size="md" />
                    </CardBody>
                    <CardFooter className="pt-0">
                        <Button variant="text" color="gray" onClick={changeOpenState}>
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