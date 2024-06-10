import reactlogo from '../assets/react.svg'
import { UserContext } from '../App';
import { useContext } from 'react';

function Navbar () {
    const { token, setToken } = useContext(UserContext);

    const onSignOut = () => {
        sessionStorage.removeItem("token");
        setToken(null);
    };

    return (
        <div className='shadow md:flex'>
            <nav className="py-3 px-4 md:flex md:w-screen md:justify-between">
                <div className='flex items-center'>
                    <img className="h-6 items-center" src={reactlogo}/>
                    <p className='ml-2 text-lg font-semibold'>PetCare</p>
                </div>
                <ul className=' md:flex w-auto items-center'>
                    <li className='mr-3'>Thú cưng</li>
                    <li className=''>
                        <button onClick={onSignOut} className='bg-[#35b8be] hover:bg-[#03a8be] duration-300 text-white rounded px-2.5 py-1'>Đăng xuất</button>
                    </li>
                    
                </ul>
            </nav>
        </div>
    );
}

export default Navbar;