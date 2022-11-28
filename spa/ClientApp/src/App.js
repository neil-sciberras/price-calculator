import { useState } from "react";
import "./App.css";

function App() {
    const [weight, setWeight] = useState("");
    const [height, setHeight] = useState("");
    const [width, setWidth] = useState("");
    const [depth, setDepth] = useState("");
    const [statusCode, setStatusCode] = useState("");
    const [price, setPrice] = useState("");
    const [message, setMessage] = useState("");

    let handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await fetch(
                'https://localhost:7181/Price',
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({
                        weight: weight,
                        dimensions: {
                            width: width,
                            height: height,
                            depth: depth
                        }
                    })
                })
                .then(response =>
                {
                    setStatusCode(response.status);
                    return response.json();
                })
                .then(data => setPrice(data));

            if (statusCode === 200)
            {
                setWeight("");
                setHeight("");
                setWidth("");
                setDepth("");
                setMessage("Request sent successfull. Price is: " + price);
            }
            else
            {
                setMessage("Some error occured. Status code is:" + statusCode);
            }
        }
        catch (err)
        {
            console.log(err);
        }
    };

    return (
        <div className="App">
            <form onSubmit={handleSubmit}>
                <input
                    type="number"
                    value={weight}
                    placeholder="Weight"
                    onChange={(e) => setWeight(e.target.value)}
                />
                <input
                    type="number"
                    value={height}
                    placeholder="Height"
                    onChange={(e) => setHeight(e.target.value)}
                />
                <input
                    type="number"
                    value={width}
                    placeholder="Width"
                    onChange={(e) => setWidth(e.target.value)}
                />
                <input
                    type="number"
                    value={depth}
                    placeholder="Depth"
                    onChange={(e) => setDepth(e.target.value)}
                />

                <button type="submit">Calculate</button>

                <div className="message">{message ? <p>{message}</p> : null}</div>
            </form>
        </div>
    );
}

export default App;