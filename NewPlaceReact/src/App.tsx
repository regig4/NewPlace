import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { Catalog } from './components/Catalog';
import { default as Details } from './components/Details';
    
import './custom.css'
import { BrowserRouter, Link, Routes } from 'react-router-dom';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                    

                
            </Layout>
        );
    }
}

