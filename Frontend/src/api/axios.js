import axios from 'axios';

const instance = axios.create({
  baseURL: 'http://localhost:5069/api/',
});

export default instance;
