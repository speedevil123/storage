import React from 'react'
import '../Navbar.css';

const Navbar = () => {
    return (
        <nav className="navbar">
            <div className="navbar-left">
                <a href="/" className="logo">
                    ToolBox
                </a>
            </div>
            <div className="navbar-center">
                <ul className="nav-links">
                    <li>
                        <a href="/Contacts">Контакты</a>
                    </li>
                    <li>
                        <a href="/Help">Помощь</a>
                    </li>
                    <li>
                        <a className="rental-link" href="/Rental">Аренда</a>
                    </li>
                    <li>
                        <a href="/Statistics">Статистика</a>
                    </li>
                </ul>
            </div>
            {/* Здесь будет про отчеты */}
            <div className="navbar-right">
                <a href="/cart" className="cart-icon">
                    <i className="fas fa-shopping-cart"></i>
                    <span className="cart-count">0</span>
                </a>
                <a href="/account" className="user-icon">
                    <i className="fas fa-user"></i>
                </a>
            </div>
        </nav>
    );
};

export default Navbar;