import React, { useState } from 'react';
import {Link, useNavigate} from 'react-router-dom';

const Register = (props) => {
    const [formData, updateFormData] = useState({
        firstName: "",
        lastName: "",
        email: "",
        dateOfBirth: "",
        password: "",
        confirmPassword: ""
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

        const { firstName, lastName, email, dateOfBirth, password, confirmPassword } = formData;

        setErrorMessage(null);

        if (!firstName || !lastName || !email || !dateOfBirth || !password || !confirmPassword) {
            setErrorMessage("All fields are required.");
            return;
        }

        if (password !== confirmPassword) {
            setErrorMessage("Passwords do not match.");
            return;
        }

        props.onRegister(firstName, lastName, email, dateOfBirth, password)
            .then(() => {
                navigate('/cars');
            })
            .catch(error => {
                setErrorMessage(error.response ? error.response.data.message : "An error occurred during registration.");
            });
    };

    return (
        <div className="container my-5">
            <div className="row justify-content-center">
                <div className="col-md-6">
                    <div className="card shadow-sm p-4">
                        <h4 className="text-center mb-4">Register</h4>
                        <form onSubmit={onFormSubmit}>
                            <div className="form-group mb-3">
                                <label htmlFor="firstName">First Name</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    id="firstName"
                                    name="firstName"
                                    required
                                    onChange={handleInputChange}
                                />
                            </div>
                            <div className="form-group mb-3">
                                <label htmlFor="lastName">Last Name</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    id="lastName"
                                    name="lastName"
                                    required
                                    onChange={handleInputChange}
                                />
                            </div>
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
                                <label htmlFor="dateOfBirth">Date of Birth</label>
                                <input
                                    type="date"
                                    className="form-control"
                                    id="dateOfBirth"
                                    name="dateOfBirth"
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
                            <div className="form-group mb-3">
                                <label htmlFor="confirmPassword">Confirm Password</label>
                                <input
                                    type="password"
                                    className="form-control"
                                    id="confirmPassword"
                                    name="confirmPassword"
                                    required
                                    onChange={handleInputChange}
                                />
                            </div>
                            <div className="mt-3 text-center">
                                <p>
                                  Back to log in page   <Link to="/login">Log in</Link>
                                </p>
                            </div>
                            {errorMessage && <div className="alert alert-danger mt-2" role="alert">{errorMessage}</div>}
                            <button type="submit" className="btn btn-success w-100">Register</button>
                        </form>

                    </div>
                </div>
            </div>
        </div>
    );
};

export default Register;
