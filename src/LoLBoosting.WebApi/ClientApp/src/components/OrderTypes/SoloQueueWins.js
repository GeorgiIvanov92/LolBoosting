import React, { Component } from 'react';
import Slider from 'react-input-slider';

export class SoloQueueWins extends Component {
    static displayName = SoloQueueWins.name;

    constructor(props) {
        super(props);
        this.state = {
            x: 10
        }
        this.RenderSoloQueueWinsOrder = this.RenderSoloQueueWinsOrder.bind(this);
    }

    RenderSoloQueueWinsOrder() {
        return (
                <div>
                <h2 style={{ "text-align": "center" }}>{this.state.x} {this.state.x === 1 ? "Win" : "Wins"}</h2>
            <Slider
                axis="x"
                xstep={1}
                xmin={1}
                xmax={15}
                x={this.state.x}
                onChange={({ x }) => this.setState({ x: parseInt(x) })}
                />
                </div>
        );
    }

    render() {

        return (
            this.RenderSoloQueueWinsOrder()
        );
    }
}