class ApiBaseService {
    static _apiBase = 'http://localhost:5000/';
    static guestRegisterUrl = `${this._apiBase + 'guestRegister'}`;
    static guestAuthenticateUrl = `${this._apiBase + 'guestAuthenticate'}`;
    static plagiarismCheckUrl = `${this._apiBase + 'plagiarismCheck'}`;

    static async postRequest(url, headers, form, signal) {
        const requestOptions = {
            method: 'POST',
            headers: headers,
            body: form,
            signal: signal
        };

        const response = await
            fetch(url, requestOptions);

        const data = await response.json();

        if (response.status != 200) {
            throw data;
        }

        return data;
    }
}

export default ApiBaseService;