import reactlogo from '../assets/react.svg'

function Navbar () {
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
                        <button className='bg-cyan-100 rounded px-2.5 py-1'>Đăng xuất</button>
                    </li>
                    
                </ul>
            </nav>
        </div>
    );
}

export default Navbar;