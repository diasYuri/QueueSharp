import http from 'k6/http';

export const options = {
    vus: 10,
    duration: '60s',
};

export default function () {
    const url = 'https://localhost:7297/Queue/TESTE';
    
    const payload = JSON.stringify({
        email: 'aaa',
        password: 'bbb',
    });
    
    const body = JSON.stringify({
        payload: payload
    })

    
    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    const res = http.post(url, body, params);
}