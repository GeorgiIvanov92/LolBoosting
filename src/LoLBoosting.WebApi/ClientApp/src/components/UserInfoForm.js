import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'
import { Col, Form, InputGroup, Button, Spinner } from 'react-bootstrap';
import { Formik, Field, Feedback } from 'formik';
import * as yup from 'yup';
import { WinsBoost } from "./OrderTypes/WinsBoost";
import { OrderType } from "./Utilities/Enums";
import { OrderSpecific } from "./OrderSpecific";
import { Server } from "./Utilities/Enums";
import { ValidationState } from "./Utilities/Enums";

export class UserInfoForm extends Component {
    static displayName = UserInfoForm.name;

    constructor(props) {
        super(props);

        this.state = {
            schema: yup.object({
                username: yup.string().min(2, 'Too Short!').max(30, 'Too Long!').required('Required!'),
                password: yup.string().min(2, 'Too Short!').max(30, 'Too Long!').required('Required!'),
                nickname: yup.string().min(2, 'Too Short!').max(30, 'Too Long!').required('Required!'),
                server: yup.string().required('Server is required!'),
            }),
            orderType: 0,
            currentValidationState: ValidationState.NotStarted,
            selectedServer: Server.None,
            nickName: '',
            orderMetadata: [],
            RiotApiGetUserUrl: 'order/CalculatePrice?username='
        };

        this.GenericForm = this.GenericForm.bind(this);
        this.CheckForValidation = this.CheckForValidation.bind(this);
        this.ValidateUser = this.ValidateUser.bind(this);
        this.RenderOrderSpecifics = this.RenderOrderSpecifics.bind(this);
    }

    RenderOrderSpecifics(orderType) {
        if (this.state.currentValidationState !== ValidationState.Confirmed) {
            return <OrderSpecific orderType={orderType} />
        }
        return <OrderSpecific price={this.state.orderMetadata.price} orderType={orderType} />
    }

    GenericForm() {
        return (
            <Formik
                validationSchema={this.state.schema}
                onSubmit={console.log}
                isInvalid={<div>Required!</div>}
                initialValues={{
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
                                        onChange={(nick) => { handleChange(nick); this.setState({ nickName: nick.target.value, currentValidationState: ValidationState.NotStarted }) }}
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
                                        onChange={(server) => {
                                            handleChange(server);
                                            this.setState({
                                                currentServer: Server[server.target.value],
                                                currentValidationState: ValidationState.NotStarted
                                            });
                                        }}
                                        onBlur={handleBlur}
                                        style={{ display: 'block' }}
                                    >
                                        <option value="" label="Select a server" />
                                        <option value="EUW" label="EUW" />
                                        <option value="EUNE" label="EUNE" />
                                        <option value="NA" label="NA" />
                                    </select>
                                    {errors.server &&
                                        <div style={{ color: 'red' }} className="input-feedback">
                                            {errors.server}
                                        </div>}

                                    <Form.Control.Feedback>Looks good!</Form.Control.Feedback>
                                </Form.Group>
                            </Form.Row>
                            <Form.Row className='mt-5'>
                                <Button disabled={errors.server || errors.nickname} variant="dark" onClick={this.ValidateUser}>Calculate Price</Button>
                                {this.CheckForValidation()}
                            </Form.Row>
                            <Form.Row className='mt-5'>
                                {this.RenderOrderSpecifics(this.props.orderType)}
                            </Form.Row>
                            <Button className='mt-5' disabled={this.state.currentValidationState !== ValidationState.Confirmed} type="submit">Proceed to Payment</Button>
                        </Form>
                    )}
            </Formik>
        );
    }

    render() {
        if (this.state.orderType !== this.props.orderType && this.state.currentValidationState === ValidationState.Confirmed) {

            this.ValidateUser();
        }
        return (
            this.GenericForm()
        );
    }

    CheckForValidation() {
        switch (this.state.currentValidationState) {
        case ValidationState.NotStarted:
            return <div></div>;
        case ValidationState.Validating:
            return <Spinner margin="sm" animation="border" role="status">
                    <span className="sr-only">Loading...</span>
                </Spinner>
        case ValidationState.Confirmed:
            return <div></div>
        case ValidationState.Rejected:
            return <h2> No such account found!</h2>
        }
    }

    async ValidateUser() {
        if (this.state.currentValidationState === ValidationState.Confirmed && this.state.orderType === this.props.orderType) {
            return;
        }
        this.setState({
            currentValidationState:
                ValidationState.Validating,
            orderType: this.props.orderType
        });
        const response = await fetch(this.state.RiotApiGetUserUrl +
            this.state.nickName +
            '&server=' + this.state.currentServer +
            '&orderType=' + this.props.orderType).then(response => response.json());

        if (response) {
            this.setState({ orderMetadata: response, currentValidationState: ValidationState.Confirmed });
        } else {
            this.setState({ currentValidationState: ValidationState.Rejected });
        }
    }
}