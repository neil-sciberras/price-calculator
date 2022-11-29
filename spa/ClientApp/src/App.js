import { useState } from "react";
import "./App.css";

function App() {
	const [weight, setWeight] = useState("");
	const [height, setHeight] = useState("");
	const [width, setWidth] = useState("");
	const [depth, setDepth] = useState("");
	const [message, setMessage] = useState("");

	let handleSubmit = async (e) => {
		e.preventDefault();
		try {
			let response = await fetch('https://localhost:7181/Price',
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
				});
			
			if (response.status === 200) {
				let price = await response.json();

				setMessage("Price is: " + price);
			}
			else {
				let errorMessage = await response.text();

				setMessage("Something went wrong. (" +  errorMessage + ")");
			}

			setWeight("");
			setHeight("");
			setWidth("");
			setDepth("");
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
				required />
				<input
					type="number"
					value={height}
					placeholder="Height"
					onChange={(e) => setHeight(e.target.value)}
				required />
				<input
					type="number"
					value={width}
					placeholder="Width"
					onChange={(e) => setWidth(e.target.value)}
				required />
				<input
					type="number"
					value={depth}
					placeholder="Depth"
					onChange={(e) => setDepth(e.target.value)}
				required />

				<button type="submit">Calculate</button>

				<div className="message">{message ? <p>{message}</p> : null}</div>
			</form>
		</div>
	);
}

export default App;