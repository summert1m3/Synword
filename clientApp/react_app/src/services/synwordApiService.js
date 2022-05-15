import ApiBaseService from "./apiBaseService";
import AuthService from "./authService";

class SynwordApiService {
    _authService = new AuthService();

    async plagiarismCheck(text, signal) {
        let form = new FormData();
        form.append("text", text);

        let data = await this._authService.authorizedPostRequest(
                ApiBaseService.plagiarismCheckUrl,
                form,
                signal
            );

        return data;
    }

    async rephrase(text, signal) {
        let form = new FormData();
        form.append("text", text);
        form.append("language", "rus");

        let data = await this._authService.authorizedPostRequest(
                ApiBaseService.rephraseUrl,
                form,
                signal
            );

        return data;
    }
}

export default SynwordApiService;