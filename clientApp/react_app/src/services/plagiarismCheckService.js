import ApiBaseService from "./apiBaseService";
import AuthService from "./authService";

class PlagiarismCheckService {
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
}

export default PlagiarismCheckService;