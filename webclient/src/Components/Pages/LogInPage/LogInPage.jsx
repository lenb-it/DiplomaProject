import Navigation from "Components/Navigation/Navigation";
import { useState, useContext, useEffect } from "react";
import { Button, Form } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { login } from "Api/ApiRequest";
import { observer } from "mobx-react-lite";
import { Context } from "index";
import "./LogInPage.css";

const LogInPage = observer(() => {
	const { user } = useContext(Context);
	const [email, setEmail] = useState();
	const [password, setPassword] = useState();
	const history = useNavigate();
	useEffect(() => { }, []);

	async function auth(e) {
		e.preventDefault();
		await login(email, password)
			.then(x => {
				user.setIsAuth(true);
				localStorage.setItem("token", x.data.token);
				localStorage.setItem("email", x.data.email);
				localStorage.setItem('userStore', JSON.stringify(user));
				history("/");
			})
			.catch(x => console.log(x));
		//todo сделай нормально
	}

	return <>
		<Navigation />
		<div className="back-grey">
			<div className="block-center ">
				<Form>
					<Form.Group className="mb-3">
						<h3 className="text-center">Вход</h3>
					</Form.Group>
					<Form.Group className="mb-3" controlId="formBasicEmail">
						<Form.Label>Email адрес</Form.Label>
						<Form.Control type="email" placeholder="Введите email"
							onChange={(e) => setEmail(e.target.value)} value={email} />
					</Form.Group>
					<Form.Group className="mb-3" controlId="formBasicPassword">
						<Form.Label>Password</Form.Label>
						<Form.Control type="password" placeholder="Введите пароль"
							onChange={(e) => setPassword(e.target.value)} value={password} />
					</Form.Group>
					<Form.Group className="d-grid">
						<Button variant="primary" type="submit" onClick={(e) => auth(e)}>Войти</Button>
					</Form.Group>
				</Form>
			</div>
		</div>
	</>
})

export default LogInPage;