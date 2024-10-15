import React from 'react';
import {Link} from "react-router-dom";

const CarTerm = (props) => {
    const { id, name, description, model, dateManufactured, kilometersTraveled, licensePlate, pricePerDay } = props;

    return (
        <tr>
            <td>{name}</td>
            <td>{description}</td>
            <td>{model}</td>
            <td>{dateManufactured}</td>
            <td>{kilometersTraveled}</td>
            <td>{licensePlate}</td>
            <td>{pricePerDay}</td>
            <td className="text-right">
                <Link className={"btn btn-info ml-3"}
                      onClick={() => props.getCarById(id)}
                      to={`/cars/${props.term.id}`}>
                    Details
                </Link>
                <Link className={"btn btn-success"}
                      onClick={() => props.getCarById(id)}
                      to={`/cars/edit/${props.term.id}`}>
                    Edit
                </Link>
                <a className="btn btn-danger" onClick={() => props.onDelete(id)}>Delete</a>


            </td>
        </tr>
    );
}

export default CarTerm;
