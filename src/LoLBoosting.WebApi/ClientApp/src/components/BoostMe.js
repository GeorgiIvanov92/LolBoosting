import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'
import { Col, Form, InputGroup, Button } from 'react-bootstrap';
import { Formik, Field, Feedback } from 'formik';
import * as yup from 'yup';
import { OrderSpecific } from "./OrderSpecific";

export class BoostMe extends Component {
    static displayName = BoostMe.name;

    constructor(props) {
        super(props);
        this.state = {
            orderType: '',
            nickName: '',
            RiotApiGetUserUrl: 'order/ValidateUsername?username=',
            schema: yup.object({
                username: yup.string().min(2, 'Too Short!').max(30, 'Too Long!').required('Required!'),
                password: yup.string().min(2, 'Too Short!').max(30, 'Too Long!').required('Required!'),
                nickname: yup.string().min(2, 'Too Short!').max(30, 'Too Long!').required('Required!'),
                server: yup.string().required('Server is required!'),
                orderType: yup.string().required('Order Type is required!'),
            })
        }

        this.FormExample = this.FormExample.bind(this);
        this.ValidateUser = this.ValidateUser.bind(this);
        this.RenderOrderSpecifics = this.RenderOrderSpecifics.bind(this);
    }

    RenderOrderSpecifics(orderType) {
        return <OrderSpecific orderType={orderType}/>
    }

    FormExample() {
    return (
        <Formik
            validationSchema={this.state.schema}
            onSubmit={console.log}
            isInvalid={<div>Required!</div>}
            initialValues={{
                //firstName: 'Mark',
                //lastName: 'Otto',
            }}
        >
            {({
                handleSubmit,
                handleChange,
                handleBlur,
                values,
                touched,
                isValid,
                errors,

            }) => (
                    <Form noValidate onSubmit={handleSubmit}>
                        <Form.Row>
                            <Form.Group as={Col} md="4" controlId="validationFormik01">
                                <Form.Label>Riot Account Username</Form.Label>
                                <Form.Control
                                    required
                                    type="text"
                                    name="username"
                                    value={values.firstName}
                                    onChange={handleChange}
                                    isValid={touched.username && !errors.username}
                                    isInvalid={errors.username}
                                />
                                <Form.Control.Feedback>Looks good!</Form.Control.Feedback>
                                <Form.Control.Feedback type="invalid">
                                {errors.username}
                                </Form.Control.Feedback>
                            </Form.Group>
                            <Form.Group as={Col} md="4" controlId="validationFormik02">
                                <Form.Label>Riot Account Password</Form.Label>
                                <Form.Control
                                    type="password"
                                    name="password"
                                    onChange={handleChange}
                                    isValid={touched.password && !errors.password}
                                    isInvalid={!!errors.password}
                                />

                                <Form.Control.Feedback>Looks good!</Form.Control.Feedback>
                                <Form.Control.Feedback type="invalid">
                                    {errors.password}
                                </Form.Control.Feedback>
                            </Form.Group>

                        </Form.Row>
                        <Form.Row>
                            <Form.Group as={Col} md="4" controlId="validationFormik03">
                                <Form.Label>InGame Username</Form.Label>
                                <Form.Control
                                    type="text"
                                    name="nickname"
                                    onChange={nick => this.setState({ nickName: nick.target.value })}
                                    isValid={touched.nickname && !errors.nickname}
                                    isInvalid={!!errors.nickname}
                                />

                                <Form.Control.Feedback>Looks good!</Form.Control.Feedback>
                                <Form.Control.Feedback type="invalid">
                                    {errors.nickname}
                                </Form.Control.Feedback>
                            </Form.Group>

                            <Form.Group as={Col} md="4" controlId="validationFormik04">
                                <Form.Label>Account Server</Form.Label>
                                <select
                                    name="server"
                                    onChange={handleChange}
                                    onBlur={handleBlur}
                                    style={{ display: 'block' }}
                                >
                                    <option value="" label="Select a server" />
                                    <option value="EUW" label="EUW" />
                                    <option value="EUNE" label="EUNE" />
                                    <option value="NA" label="NA" />
                                </select>
                                {errors.server &&
                                    <div style={{color:'red'}}className="input-feedback">
                                    {errors.server}
                                    </div>}

                                <Form.Control.Feedback>Looks good!</Form.Control.Feedback>
                            </Form.Group>

                            <Button onClick={this.ValidateUser}type="submit">Check my Account</Button>

                        </Form.Row>
                        <Form.Row>
                            <Form.Group as={Col} md="4" controlId="validationFormik05">
                                <Form.Label>Order Type</Form.Label>
                                <select
                                    name="orderType"
                                    onBlur={handleBlur}
                                    style={{ display: 'block' }}
                                    onChange=
                                    {
                                        type => this.setState({ orderType: type.target.value })
                                    }
                                >
                                    <option value="" label="Select an Order Type" />
                                    <option value="SoloQueueWins" label="Solo Queue Wins" />
                                </select>
                                {errors.orderType &&
                                    <div style={{ color: 'red' }} className="input-feedback">
                                        {errors.orderType}
                                    </div>}
                                <Form.Control.Feedback>Looks good!</Form.Control.Feedback>
                            </Form.Group>
                        </Form.Row>
                        <Form.Row>
                            {this.RenderOrderSpecifics(this.state.orderType)}
                            </Form.Row>
                        <Button type="submit">Proceed to Payment</Button>
                    </Form>
                )}
        </Formik>
    );
}

    render() {

        return (
            this.FormExample()
        );
    }

    async ValidateUser() {
        const response = await fetch(this.state.RiotApiGetUserUrl + this.state.nickName + '&server=1').then(response => response.json());
    }

    async populateWeatherData() {
        const token = await authService.getAccessToken();
        const response = await fetch('order', {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });
        const data = await response.json();
        this.setState({ forecasts: data, loading: false });
    }
}