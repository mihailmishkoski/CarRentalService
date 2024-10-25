import {BrowserRouter as Router, Navigate, Route, Routes} from 'react-router-dom';
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
import Register from "../Register/register";
import Login from "../Login/login"
import Cookies from "js-cookie";


class App extends Component {
    constructor(props) {
        super(props);
        this.state = {
            cars: [],
            rents: [],
            returns: [],
            selectedCar: {},
            selectedRent: {},
            isAuthenticated: false
        }

    }

    render() {
        return (
            <Router>
                <Header clearState={this.clearState} />
                <main>
                    <div className={"container"}>
                        <br />
                        <Routes>
                            <Route path="/" element={
                                this.state.isAuthenticated ? (
                                    <Navigate to="/cars" />
                                ) : (
                                    <Login onLogin={this.logIn}
                                           loadInitialData={this.loadInitialData}
                                           Authenticate={this.Authenticate} />
                                )
                            } />

                            <Route path="/cars" exact element={
                                this.state.isAuthenticated ? (
                                    <Car cars={this.state.cars}
                                         getCarById={this.getCar}
                                         onDelete={this.deleteCar}
                                         onRent={this.rentCar}
                                         onSearchCar={this.searchCar}
                                         loadCars={this.loadCars}
                                         setCars={this.setCars}/>
                                ) : (
                                    <Navigate to="/login" />
                                )
                            } />

                            <Route path="/cars/add" exact element={
                                this.state.isAuthenticated ? (
                                    <CarAdd onAddCar={this.addCar} />
                                ) : (
                                    <Navigate to="/login" />
                                )
                            } />

                            <Route path="/cars/edit/:id" exact element={
                                this.state.isAuthenticated ? (
                                    <CarEdit onEditCar={this.editCar}
                                             car={this.state.selectedCar} />
                                ) : (
                                    <Navigate to="/login" />
                                )
                            } />

                            <Route path="/cars/:id" exact element={
                                this.state.isAuthenticated ? (
                                    <CarDetails car={this.state.selectedCar} />
                                ) : (
                                    <Navigate to="/login" />
                                )
                            } />

                            <Route path="/rents" exact element={
                                this.state.isAuthenticated ? (
                                    <Rent rents={this.state.rents}
                                          getRentById={this.getRent} />
                                ) : (
                                    <Navigate to="/login" />
                                )
                            } />

                            <Route path="/rents/add/:id" exact element={
                                this.state.isAuthenticated ? (
                                    <RentAdd car={this.state.selectedCar}
                                             onAddRent={this.rentCar} />
                                ) : (
                                    <Navigate to="/login" />
                                )
                            } />

                            <Route path="/returns" exact element={
                                this.state.isAuthenticated ? (
                                    <Return returns={this.state.returns} />
                                ) : (
                                    <Navigate to="/login" />
                                )
                            } />

                            <Route path="/return/add/:id" exact element={
                                this.state.isAuthenticated ? (
                                    <ReturnAdd rent={this.state.selectedRent}
                                               onAddReturn={this.returnCar} />
                                ) : (
                                    <Navigate to="/login" />
                                )
                            } />

                            <Route path="/register" exact element={
                                !this.state.isAuthenticated ? (
                                    <Register onRegister={this.register} />
                                ) : (
                                    <Navigate to="/cars" />
                                )
                            } />

                            <Route path="/login" exact element={
                                !this.state.isAuthenticated ? (
                                    <Login onLogin={this.logIn}
                                           loadInitialData={this.loadInitialData}
                                           Authenticate={this.Authenticate} />
                                ) : (
                                    <Navigate to="/cars" />
                                )
                            } />
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
        return CarRentalService.getRent(id)
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
    register = (FirstName, LastName, Email, DateOfBirth, Password) => {
        return CarRentalService.register(FirstName, LastName, Email, DateOfBirth, Password)
    }
    logIn = (email, password) => {
        return CarRentalService.logIn(email, password);
    }
    componentDidMount() {
        const token = Cookies.get('jwtToken');
        if (token) {
            this.loadInitialData()
            this.Authenticate();
        }
    }
    clearState = () => {
        this.setState({
            cars: [],
            rents: [],
            returns: [],
            selectedCar: {},
            selectedRent: {},
            isAuthenticated: false,
        });
    }
    Authenticate = () => {
        this.setState({
            isAuthenticated:  true,
        });
    }
    searchCar = (name) => {
        if(name.isEmpty || name === "")
        {
            this.loadCars();
        }
        else
        {
             CarRentalService.getCarByName(name)
                .then((data) => {
                    this.setState({
                        cars: data.data
                    })
                });
        }
    }
    setCars = (cars) => {
        this.setState({
            cars: cars
        })
    }
    loadInitialData = () => {
        this.loadCars();
        this.loadRents();
        this.loadReturns();
    }

}

export default App;