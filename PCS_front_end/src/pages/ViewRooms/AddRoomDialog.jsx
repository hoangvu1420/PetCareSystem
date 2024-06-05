import { useState, useContext } from "react";
import {
  Button,
  Dialog,
  Card,
  CardBody,
  CardFooter,
  Typography,
  Input
} from "@material-tailwind/react";
import { UserContext } from "../../App";
import { toast } from "react-toastify";
import crudRoomService from "../../services/crudRoomService";

export default function AddRoomDialog({ open, handleOpen, petId, getRooms }) {
  const [roomData, setRoomData] = useState({
    "name": "",
    "price": "",
    "description": ""
  });

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
      const res = await crudRoomService.addRoom(JSON.parse(user_data).token, roomData)
      if (res.data.isSucceed) {
          getRooms();
          toast.success("Thêm bệnh án thành công", { autoClose: 2000 });
      }
    }
    catch (e) {
      console.log(e)
    }
    // axios.defaults.headers.common['Authorization'] = "Bearer " + JSON.parse(user_data).token;
    // axios.post(api_url + '/api/rooms', roomData)
    //   .then((res) => {
    //     console.log(res);
    //     if (res.data.isSucceed === true) {
    //       getRooms();
    //       toast.success("Thêm bệnh án thành công", { autoClose: 2000 });
    //     }
    //   })
    //   .catch((e) => console.log(e));
  };

  return (
    <Dialog
      open={open}
      handler={handleOpen}
      className="bg-transparent shadow-none"
    >
      <Card className="mx-auto w-full max-w-[24rem]">
        <CardBody className="flex flex-col gap-4">
          <Typography variant="h4" color="blue-gray">
            Thêm Bệnh Án
          </Typography>
          <Input name="name" value={roomData.name} onChange={handleChange} label="Tên phòng" size="md" />
          <Input name="price" value={roomData.price} type="number" onChange={handleChange} label="Giá" size="md" />
          <Input name="description" value={roomData.description} onChange={handleChange} label="Mô tả" size="md" />
        </CardBody>
        <CardFooter className="pt-0">
          <Button variant="text" color="gray" onClick={handleOpen}>
            Huỷ
          </Button>
          <Button variant="gradient" onClick={onSaveRoom}>
            Lưu
          </Button>
        </CardFooter>
      </Card>
    </Dialog>
  );
}
