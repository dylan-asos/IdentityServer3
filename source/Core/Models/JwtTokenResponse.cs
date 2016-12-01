namespace IdentityServer3.Core.Models
{
    using System;

    using IdentityServer3.Core.Extensions;

    using Newtonsoft.Json;

    public class JwtTokenResponse
    {
        [JsonProperty(PropertyName = "id_token")]
        public string IdToken { get; set; }

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "issued")]
        public DateTime Issued { get; set; }

        [JsonProperty(PropertyName = "expires")]
        public DateTime Expires { get; set; }

        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "session_state")]
        public string SessionState { get; set; }

        public static JwtTokenResponse ConvertFromAuthorizationResponse(AuthorizeResponse response)
        {
            var jwtResponse = new JwtTokenResponse();

            if (response.IsError)
            {
                if (response.Error.IsPresent())
                {
                    // wat do?
                }
            }
            else
            {
                if (response.IdentityToken.IsPresent())
                {
                    jwtResponse.IdToken = response.IdentityToken;
                }

                if (response.AccessToken.IsPresent())
                {
                    jwtResponse.AccessToken = response.AccessToken;
                    jwtResponse.TokenType = "Bearer";
                    jwtResponse.ExpiresIn = response.AccessTokenLifetime;
                    jwtResponse.Expires = DateTime.UtcNow.AddSeconds(response.AccessTokenLifetime);
                }

                if (response.Scope.IsPresent())
                {
                    jwtResponse.Scope = response.Scope;
                }
            }

            if (response.State.IsPresent())
            {
                jwtResponse.State = response.State;
            }

            if (response.SessionState.IsPresent())
            {
                jwtResponse.SessionState = response.SessionState;
            }

            jwtResponse.Issued = DateTime.UtcNow;

            return jwtResponse;
        }
    }
}