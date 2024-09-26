import {BrowserRouter as Router, Route, Routes} from 'react-router-dom';
import './App.css';
import {Component} from "react";
import CarRentalService from "../../repository/CarServiceRepository";
import Car from "../Car/CarList/cars";
import Rent from "../Rent/RentList/rents";
import Header from "../Header/header";
import CarAdd from "../CarAdd/carAdd";
import axios from "../../custom-axios/axios";


class App extends Component {
    constructor(props) {
        super(props);
        this.state = {
            cars: [],
            rents: [],
            returns: []
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
                            <Route path={"/cars"} exact element={<Car cars={this.state.cars}/>}/>
                            <Route path={"/cars/add"} exact element={<CarAdd onAddCar={this.addCar}/>}/>

                            <Route path={"/rents"} exact element={<Rent rents={this.state.rents}/>}/>
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
    addCar = (bookName, bookCategory, authorId, availableCopies, kilometersTraveled, licensePlate) => {
        CarRentalService.addCar(bookName, bookCategory, authorId, availableCopies, kilometersTraveled, licensePlate)
        .then(() => {
            this.loadCars();

        });
    }


    componentDidMount() {
    this.loadCars();
    this.loadRents();
    }
}

export default App;
