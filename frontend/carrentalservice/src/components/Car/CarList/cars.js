import React, {useState} from "react";
import CarTerm from "../CarTerm/carTerm";


const Car = (props) => {

    return (
        <div className={"container mm-4 mt-5"}>
            <div className={"row"}>
                <div className={"row"}>
                    <table className={"table table-striped"}>
                        <thead>
                        <tr>
                            <th scope={"col"}>Name</th>
                        </tr>
                        </thead>
                        <tbody>
                        {props.cars.map((term) => (
                            <CarTerm
                                key={term.id}
                                term={term}
                            />
                        ))}
                        </tbody>
                    </table>
                </div>
                <div className="col mb-3">
                    <div className="col-sm-12 col-md-12">
                        <div className="col-sm-12 col-md-12">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};
export default Car;