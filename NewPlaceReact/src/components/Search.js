import React, { Component } from 'react';

export default class Search extends Component {

    constructor() {
        super();
        this.msg = "Search";
        this.estateType = ["room", "flat", "house"];
    }

    render() {
        return (
            <div>
                <div id="searchArea">
                    <h1>{this.msg}</h1>
                    <form>
                        <fieldset>
                            <legend>
                                Search
                                <small className="text-muted float-right">Find your perfect place</small>
                            </legend>
                            <div className="container">
                                <div className='row'>
                                    <div className="col-sm">
                                        Locality: <input className="form-control" name="city" type="text" required minLength="3" placeholder="e.g. city name, country..." />
                                    </div>




                                    <div className="col-sm">
                                        Type of estate:
                                        <select className="form-control" name="estateType" required>
                                            {this.estateType.map((type) => <option key={type.toString()}>{type}</option>)}
                                        </select>
                                    </div>



                                    <div className="col-sm">
                                        Type of payment:
                                        <select className="form-control" name="paymentType">
                                            <option value="rent">Rent</option>
                                            <option value="forSale">For Sale</option>
                                            <option value="exchange">Exchange</option>
                                        </select>
                                    </div>
                                </div>
                            </div >
                            <br />
                            <button className="btn btn-primary float-right" type="submit">Search</button>
                        </fieldset >
                    </form >
                </div >
            </div >

        );
    }
}