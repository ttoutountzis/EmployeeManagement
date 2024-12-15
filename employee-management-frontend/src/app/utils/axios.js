import axios from "axios";

const axiosInstance = axios.create({
  baseURL: "https://localhost:44379/api", // Update with your backend API base URL
});

export default axiosInstance;
