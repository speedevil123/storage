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
        console.log('Sending CREATE values:', values);
        const response = await fetch(serverPath + path, {
            headers: {
                'Content-Type': 'application/json'
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
        console.error("POST request failed:", error);
        throw error;
    }
};

export let PUTRequest = async (path, values, AbortSignal = new AbortController().signal) => {
    try {
        console.log('Sending PUT values:', values);
        const response = await fetch(serverPath + path, {
            headers: {
                'Content-Type': 'application/json'
            },
            method: 'PUT',
            credentials: 'include',
            body: JSON.stringify(values),
            signal: AbortSignal
        });
        if (!response.ok) {
            throw new Error(`Error ${response.status}: ${response.statusText}`);
        }
        return await response.json();
    } catch (error) {
        console.error("PUT request failed:", error);
        throw error;
    }
};

export let DELETERequest = async (path, AbortSignal = new AbortController().signal) => {
    try {
        console.log('Sending DELETE request path: ', path);
        const response = await fetch(serverPath + path, {
            headers: {
                'Content-Type': 'application/json'
            },
            method: 'DELETE',
            credentials: 'include',
            signal: AbortSignal
        });
        if (!response.ok) {
            throw new Error(`Error ${response.status}: ${response.statusText}`);
        }
        return await response.json(); 
    } catch (error) {
        console.error("DELETE request failed:", error);
        throw error;
    }
};


