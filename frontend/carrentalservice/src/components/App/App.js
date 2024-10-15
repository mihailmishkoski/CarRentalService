import {BrowserRouter as Router, Route, Routes} from 'react-router-dom';
import './App.css';
import {Component} from "react";
import CarRentalService from "../../repository/CarServiceRepository";
import Car from "../Car/CarList/cars";
import Rent from "../Rent/RentList/rents";
import Header from "../Header/header";
import CarAdd from "../Car/CarAdd/carAdd";
import CarEdit from "../Car/CarEdit/carEdit";
import CarDetails from "../Car/CarDetails/carDetails";
import RentAdd from "../Rent/RentAdd/rentAdd";
import Return from "../Return/ReturnList/returns";
import ReturnAdd from "../Return/ReturnAdd/returnAdd";

class App extends Component {
    constructor(props) {
        super(props);
        this.state = {
            cars: [],
            rents: [],
            returns: [],
            selectedCar: {},
            selectedRent: {}
        }
    }

    render() {
        return (
            <Router>
                <Header/>
                <main>
                    <div className={"container"}>
                        <br/>
                        <Routes>
                            <Route path={"/cars"} exact element={
                                <Car cars={this.state.cars}
                                     getCarById={this.getCar}
                                     onDelete={this.deleteCar}
                                     onRent={this.rentCar}
                                />}/>
                            <Route path={"/cars/add"} exact element={
                                <CarAdd onAddCar={this.addCar}/>}/>
                            <Route path={"/cars/edit/:id"} exact element={
                                <CarEdit onEditCar={this.editCar}
                                         car={this.state.selectedCar}/>}/>
                            <Route path={"/cars/:id"} exact element={
                                <CarDetails car={this.state.selectedCar}/>}/>
                            <Route path={"/rents"} exact element={
                                <Rent rents={this.state.rents}
                                      getRentById={this.getRent}/>}/>
                            <Route path={"/rents/add/:id"} exact element={
                                <RentAdd car={this.state.selectedCar}
                                      onAddRent={this.rentCar}/>}/>
                            <Route path={"/returns/"} exact element={
                                <Return returns={this.state.returns}/>}/>
                            <Route path={"/return/add/:id"} exact element={
                                <ReturnAdd rent={this.state.selectedRent}
                                        onAddReturn={this.returnCar}/>}/>
                        </Routes>
                    </div>
                </main>
            </Router>
        );
    }

    loadCars = () => {
        CarRentalService.fetchCars()
            .then((result) => {
                this.setState({
                    cars: result.data
                })
            });
    }
    loadRents = () => {
        CarRentalService.fetchRents()
            .then((result) => {
                this.setState({
                    rents: result.data
                })
            });
    }
    loadReturns = () => {
        CarRentalService.fetchReturns()
            .then((result) => {
                this.setState({
                    returns: result.data
                })
            });
    }
    addCar = (name, description, model, dateManufactured, kilometersTraveled, licensePlate, pricePerDay) => {
        return CarRentalService.addCar(name, description, model, dateManufactured, kilometersTraveled, licensePlate, pricePerDay)
        .then(() => {
            this.loadCars();
            this.setState({ carError: "" });
        });
    }
    getCar = (id) => {
        CarRentalService.getCar(id)
            .then((data) => {
                this.setState({
                    selectedCar: data.data
                })
            })
    }
    editCar = (id, name, description, model, dateManufactured, kilometersTraveled, licensePlate, pricePerDay) => {
        CarRentalService.editCar(id, name, description, model, dateManufactured, kilometersTraveled, licensePlate, pricePerDay)
            .then(() => {
                this.loadCars();
                this.setState({ carError: "" });
            });
    }
    deleteCar = (id) => {
        CarRentalService.deleteCar(id)
            .then(() => {
                this.loadCars();
                this.setState({ carError: "" });
            })
    }
    rentCar = (id, rentDate, returnDate) => {
        return CarRentalService.rentCar(id, rentDate, returnDate)
            .then(() => {
                    this.loadRents();
                }
            )
    }
    getRent = (id) => {
        CarRentalService.getRent(id)
            .then((data) => {
                this.setState({
                    selectedRent: data.data
                })
            })
    }
    returnCar = (rentId, returnDate) => {
        return CarRentalService.returnCar(rentId, returnDate)
            .then(() => {
                    this.loadReturns();
                    this.loadRents();
                }
            )
    }
    componentDidMount() {
        this.loadCars();
        this.loadRents();
        this.loadReturns();
    }
}

export default App;
