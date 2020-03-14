import React, { Component } from 'react';
import { Col, Form, InputGroup, Button } from 'react-bootstrap';
import { Formik, Field, Feedback } from 'formik';
import * as yup from 'yup';

export class SoloQueueWins extends Component {
    static displayName = SoloQueueWins.name;

    constructor(props) {
        super(props);
        this.state = {

        }
        this.RenderSoloQueueWinsOrder = this.RenderSoloQueueWinsOrder.bind(this);
    }

    RenderSoloQueueWinsOrder() {
        return (
            <Form>
                <Form.Group controlId="formBasicRangeCustom">
                    <Form.Label>Range</Form.Label>
                    <Form.Control type="range" custom />
                </Form.Group>
            </Form>
        );
    }

    render() {

        return (
            this.RenderSoloQueueWinsOrder()
        );
    }
}