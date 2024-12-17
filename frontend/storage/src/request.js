export var serverPath = 'https://localhost:7267';

export let GETRequest = async (path) => {
    try {
        const response = await fetch(serverPath + path);
        if(!response.ok) {
            throw new Error(`Error ${response.status}: ${response.statusText}`);
        }
        return await response.json();
    } 
    catch (error) {
        console.error("GET request failed:", error);
        throw error;
    }
};

export let POSTRequest = async (path, values, AbortSignal = new AbortController().signal) => {
    try {
        const response = await fetch(serverPath + path, {
            headers: {
                'Content-Type': 'application/json',
            },
            method: 'POST',
            credentials: 'include',
            body: JSON.stringify(values),
            signal: AbortSignal
        });
        if(!response.ok) {
            throw new Error(`Error ${response.status}: ${response.statusText}`);
        }
        return await response.json();
    }
    catch (error) {
        console.error("GET request failed:", error);
        throw error;
    }
};

