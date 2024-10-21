import axios from "axios";
import Cookies from 'js-cookie';

const instance = axios.create({
    baseURL: "http://localhost:5272/api",
    headers: {
        'Access-Control-Allow-Origin': '*',
    },
});


instance.interceptors.request.use(
    (config) => {
        const token = Cookies.get('jwtToken');
        if (token) {
            config.headers['Authorization'] = `Bearer ${token}`;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

export default instance;
