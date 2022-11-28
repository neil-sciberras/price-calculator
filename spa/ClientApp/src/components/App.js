import { useState } from "react";
import "./App.css";

function App() {
	const [name, setName] = useState("");
	const [email, setEmail] = useState("");
	const [mobileNumber, setMobileNumber] = useState("");
	const [message, setMessage] = useState("");

    let handleSubmit = async (e) => {
        e.preventDefault();
        try {
            let res = fetch(
                'https://localhost:7181/Price',
                {
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
                });
            let resJson = await res.json();
            if (res.status === 200) {
                setName("");
                setEmail("");
                setMessage("User created successfully");
            } else {
                setMessage("Some error occured");
            }
        } catch (err) {
            console.log(err);
        }
    };

    return (
        <div className="App">
            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    value={name}
                    placeholder="Name"
                    onChange={(e) => setName(e.target.value)}
                />
                <input
                    type="text"
                    value={email}
                    placeholder="Email"
                    onChange={(e) => setEmail(e.target.value)}
                />
                <input
                    type="text"
                    value={mobileNumber}
                    placeholder="Mobile Number"
                    onChange={(e) => setMobileNumber(e.target.value)}
                />

                <button type="submit">Create</button>

                <div className="message">{message ? <p>{message}</p> : null}</div>
            </form>
        </div>
    );
}

export default App;