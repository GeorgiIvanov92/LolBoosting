import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'
import { Col, Form, InputGroup, Button } from 'react-bootstrap';
import { Formik, Field, Feedback } from 'formik';
import * as yup from 'yup';
import { WinsBoost } from "./OrderTypes/WinsBoost";
import { OrderType } from "./Utilities/Enums";

export class OrderSpecific extends Component {
    static displayName = OrderSpecific.name;

    constructor(props) {
        super(props);
        this.RenderOrderSpecifics = this.RenderOrderSpecifics.bind(this);
    }

    RenderOrderSpecifics(orderType) {
        if (orderType === OrderType.WinsBoost) {
            return <WinsBoost/>
        }

        return <div></div>
    }


    render() {

        return (
            this.RenderOrderSpecifics(this.props.orderType)
        );
    }
}