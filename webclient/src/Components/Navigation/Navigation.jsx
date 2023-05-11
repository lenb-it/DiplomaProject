import "./Navigation.css"
import { Container, Nav, Navbar } from 'react-bootstrap';
import { useNavigate } from "react-router-dom";
import { observer } from "mobx-react-lite";
import { Context } from "index";
import { useContext } from "react";

const Navigation = observer(() => {
	const { user } = useContext(Context);
	const history = useNavigate();

	return <>
		<Navbar bg="light" expand="lg" fixed="top">
			<Container>
				<Navbar.Brand className="brand" onClick={() => { history("/") }}>
					<p className="fs-4">Клевер</p>
				</Navbar.Brand>
				<Navbar.Toggle aria-controls="basic-navbar-nav" />
				<Navbar.Collapse id="basic-navbar-nav">
					<Nav className="me-auto">
						<Nav.Link onClick={() => history("/menu")}>Меню</Nav.Link>
						<Nav.Link onClick={() => history("/order")}>Заказ</Nav.Link>
						<Nav.Link onClick={() => history("/reservation")}>Бронирование</Nav.Link>
						{
							user.isAuth ?
								<Nav.Link>{localStorage.getItem("email")}</Nav.Link> :
								<>
									<Nav.Link onClick={() => history("/login")}>Вход</Nav.Link>
									<Nav.Link onClick={() => history("/registration")}>Регистрация</Nav.Link>
								</>
						}
					</Nav>
				</Navbar.Collapse>
			</Container>
		</Navbar>
	</>
});

export default Navigation;