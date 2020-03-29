import React, { Component } from 'react';
import { Col, Form, Button, Spinner } from 'react-bootstrap';
import { Formik } from 'formik';
import * as yup from 'yup';
import { OrderSpecific } from "./OrderSpecific";
import { Server } from "./Utilities/Enums";
import { ValidationState } from "./Utilities/Enums";

import iron from '../Images/Emblem_Iron.png';
import bronze from '../Images/Emblem_Bronze.png';
import silver from '../Images/Emblem_Silver.png';
import gold from '../Images/Emblem_Gold.png';
import platinum from '../Images/Emblem_Platinum.png';
import diamond from '../Images/Emblem_Diamond.png';
import Grandmaster from '../Images/Emblem_Grandmaster.png';
import Challenger from '../Images/Emblem_Challenger.png';

let renderedAccountInfo = <div></div>;

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
            RiotApiGetUserUrl: 'order/CalculatePrice',
        };

        this.GenericForm = this.GenericForm.bind(this);
        this.CheckForValidation = this.CheckForValidation.bind(this);
        this.ValidateUser = this.ValidateUser.bind(this);
        this.RenderOrderSpecifics = this.RenderOrderSpecifics.bind(this);
        this.MapTierToImage = this.MapTierToImage.bind(this);
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
0                                        <option value="NA" label="NA" />
                                    </select>
                                    {errors.server &&
                                        <div style={{ color: 'red' }} className="input-feedback">
                                            {errors.server}
                                        </div>}

                                    <Form.Control.Feedback>Looks good!</Form.Control.Feedback>
                                </Form.Group>
                            </Form.Row>
                            <Form.Row className='mt-5'>
                                <Button style={{ width: "13%", height: "50%" }} disabled={errors.server || errors.nickname} variant="dark" onClick={this.ValidateUser}>Calculate Price</Button>
                                {this.CheckForValidation()}
                            </Form.Row>
                            <Form.Row className='mt-5'>
                                {renderedAccountInfo}
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

    MapTierToImage() {
        switch (this.state.orderMetadata.currentTier) {
            case 1:
                {
                    renderedAccountInfo = <div>
                        <h2 style={{ fontFamily: 'Times New Roman', color: "brown", textAlign: 'center' }}>
                            Iron Division {this.state.orderMetadata.currentDivision} ({this.state.orderMetadata.currentPoints}LP)
                            </h2>
                        <img src={iron} style={{ height: '200px', width: '150px' }} />
                    </div>

                    return;
                }
            case 2:
                {
                    renderedAccountInfo = <div>
                        <h2 style={{ fontFamily: 'Times New Roman', color: "brown", textAlign: 'center' }}>
                            Bronze Division {this.state.orderMetadata.currentDivision} ({this.state.orderMetadata.currentPoints}LP)
                            </h2>
                        <img src={bronze} style={{ height: '200px', width: '150px' }} />
                    </div>

                    return;
                }
            case 3:
                {
                    renderedAccountInfo = <div>
                        <h2 style={{ fontFamily: 'Times New Roman', color: "brown", textAlign: 'center' }}>
                            Silver Division {this.state.orderMetadata.currentDivision} ({this.state.orderMetadata.currentPoints}LP)
                            </h2>
                        <img src={silver} style={{ height: '200px', width: '150px' }} />
                    </div>

                    return;
                }
            case 4:
                {
                    renderedAccountInfo = <div>
                        <h2 style={{ fontFamily: 'Times New Roman', color: "brown", textAlign: 'center' }}>
                            Gold Division {this.state.orderMetadata.currentDivision} ({this.state.orderMetadata.currentPoints}LP)
                            </h2>
                        <img src={gold} style={{ height: '200px', width: '150px' }} />
                    </div>

                    return;
                }
            case 5:
                {
                    renderedAccountInfo = <div>
                        <h2 style={{ fontFamily: 'Times New Roman', color: "brown", textAlign: 'center' }}>
                            Platinum Division {this.state.orderMetadata.currentDivision} ({this.state.orderMetadata.currentPoints}LP)
                            </h2>
                        <img src={platinum} style={{ height: '200px', width: '150px' }} />
                    </div>

                    return;
                }
            case 6:
                {
                    renderedAccountInfo = <div>
                        <h2 style={{ fontFamily: 'Times New Roman', color: "brown", textAlign: 'center' }}>
                            Diamond Division {this.state.orderMetadata.currentDivision} ({this.state.orderMetadata.currentPoints}LP)
                            </h2>
                        <img src={diamond} style={{ height: '200px', width: '150px' }} />
                    </div>

                    return;
                }

            default:
        }
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
                return this.MapTierToImage();
        case ValidationState.Rejected:
                return <h2> No such account found!</h2>
            default: return <div></div>
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
        const response = await fetch(this.state.RiotApiGetUserUrl, {
            method: 'post',
            headers: {'Content-Type':'application/json'},
            body: JSON.stringify({
                "username": this.state.nickName,
                "server": this.state.currentServer,
                "orderType": this.props.orderType
            })
        }).then(response => response.json());

        if (response) {
            this.setState({ orderMetadata: response, currentValidationState: ValidationState.Confirmed });
        } else {
            this.setState({ currentValidationState: ValidationState.Rejected });
        }
    }
}