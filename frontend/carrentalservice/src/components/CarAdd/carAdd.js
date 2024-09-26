import React from 'react';


const CarAdd = (props) => {

    const [formData, updateFormData] = React.useState({
        name: "",
        description: "",
        model: "",
        dateManufactured: "",
        kilometersTraveled: 0,
        licensePlate: ""
    });

    const handleChange = (e) => {
        updateFormData({
            ...formData,
            [e.target.name]: e.target.value.trim()
        });
    };

    const onFormSubmit = (e) => {
        e.preventDefault();
        const { name, description, model, dateManufactured, kilometersTraveled, licensePlate } = formData;
        props.onAddCar(name, description, model, dateManufactured, kilometersTraveled, licensePlate);

    };

    return (
        <div className="row mt-5">
            <div className="col-md-5">
                <form onSubmit={onFormSubmit}>
                    <div className="form-group">
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
                    <div className="form-group">
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
                    <div className="form-group">
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
                    <div className="form-group">
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
                    <div className="form-group">
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
                    <div className="form-group">
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
                    </div>
                    <button type="submit" className="btn btn-primary">Add Car</button>
                </form>
            </div>
        </div>
    );
};

export default CarAdd;
