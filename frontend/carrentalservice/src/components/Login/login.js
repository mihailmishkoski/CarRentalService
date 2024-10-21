import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom'; // Import Link
import Cookies from "js-cookie";

const Login = (props) => {
    const [formData, updateFormData] = useState({
        email: "",
        password: ""
    });

    const [errorMessage, setErrorMessage] = useState(null);
    const navigate = useNavigate();

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        updateFormData({
            ...formData,
            [name]: value.trim(),
        });
    };

    const onFormSubmit = (e) => {
        e.preventDefault();

        const { email, password } = formData;

        setErrorMessage(null);

        if (!email || !password) {
            setErrorMessage("Both email and password are required.");
            return;
        }

        props.onLogin(email, password)
            .then((response) => {
                const token = response.data.token;
                Cookies.set('jwtToken', token);
                props.Authenticate()
                return props.loadInitialData();
            })
            .then(() => {
                navigate("/cars");
            })
            .catch(error => {
                setErrorMessage(error.response ? error.response.data.message : "Login failed. Please try again.");
            });
    };

    return (
        <div className="container my-5">
            <div className="row justify-content-center">
                <div className="col-md-6">
                    <div className="card shadow-sm p-4">
                        <h4 className="text-center mb-4">Login</h4>
                        <form onSubmit={onFormSubmit}>
                            <div className="form-group mb-3">
                                <label htmlFor="email">Email</label>
                                <input
                                    type="email"
                                    className="form-control"
                                    id="email"
                                    name="email"
                                    required
                                    onChange={handleInputChange}
                                />
                            </div>
                            <div className="form-group mb-3">
                                <label htmlFor="password">Password</label>
                                <input
                                    type="password"
                                    className="form-control"
                                    id="password"
                                    name="password"
                                    required
                                    onChange={handleInputChange}
                                />
                            </div>
                            {errorMessage && <div className="alert alert-danger mt-2" role="alert">{errorMessage}</div>}
                            <button type="submit" className="btn btn-primary w-100">Login</button>
                        </form>
                        <div className="mt-3 text-center">
                            <p>
                                Don't have an account? <Link to="/register">Create an account</Link>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Login;
