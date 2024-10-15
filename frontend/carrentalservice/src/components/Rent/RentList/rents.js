import React from "react";
import { Link } from "react-router-dom";

const Rent = (props) => {
    return (
        <div className="container my-5">
            <div className="d-flex justify-content-between align-items-center mb-4">
                <h2>Rents List</h2>
            </div>

            <div className="row">
                {props.rents.length > 0 ? (
                    props.rents.map((term) => (
                        <div key={term.id} className="col-md-4 mb-4">
                            <div className="card shadow-sm h-100">
                                <div className="card-body">
                                    <h5 className="card-title text-info">{term.car.name}</h5>
                                    <p className="card-text">
                                        <strong>Rent Date:</strong> {term.rentDate ? new Date(term.rentDate).toLocaleDateString() : 'N/A'}
                                        <br />
                                        <strong>Return Date:</strong> {term.returnDate ? new Date(term.returnDate).toLocaleDateString() : 'N/A'}
                                        <br />
                                        <strong>Rent Amount:</strong> {term.rentAmount ? `$${term.rentAmount}` : 'N/A'}
                                    </p>
                                    <p className="text-muted">
                                        <small>
                                            <strong>Status:</strong> {term.isActive ? 'Active' : 'Completed'}
                                        </small>
                                    </p>
                                    <div className="d-flex justify-content-end align-items-center">
                                        {term.isActive ? (
                                            <Link
                                                to={`/return/add/${term.id}`}
                                                onClick={() => props.getRentById(term.id)}
                                                className="btn btn-outline-success btn-sm me-2">
                                                <i className="bi bi-pencil-square"></i> Return
                                            </Link>
                                        ) : (
                                            <button className="btn btn-outline-secondary btn-sm me-2" disabled>
                                                <i className="bi bi-pencil-square"></i> Return
                                            </button>
                                        )}
                                    </div>
                                </div>
                            </div>
                        </div>
                    ))
                ) : (
                    <div className="col-12">
                        <p className="text-center text-muted">No rents available.</p>
                    </div>
                )}
            </div>
        </div>
    );
};

export default Rent;
