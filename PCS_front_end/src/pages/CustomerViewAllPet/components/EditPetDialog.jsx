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
  Option
} from "@material-tailwind/react";
import { FaEdit } from "react-icons/fa";
import { UserContext } from "../../../App";
import { toast } from "react-toastify";
import crudPetService from "../../../services/crudPetService";

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

  const { user_data, setUserData } = useContext(UserContext);

  const handleOpen = () => setOpen((cur) => !cur);

  const handleChange = (e) => {
    setPetData({
      ...pet_data,
      [e.target.name]: e.target.value
    })
  }

  const onSavingChange = async () => {
    handleOpen();

    try {
      const res = await crudPetService.editPet(
        JSON.parse(user_data).token,
        pet_data
      )
      console.log(res);
      if (res.data.isSucceed) {
        props.getPetByCurrentId();
        toast.success("Sửa thành công", { autoClose: 2000 });
      }
    }
    catch (e) {
      console.log(e)
    }
  }

  return (
    <>
      <Button variant="text" className="px-3" onClick={() => handleOpen()}>
        <FaEdit className="w-4 h-4"/>
      </Button>
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
            <Input name="name" value={pet_data.name} onChange={handleChange} label="Tên" size="md" />
            <Input className="w-1/2" name="species" value={pet_data.species} onChange={handleChange} label="Loài" size="md" />
            <Input className="w-1/2" name="age" value={pet_data.age} onChange={handleChange} label="Tuổi" type="number" size="md" />
            <Select value={pet_data.gender} onChange={(val) => setPetData({ ...pet_data, ["gender"]: val })} label="Giới tính" size="md">
              <Option value="Male" onChange={handleChange}>Male</Option>
              <Option value="Female" onChange={handleChange}>Female</Option>
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