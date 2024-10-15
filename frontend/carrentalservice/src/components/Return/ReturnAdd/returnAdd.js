import React, { useState, useEffect } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import CarRentalService from "../../../repository/CarServiceRepository";

const ReturnAdd = (props) => {
    const { id } = useParams();
    const [formData, updateFormData] = useState({
        rentId: id,
        returnDate: new Date().toISOString().slice(0, 10) // Set the current date
    });
    const [rent, setRent] = useState({});
    const [errorMessage, setErrorMessage] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {

        CarRentalService.getRent(id).then((response) => {
            setRent(response.data);
        });
    }, [id]);

    const onFormSubmit = (e) => {
        e.preventDefault();

        const { rentId, returnDate } = formData;

        setErrorMessage(null);

        props.onAddReturn(rentId, returnDate)
            .then(() => {
                navigate('/returns');
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
                        <h4 className="text-center mb-4">Create New Return</h4>
                        <form onSubmit={onFormSubmit}>
                            {rent.car && (
                                <>
                                    <div className="form-group mb-3">
                                        <label htmlFor="carName">Car</label>
                                        <input type="text" className="form-control" id="carName" name="carName"
                                               value={rent.car.name || ""} disabled />
                                    </div>
                                    <div className="form-group mb-3">
                                        <label htmlFor="carModel">Model</label>
                                        <input type="text" className="form-control" id="carModel" name="carModel"
                                               value={rent.car.model || ""} disabled />
                                    </div>
                                </>
                            )}
                            <div className="form-group mb-3">
                                <label htmlFor="rentDate">Rent Date</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    id="rentDate"
                                    name="rentDate"
                                    value={rent.rentDate ? new Date(rent.rentDate).toLocaleDateString() : "N/A"}
                                    disabled
                                />
                            </div>
                            <div className="form-group mb-3">
                                <label htmlFor="returnDate">Return Date</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    id="returnDate"
                                    name="returnDate"
                                    value={formData.returnDate}
                                    disabled
                                />
                            </div>
                            {errorMessage && <div className="alert alert-danger mt-2" role="alert">{errorMessage}</div>}
                            <button type="submit" className="btn btn-success w-100">Submit</button>
                        </form>
                        <div className="text-center mt-4">
                            <Link to="/rents" className="btn btn-outline-secondary">Cancel</Link>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default ReturnAdd;
