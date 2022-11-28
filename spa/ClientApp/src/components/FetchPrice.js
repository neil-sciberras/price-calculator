import React, { Component } from 'react';

export class FetchPrice extends Component {
    static displayName = FetchPrice.name;

    constructor(props) {
        super(props);
        this.state = { price: 0, loading: true };
    }

    componentDidMount() {
        this.getPrice();
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.state.price;

        return (
            <div>
                <h1 id="tabelLabel" >Price</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async getPrice() {
        // Simple POST request with a JSON body using fetch
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                weight: 12,
                dimensions: {
                    width: 12,
                    height: 12,
                    depth: 12
				}
            })
        };
        fetch('https://localhost:7181/Price', requestOptions)
            .then(response => response.json())
            .then(data => this.setState({ price: data, loading: false }));
    }
}
