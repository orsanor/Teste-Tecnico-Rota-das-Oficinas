import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5087/api", // URL base da API
});

export default api;