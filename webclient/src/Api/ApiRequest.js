import axios from "axios"

const baseUrl = "https://localhost:5000";

const config = {
	headers: {
		'Authorization': `Bearer ${localStorage.getItem('token')}`
	}
}

export const regin = async (data) => {
	const res = await axios.post(baseUrl + '/account/register', data);
	return res;
}

export const login = async (email, password) => {
	const data = await axios.post(baseUrl + '/account/login', { email, password });
	return data;
}

export const reservation = async (email, date, countPeople) => {
	const data = await axios.post(baseUrl + "/reservation/add", { email, date, countPeople }, config);
	return data;
}

export const order = async (numberTable, email, orders) => {
	const data = await axios.post(baseUrl + '/order/add', { numberTable, email, orders });
	console.log(data);
	return data;
}

export const getMenu = async () => {
	const data = await axios.get(baseUrl + '/menu/GetValidMenuWithDiscount');
	return data;
}