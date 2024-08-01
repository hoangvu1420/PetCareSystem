import React from "react";
import {
  Card,
  CardHeader,
  CardBody,
  CardFooter,
  Typography,
  Button,
} from "@material-tailwind/react";
import { useNavigate } from "react-router-dom";

const HomePage = () => {
  const navigate = useNavigate();

  const handleButtonClick = () => {
    navigate("/protected/pets");
  };

  return (
    <div
      className={
        "border border-gray-200 shadow-2xl flex flex-col gap-6 p-4 rounded-xl"
      }
    >
      <img
        className={"col-span-6 rounded-lg w-full h-auto"}
        width={1128}
        height={600}
        src="https://images.unsplash.com/photo-1546377791-2e01b4449bf0?q=80&w=2533&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
        alt="pet-image"
      />
      <div className={"col-span-6 col-start-7"}>
        <Typography variant="h5" color="blue-gray" className="mb-2">
          Chào mừng đến với Paw Mingle - Hệ thống quản lý thú cưng của bạn
        </Typography>
        <Typography>
          <p>
            Paw Mingle là một hệ thống quản lý thú cưng đa dạng và hấp dẫn, phục
            vụ cho các cửa hàng thú cưng, bệnh viện thú cưng và cả những cá nhân
            yêu thú cưng. Với Paw Mingle, bạn sẽ trải nghiệm nhiều dịch vụ hữu
            ích và tiện lợi:
          </p>
          <p className="mt-2 mb-2">
            <span className="font-bold">🦆 Quản lý Thông Tin Thú Cưng: </span>
            <span>
              Hệ thống cho phép bạn lưu trữ thông tin chi tiết về thú cưng của
              mình. Từ thông tin cơ bản như tên, loại, tuổi đến lịch sử khám
              bệnh và tiêm phòng, bạn có thể dễ dàng theo dõi sức khỏe của thú
              cưng.
            </span>
          </p>
          <p className="mt-2 mb-2">
            <span className="font-bold">🦜 Lịch Hẹn Khám Bệnh: </span>
            <span>
              Bạn có thể đặt lịch hẹn khám bệnh cho thú cưng một cách thuận
              tiện. Hệ thống sẽ thông báo cho bạn khi đến lịch hẹn, giúp bạn
              không bỏ lỡ bất kỳ cuộc hẹn nào.
            </span>
          </p>
          <p className="mt-2 mb-2">
            <span className="font-bold">🐦 Nhắc Nhở Tiêm Phòng: </span>
            <span>
              Paw Mingle sẽ gợi ý lịch tiêm phòng cho thú cưng của bạn. Điều này
              giúp đảm bảo thú cưng luôn được bảo vệ khỏi các bệnh truyền nhiễm.
            </span>
          </p>
          <p className="mt-2 mb-2">
            <span className="font-bold">🦢 Tích Hợp Hệ Thống: </span>
            <span>
              Hệ thống này tích hợp giữa thông tin thú cưng, lịch hẹn khám bệnh
              và tiêm phòng, giúp bạn quản lý toàn diện và hiệu quả.
            </span>
          </p>
        </Typography>
        <Typography>
          Paw Mingle không chỉ là một công cụ quản lý thông tin, mà còn là người
          bạn đồng hành đáng tin cậy trong việc chăm sóc thú cưng của bạn. Hãy
          khám phá và trải nghiệm ngay! 🐾🐾🐾
        </Typography>
        <Button className={"mt-2"} onClick={handleButtonClick}>
          Xem pet của bạn
        </Button>
      </div>
    </div>
  );
};

export default HomePage;
