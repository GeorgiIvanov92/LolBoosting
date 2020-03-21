import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'
import { Col, Form, InputGroup, Button, Nav } from 'react-bootstrap';
import { Formik, Field, Feedback } from 'formik';
import * as yup from 'yup';
import { OrderSpecific } from "./OrderSpecific";
import { Server } from "./Utilities/Enums";
import { ValidationState } from "./Utilities/Enums";
import { UserInfoForm } from "./UserInfoForm";
import { OrderType } from "./Utilities/Enums";

export class BoostMe extends Component {
    static displayName = BoostMe.name;

    constructor(props) {
        super(props);
        this.state = {
            orderType: OrderType.WinsBoost,
        }
    }

    render() {

        return (
            <div>
                <Nav justify variant="pills" defaultActiveKey="winsSoloBoost">
            <Nav.Item>
                        <Nav.Link onClick={() => this.setState({orderType: OrderType.WinsBoost })} eventKey="winsSoloBoost"> Wins Solo Boost</Nav.Link>
                </Nav.Item>
                <Nav.Item>
                        <Nav.Link onClick={() => this.setState({ orderType: OrderType.GamesBoost })}eventKey="gamesSoloBoost">Games Solo Boost</Nav.Link>
                </Nav.Item>
            </Nav>
            <UserInfoForm className='mt-5' orderType={this.state.orderType} />
</div >
        );
    }
}