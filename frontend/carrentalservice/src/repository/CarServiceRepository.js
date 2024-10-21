import axios from '../custom-axios/axios';



const CarRentalService = {
    fetchCars: () => {
        return axios.get("/car/", { withCredentials: true });
    },
    fetchRents: () => {
        return axios.get("/rent/GetAllRents", { withCredentials: true });
    },
    fetchReturns: () => {
        return axios.get("/return/GetAllReturns", { withCredentials: true });
    },
    addCar: (name, description, model, dateManufactured, kilometersTraveled, licensePlate, pricePerDay) => {
        return axios.post("/car/", {
            "name": name,
            "description": description,
            "model": model,
            "dateManufactured": dateManufactured,
            "kilometersTraveled": kilometersTraveled,
            "licensePlate": licensePlate,
            "pricePerDay": pricePerDay
        }, { withCredentials: true });
    },
    editCar: (id, name, description, model, dateManufactured, kilometersTraveled, licensePlate, pricePerDay) => {
        return axios.put(`/car/${id}`, {
            "name": name,
            "description": description,
            "model": model,
            "dateManufactured": dateManufactured,
            "kilometersTraveled": kilometersTraveled,
            "licensePlate": licensePlate,
            "PricePerDay": pricePerDay
        }, { withCredentials: true });
    },
    getCar: (id) => {
        return axios.get(`/car/${id}`, { withCredentials: true });
    },
    deleteCar: (id) => {
        return axios.delete(`/car/${id}`, { withCredentials: true });
    },
    rentCar: (id, rentDate, returnDate) => {
        return axios.post(`/rent/`, {
            "carId": id,
            "rentDate": rentDate,
            "returnDate": returnDate
        }, { withCredentials: true });
    },
    returnCar: (rentId, returnDate) => {
        return axios.post(`/return/`, {
            "rentId": rentId,
            "returnDate": returnDate,
        }, { withCredentials: true });
    },
    getRent: (id) => {
        return axios.get(`/rent/${id}`, { withCredentials: true });
    },
    register: (FirstName, LastName, Email, DateOfBirth, Password) => {
        return axios.post(`/account/register`, {
            "Email": Email,
            "FirstName": FirstName,
            "LastName": LastName,
            "DateOfBirth": DateOfBirth,
            "Password": Password
        });
    },
    logIn: (email, password) => {
        return axios.post(`/account/login`, {
            "email": email,
            "password": password
        });
    }
}

export default CarRentalService;
