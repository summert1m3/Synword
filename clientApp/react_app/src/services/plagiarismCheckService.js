class PlagiarismCheckService {
    _apiBase = 'https://api.synword.com/'
    _plagiarismCheckUrl = `${this._apiBase + 'plagiarismCheck'}`;

    async plagiarismCheck(text, signal) {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ text: text }),
            signal: signal
        };
        const response = await
            fetch(this._plagiarismCheckUrl, requestOptions);

        const data = await response.json();

        if (response.status != 200) {
            throw data;
        }

        return data;
    }
}

export default PlagiarismCheckService;