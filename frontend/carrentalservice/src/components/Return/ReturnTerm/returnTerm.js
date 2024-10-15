import React from 'react';


const ReturnTerm = (props) => {
    const { id, RentId, ReturnDate, LateFee, TotalPrice } = props

    return (
        <tr>
            <td>{ReturnDate ? new Date(ReturnDate).toLocaleDateString() : 'N/A'}</td>
            <td>{LateFee ? `$${LateFee}` : 'N/A'}</td>
            <td>{TotalPrice ? `$${TotalPrice}` : 'N/A'}</td>
            <td className="text-right">
            </td>
        </tr>
    );
}

export default ReturnTerm;
