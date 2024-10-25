import React, { useState } from "react";
import { Link } from "react-router-dom";
import CarServiceRepository from "../../../repository/CarServiceRepository";

const Car = (props) => {
    const [startDate, setStartDate] = useState("");
    const [endDate, setEndDate] = useState("");

    const handleSearchChange = (event) => {
        props.onSearchCar(event.target.value);
    };

    const filterKilometers = (event) => {
        let sortedCars;
        const sortOption = parseInt(event.target.value);
        if (sortOption === 0) { // from lower to higher
            sortedCars = [...props.cars].sort((a, b) => a.kilometersTraveled - b.kilometersTraveled);
            props.setCars(sortedCars);
        } else if (sortOption === 1) { // from higher to lower
            sortedCars = [...props.cars].sort((a, b) => b.kilometersTraveled - a.kilometersTraveled);
            props.setCars(sortedCars);
        }
    };

    const filterByDate = (event) => {
        event.preventDefault();
        const filteredCars = props.cars.filter(car => {
            const dateManufactured = new Date(car.dateManufactured);
            const start = new Date(startDate);
            const end = new Date(endDate);
            return dateManufactured >= start && dateManufactured <= end;
        });
        props.setCars(filteredCars);
    };

    const clearFilters = () => {
        props.loadCars();
        setStartDate("");
        setEndDate("");
    };

    return (
        <div className="container-fluid">
            <div className="row">
                <div className="col-md-3">
                    <div className="p-3 border bg-light">
                        <h5>Filter Options</h5>

                        {/* Kilometers Filter */}
                        <div className="my-4">
                            <label htmlFor="kilometerFilter" className="form-label">
                                Kilometers Traveled:
                            </label>
                            <select
                                id="kilometerFilter"
                                onChange={filterKilometers}
                                className="form-select form-select-sm"
                            >
                                <option value="">Select...</option>
                                <option value="0">Lowest First</option>
                                <option value="1">Highest First</option>
                            </select>
                        </div>

                        {/* Manufactured Date Filter */}
                        <div className="my-4">
                            <label>Manufactured On:</label>
                            <form onSubmit={filterByDate} className="border p-3 bg-light rounded">
                                <div className="mb-3">
                                    <label htmlFor="startDate" className="form-label">From Date:</label>
                                    <input
                                        type="date"
                                        id="startDate"
                                        className="form-control"
                                        value={startDate}
                                        onChange={(e) => setStartDate(e.target.value)}
                                    />
                                </div>
                                <div className="mb-3">
                                    <label htmlFor="endDate" className="form-label">To Date:</label>
                                    <input
                                        type="date"
                                        id="endDate"
                                        className="form-control"
                                        value={endDate}
                                        onChange={(e) => setEndDate(e.target.value)}
                                    />
                                </div>
                                <button type="submit" className="btn btn-primary">Filter</button>
                            </form>
                        </div>

                        <button className="btn btn-outline-success" onClick={clearFilters}>Clear Filters</button>
                    </div>
                </div>

                <div className="col-md-9">
                    {/* Main Content Area */}
                    <div className="mb-3">
                        <input
                            type="text"
                            onChange={handleSearchChange}
                            placeholder="Search cars..."
                            className="form-control"
                        />
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
                                <th>Kilometers Traveled</th>
                                <th></th>
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
            </div>
        </div>
    );
};

export default Car;
