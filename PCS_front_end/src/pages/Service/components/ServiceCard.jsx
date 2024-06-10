import {
    Card,
    CardHeader,
    CardBody,
    CardFooter,
    Typography,
    Button,
} from "@material-tailwind/react";
import BookServiceDialog from "./BookServiceDialog";
import EditServiceDialog from "./EditServiceDialog";
import DeleteServiceButton from "./DeleteServiceButton";
import { useNavigate } from "react-router-dom";
import { useContext } from "react";
import { UserContext } from "../../../App";
import utils from "../../../utils/utils";

export default function ServiceCard(props) {
    const navigate = useNavigate();
    const { user_data } = useContext(UserContext);
    return (
        <Card className="mt-6 w-80">
            <CardHeader color="blue-gray" className="relative h-48">
                <img
                    className="object-cover w-full h-full"
                    src="https://smartguy.com/webservice/storage/category/animal-grooming.jpg"
                    alt="card-image"
                />
            </CardHeader>
            <CardBody className="pt-4 pb-3">
                <Typography variant="h5" color="blue-gray" className="mb-1">
                    {props.name}
                </Typography>
                <div className="mb-0 pb-0">
                    <div className="flex flex-col">
                        <div className="w-full"><span className="font-semibold">Mô tả:</span> {props.description}</div>
                        <div className="w-full flex">
                            <div className="w-1/2"><span className="font-semibold">Giá:</span> {utils.formatPrice(props.price)}đ</div>
                            <div className="w-1/2"><span className="font-semibold">Đã đặt:</span> {props.bookedCount}</div>
                        </div>
                    </div>
                </div>
            </CardBody>
            <CardFooter className="pt-0 mt-0">
                <BookServiceDialog className="mr-2"
                    id={props.id}
                    name={props.name}
                    getGroomingServices={props.getGroomingServices} />
                {JSON.parse(user_data).userInfo.roles.includes("Admin") ?
                <>
                <EditServiceDialog className="mr-2"
                    id={props.id} name={props.name}
                    price={props.price}
                    description={props.description}
                    bookedCount={props.bookedCount}
                    getGroomingServices={props.getGroomingServices} />
                <DeleteServiceButton className="mr-2"
                    id={props.id}
                    getGroomingServices={props.getGroomingServices} />
                </>
                : null}
            </CardFooter>
        </Card>
    );
}