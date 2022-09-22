<template>
    <div>
        <header>
            <a href="#/">Home</a> | 
            <a href="#/catalog">Catalog</a>
        </header>
        <component :is="currentView"></component>
        <router-view></router-view>
    </div>
</template>


<script lang="ts">
    import Vue from 'vue';
    import Home from './Home.vue';
    import Catalog from './Catalog.vue';

    const routes: any = {
        '/': Home,
        '/catalog': Catalog
    }

    export default Vue.extend({
        name: 'NavMenu',
        data() {
            return {
                currentPath: window.location.hash as string
            }
        },
        computed: {
            currentView() {
                return routes[this.currentPath.slice(1)];
            }
        },
        mounted() {
            window.addEventListener('hashchange', () => {
                this.currentPath = window.location.hash
            })
        }
    })

</script>

<style scoped>
    a {
        display: inline-block;
        border: 1px solid azure;
        border-radius: 3px;
        padding: 5px;
        margin: 10px;
        background-color: antiquewhite;
        text-decoration: none;
        transition: all 0.2s ease-in-out;
    }

    a:hover {
        transform: scale(1.2);
    }
</style>