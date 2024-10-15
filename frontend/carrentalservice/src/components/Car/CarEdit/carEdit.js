import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

const CarEdit = (props) => {

    const [formData, updateFormData] = React.useState({
        name: "",
        description: "",
        model: "",
        dateManufactured: "",
        kilometersTraveled: 0,
        licensePlate: ""
    });

    const navigate = useNavigate();

    useEffect(() => {
        if (props.car) {
            updateFormData({
                name: props.car.name || "",
                description: props.car.description || "",
                model: props.car.model || "",
                dateManufactured: props.car.dateManufactured || "",
                kilometersTraveled: props.car.kilometersTraveled || 0,
                licensePlate: props.car.licensePlate || "",
            });
        }
    }, [props.car]);

    const handleChange = (e) => {
        updateFormData({
            ...formData,
            [e.target.name]: e.target.value,
        });
    };

    const onFormSubmit = (e) => {
        e.preventDefault();
        const { name, description, model, dateManufactured, kilometersTraveled, licensePlate } = formData;
        props.onEditCar(props.car.id, name, description, model, dateManufactured, kilometersTraveled, licensePlate);
        navigate('/cars');
    }

    return (
        <div className="container my-5">
            <div className="row justify-content-center">
                <div className="col-md-6">
                    <h3 className="text-center mb-4">Editing</h3>
                    <form onSubmit={onFormSubmit} className="shadow-sm p-4 rounded bg-light">

                        <div className="mb-3">
                            <label htmlFor="name" className="form-label">Car Name</label>
                            <input
                                type="text"
                                className="form-control"
                                id="name"
                                name="name"
                                required
                                value={formData.name}
                                placeholder="Enter car name"
                                onChange={handleChange}
                            />
                        </div>

                        <div className="mb-3">
                            <label htmlFor="description" className="form-label">Description</label>
                            <input
                                type="text"
                                className="form-control"
                                id="description"
                                name="description"
                                required
                                value={formData.description}
                                placeholder="Enter car description"
                                onChange={handleChange}
                            />
                        </div>

                        <div className="mb-3">
                            <label htmlFor="model" className="form-label">Model</label>
                            <input
                                type="text"
                                className="form-control"
                                id="model"
                                name="model"
                                required
                                value={formData.model}
                                placeholder="Enter car model"
                                onChange={handleChange}
                            />
                        </div>

                        <div className="mb-3">
                            <label htmlFor="dateManufactured" className="form-label">Date Manufactured</label>
                            <input
                                type="date"
                                className="form-control"
                                id="dateManufactured"
                                name="dateManufactured"
                                required
                                value={formData.dateManufactured}
                                onChange={handleChange}
                            />
                        </div>

                        <div className="mb-3">
                            <label htmlFor="kilometersTraveled" className="form-label">Kilometers Traveled</label>
                            <input
                                type="number"
                                className="form-control"
                                id="kilometersTraveled"
                                name="kilometersTraveled"
                                required
                                value={formData.kilometersTraveled}
                                placeholder="Enter kilometers traveled"
                                onChange={handleChange}
                            />
                        </div>

                        <div className="mb-3">
                            <label htmlFor="licensePlate" className="form-label">License Plate</label>
                            <input
                                type="text"
                                className="form-control"
                                id="licensePlate"
                                name="licensePlate"
                                required
                                value={formData.licensePlate}
                                placeholder="Enter license plate"
                                onChange={handleChange}
                            />
                        </div>

                        <div className="d-grid">
                            <button type="submit" className="btn btn-primary">
                                <i className="bi bi-check-circle"></i> Update Car
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default CarEdit;
