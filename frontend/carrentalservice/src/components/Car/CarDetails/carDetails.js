import React, { useEffect, useState } from 'react';
import {Link} from "react-router-dom";

const CarDetails = (props) => {
    const [carDetails, setCarDetails] = useState({
        name: "",
        description: "",
        model: "",
        dateManufactured: "",
        kilometersTraveled: 0,
        licensePlate: "",
        pricePerDay: 0
    });

    useEffect(() => {
        if (props.car) {
            setCarDetails({
                name: props.car.name || "",
                description: props.car.description || "",
                model: props.car.model || "",
                dateManufactured: props.car.dateManufactured || "",
                kilometersTraveled: props.car.kilometersTraveled || 0,
                licensePlate: props.car.licensePlate || "",
                pricePerDay:  props.car.pricePerDay || 0
            });
        }
    }, [props.car]);

    return (
        <div className="container my-5">
            <div className="row justify-content-center">
                <div className="col-md-6">
                    <div className="card shadow-sm p-4">
                        <h4 className="text-center mb-4">Details</h4>
                        <ul className="list-group list-group-flush">
                            <li className="list-group-item d-flex justify-content-between">
                                <strong>Name:</strong> <span>{carDetails.name}</span>
                            </li>
                            <li className="list-group-item d-flex justify-content-between">
                                <strong>Description:</strong> <span>{carDetails.description}</span>
                            </li>
                            <li className="list-group-item d-flex justify-content-between">
                                <strong>Model:</strong> <span>{carDetails.model}</span>
                            </li>
                            <li className="list-group-item d-flex justify-content-between">
                                <strong>Date Manufactured:</strong> <span>{carDetails.dateManufactured}</span>
                            </li>
                            <li className="list-group-item d-flex justify-content-between">
                                <strong>Kilometers Traveled:</strong> <span>{carDetails.kilometersTraveled} km</span>
                            </li>
                            <li className="list-group-item d-flex justify-content-between">
                                <strong>License Plate:</strong> <span>{carDetails.licensePlate}</span>
                            </li>
                            <li className="list-group-item d-flex justify-content-between">
                                <strong>Price per day:</strong> <span>{carDetails.pricePerDay}</span>
                            </li>
                        </ul>
                    </div>
                    <div className="text-center mt-4">
                        {/* Back to Cars Button */}
                        <Link to="/cars" className="btn btn-outline-primary">
                            Back to Cars
                        </Link>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default CarDetails;
