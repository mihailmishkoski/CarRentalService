import React from "react";
import { Link } from "react-router-dom";

const Car = (props) => {

    const handleSearchChange = (event) => {

        props.onSearchCar(event.target.value)

    };

    return (
        <div className="container my-5">
            <div className="row">
                <input
                    type="text"
                    onChange={handleSearchChange}
                    placeholder="Search cars..."
                />
            </div>
            <div className="d-flex justify-content-between align-items-center mb-4">

                <Link to="/cars/add" className="btn btn-success">
                    <i className="bi bi-plus-circle"></i> Add New Car
                </Link>
            </div>

            <div className="row">
                {props.cars.length > 0 ? (
                    props.cars.map((term) => (
                        <div key={term.id} className="col-md-4 mb-4">
                            <div className="card shadow-sm h-100">
                                <div className="card-body">
                                    <h5 className="card-title text-info">{term.name}</h5>
                                    <p className="card-text">
                                        <strong>Model:</strong> {term.model}
                                        <br/>
                                        <strong>Description:</strong> {term.description}
                                    </p>
                                    <p className="text-muted">
                                        <small>
                                            <strong>Manufactured on:</strong> {term.dateManufactured}
                                        </small>
                                    </p>
                                    <div className="d-flex justify-content-end align-items-center">
                                        <Link
                                            to={`/rents/add/${term.id}`}
                                            onClick={() => props.getCarById(term.id)}
                                            className="btn btn-outline-success btn-sm me-2"> {/* Changed to btn-outline-success for green */}
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
                                </div>
                            </div>
                        </div>
                    ))
                ) : (
                    <div className="col-12">
                        <p className="text-center text-muted">No cars available.</p>
                    </div>
                )}
            </div>
        </div>
    );
};

export default Car