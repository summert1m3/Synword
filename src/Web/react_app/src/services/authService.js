import ApiBaseService from "./apiBaseService";
import Cookies from 'js-cookie';
import jwt_decode from "jwt-decode";

class AuthService {
    async authorizedPostRequest(url, form, signal) {
        await this.verifyJWT(signal);

        const jwt = Cookies.get('jwt');

        let headers = {
            "Accept": "application/json",
            "Authorization": `Bearer ${jwt}`
        };

        let data = await ApiBaseService.postRequest(
            url,
            headers,
            form,
            signal
        );

        return data;
    }

    async verifyJWT(signal) {
        const jwt = Cookies.get('jwt');
        const decodedJwt = jwt_decode(token);
        
        var current_time = Date.now() / 1000;

        if (!jwt || decodedJwt.exp < current_time) {
            await this.guestAuthenticateRequest(signal);
        }
    }

    async guestAuthenticateRequest(signal) {
        await this.verifyUserId();

        const userId = Cookies.get('userId');

        let headers = {
            "Accept": "application/json",
        };

        let form = new FormData();
        form.append("userId", userId);

        let data = await ApiBaseService.postRequest(
            ApiBaseService.guestAuthenticateUrl,
            headers,
            form,
            signal
        );

        Cookies.set('jwt', data.token);
    }

    async verifyUserId(signal) {
        const userId = Cookies.get('userId');

        if (!userId) {
            await this.guestRegisterRequest(signal);
        }
    }

    async guestRegisterRequest(signal) {
        let headers = {
            "Accept": "application/json",
            "Content-Type": "multipart/form-data",
        };

        let data = await ApiBaseService.postRequest(
            ApiBaseService.guestRegisterUrl,
            headers,
            null,
            signal
        );

        Cookies.set('userId', data.userId);
    }
}

export default AuthService;