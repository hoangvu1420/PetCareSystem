import React, { useContext, useState } from "react";
import {
  Button,
  Dialog,
  DialogHeader,
  DialogBody,
  DialogFooter,
} from "@material-tailwind/react";
import TableWithStripedRows from "../../TableWithStripedRows";
import { UserContext } from "../../../App";
import axios from "axios";
 
export default function ViewMedicalRecordButton(props) {
  const [open, setOpen] = React.useState(false);
  const { user_data, setUserData } = useContext(UserContext);
  const { med_data, setMedData } = useState({});
  const api_url = 'https://petcaresystem20240514113535.azurewebsites.net'

  const getPetMedicalRecord = () => {
    axios.defaults.headers.common['Authorization'] = "Bearer " + JSON.parse(user_data).token;
    axios.get(api_url + '/api/medical-records?petId=' + props.id)
    .then((res) => console.log(res))
    .catch(
        (err) => console.log(err)
    );
  };

  const handleOpen = () => {
    getPetMedicalRecord();
    setOpen(!open);
  }
 
  return (
    <>
      <Button onClick={handleOpen} variant="gradient">
        Bệnh án
      </Button>
      <Dialog open={open} handler={handleOpen}>
        <DialogHeader className="pb-0">Bệnh án</DialogHeader>
        <DialogBody>
          <TableWithStripedRows/>
        </DialogBody>
        <DialogFooter>
          <Button
            variant="text"
            color="red"
            onClick={handleOpen}
            className="mr-1"
          >
            <span>Đóng</span>
          </Button>
          <Button variant="gradient" color="green" onClick={handleOpen}>
            <span>Thêm bệnh án</span>
          </Button>
        </DialogFooter>
      </Dialog>
    </>
  );
}