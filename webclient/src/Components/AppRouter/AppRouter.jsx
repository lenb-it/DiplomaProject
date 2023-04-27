import MainPage from "Components/Pages/MainPage/MainPage";
import MenuPage from "Components/Pages/MenuPage/MenuPage";
import LogInPage from "Components/Pages/LogInPage/LogInPage";
import RegistrationPage from "Components/Pages/RegistrationPage/RegistrationPage";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { useContext, useEffect } from "react";
import { Context } from "index";
import { observer } from "mobx-react-lite";

const AppRouter = observer(() => {
	const { user } = useContext(Context);
	useEffect(() => {
		user.setIsAuth(JSON.parse(localStorage.getItem("userStore"))._isAuth)
	}, []);

	return <>
		<BrowserRouter>
			<Routes>
				<Route path="/" element={<MainPage />} />
				<Route path="menu" element={<MenuPage />} />
				<Route path="login" element={<LogInPage />} />
				<Route path="registration" element={<RegistrationPage />} />
			</Routes>
		</BrowserRouter>
	</>
})

export default AppRouter;