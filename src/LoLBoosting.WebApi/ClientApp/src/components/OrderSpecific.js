import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'
import { Col, Form, InputGroup, Button } from 'react-bootstrap';
import { Formik, Field, Feedback } from 'formik';
import * as yup from 'yup';
import { SoloQueueWins } from "./OrderTypes/SoloQueueWins";

export class OrderSpecific extends Component {
    static displayName = OrderSpecific.name;

    constructor(props) {
        super(props);
        this.state = {
           
        }
        this.RenderOrderSpecifics = this.RenderOrderSpecifics.bind(this);
    }

    RenderOrderSpecifics(orderType) {
        if (orderType === "SoloQueueWins") {
            return <SoloQueueWins/>
        }
    }


    render() {

        return (
            this.RenderOrderSpecifics(this.props.orderType)
        );
    }
}