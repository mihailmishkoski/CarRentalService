import React from 'react';
import { useNavigate, Link } from 'react-router-dom';

const CarAdd = (props) => {
    const [formData, updateFormData] = React.useState({
        name: "",
        description: "",
        model: "",
        dateManufactured: "",
        kilometersTraveled: 0,
        licensePlate: "",
        pricePerDay: 0
    });
    const [errorMessage, setErrorMessage] = React.useState(null); // Change to a function-based state
    const navigate = useNavigate();

    const handleChange = (e) => {
        updateFormData({
            ...formData,
            [e.target.name]: e.target.value.trim(),
        });
    };

    const onFormSubmit = (e) => {
        e.preventDefault();

        const { name, description, model, dateManufactured, kilometersTraveled, licensePlate, pricePerDay } = formData;

        setErrorMessage(null);


        if (!name || !description || !model || !dateManufactured || !kilometersTraveled || !licensePlate) {
            setErrorMessage("All fields are required"); // Update the error message
            return;
        }

        props.onAddCar(name, description, model, dateManufactured, kilometersTraveled, licensePlate, pricePerDay)
            .then(() => {
                navigate('/cars');
            })
            .catch(error => {
                setErrorMessage(error.response.data.message);
            });
    }

    return (
        <div className="container my-5">
            <div className="row justify-content-center">
                <div className="col-md-6">
                    <div className="card shadow-sm p-4">
                        <h4 className="text-center mb-4">Add New Car</h4>
                        <form onSubmit={onFormSubmit}>
                            <div className="form-group mb-3">
                                <label htmlFor="name">Car Name</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    id="name"
                                    name="name"
                                    required
                                    placeholder="Enter car name"
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="form-group mb-3">
                                <label htmlFor="description">Description</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    id="description"
                                    name="description"
                                    required
                                    placeholder="Enter car description"
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="form-group mb-3">
                                <label htmlFor="model">Model</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    id="model"
                                    name="model"
                                    required
                                    placeholder="Enter car model"
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="form-group mb-3">
                                <label htmlFor="dateManufactured">Date Manufactured</label>
                                <input
                                    type="date"
                                    className="form-control"
                                    id="dateManufactured"
                                    name="dateManufactured"
                                    required
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="form-group mb-3">
                                <label htmlFor="kilometersTraveled">Kilometers Traveled</label>
                                <input
                                    type="number"
                                    className="form-control"
                                    id="kilometersTraveled"
                                    name="kilometersTraveled"
                                    required
                                    placeholder="Enter kilometers traveled"
                                    onChange={handleChange}
                                />
                            </div>
                            <div className="form-group mb-4">
                                <label htmlFor="licensePlate">License Plate</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    id="licensePlate"
                                    name="licensePlate"
                                    required
                                    placeholder="Enter license plate"
                                    onChange={handleChange}
                                />
                                {errorMessage &&
                                    <div className="alert alert-danger  mt-2" role="alert">{errorMessage}</div>}
                            </div>
                            <div className="form-group mb-3">
                                <label htmlFor="pricePerDay">Price for one day</label>
                                <input
                                    type="number"
                                    className="form-control"
                                    id="pricePerDay"
                                    name="pricePerDay"
                                    required
                                    placeholder="Enter price per day"
                                    onChange={handleChange}
                                />
                            </div>
                            <button type="submit" className="btn btn-primary w-100">Add Car</button>
                        </form>
                    </div>
                    <div className="text-center mt-4">
                        <Link to="/cars" className="btn btn-outline-secondary">
                            Back to Cars
                        </Link>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default CarAdd;
