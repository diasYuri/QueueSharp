import http from 'k6/http';

export const options = {
    vus: 10,
    duration: '60s',
};

export default function () {
    const url = 'https://localhost:7297/Queue/TESTE';

    const body = ''

    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    const res = http.get(url, body, params);
}