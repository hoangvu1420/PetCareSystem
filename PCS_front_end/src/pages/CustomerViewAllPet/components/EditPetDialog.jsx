import { useContext, useState } from "react";
import {
  Button,
  Dialog,
  Card,
  CardHeader,
  CardBody,
  CardFooter,
  Typography,
  Input,
  Checkbox,
  Select,
  Option
} from "@material-tailwind/react";
import axios from "axios";
import { UserContext } from "../../../App";
import { toast } from "react-toastify";

export default function EditPetDialog(props) {
  const [open, setOpen] = useState(false);

  const [pet_data, setPetData] = useState({
    "id": props.id,
    "name": props.name,
    "age": props.age,
    "gender": props.gender,
    "hairColor": props.hairColor,
    "species": props.species,
    "breed": props.breed,
    "imageUrl": props.imageUrl
  });

  const api_url = 'https://petcaresystem20240514113535.azurewebsites.net';
  const { user_data, setUserData } = useContext(UserContext);

  const handleOpen = () => setOpen((cur) => !cur);

  const handleChange = (e) => {
    setPetData({
      ...pet_data,
      [e.target.name]: e.target.value
    })
  }

  const onSavingChange = () => {
    handleOpen();
    axios.defaults.headers.common['Authorization'] = "Bearer " + JSON.parse(user_data).token;
    axios.put(api_url + '/api/pets/' + pet_data.id, pet_data)
      .then((res) => {
        console.log(res);
        if (res.data.isSucceed === true) {
          props.getPetByCurrentId();
          toast.success("Sửa thành công", { autoClose: 2000 });
        }
      })
      .catch((e) => console.log(e));
  }

  return (
    <>
      <Button onClick={handleOpen}>Sửa</Button>
      <Dialog
        size="xs"
        open={open}
        handler={handleOpen}
        className="bg-transparent shadow-none"
      >
        <Card className="mx-auto w-full max-w-[24rem]">
          <CardBody className="flex flex-col gap-4">
            <Typography variant="h4" color="blue-gray">
              Chỉnh sửa
            </Typography>
            <Input name="name" value={pet_data.name} onChange={handleChange} label="Tên" size="md" />
            <Input className="w-1/2" name="species" value={pet_data.species} onChange={handleChange} label="Loài" size="md" />
            <Input className="w-1/2" name="age" value={pet_data.age} onChange={handleChange} label="Tuổi" type="number" size="md" />
            <Select name="gender" value={pet_data.gender} onChange={handleChange} label="Giới tính" size="md">
              <Option>Male</Option>
              <Option>Female</Option>
            </Select>
            <Input name="breed" value={pet_data.breed} onChange={handleChange} label="Giống" size="md" />
            <Input name="hairColor" value={pet_data.hairColor} onChange={handleChange} label="Màu lông" size="md" />
            <Input name="imageUrl" value={pet_data.imageUrl} onChange={handleChange} label="Link ảnh" type="url" size="md" />
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