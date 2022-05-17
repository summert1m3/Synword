import ApiBaseService from "./apiBaseService";
import Cookies from 'js-cookie';

class AuthService {
    
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

    async verifyUserId(signal) {
        const userId = Cookies.get('userId');
        
        if(!userId) {
            await this.guestRegisterRequest(signal);
        }
    }

    async verifyJWT(signal) {
        const jwt = Cookies.get('jwt');

        if(!jwt) {
            await this.guestAuthenticateRequest(signal);
        }
    }
}

export default AuthService;