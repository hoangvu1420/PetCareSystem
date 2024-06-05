import { Card, Typography, Button } from "@material-tailwind/react";
import { useContext, useEffect, useState } from "react";
import { UserContext } from "../../App";
import { FaEdit, FaTrash, FaPlus } from "react-icons/fa";
import axios from "axios";
import AddRoomDialog from "./AddRoomDialog";
import EditMedicalRecordDialog from "./EditMedicalRecordDialog";
import { toast } from "react-toastify";
import crudRoomService from "../../services/crudRoomService";

const TABLE_HEAD = ["Phòng", "Giá (VND)", "Lượt book", "Mô tả", ""];

export default function ViewRooms() {
    const { user_data } = useContext(UserContext);
    const [med_data, setMedData] = useState([]);
    const [openAdd, setOpenAdd] = useState(false);
    const [openEdit, setOpenEdit] = useState(false);
    const [currentRecord, setCurrentRecord] = useState(null);
    const api_url = 'https://petcaresystem20240514113535.azurewebsites.net';

    const handleOpenAdd = () => setOpenAdd(!openAdd);
    const handleOpenEdit = (record) => {
        setCurrentRecord(record);
        setOpenEdit(!openEdit);
    };
    const handleDelete = (record) => {
        axios.defaults.headers.common['Authorization'] = "Bearer " + JSON.parse(user_data).token;
        axios.delete(api_url + '/api/rooms/' + record.id)
        .then(
            (res) => {
                console.log(res);
                if (res.data.isSucceed) {
                    toast.success("Xoá thành công", {autoClose: 2000});
                    getRooms();
                }
            }
        )
        .catch(
            (error) => {
                console.log(error);
            }
        )
    };

    const getRooms = async () => {
        try {
            const res = await crudRoomService.getRooms(JSON.parse(user_data).token)
            setMedData(res.data.data)
        }
        catch (e) {
            console.log(e)
        }
        // axios.defaults.headers.common['Authorization'] = "Bearer " + JSON.parse(user_data).token;
        // axios.get(api_url + '/api/rooms')
        // .then((res) => {
        //     setMedData(res.data.data);
        // })
        // .catch((err) => console.log(err));
    };

    useEffect(() => {
        getRooms();
    }, []);

  return (
    <div className="h-full w-4/5 mx-auto my-5">
        <div className="w-full flex justify-between p-5">
        <Typography className="" variant="h4" color="blue-gray">Phòng</Typography>
        <Button className="px-3" onClick={handleOpenAdd}>
            <div className="flex justify-center items-center">
                <FaPlus className="mr-1"/>
                Thêm phòng
            </div>
        </Button>
        </div>
    
        <Card className="h-full overflow-scroll">
        <table className="w-full min-w-max table-auto text-left">
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
            {med_data.map((record, index) => (
                <tr key={index} className="even:bg-blue-gray-50/50">
                <td className="p-4"><Typography variant="small" color="blue-gray" className="font-normal">{record.name}</Typography></td>
                <td className="p-4"><Typography variant="small" color="blue-gray" className="font-normal">{record.price}</Typography></td>
                <td className="p-4"><Typography variant="small" color="blue-gray" className="font-normal">{record.bookedCount}</Typography></td>
                <td className="p-4"><Typography variant="small" color="blue-gray" className="font-normal">{record.description}</Typography></td>
                <td className="p-4 sticky right-0">
                    <Button className="px-3 mr-3" onClick={() => handleOpenEdit(record)}><FaEdit/></Button>
                    <Button className="px-3" variant="outlined" onClick={() => handleDelete(record)}><FaTrash/></Button>
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
          <EditMedicalRecordDialog 
            open={openEdit} 
            handleOpen={handleOpenEdit} 
            recordData={currentRecord} 
            getRooms={getRooms} 
          />
        )}
    </div>
  );
}
