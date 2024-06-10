import { Card, Typography, Button } from "@material-tailwind/react";
import { useContext, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { UserContext } from "../../App";
import { FaEdit, FaTrash } from "react-icons/fa";
import axios from "axios";
import AddMedicalRecordDialog from "./AddMedicalRecordDialog";
import EditMedicalRecordDialog from "./EditMedicalRecordDialog";
import { toast } from "react-toastify";
import utils from "../../utils/utils";

const TABLE_HEAD = ["Chẩn đoán", "Bác sĩ", "Thuốc", "Chế độ ăn", "Ngày", "Hẹn khám lại", "Ghi chú", ""];

export default function ViewPetMedicalRecords() {
    const { pet_id } = useParams();
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
        axios.delete(api_url + '/api/medical-records/' + record.id)
        .then(
            (res) => {
                console.log(res);
                if (res.data.isSucceed) {
                    toast.success("Xoá thành công", {autoClose: 2000});
                    getPetMedicalRecords();
                }
            }
        )
        .catch(
            (error) => {
                console.log(error);
            }
        )
    };

    const getPetMedicalRecords = () => {
        axios.defaults.headers.common['Authorization'] = "Bearer " + JSON.parse(user_data).token;
        axios.get(api_url + '/api/medical-records?petId=' + pet_id)
        .then((res) => {
            setMedData(res.data.data);
        })
        .catch((err) => console.log(err));
    };

    useEffect(() => {
        getPetMedicalRecords();
    }, []);

  return (
    <div className="">
        <div className="w-full flex justify-between p-5">
        <Typography className="" variant="h4" color="blue-gray">Bệnh án</Typography>
        <Button className="px-3" onClick={handleOpenAdd}>Thêm bệnh án</Button>
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
            {med_data.map((record, index) => (
                <tr key={index} className="even:bg-blue-gray-50/50">
                <td className="p-4"><Typography variant="small" color="blue-gray" className="font-normal">{record.diagnosis}</Typography></td>
                <td className="p-4"><Typography variant="small" color="blue-gray" className="font-normal">{record.doctor}</Typography></td>
                <td className="p-4"><Typography variant="small" color="blue-gray" className="font-normal">{record.medication}</Typography></td>
                <td className="p-4"><Typography variant="small" color="blue-gray" className="font-normal">{record.diet}</Typography></td>
                <td className="p-4"><Typography variant="small" color="blue-gray" className="font-normal">{utils.convertDate(record.date)}</Typography></td>
                <td className="p-4"><Typography variant="small" color="blue-gray" className="font-normal">{utils.convertDate(record.nextAppointment)}</Typography></td>
                <td className="p-4"><Typography variant="small" color="blue-gray" className="font-normal">{record.notes}</Typography></td>
                <td className="p-4 sticky right-0">
                    <Button className="px-3 mr-3" onClick={() => handleOpenEdit(record)}><FaEdit/></Button>
                    <Button className="px-3" variant="outlined" onClick={() => handleDelete(record)}><FaTrash/></Button>
                </td>
                </tr>
            ))}
            </tbody>
        </table>
        </Card>
        
        <AddMedicalRecordDialog 
          open={openAdd} 
          handleOpen={handleOpenAdd} 
          petId={pet_id} 
          getPetMedicalRecords={getPetMedicalRecords} 
        />

        {currentRecord && (
          <EditMedicalRecordDialog 
            open={openEdit} 
            handleOpen={handleOpenEdit} 
            recordData={currentRecord} 
            getPetMedicalRecords={getPetMedicalRecords} 
          />
        )}
    </div>
  );
}