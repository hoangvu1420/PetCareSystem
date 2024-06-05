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
import axios from "axios";
import { UserContext } from "../../App";
import { toast } from "react-toastify";

export default function AddMedicalRecordDialog({ open, handleOpen, petId, getPetMedicalRecords }) {
  const [medicalRecordData, setMedicalRecordData] = useState({
    "petId": petId,
    "diagnosis": "",
    "doctor": "",
    "medication": "",
    "diet": "",
    "date": "",
    "nextAppointment": "",
    "notes": ""
  });

  const api_url = 'https://petcaresystem20240514113535.azurewebsites.net';
  const { user_data } = useContext(UserContext);

  const handleChange = (e) => {
    setMedicalRecordData({
      ...medicalRecordData,
      [e.target.name]: e.target.value
    });
  };

  const onSaveMedicalRecord = () => {
    handleOpen();
    axios.defaults.headers.common['Authorization'] = "Bearer " + JSON.parse(user_data).token;
    axios.post(api_url + '/api/medical-records', medicalRecordData)
      .then((res) => {
        console.log(res);
        if (res.data.isSucceed === true) {
          getPetMedicalRecords();
          toast.success("Thêm bệnh án thành công", { autoClose: 2000 });
        }
      })
      .catch((e) => console.log(e));
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
          <Input name="diagnosis" value={medicalRecordData.diagnosis} onChange={handleChange} label="Chẩn đoán" size="md" />
          <Input name="doctor" value={medicalRecordData.doctor} onChange={handleChange} label="Bác sĩ" size="md" />
          <Input name="medication" value={medicalRecordData.medication} onChange={handleChange} label="Thuốc" size="md" />
          <Input name="diet" value={medicalRecordData.diet} onChange={handleChange} label="Chế độ ăn" size="md" />
          <Input name="date" value={medicalRecordData.date} onChange={handleChange} label="Ngày" type="date" size="md" />
          <Input name="nextAppointment" value={medicalRecordData.nextAppointment} onChange={handleChange} label="Hẹn khám lại" type="date" size="md" />
          <Input name="notes" value={medicalRecordData.notes} onChange={handleChange} label="Ghi chú" size="md" />
        </CardBody>
        <CardFooter className="pt-0">
          <Button variant="text" color="gray" onClick={handleOpen}>
            Huỷ
          </Button>
          <Button variant="gradient" onClick={onSaveMedicalRecord}>
            Lưu
          </Button>
        </CardFooter>
      </Card>
    </Dialog>
  );
}
