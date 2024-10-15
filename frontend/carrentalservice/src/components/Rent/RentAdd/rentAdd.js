import React, { useState, useEffect } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import CarRentalService from "../../../repository/CarServiceRepository";

const RentAdd = (props) => {
    const { id } = useParams();
    const [formData, updateFormData] = useState({
        carId: id,
        rentDate: "",
        returnDate: ""
    });
    const [car, setCar] = useState({});
    const [errorMessage, setErrorMessage] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        // Fetch the car details based on the carId
        CarRentalService.getCar(id).then((response) => {
            setCar(response.data);
        });
    }, [id]);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        updateFormData({
            ...formData,
            [name]: value.trim(),
        });
    };

    const onFormSubmit = (e) => {
        e.preventDefault();

        const {carId, rentDate, returnDate } = formData;


        setErrorMessage(null);


        if (!rentDate || !returnDate) {
            setErrorMessage("All fields are required");
            return;
        }

        // Call the parent prop function to handle rent addition
        props.onAddRent(carId, rentDate, returnDate)
            .then(() => {
                navigate('/rents');
            })
            .catch(error => {
                setErrorMessage(error.response ? error.response.data.message : "An error occurred");
            });
    };

    return (
        <div className="container my-5">
            <div className="row justify-content-center">
                <div className="col-md-6">
                    <div className="card shadow-sm p-4">
                        <h4 className="text-center mb-4">Create New Rent</h4>
                        <form onSubmit={onFormSubmit}>
                            <div className="form-group mb-3">
                                <label htmlFor="carName">Car</label>
                                <input type="text" className="form-control" id="carName" name="carName"
                                       value={car.name || ""} disabled />
                            </div>
                            <div className="form-group mb-3">
                                <label htmlFor="carModel">Model</label>
                                <input type="text" className="form-control" id="carModel" name="carModel"
                                       value={car.model || ""} disabled />
                            </div>
                            <div className="form-group mb-3">
                                <label htmlFor="rentDate">Rent Date</label>
                                <input
                                    type="date"
                                    className="form-control"
                                    id="rentDate"
                                    name="rentDate"
                                    required
                                    onChange={handleInputChange}
                                />
                            </div>
                            <div className="form-group mb-3">
                                <label htmlFor="returnDate">Return Date</label>
                                <input
                                    type="date"
                                    className="form-control"
                                    id="returnDate"
                                    name="returnDate"
                                    required
                                    onChange={handleInputChange}
                                />
                            </div>
                            {errorMessage && <div className="alert alert-danger mt-2" role="alert">{errorMessage}</div>}
                            <button type="submit" className="btn btn-success w-100">Submit</button>
                        </form>
                        <div className="text-center mt-4">
                            <Link to="/cars" className="btn btn-outline-secondary">Cancel</Link>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default RentAdd;
