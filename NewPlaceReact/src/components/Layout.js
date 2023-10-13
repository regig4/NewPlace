import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import { Outlet, Link } from "react-router-dom";

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
            <NavMenu />
            <Outlet />
        {/*<Container>*/}
        {/*  {this.props.children}*/}
        {/*</Container>*/}
      </div>
    );
  }
}
