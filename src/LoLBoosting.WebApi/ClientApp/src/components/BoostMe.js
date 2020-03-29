import React, { Component } from 'react';
import { Nav } from 'react-bootstrap';
import { UserInfoForm } from "./UserInfoForm";
import { OrderType } from "./Utilities/Enums";

export class BoostMe extends Component {
    static displayName = BoostMe.name;

    constructor(props) {
        super(props);
        this.state = {
            orderType: OrderType.WinsBoost
        }
    }

    render() {
        return (
            <div>
                <h1 style={{ fontFamily: 'Times New Roman', color: "brown", textAlign: 'center' }}>Boost Purchase</h1>
                <Nav className="mt-5" justify variant="pills" defaultActiveKey="winsSoloBoost">
            <Nav.Item>
                        <Nav.Link onClick={() => this.setState({orderType: OrderType.WinsBoost })} eventKey="winsSoloBoost"> Wins Solo Boost</Nav.Link>
                </Nav.Item>
                <Nav.Item>
                        <Nav.Link onClick={() => this.setState({ orderType: OrderType.GamesBoost })}eventKey="gamesSoloBoost">Games Solo Boost</Nav.Link>
                </Nav.Item>
            </Nav>
                <UserInfoForm className='mt-5' orderType={this.state.orderType}>
                    </UserInfoForm>

            </div >
        );
    }
}