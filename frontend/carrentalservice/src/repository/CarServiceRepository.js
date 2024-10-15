import axios from '../custom-axios/axios';

const CarRentalService = {
    fetchCars: () => {
        return axios.get("/car/");
    },
    fetchRents: () => {
        return axios.get("/rent/GetAllRents");
    },
    fetchReturns: () => {
        return axios.get("/return/GetAllReturns");
    },
    addCar: (name, description, model, dateManufactured, kilometersTraveled, licensePlate, pricePerDay) => {
        return axios.post("/car/", {
            "name": name,
            "description": description,
            "model": model,
            "dateManufactured": dateManufactured,
            "kilometersTraveled" : kilometersTraveled,
            "licensePlate" : licensePlate,
            "pricePerDay" : pricePerDay
        });
    },
    editCar: (id, name, description, model, dateManufactured, kilometersTraveled, licensePlate, pricePerDay) => {
        return axios.put(`/car/${id}`, {
            "name": name,
            "description": description,
            "model": model,
            "dateManufactured": dateManufactured,
            "kilometersTraveled" : kilometersTraveled,
            "licensePlate" : licensePlate,
            "PricePerDay" : pricePerDay
        });
    },
    getCar:(id) => {
      return axios.get(`/car/${id}`);
    },
    deleteCar: (id) => {
        return axios.delete(`/car/${id}`);
    },
    rentCar: (id, rentDate, returnDate) => {
        return axios.post(`/rent/`, {
            "carId": id,
            "rentDate" : rentDate,
            "returnDate" : returnDate
        })
    },
    returnCar: (rentId, returnDate) => {
        return axios.post(`/return/`, {
            "rentId": rentId,
            "returnDate" : returnDate,
        })
    },
    getRent:(id) => {
        return axios.get(`/rent/${id}`);
    },
}
export default CarRentalService;