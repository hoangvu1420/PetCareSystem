import { useState, useContext } from "react";
import { Button, Dialog, Card, CardBody, CardFooter, Typography, Input, Textarea } from "@material-tailwind/react";
import { UserContext } from "../../App";
import { toast } from "react-toastify";
import crudRoomService from "../../services/crudRoomService";

export default function EditRoomDialog({ open, handleOpen, recordData, getRooms }) {
  const [roomData, setRoomData] = useState(recordData);
  const { user_data } = useContext(UserContext);

  const handleChange = (e) => {
    setRoomData({
      ...roomData,
      [e.target.name]: e.target.value
    });
  };

  const onSaveRoom = async () => {
    handleOpen();
    try {
      const res = await crudRoomService.editRoom(JSON.parse(user_data).token, roomData.id, roomData)
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

  return (
    <Dialog open={open} handler={handleOpen} className="bg-transparent shadow-none">
      <Card className="mx-auto w-full max-w-[24rem]">
        <CardBody className="flex flex-col gap-4">
          <Typography variant="h4" color="blue-gray">Sửa thông tin phòng</Typography>
          <Input name="name" value={roomData.name} onChange={handleChange} label="Tên phòng" size="md" />
          <Input name="price" value={roomData.price} onChange={handleChange} type="number" label="Giá" size="md" />
          <Textarea name="description" value={roomData.description} onChange={handleChange} label="Mô tả" size="md" />
        </CardBody>
        <CardFooter className="pt-0">
          <Button variant="text" color="gray" onClick={handleOpen}>Huỷ</Button>
          <Button variant="gradient" onClick={onSaveRoom}>Lưu</Button>
        </CardFooter>
      </Card>
    </Dialog>
  );
}
