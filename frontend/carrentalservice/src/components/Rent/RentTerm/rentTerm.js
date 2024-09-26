import React from 'react';
import { Link } from "react-router-dom";

const RentTerm = (props) => {
    const { id, rentDate, returnDate, rentAmount, car, isActive } = props.term;

    return (
        <tr>
            <td>{car?.name}</td>
            <td>{rentDate ? new Date(rentDate).toLocaleDateString() : 'N/A'}</td>
            <td>{returnDate ? new Date(returnDate).toLocaleDateString() : 'N/A'}</td>
            <td>{rentAmount ? `$${rentAmount}` : 'N/A'}</td>
            <td>{isActive ? 'Active' : 'Completed'}</td>
            <td className="text-right">
                <a className="btn btn-danger" onClick={() => props.onDelete(id)}>Delete</a>
                <Link className="btn btn-info ml-2"
                      onClick={() => props.onEditRent(id)}
                      to={`/rents/edit/${id}`}>
                    Edit
                </Link>

            </td>
        </tr>
    );
}

export default RentTerm;
