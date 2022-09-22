<template>
    <div>
        <NavMenu />
    </div>
</template>

<script lang="ts">
    import Vue from 'vue';
    import NavMenu from './NavMenu.vue';
    import Home from './Home.vue';

    export default Vue.extend({
        name: "Layout",
        components: {
            NavMenu,
            Home
        }
    })

</script>