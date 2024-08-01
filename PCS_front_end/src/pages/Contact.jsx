import { MdHome, MdPhone, MdEmail } from "react-icons/md";

const Contact = () => {
  return (
    <div className="flex flex-col items-center justify-center">
      {/* Header */}
      <div className="text-center font-bold mb-20 mt-20">
        <h1 className="text-7xl font-bold mb-6">Liên hệ</h1>
        <p className="text-gray-600 font-light">
        Nếu bạn muốn được chúng tôi tư vấn hoặc cần giúp đỡ <br/>
        vui lòng liên hệ theo các cách sau để được hỗ trợ 24/7.
        </p>
      </div>

      {/* Contact Form */}
      <div className="flex justify-arrond w-full space-x-4 mb-20">
        <div className="flex flex-col items-center">
          <div className="mb-4 border rounded-full p-2 text-2xl bg-black text-white">
            <MdPhone />
          </div>
          <p className="px-16">0914.032.260</p>
        </div>

        <div className="flex flex-col items-center">
          <div className="mb-4 border rounded-full p-2 text-2xl bg-black text-white">
            <MdHome />
          </div>
          <p>
          Lô E2a-7, Đường D1, Đ. D1, Long Thạnh Mỹ, Thành Phố Thủ Đức, Thành phố Hồ Chí Minh
          </p>
        </div>

        <div className="flex flex-col items-center">
          <div className="mb-4 border rounded-full p-2 text-2xl bg-black text-white">
            <MdEmail />
          </div>
          <p className="px-4">PawMinglewluv@gmail.com</p>
        </div>
      </div>

      {/* Footer */}
      <div className="text-center text-sm mb-20">
        <h1 className="text-3xl font-bold mb-4">Liên hệ với chúng tôi</h1>
        <p className="text-gray-600 font-light">
        Bạn muốn nhận thông tin cập nhật mới nhất về chăm sóc thú cưng? <br/>
        Đăng ký nhận bản tin của chúng tôi để
        nhận các mẹo thú vị, hướng dẫn và nhiều hơn nữa
        </p>
        <div className="flex shadow-2xl shadow-gray-400 rounded-full mt-10">
          <input
            type="text"
            placeholder="Nhập email của bạn"
            className="w-full px-6 text-lg outline-none border-none bg-transparent py-2 text-gray-600" 
          />
          <button className="m-2 text-lg rounded-full bg-gray-800 px-12 py-2 text-white hover:bg-black">
            Gửi
          </button>
        </div>
      </div>
    </div>
  );
};

export default Contact;
