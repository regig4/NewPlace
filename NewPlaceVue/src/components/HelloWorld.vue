<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationvue">https://aka.ms/jspsintegrationvue</a> for more details.
        </div>

            <div>
                <div id="searchArea">
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
                                            {this.estateType.map((type) =>
                                            <option key={type.toString()}>{type}</option>)}
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
                            </div>
                            <br />
                            <button className="btn btn-primary float-right" type="submit">Search</button>
                        </fieldset>
                    </form>
                </div>

            
            <div v-if="searchResults" id="searchResults">
                <div id="result" v-for="result in searchResults" :key="result.id">
                    <div>
                        <img className="img-thumbnail" :src="'data:' + result.thumbnail.mediaType + ';base64,' + result.thumbnail.resource" />
                    </div>
                    <div class="info">
                        <strong>{{result.resource.title}}</strong><br />
                        <ul className="list-inline">
                            <li className="list-inline-item">Valid to: {{result.resource.validityTime}}</li>
                            <li className="list-inline-item">User: {{result.resource.userName}}</li>
                            <li className="list-inline-item">Apartment: {{result.resource.apartment}}</li>
                            <li className="list-inline-item">Price: ${{result.resource.price}}</li>
                            <li className="list-inline-item">Provision: ${{result.resource.provision}}</li>
                            <li className="list-inline-item">Total cost: ${{result.resource.totalCost}}</li>
                            <li className="list-inline-item">Area: {{result.resource.estateArea}}</li>
                        </ul>
                    </div>
                    <br />
                    <a :href="'/details/' + result.resource.id" class="jumbotron row searchResult">
                        Details
                    </a>
                </div>
            </div>
            <div v-else>
                Loading...
            </div>

        </div>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue';
    import { Advertisement } from '../models/advertisement';

    interface Data {
        loading: boolean,
        searchResults: null | Advertisement[]
    }

    export default Vue.extend({
        data(): Data {
            return {
                loading: false,
                searchResults: null
            };
        },
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
            this.fetchData();
        },
        watch: {
            // call again the method if the route changes
            '$route': 'fetchData'
        },
        methods: {
            fetchData(): void {
                this.searchResults = null;
                this.loading = true;

                fetch("https://localhost:44347/api/Advertisement/search")
                    .then(r => r.json())
                    .then(json => {
                        this.searchResults = json as Advertisement[];
                        this.loading = false;
                        return;
                    });
            }
        },
    });
</script>

<style scoped>
    #result {
        display: inline-flex;
        flex-direction: row;
        justify-content: center;    
    }

    .info {
        display: block;
    }
</style>