import {UserContext} from '../App';
import {
    MdPets,
    MdRoomService,
    MdRoom,
    MdPhone
} from "react-icons/md";
import {useState, useEffect, useContext} from "react";
import {
    Navbar,
    Collapse,
    Typography,
    Button,
    IconButton,
} from "@material-tailwind/react";
import {Link, useNavigate} from 'react-router-dom';
import {BiLogOut, BiLogIn} from "react-icons/bi";
import Logo from '../assets/PawMingle.png';


export default function NavbarDefault() {
    const [openNav, setOpenNav] = useState(false);
    const {user_data, setUserData} = useContext(UserContext);
    const navigate = useNavigate()

    useEffect(() => {
        window.addEventListener(
            "resize",
            () => window.innerWidth >= 960 && setOpenNav(false),
        );
    }, []);

    const onSignOut = () => {
        setOpenNav(false)
        navigate('/')
        setUserData(null);
    }

    const navList = (
        user_data ?
            <ul className="mt-2 mb-4 flex flex-col gap-2 lg:mb-0 lg:mt-0 lg:flex-row lg:items-center lg:gap-6">
                <Typography
                    as="li"
                    variant="small"
                    color="blue-gray"
                    className="flex items-center gap-x-2 p-1 font-bold">
                    <MdPets className='h-5 w-5'/>

                    <Link to="/protected/pets">
                        Thú cưng
                    </Link>
                </Typography>
                <Typography
                    as="li"
                    variant="small"
                    color="blue-gray"
                    className="flex items-center gap-x-2 p-1 font-bold">
                    <MdRoomService className='h-5 w-5'/>

                    <Link to="/protected/services">
                        Dịch vụ
                    </Link>
                </Typography>
                <Typography
                    as="li"
                    variant="small"
                    color="blue-gray"
                    className="flex items-center gap-x-2 p-1 font-bold">
                    <MdRoom className='h-5 w-5'/>
                    <Link to='/protected/rooms'>
                        Đặt phòng
                    </Link>
                </Typography>
                <Typography
                    as="li"
                    variant="small"
                    color="blue-gray"
                    className="flex items-center gap-x-2 p-1 font-bold">
                    <MdPhone className='h-5 w-5'/>
                    <Link to='/protected/contact'>
                        Liên hệ
                    </Link>
                </Typography>
            </ul>
            :
            null
    );

    return (
        <header>
            <Navbar className="w-screen max-w-none mx-auto px-4 py-2">
                <div className="container mx-auto flex items-center justify-between text-blue-gray-900">
                    <Link path='relative' to='/' className="flex items-center gap-2">
                        <img src={Logo} alt="Logo" className='h-12'/>
                        {/* <h1 className={'text-lg font-bold'}>Paw Mingle</h1> */}
                    </Link>
                    <div className="hidden lg:block">{navList}</div>
                    <div className="flex items-center gap-x-1">
                        {user_data ?
                            <Button
                                variant="gradient"
                                size="sm"
                                className="hidden lg:inline-block"
                                onClick={onSignOut}
                            >
                                <div className='flex items-center'>
                                    <BiLogOut className='mr-2 w-4 h-4'/>
                                    Đăng xuất
                                </div>
                            </Button>
                            :
                            <Link to='/auth/login'>
                                <Button
                                    variant="gradient"
                                    size="sm"
                                    className="hidden lg:inline-block"
                                    onClick={onSignOut}
                                >
                                    <div className='flex items-center'>
                                        <BiLogIn className='mr-2 w-4 h-4'/>
                                        Đăng nhập
                                    </div>
                                </Button>
                            </Link>
                        }


                    </div>
                    <IconButton
                        variant="text"
                        className="ml-auto h-6 w-6 text-inherit hover:bg-transparent focus:bg-transparent active:bg-transparent lg:hidden"
                        ripple={false}
                        onClick={() => setOpenNav(!openNav)}>
                        {openNav ? (
                            <svg
                                xmlns="http://www.w3.org/2000/svg"
                                fill="none"
                                className="h-6 w-6"
                                viewBox="0 0 24 24"
                                stroke="currentColor"
                                strokeWidth={2}>
                                <path
                                    strokeLinecap="round"
                                    strokeLinejoin="round"
                                    d="M6 18L18 6M6 6l12 12"/>
                            </svg>
                        ) : (
                            <svg
                                xmlns="http://www.w3.org/2000/svg"
                                className="h-6 w-6"
                                fill="none"
                                stroke="currentColor"
                                strokeWidth={2}>
                                <path
                                    strokeLinecap="round"
                                    strokeLinejoin="round"
                                    d="M4 6h16M4 12h16M4 18h16"/>
                            </svg>
                        )}
                    </IconButton>
                </div>
                <Collapse open={openNav}>
                    <div className="container mx-auto">
                        {navList}
                        <div className="flex items-center gap-x-1">
                            {user_data ?
                                <Button onClick={onSignOut} fullWidth variant="gradient" size="sm" className="">
                                    <div className='flex items-center'>
                                        <BiLogOut className='mr-2 w-4 h-4'/>
                                        Đăng xuất
                                    </div>
                                </Button>
                                :
                                <Button fullWidth variant="gradient" size="sm" className="">
                                    <Link to='/auth/login'>
                                        <div className='flex items-center'>
                                            <BiLogIn className='mr-2 w-4 h-4'/>
                                            Đăng nhập
                                        </div>
                                    </Link>
                                </Button>
                            }
                        </div>
                    </div>
                </Collapse>
            </Navbar>
        </header>
    );
}