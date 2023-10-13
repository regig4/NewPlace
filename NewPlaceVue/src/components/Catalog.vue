<template>
    <div class="catalog-container">
        <div v-if="loading" class="loading">
            Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationvue">https://aka.ms/jspsintegrationvue</a> for more details.
        </div>

        <div>
            <div id="search-area">
                <form>
                    <fieldset>
                        <legend>
                            <small className="text-muted float-right">Find your perfect place</small>
                        </legend>
                        <div class="search-filter">
                            <div class='row'>
                                
                                <div class="col-sm">
                                    Locality: <input class="form-control" name="city" type="text" required minLength="3" placeholder="e.g. city, street..." />
                                </div>




                                <div class="col-sm">
                                    Type of estate:
                                    <select className="form-control" name="estateType" required>
                                        {this.estateType.map((type) =>
                                        <option key={type.toString()}>{type}</option>)}
                                    </select>
                                </div>



                                <div class="col-sm">
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
                        <button class="btn btn-primary float-right" type="submit">Search</button>
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
                            <li class="list-inline-item">Valid to: {{result.resource.validityTime}}</li>
                            <li class="list-inline-item">User: {{result.resource.userName}}</li>
                            <li class="list-inline-item">Apartment: {{result.resource.apartment}}</li>
                            <li class="list-inline-item">Price: ${{result.resource.price}}</li>
                            <li class="list-inline-item">Provision: ${{result.resource.provision}}</li>
                            <li class="list-inline-item">Total cost: ${{result.resource.totalCost}}</li>
                            <li class="list-inline-item">Area: {{Math.round(result.resource.estateArea)}}</li>
                            <router-link to='/details/1'>Details</router-link>
                        </ul>
                    </div>
                    <br />
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
    .catalog-container {
        padding: 0px 120px 0px 120px;
        color: burlywood;
        border-color: darkviolet;
    }

    #result {
        display: inline-flex;
        flex-direction: row;
        justify-content: center;
        border: solid 2px;
        border-radius: 10px;
        margin: 0.7em;
        padding: 0.5em;
        width: 30%;
        height: 250px;
    }

    #result img {
        height: 200px;
        width: 300px;
    }

    .info {
        display: block;
    }

    fieldset {
        border-color: burlywood;
        border-radius: 10px;
        display: flex;
        flex-direction: row;
        justify-content: space-around;
    }

    fieldset button {
        border-radius: 50px;
        background-color: sandybrown;
        font-weight:900;
    }

    .btn {
        display: inline-block;
        transition: all .2s ease-in-out;
        cursor: pointer;
        width: 15em;
    }

    .btn:hover {
        transform: scale(1.1);
    }

    #search-area {
        font-size: larger;
    }

    .search-filter {
        display:flex;
        flex-direction:row;
        justify-content:flex-start;
        align-items:flex-start;
    }

    input, select {
        height: 30px;
        display: flex;
        flex-direction: row;
        margin-left: 10px;
    }

    div .row {
        display: flex;
        flex-direction: row;
    }

    div .col-sm {
        margin-left: 33%;
        margin-right: 33%;
    }

    img-thumbnail {
        width: auto;
    }
</style>