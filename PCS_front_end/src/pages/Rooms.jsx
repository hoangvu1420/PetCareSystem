import {
    Tabs,
    TabsHeader,
    TabsBody,
    Tab,
    TabPanel,
  } from "@material-tailwind/react";
import { FaHistory, FaList } from "react-icons/fa";
import ViewRooms from "./ViewRooms/ViewRooms";
import RoomsHistory from "./RoomsHistory/RoomsHistory";

export default function Rooms () {
    return (
        <Tabs value="list">
            <TabsHeader>
                <Tab key="list" value="list">
                    <div className="flex items-center">
                        <FaList className="mr-1.5"/> Danh sách phòng
                    </div>
                </Tab>
                <Tab key="history "value="history">
                    <div className="flex items-center">
                        <FaHistory className="mr-1.5"/> Lịch sử đặt phòng
                    </div>
                </Tab>
            </TabsHeader>
            <TabsBody>
                <TabPanel key="list" value="list">
                    <ViewRooms/>
                </TabPanel>
                <TabPanel key="history" value="history">
                    <RoomsHistory/>
                </TabPanel>
            </TabsBody>
        </Tabs>
    )
}