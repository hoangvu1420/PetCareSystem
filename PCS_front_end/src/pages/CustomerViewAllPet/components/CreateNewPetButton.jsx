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
} from "@material-tailwind/react";
import { IoIosAdd } from "react-icons/io";

export default function CreateNewPetButton() {
    const [open, setOpen] = useState(false);
    const handleOpen = () => setOpen((cur) => !cur);

    return (
        <div className="">
            <button onClick={handleOpen} className="mt-1 flex justify-center items-center rounded-xl hover:bg-gray-200 duration-300 w-80 h-96 shadow-xl">
                <div>
                    <IoIosAdd className="fill-gray-600 w-36 h-36"/>
                    <div className="text-gray-600 text-2xl">Thêm pet mới</div>
                </div>
            </button>
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
                    <Input label="Tên" size="md"/>
                    <Input label="Loài" size="md" />
                    <Input label="Tuổi" type="number" size="md" />
                    <Input label="Giới tính" size="md"/>
                    <Input label="Giống" size="md" />
                    <Input label="Màu lông" size="md" />
                    <Input label="Link ảnh" type="url" size="md" />
                </CardBody>
                <CardFooter className="pt-0">
                    <Button variant="text" color="gray" onClick={handleOpen}>
                        Huỷ
                    </Button>
                    <Button variant="gradient" onClick={() => {}}>
                    Lưu
                    </Button>
                </CardFooter>
                </Card>
            </Dialog>
        </div>
    );
};