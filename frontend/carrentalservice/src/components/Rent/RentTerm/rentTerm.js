import React from 'react';


const RentTerm = (props) => {
    const { id, rentDate, returnDate, rentAmount, car, isActive } = props

    return (
        <tr>
            <td>{car?.name}</td>
            <td>{rentDate ? new Date(rentDate).toLocaleDateString() : 'N/A'}</td>
            <td>{returnDate ? new Date(returnDate).toLocaleDateString() : 'N/A'}</td>
            <td>{rentAmount ? `$${rentAmount}` : 'N/A'}</td>
            <td>{isActive ? 'Active' : 'Completed'}</td>
            <td className="text-right">
            </td>
        </tr>
    );
}

export default RentTerm;
