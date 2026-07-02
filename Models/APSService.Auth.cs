using Autodesk.Authentication.Model;

public partial class APSService
{
	public string GetAuthorizationURL(string client_id = "", string client_secret = "")
	{
		if (string.IsNullOrEmpty(client_id) || string.IsNullOrEmpty(client_secret))
		{
			_customCredentials = false;
			return _authClient.Authorize(_clientId, ResponseType.Code, _callbackUri, InternalTokenScopes);
		}
		else
		{
			_customCredentials = true;
			_customclientId = client_id;
			_customclientSecret = client_secret;
			return _authClient.Authorize(client_id, ResponseType.Code, _callbackUri, InternalTokenScopes);
		}
	}

	public void RemoveCustomCredentials()
	{
		_customclientId = "";
		_customclientSecret = "";
		_customCredentials = false;
	}

	public async Task<Tokens> GenerateTokens(string code)
	{
		var clientId = _customCredentials ? _customclientId : _clientId;
		var clientSecret = _customCredentials ? _customclientSecret : _clientSecret;

		ThreeLeggedToken internalAuth = await _authClient.GetThreeLeggedTokenAsync(clientId: clientId, code: code, redirectUri: _callbackUri, clientSecret: clientSecret);
		ThreeLeggedToken publicAuth = await _authClient.RefreshTokenAsync(refreshToken: internalAuth.RefreshToken!, clientId: clientId, clientSecret: clientSecret, scopes: PublicTokenScopes);
		return new Tokens
		{
			PublicToken = publicAuth.AccessToken!,
			InternalToken = internalAuth.AccessToken!,
			RefreshToken = publicAuth.RefreshToken!,
			ExpiresAt = DateTime.Now.ToUniversalTime().AddSeconds((double)internalAuth.ExpiresIn!)
		};
	}

	public async Task<Tokens> RefreshTokens(Tokens tokens)
	{
		ThreeLeggedToken internalAuth = await _authClient.RefreshTokenAsync(refreshToken: tokens.RefreshToken, clientId: _clientId, clientSecret: _clientSecret, scopes: InternalTokenScopes);
		ThreeLeggedToken publicAuth = await _authClient.RefreshTokenAsync(refreshToken: internalAuth.RefreshToken!, clientId: _clientId, clientSecret: _clientSecret, scopes: PublicTokenScopes);
		return new Tokens
		{
			PublicToken = publicAuth.AccessToken!,
			InternalToken = internalAuth.AccessToken!,
			RefreshToken = publicAuth.RefreshToken!,
			ExpiresAt = DateTime.Now.ToUniversalTime().AddSeconds((double)internalAuth.ExpiresIn!).AddSeconds(-1700)
		};
	}

	public async Task<dynamic> GetUserProfile(Tokens tokens)
	{
		var userInfo = await _authClient.GetUserInfoAsync(tokens.InternalToken);
		return new { firstName = userInfo.GivenName, lastName = userInfo.FamilyName };
	}
}
