import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'
import { Col, Form, InputGroup, Button } from 'react-bootstrap';
import { Formik, Field, Feedback } from 'formik';
import * as yup from 'yup';

export class BoostMe extends Component {
    static displayName = BoostMe.name;

    constructor(props) {
        super(props);
        this.state = {
            forecasts: [],
            loading: true,
            schema: yup.object({
                username: yup.string().min(4, 'Too Short!').max(30, 'Too Long!').required('Required!'),
                password: yup.string().required(),
                nickname: yup.string().required('InGame Username is required!'),
                server: yup.string().required('Server is required!'),
                terms: yup.bool().required()
            })
        }

        this.FormExample = this.FormExample.bind(this);
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
                            </Form.Group>
                            <Form.Group as={Col} md="4" controlId="validationFormik03">
                                <Form.Label>InGame Username</Form.Label>
                                <Form.Control
                                    type="text"
                                    name="nickname"
                                    onChange={handleChange}
                                    isValid={touched.nickname && !errors.nickname}
                                    isInvalid={!!errors.nickname}
                                />

                                <Form.Control.Feedback>Looks good!</Form.Control.Feedback>
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
                        </Form.Row>
                        <Form.Row>
                            <Form.Group as={Col} md="6" controlId="validationFormik04">
                                <Form.Label>City</Form.Label>
                                <Form.Control
                                    type="text"
                                    placeholder="City"
                                    name="city"
                                    value={values.city}
                                    onChange={handleChange}
                                    isInvalid={!!errors.city}
                                />

                                <Form.Control.Feedback type="invalid">
                                    {errors.city}
                                </Form.Control.Feedback>
                            </Form.Group>
                            <Form.Group as={Col} md="3" controlId="validationFormik05">
                                <Form.Label>State</Form.Label>
                                <Form.Control
                                    type="text"
                                    placeholder="State"
                                    name="state"
                                    value={values.state}
                                    onChange={handleChange}
                                    isInvalid={!!errors.state}
                                />
                                <Form.Control.Feedback type="invalid">
                                    {errors.state}
                                </Form.Control.Feedback>
                            </Form.Group>
                            <Form.Group as={Col} md="3" controlId="validationFormik06">
                                <Form.Label>Zip</Form.Label>
                                <Form.Control
                                    type="text"
                                    placeholder="Zip"
                                    name="zip"
                                    value={values.zip}
                                    onChange={handleChange}
                                    isInvalid={!!errors.zip}
                                />

                                <Form.Control.Feedback type="invalid">
                                    {errors.zip}
                                </Form.Control.Feedback>
                            </Form.Group>
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

    async populateWeatherData() {
        const token = await authService.getAccessToken();
        const response = await fetch('order', {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });
        const data = await response.json();
        this.setState({ forecasts: data, loading: false });
    }
}