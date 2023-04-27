import { makeAutoObservable } from "mobx"

class UserStore {
	constructor() {
		this._isAuth = false;
		makeAutoObservable(this);
	}

	setIsAuth(value) {
		this._isAuth = value;
	}

	get isAuth() {
		return this._isAuth;
	}
}

export default UserStore;