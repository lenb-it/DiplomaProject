import axios from "axios"
const baseUrl = "https://localhost:5000";

export const login = async (email, password) => {
	const data = await axios.post(baseUrl + '/account/login', { email, password });
	console.log(data);
	return data;
}

export const checkToken = async () => {

}

const config = {
	headers: {
		'Authorization': `Bearer ${localStorage.getItem('token')}`
	}
}