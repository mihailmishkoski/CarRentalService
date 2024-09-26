import React from 'react';
import {Link} from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.css';
const header = (props) => {


    return (
        <header>
            <nav className="navbar navbar-expand-md navbar-dark navbar-fixed bg-dark">
                <a className="navbar-brand" href="/cars">Car Rental Service</a>
                <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarCollapse"
                        aria-controls="navbarCollapse" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarCollapse">
                    <ul className="navbar-nav mr-auto">
                        <li className="nav-item active">
                            <Link className="nav-link" to={"/cars"}>Cars</Link>
                        </li>
                        <li className="nav-item active">
                            <Link className="nav-link" to={"/rents"}>Rents</Link>
                        </li>
                        <li className="nav-item active">
                            <Link className={"nav-link"} to={"/returns"}>Returns</Link>
                        </li>
                    </ul>

                </div>
            </nav>
        </header>
    )
}

export default header;