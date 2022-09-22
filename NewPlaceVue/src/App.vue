<template>
  <div id="app">
      <Layout />
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import Layout from './components/Layout.vue';


export default Vue.extend({
  name: 'App',
  components: {
    Layout
  }
});
</script>

<style>
    #app {
      font-family: Avenir, Helvetica, Arial, sans-serif;
      -webkit-font-smoothing: antialiased;
      -moz-osx-font-smoothing: grayscale;
      text-align: center;
      color: #2c3e50;
      margin-top: 60px;
    }

    body {
        /*background: linear-gradient(black, burlywood);*/
        background-color: black;
    }
</style>
