import { FaFacebook, FaInstagram } from "react-icons/fa";
import { MdHome, MdPhone, MdEmail } from "react-icons/md";
import Logo from "../assets/PawMingle_Logo_White.png";

const BottomBar = () => {
  return (
    <footer className="bg-gray-800 text-white">
      <div className="flex justify-center items-center py-6">
        <div className="text-lg mx-20 flex items-center gap-2">
          <img src={Logo} alt="Logo" className="h-16"/>
          {/* <h1 className="text-lg">Paw Mingle</h1> */}
        </div>
        <div>
          <h1 className="text-lg">Liên hệ chúng tôi</h1>
          <ul className="list-disc pl-6">
            <li className="flex items-center">
              <MdPhone className="mr-2" />
              Hotline: 0914.032.260
            </li>
            <li className="flex items-center">
              <MdHome className="mr-2" />
              <div className="truncate">
                Địa chỉ: Lô E2a-7, Đường D1, Đ. D1, Long Thạnh Mỹ, Thành Phố Thủ Đức, Thành phố Hồ Chí Minh
              </div>
            </li>
            <li className="flex items-center">
              <MdEmail className="mr-2" />
              Email: PawMinglewluv@gmail.com
            </li>
          </ul>
          <div className="flex space-x-4 mx-6">
          <a
            href="https://www.facebook.com/profile.php?id=61560014007977"
            target="_blank"
            className="hover:text-blue-500"
          >
            <FaFacebook />
          </a>
          <a
            href="https://www.instagram.com/pawmingle_/"
            target="_blank"
            className="hover:text-pink-500"
          >
            <FaInstagram />
          </a>
        </div>
        </div>
        <div className="mx-20">
          <h1 className="text-lg">Danh sách dịch vụ</h1>
          <ul className="list-disc pl-6">
            <li>Cạo lông</li>
            <li>Cắt móng</li>
            <li>Tắm rửa</li>
            <li>...</li>
          </ul>
        </div>
      </div>
      <div className="py-4">
        <div className="text-center">© 2024 Bản quyền thuộc về Paw Mingle.</div>
      </div>
    </footer>
  );
};

export default BottomBar;