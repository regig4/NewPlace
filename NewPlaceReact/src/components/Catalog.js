import React, { Component } from 'react';
import Search from './Search.js';
import { Link, Outlet } from "react-router-dom";


export class Catalog extends Component {

    constructor() {
        super();

        this.state = { advertisements: undefined };
    }

    componentDidMount() {
        this.fetchAdvertisements();
    }

    async fetchAdvertisements() {
        const response = await fetch("https://localhost:44347/api/Advertisement/search");
        const data = await response.json();
        console.log(data);
        console.log(document.location.pathname);
        this.setState({ advertisements: data});
    }


    render() {
        return (
            <div id="catalog">
                <Search />

                {
                    !(this.state.advertisements) ? <p>Loading</p> : 
                    <div id="searchResults">
                        {this.state.advertisements.map((result) => 
                            <div>
                                <Link to={"/details/" + result.resource.id} className="jumbotron row searchResult"> 
                                <div className="col-3">
                                    <img className="img-thumbnail" src={"data:" + result.thumbnail.mediaType + ";base64," + result.thumbnail.resource} />
                                </div>
                                <div className="col">
                                    <strong>{result.resource.title}</strong><br />
                                    <ul className="list-inline">
                                        <li className="list-inline-item">Valid to: {result.resource.validityTime}</li>
                                        <li className="list-inline-item">User: {result.resource.userName}</li>
                                        <li className="list-inline-item">Apartment: {result.resource.apartment}</li>
                                        <li className="list-inline-item">Price: ${result.resource.price}</li>
                                        <li className="list-inline-item">Provision: ${result.resource.provision}</li>
                                        <li className="list-inline-item">Total cost: ${result.resource.totalCost}</li>
                                        <li className="list-inline-item">Area: {result.resource.estateArea}</li>
                                    </ul>
                                </div>
                                </Link>
                            </div>
                        )}
                    </div>
                }
                <Outlet />
            </div>
        );
    }
}