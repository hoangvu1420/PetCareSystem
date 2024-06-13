import { Card, Typography, Button } from "@material-tailwind/react";
import { useContext, useEffect, useState } from "react";
import { UserContext } from "../../App";
import { FaEdit, FaTrash, FaPlus } from "react-icons/fa";
import AddRoomDialog from "./AddRoomDialog";
import EditRoomDialog from "./EditRoomDialog";
import { toast } from "react-toastify";
import crudRoomService from "../../services/crudRoomService";
import BookRoomDialog from "./BookRoomDialog";
import utils from "../../utils/utils";

const TABLE_HEAD = ["Phòng", "Giá (VND)", "Lượt book", "Mô tả", ""];

export default function ViewRooms() {
    const { user_data } = useContext(UserContext);
    const [room_data, setRoomData] = useState([]);
    const [openAdd, setOpenAdd] = useState(false);
    const [openEdit, setOpenEdit] = useState(false);
    const [currentRecord, setCurrentRecord] = useState(null);

    const handleOpenAdd = () => setOpenAdd(!openAdd);
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
            const res = await crudRoomService.getRooms(JSON.parse(user_data).token)
            setRoomData(res.data.data)
        }
        catch (e) {
            console.log(e)
        }
    };

    useEffect(() => {
        getRooms();
    }, []);

  return (
    <div className="">
        <div className="w-full flex justify-between pb-5">
        <Typography className="" variant="h4" color="blue-gray">Danh sách phòng</Typography>
        {JSON.parse(user_data).userInfo.roles.includes("Admin") ?
        <Button className="px-3" onClick={handleOpenAdd}>
            <div className="flex justify-center items-center">
                <FaPlus className="mr-1"/>
                Thêm
            </div>
        </Button>
        : null
        }
        </div>
    
        <Card className="h-full overflow-scroll">
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
                <td className="p-4"><Typography variant="small" color="blue-gray" className="font-normal">{record.name}</Typography></td>
                <td className="p-4"><Typography variant="small" color="blue-gray" className="font-normal">{utils.formatPrice(record.price)}</Typography></td>
                <td className="p-4"><Typography variant="small" color="blue-gray" className="font-normal">{record.bookedCount}</Typography></td>
                <td className="p-4"><Typography variant="small" color="blue-gray" className="font-normal">{record.description}</Typography></td>
                <td className="p-4 sticky right-0 flex flex-col md:flex-row gap-3">
                    <BookRoomDialog roomId={record.id}/>
                    {JSON.parse(user_data).userInfo.roles.includes("Admin") ?
                    <>
                        <Button className="px-3" onClick={() => handleOpenEdit(record)}><FaEdit/></Button>
                        <Button className="px-3" variant="outlined" onClick={() => handleDelete(record)}><FaTrash/></Button> 
                    </>
                    : null
                    }
                </td>
                </tr>
            ))}
            </tbody>
        </table>
        </Card>
        
        <AddRoomDialog 
          open={openAdd} 
          handleOpen={handleOpenAdd} 
          petId={null} 
          getRooms={getRooms} 
        />

        {currentRecord && (
          <EditRoomDialog 
            open={openEdit} 
            handleOpen={handleOpenEdit} 
            recordData={currentRecord} 
            getRooms={getRooms} 
          />
        )}
    </div>
  );
}