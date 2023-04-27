import Navigation from "Components/Navigation/Navigation";
import { Button, Form, InputGroup } from "react-bootstrap";
import "./RegistrationPage.css"

function RegistrationPage() {
	return <>
		<Navigation />
		<div className="back-grey">
			<div className="block-center">
				<Form>
					<Form.Group className="mb-3">
						<h3 className="text-center">Регистрация</h3>
					</Form.Group>
					<InputGroup className="mb-3">
						<InputGroup.Text>Имя и фамилия</InputGroup.Text>
						<Form.Control aria-label="First name" />
						<Form.Control aria-label="Last name" />
					</InputGroup>
					<Form.Group className="mb-3" controlId="formBasicEmail">
						<Form.Label>Email адрес</Form.Label>
						<Form.Control type="email" placeholder="Введите email" required />
					</Form.Group>
					<Form.Group className="mb-3" controlId="formBasicPassword">
						<Form.Label>Пароль</Form.Label>
						<Form.Control type="password" placeholder="Введите пароль" />
					</Form.Group>
					<Form.Group className="mb-3" controlId="formBasicConfirmPassword">
						<Form.Label>Повторите пароль</Form.Label>
						<Form.Control type="password" placeholder="Повторите пароль" />
					</Form.Group>
					<Form.Group className="mb-3" controlId="formBasicPhone">
						<Form.Label>Номер телефона</Form.Label>
						<Form.Control type="tel" placeholder="Номер телефона" />
					</Form.Group>
					<Form.Group className="d-grid">
						<Button variant="primary" type="submit">Зарегистрироваться</Button>
					</Form.Group>
				</Form>
			</div>
		</div>
	</>
}

export default RegistrationPage;