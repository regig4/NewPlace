<template>
    <div>
        <div v-if="this.state.advertisement">
            <div className="container">
                <img src={{"data:" + this.state.advertisement.thumbnail.mediaType + ";base64," + this.state.advertisement.thumbnail.resource}} alt="Not found" />

                <div className="card">
                    <div className="jumbotron float-right"><h1>{{this.state.advertisement.resource.title}}</h1></div>
                    <br /><strong>Type of estate: </strong> {{this.state.advertisement.resource.apartmentType}}
                    <br /><strong>Date of creating ad: </strong>{{this.state.advertisement.resource.createDate}}
                    <br /><strong>Available to: </strong>{{this.state.advertisement.resource.validTo}}
                    <br /><strong>User: </strong>{{this.state.advertisement.resource.userName}}
                    <br /><strong>Address: </strong>{{this.state.advertisement.resource.apartmentAddress}}
                    <br /><strong>Price: </strong>{{this.state.advertisement.resource.price}}
                    <br /><strong>Provision: </strong>{{this.state.advertisement.resource.provision}}
                    <br /><strong>Total cost: </strong>{{this.state.advertisement.resource.totalCost}}
                    <br /><strong>Utilities cost: </strong>{{this.state.advertisement.resource.utilitesCost}}
                    <br /><strong>Description: </strong>
                    <p>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
                    </p>
                </div>
            </div>
        </div>
        <div v-else>
            <p>Loading...</p>
        </div>
    </div>
</template>

<script lang="ts">

    import Vue from 'vue';

    export default Vue.extend({
        name: 'Details'
    })

</script>