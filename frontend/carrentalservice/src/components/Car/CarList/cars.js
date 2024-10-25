import React from "react";
import { Link } from "react-router-dom";
import CarServiceRepository from "../../../repository/CarServiceRepository";

const Car = (props) => {

    const handleSearchChange = (event) => {
        props.onSearchCar(event.target.value);
    };

    const filterKilometers = (event) =>
    {
        let sortedCars;
        const sortOption = parseInt(event.target.value);
        if(sortOption === 0) //from lower to higher
        {
            sortedCars = [...props.cars].sort((a, b) => a.kilometersTraveled - b.kilometersTraveled);
            props.setCars(sortedCars)
        }
        else if(sortOption === 1)//from higher to lower
        {
            sortedCars = [...props.cars].sort((a, b) => b.kilometersTraveled - a.kilometersTraveled);
            props.setCars(sortedCars)
        }
    }
    return (
        <div className="container my-5">
            <div className="row mb-3">
                <input
                    type="text"
                    onChange={handleSearchChange}
                    placeholder="Search cars..."
                    className="form-control"
                />
            </div>
            <div className="row mb-3">
                <label htmlFor="kilometerFilter" className="form-label">
                    Kilometers Traveled:
                </label>
                <select
                    id="kilometerFilter"
                    onChange={filterKilometers}
                    className="form-select"
                >
                    <option value="">Select...</option>
                    <option value="0">Lowest First</option>
                    <option value="1">Highest First</option>
                </select>
            </div>
            <div className="d-flex justify-content-between align-items-center mb-4">
                <Link to="/cars/add" className="btn btn-success">
                    <i className="bi bi-plus-circle"></i> Add New Car
                </Link>
            </div>

            {props.cars.length > 0 ? (
                <table className="table table-striped">
                    <thead>
                    <tr>
                        <th>Name</th>
                        <th>Model</th>
                        <th>Description</th>
                        <th>Manufactured On</th>
                        <th>Kilometers traveled</th>
                        <th>Actions</th>
                    </tr>
                    </thead>
                    <tbody>
                    {props.cars.map((term) => (
                        <tr key={term.id}>
                            <td className="text-info">{term.name}</td>
                            <td>{term.model}</td>
                            <td>{term.description}</td>
                            <td>{term.dateManufactured}</td>
                            <td>{term.kilometersTraveled} km</td>
                            <td>
                                <div className="d-flex">
                                    <Link
                                        to={`/rents/add/${term.id}`}
                                        onClick={() => props.getCarById(term.id)}
                                        className="btn btn-outline-success btn-sm me-2">
                                        <i className="bi bi-pencil-square"></i> Rent
                                    </Link>
                                    <Link
                                        to={`/cars/${term.id}`}
                                        onClick={() => props.getCarById(term.id)}
                                        className="btn btn-outline-primary btn-sm me-2">
                                        <i className="bi bi-info-circle"></i> Details
                                    </Link>
                                    <Link
                                        to={`/cars/edit/${term.id}`}
                                        onClick={() => props.getCarById(term.id)}
                                        className="btn btn-outline-warning btn-sm me-2">
                                        <i className="bi bi-pencil-square"></i> Edit
                                    </Link>
                                    <button
                                        className="btn btn-danger btn-sm"
                                        onClick={() => props.onDelete(term.id)}>
                                        <i className="bi bi-trash"></i> Delete
                                    </button>
                                </div>
                            </td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            ) : (
                <div className="col-12">
                    <p className="text-center text-muted">No cars available.</p>
                </div>
            )}
        </div>
    );
};

export default Car;
