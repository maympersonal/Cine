import axios from 'axios';

const instance = axios.create({
  baseURL: 'http://tu-backend.com/api/',
});

export default instance;
