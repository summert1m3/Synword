import ApiBaseService from "./apiBaseService";
import Cookies from 'js-cookie';
import jwt_decode from "jwt-decode";

class AuthService {
    async authorizedPostRequest(url, form, signal) {
        const jwt = await this.getJwt(signal);

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

    async getJwt(signal) {
        let jwt = Cookies.get('jwt');

        if (!jwt) {
            jwt = await this.authenticate(signal);
            Cookies.set('jwt', jwt);
        }
        
        const decodedJwt = jwt_decode(jwt);

        var current_time = Date.now() / 1000;

        if(decodedJwt.exp < current_time) {
            jwt = await this.authenticate(signal);
            Cookies.set('jwt', jwt);
        }

        return jwt;
    }

    async authenticate(signal) {
        let userId = await this.getUserId();

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

        return data.accessToken;
    }

    async getUserId(signal) {
        let userId = Cookies.get('userId');

        if (!userId) {
            userId = await this.registration(signal);
            Cookies.set('userId', userId);
        }

        return userId;
    }

    async registration(signal) {
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
        
        return data.userId;
    }
}

export default AuthService;