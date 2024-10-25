import React from "react";
import {Link} from "react-router-dom";


const Return = (props) => {
    return (
        <div className="container my-5">
            <div className="d-flex justify-content-between align-items-center mb-4">
                <h2>Returns List</h2>
            </div>

            {props.returns.length > 0 ? (
                <table className="table table-striped">
                    <thead>
                    <tr>
                        <th>Return Date</th>
                        <th>Rent Amount</th>
                        <th>Late Fee</th>
                    </tr>
                    </thead>
                    <tbody>
                    {props.returns.map((term) => (
                        <tr key={term.id}>
                            <td>{term.returnDate ? new Date(term.returnDate).toLocaleDateString() : 'N/A'}</td>
                            <td>{term.totalPrice ? `$${term.totalPrice}` : 'N/A'}</td>
                            <td>{term.lateFee ? `$${term.lateFee}` : 'No'}</td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            ) : (
                <div className="col-12">
                    <p className="text-center text-muted">No returns available.</p>
                </div>
            )}
        </div>
    );
};

export default Return;
