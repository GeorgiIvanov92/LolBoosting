import React, { Component } from 'react';
import Slider from 'react-input-slider';

export class GamesBoost extends Component {
    static displayName = GamesBoost.name;

    constructor(props) {
        super(props);
        this.state = {
            x: 10
        }
        this.RenderSoloQueueGamesOrder = this.RenderSoloQueueGamesOrder.bind(this);
    }

    RenderSoloQueueGamesOrder() {
        return (
            <div>
                <h2 style={{ fontFamily: 'Times New Roman', "textAlign": "center" }}>{this.state.x} {this.state.x === 1 ? "Game " : "Games "}
                    {this.props.price ? '(' + (this.props.price * this.state.x).toFixed(2) + '€)' : ''}</h2>
                <Slider
                    axis="x"
                    xstep={1}
                    xmin={1}
                    xmax={15}
                    x={this.state.x}
                    onChange={({ x }) => {
                        this.setState({ x: parseInt(x) });
                        this.props.SetNumberOfGames(x);
                    }}
                />
            </div>
        );
    }

    render() {

        return (
            this.RenderSoloQueueGamesOrder()
        );
    }
}