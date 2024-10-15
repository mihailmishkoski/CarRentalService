import React from "react";


const Return = (props) => {
    return (
        <div className="container my-5">
            <div className="d-flex justify-content-between align-items-center mb-4">
                <h2>Returns List</h2>
            </div>

            <div className="row">
                {props.returns.length > 0 ? (
                    props.returns.map((term) => (
                        <div key={term.id} className="col-md-4 mb-4">
                            <div className="card shadow-sm h-100">
                                <div className="card-body">
                                    <h5 className="card-title text-info">{term.name}</h5>
                                    <p className="card-text">
                                        <strong>Return Date:</strong> {term.returnDate ? new Date(term.returnDate).toLocaleDateString() : 'N/A'}
                                        <br />
                                        <strong>Rent Amount:</strong> {term.totalPrice ? `$${term.totalPrice}` : 'N/A'}
                                        <br />
                                        <strong>Late Fee:</strong> {term.lateFee ? `$${term.lateFee}` : 'No'}
                                    </p>
                                </div>
                            </div>
                        </div>
                    ))
                ) : (
                    <div className="col-12">
                        <p className="text-center text-muted">No returns available.</p>
                    </div>
                )}
            </div>
        </div>
    );
};

export default Return;
