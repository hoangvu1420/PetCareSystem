import { Card, Typography, Button } from "@material-tailwind/react";
import { useContext, useEffect, useState } from "react";
import { UserContext } from "../../App";
import { FaEdit, FaTrash, FaPlus, FaEye } from "react-icons/fa";
import EditBookingDialog from "./EditBookingDialog";
import { toast } from "react-toastify";
import bookingService from "../../services/bookingService";

const TABLE_HEAD = ["Thú cưng", "Phòng", "Đã ở", "Thành tiền", ""];

export default function RoomsHistory() {
    const { user_data } = useContext(UserContext);
    const [room_data, setBookingData] = useState([]);
    const [openEdit, setOpenEdit] = useState(false);
    const [currentRecord, setCurrentRecord] = useState(null);

    const handleOpenEdit = (record) => {
        setCurrentRecord(record);
        setOpenEdit(!openEdit);
    };
    const handleDelete = async (record) => {
        try {
            const res = await crudRoomService.deleteRoom(JSON.parse(user_data).token, record.id)
            console.log(res)
            if (res.data.isSucceed) {
                toast.success("Xoá thành công", {autoClose: 2000});
                getRooms();
            }
        }
        catch (e) {
            console.log(e)
        }
    };

    const getRooms = async () => {
        try {
            const res = await bookingService.getBookings(JSON.parse(user_data).token)
            setBookingData(res.data.data)
        }
        catch (e) {
            console.log(e)
        }
    };

    useEffect(() => {
        getRooms();
    }, []);

    function convertDate(d) {
        return (new Date(d)).toLocaleDateString('en-GB')
    }
    
    function formatPrice(price) {
        // Convert the price to a string
        const priceString = price.toString();
    
        // Use a regular expression to add commas
        const formattedPrice = priceString.replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    
        return formattedPrice;
    }

  return (
    <div className="">
        <div className="w-full flex justify-between pb-5">
        <Typography className="" variant="h4" color="blue-gray">Lịch sử</Typography>
        </div>
    
        <Card className="h-full">
        <table className="w-full table-auto text-left">
            <thead>
            <tr>
                {TABLE_HEAD.map((head) => (
                <th key={head} className="border-b border-blue-gray-100 bg-blue-gray-50 p-4">
                    <Typography variant="small" color="blue-gray" className="font-normal leading-none opacity-70">{head}</Typography>
                </th>
                ))}
            </tr>
            </thead>
            <tbody>
            {room_data.map((record, index) => (
                <tr key={index} className="even:bg-blue-gray-50/50">
                <td className="p-4"><Typography variant="small" color="blue-gray" className="font-normal">{record.petName}</Typography></td>
                <td className="p-4"><Typography variant="small" color="blue-gray" className="font-normal">{record.roomName}</Typography></td>
                <td className="p-4"><Typography variant="small" color="blue-gray" className="font-normal">{record.totalDays} ngày</Typography></td>
                <td className="p-4"><Typography variant="small" color="blue-gray" className="font-normal">{formatPrice(record.totalPrice)} VND</Typography></td>
                <td className="p-4 flex flex-col md:flex-row gap-3">
                    <Button className="px-3" onClick={() => handleOpenEdit(record)}><FaEye/></Button>
                    <Button className="px-3" variant="outlined" onClick={() => handleDelete(record)}><FaTrash/></Button>
                </td>
                </tr>
            ))}
            </tbody>
        </table>
        </Card>

        {currentRecord && (
          <EditBookingDialog 
            open={openEdit} 
            handleOpen={handleOpenEdit} 
            recordData={currentRecord} 
            getRooms={getRooms} 
          />
        )}
    </div>
  );
}