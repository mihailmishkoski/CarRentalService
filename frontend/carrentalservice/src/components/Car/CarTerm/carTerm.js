import React from 'react';
import {Link} from "react-router-dom";

const CarTerm = (props) => {
    const { id, name, description, model, dateManufactured, kilometersTraveled, licensePlate } = props.term;

    return (
        <tr>
            <td>{name}</td>
            <td>{description}</td>
            <td>{model}</td>
            <td>{dateManufactured}</td>
            <td>{kilometersTraveled}</td>
            <td>{licensePlate}</td>
            <td className="text-right">
                <Link className={"btn btn-info ml-2"}
                      onClick={() => props.onEditCar(id)}
                      to={`/car/edit/${props.term.id}`}>
                    Edit
                </Link>


            </td>
        </tr>
    );
}

export default CarTerm;
