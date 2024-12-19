import React from 'react';
import { useLocation } from 'react-router-dom'; 
import '../Navbar.css';

const Navbar = () => {
    const location = useLocation();

    return (
        <nav className="navbar">
            <div className="navbar-left">
                <a href="/Rental" className="logo">
                    ToolBox
                </a>
            </div>
            <div className="navbar-center">
                <ul className="nav-links">
                    <li>
                        <a href="/Contacts" className={location.pathname === '/Contacts' ? 'active' : ''}>
                            Контакты
                        </a>
                    </li>
                    <li>
                        <a href="/Help" className={location.pathname === '/Help' ? 'active' : ''}>
                            Помощь
                        </a>
                    </li>
                    <li>
                        <a href="/Rental" className={location.pathname === '/Rental' ? 'active' : ''}>
                            Аренда
                        </a>
                    </li>
                    <li>
                        <a href="/Statistics" className={location.pathname === '/Statistics' ? 'active' : ''}>
                            Статистика
                        </a>
                    </li>
                </ul>
            </div>
            <div className="navbar-right">
            </div>
        </nav>
    );
};

export default Navbar;
