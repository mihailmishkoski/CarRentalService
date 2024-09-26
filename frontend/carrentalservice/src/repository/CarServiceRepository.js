import axios from '../custom-axios/axios';

const CarRentalService = {
    fetchCars: () => {
        return axios.get("/car/");
    },
    fetchRents: () => {
        return axios.get("/rent/GetAllRents");
    },
    addCar: (bookName, bookCategory, authorId, availableCopies, kilometersTraveled, licensePlate) => {
        return axios.post("/car/", {
            "name": bookName,
            "description": bookCategory,
            "model": authorId,
            "dateManufactured": availableCopies,
            "kilometersTraveled" : kilometersTraveled,
            "licensePlate" : licensePlate
        });
    },
}
export default CarRentalService;