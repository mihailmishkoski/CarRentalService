import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import Cookies from 'js-cookie';
import 'bootstrap/dist/css/bootstrap.css';

const Header = (props) => {
    const navigate = useNavigate();

    const handleLogout = () => {
        props.clearState();
        Cookies.remove('jwtToken');
        navigate('/login');
    };

    return (
        <header>
            <nav className="navbar navbar-expand-md navbar-dark bg-dark">
                <div className="container">
                    <a className="navbar-brand" href="/cars">Car Rental Service</a>
                    <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarCollapse"
                            aria-controls="navbarCollapse" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarCollapse">
                        <ul className="navbar-nav me-auto">
                            <li className="nav-item">
                                <Link className="nav-link" to={"/cars"}>Cars</Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link" to={"/rents"}>Rents</Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link" to={"/returns"}>Returns</Link>
                            </li>
                        </ul>

                        <ul className="navbar-nav ms-auto">
                            <li className="nav-item">
                                <button className="btn btn-outline-light" onClick={handleLogout}>
                                    Logout
                                </button>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
    );
}

export default Header;
