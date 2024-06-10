import { useState, useContext } from "react";
import { Button, Dialog, Card, CardBody, CardFooter, Typography, Input, Textarea } from "@material-tailwind/react";
import { UserContext } from "../../App";
import { toast } from "react-toastify";
import bookingService from "../../services/bookingService";

export default function EditBookingDialog({ open, handleOpen, recordData, getRooms }) {
  const [roomData, setRoomData] = useState(recordData);
  const { user_data } = useContext(UserContext);

  const handleChange = (e) => {
    setRoomData({
      ...roomData,
      [e.target.name]: e.target.value
    });
    console.log(roomData)
  };

  const onSaveRoom = async () => {
    handleOpen();
    try {
      const res = await bookingService.editBooking(JSON.parse(user_data).token, roomData.id, roomData)
      console.log(res)
      if (res.data.isSucceed) {
        getRooms();
        toast.success("Sửa thành công", { autoClose: 2000 });
      }
    }
    catch (e) {
      console.log(e);
      toast.error("Sửa thất bại");
    }
  };

  function normalizeDate(jsonDateString) {
    // Parse the JSON date string to a Date object
    const date = new Date(jsonDateString);

    // Extract year, month, day, hours, and minutes
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are 0-based, so add 1
    const day = String(date.getDate()).padStart(2, '0');
    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');

    // Format the date and time as YYYY-MM-DDTHH:MM
    const formattedDateTimeLocal = `${year}-${month}-${day}T${hours}:${minutes}`;

    return formattedDateTimeLocal;
  }

  return (
    <Dialog open={open} handler={handleOpen} className="bg-transparent shadow-none">
      <Card className="mx-auto w-full max-w-[24rem]">
        <CardBody className="flex flex-col gap-4">
          <Typography variant="h4" color="blue-gray">Thông tin thêm</Typography>
          <Input name="checkIn" value={normalizeDate(roomData.checkIn)} onChange={handleChange} type="datetime-local" label="Vào" size="sm" />
          <Input name="checkOut" value={normalizeDate(roomData.checkOut)} onChange={handleChange} type="datetime-local" label="Ra" size="md" />
          <Textarea name="notes" value={roomData.notes} onChange={handleChange} label="Ghi chú" size="md" />
        </CardBody>
        <CardFooter className="pt-0">
          <Button variant="text" color="gray" onClick={handleOpen}>Đóng</Button>
          {JSON.parse(user_data).userInfo.roles.includes("Admin") ?
            <Button variant="gradient" onClick={onSaveRoom}>Lưu</Button> 
            : null
          }
        </CardFooter>
      </Card>
    </Dialog>
  );
}
