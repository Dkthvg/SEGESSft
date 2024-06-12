using Blazored.LocalStorage;

namespace SEGES.FrontEnd.Helpers
{
    public class AnonymousSessionService
    {
        private readonly ILocalStorageService _localStorage;
        private const string SessionKey = "AnonymousSession";

        public AnonymousSessionService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<string> GetOrCreateSessionIdAsync()
        {
            var sessionId = await _localStorage.GetItemAsStringAsync(SessionKey);
            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId = Guid.NewGuid().ToString();
                await _localStorage.SetItemAsStringAsync(SessionKey, sessionId);
            }
            return sessionId;
        }

        public async Task SaveSessionDataAsync(string key, string value)
        {
            await _localStorage.SetItemAsStringAsync(key, value);
        }

        public async Task<string> GetSessionDataAsync(string key)
        {
            return await _localStorage.GetItemAsStringAsync(key);
        }

        public async Task ClearSessionAsync()
        {
            await _localStorage.RemoveItemAsync(SessionKey);
        }
    }
}