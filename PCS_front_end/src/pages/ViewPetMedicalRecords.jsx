import { Card, Typography, Button } from "@material-tailwind/react";
import { useContext, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { UserContext } from "../App";
import { FaEdit } from "react-icons/fa";
import axios from "axios";
 
const TABLE_HEAD = ["Chẩn đoán", "Bác sĩ", "Thuốc", "Chế độ ăn", "Ngày", "Hẹn khám lại", "Ghi chú", ""];
 
const TABLE_ROWS = [
  {
    name: "John Michael",
    job: "Manager",
    date: "23/04/18",
  },
  {
    name: "Alexa Liras",
    job: "Developer",
    date: "23/04/18",
  },
  {
    name: "Laurent Perrier",
    job: "Executive",
    date: "19/09/17",
  },
  {
    name: "Michael Levi",
    job: "Developer",
    date: "24/12/08",
  },
  {
    name: "Richard Gran",
    job: "Manager",
    date: "04/10/21",
  },
];
 
export default function ViewPetMedicalRecords() {
    const { pet_id } = useParams();

    const { user_data, setUserData } = useContext(UserContext);
    const [ med_data, setMedData ] = useState([]);
    const api_url = 'https://petcaresystem20240514113535.azurewebsites.net'
    const getPetMedicalRecord = () => {
        axios.defaults.headers.common['Authorization'] = "Bearer " + JSON.parse(user_data).token;
        axios.get(api_url + '/api/medical-records?petId=' + pet_id)
        .then((res) => {
            console.log(res);
            setMedData(res.data.data);
            console.log(res.data.data);
        })
        .catch(
            (err) => console.log(err)
        );
      };

    useEffect(() => {
        getPetMedicalRecord();
    }, []);

  return (
    <div>
    <Typography className="pl-5 py-3" variant="h4" color="blue-gray">Bệnh án</Typography>
    <Card className="h-full w-full overflow-scroll">
        
      <table className="w-full min-w-max table-auto text-left">
        <thead>
          <tr>
            {TABLE_HEAD.map((head) => (
              <th key={head} className="border-b border-blue-gray-100 bg-blue-gray-50 p-4">
                <Typography
                  variant="small"
                  color="blue-gray"
                  className="font-normal leading-none opacity-70"
                >
                  {head}
                </Typography>
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          {med_data.map(({ diagnosis, doctor, medication, diet, nextAppointment, notes, date }, index) => (
            <tr key={diagnosis} className="even:bg-blue-gray-50/50">
              <td className="p-4">
                <Typography variant="small" color="blue-gray" className="font-normal">
                  {diagnosis}
                </Typography>
              </td>
              <td className="p-4">
                <Typography variant="small" color="blue-gray" className="font-normal">
                  {doctor}
                </Typography>
              </td>
              <td className="p-4">
                <Typography variant="small" color="blue-gray" className="font-normal">
                  {medication}
                </Typography>
              </td>
              <td className="p-4">
                <Typography variant="small" color="blue-gray" className="font-normal">
                  {diet}
                </Typography>
              </td>
              <td className="p-4">
                <Typography variant="small" color="blue-gray" className="font-normal">
                  {(new Date(date)).toLocaleString()}
                </Typography>
              </td>
              <td className="p-4">
                <Typography variant="small" color="blue-gray" className="font-normal">
                  {(new Date(nextAppointment)).toLocaleString()}
                </Typography>
              </td>
              <td className="p-4">
                <Typography variant="small" color="blue-gray" className="font-normal">
                  {notes}
                </Typography>
              </td>
              <td className="p-4 sticky right-0">
                <Button className="px-3"><FaEdit/></Button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </Card>
    </div>
  );
}
