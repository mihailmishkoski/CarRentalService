import React from "react";
import { Link } from "react-router-dom";

const Rent = (props) => {
    return (
        <div className="container my-5">
            <div className="d-flex justify-content-between align-items-center mb-4">
                <h2>Rents List</h2>

            </div>

            {props.rents.length > 0 ? (
                <table className="table table-striped">
                    <thead>
                    <tr>
                        <th>Car Name</th>
                        <th>Rent Date</th>
                        <th>Return Date</th>
                        <th>Rent Amount</th>
                        <th>Status</th>
                        <th></th>
                    </tr>
                    </thead>
                    <tbody>
                    {props.rents.map((term) => (
                        <tr key={term.id}>
                            <td className="text-info">{term.car.name}</td>
                            <td>{term.rentDate ? new Date(term.rentDate).toLocaleDateString() : 'N/A'}</td>
                            <td>{term.returnDate ? new Date(term.returnDate).toLocaleDateString() : 'N/A'}</td>
                            <td>{term.rentAmount ? `$${term.rentAmount}` : 'N/A'}</td>
                            <td>{term.isActive ? 'Active' : 'Completed'}</td>
                            <td>
                                <div className="d-flex">
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
                            </td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            ) : (
                <div className="col-12">
                    <p className="text-center text-muted">No rents available.</p>
                </div>
            )}
        </div>
    );
};

export default Rent;
