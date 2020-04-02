import React, { Component } from 'react';
import { Col, Form, Button, Spinner } from 'react-bootstrap';
import { Formik } from 'formik';
import * as yup from 'yup';
import { OrderSpecific } from "./OrderSpecific";
import { Server } from "./Utilities/Enums";
import { ValidationState } from "./Utilities/Enums";

let renderedAccountInfo = <div></div>;

export class FinalizeOrder extends Component {
    static displayName = FinalizeOrder.name;

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
        };

        this.GenericForm = this.GenericForm.bind(this);
    }

    async componentWillMount() {
        let response = await fetch('order/GetUserOrder').then(result => result.json());

        if (!response) {
            window.location.href = '/boostme';
        } else {
            this.setState({ orderMetadata: response });
        }
    }

    RenderOrderSpecifics(orderType) {
        if (this.state.currentValidationState !== ValidationState.Confirmed) {
            return <OrderSpecific orderType={orderType} />
        }
        return <OrderSpecific price={this.state.orderMetadata.price} orderType={orderType} />
    }

    GenericForm() {
        return (
            <div>
                <h1>{this.state.orderMetadata.price}</h1>
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
                            </Form>
                    )}
                </Formik>
                </div>
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
}