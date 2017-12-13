using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ShoppingList.Shared.Models;
using SimpleAuth.Providers;

namespace ShoppingList.Shared.Services
{
    public class GoogleAuthService : IGoogleAuthService
    {
#if __ANDROID__
			private string GoogleClientId = "707171298949-c27i7ehhs1ectmkifma6fbuft677bie9.apps.googleusercontent.com";
			private string GoogleSecret = GoogleApi.NativeClientSecret; //"041h67ZTZOryqEbNKzDkaRms";
#else
        private string GoogleClientId = "707171298949-jbjeae1jtunvpal1gtc8v7ta37b5iq9s.apps.googleusercontent.com";
        private string GoogleSecret = "041h67ZTZOryqEbNKzDkaRms";
#endif
        private readonly GoogleApi _googleApi;

        public GoogleAuthService()
        {
            _googleApi = CreateApi();
        }


        public static GoogleProfile GoogleProfile { get; private set; }


        private GoogleApi CreateApi()
        {
            var scopes = new[]
            {
                "https://www.googleapis.com/auth/userinfo.email",
                "https://www.googleapis.com/auth/userinfo.profile"
            };

            var api = new GoogleApi("google",
                GoogleClientId, GoogleSecret)
            {
                Scopes = scopes,
            };
            return api;
        }

        public async Task<bool> TrySignIn()
        {
            try
            {
                if (!_googleApi.HasAuthenticated)
                {
                    var account = await _googleApi.Authenticate();

                    GoogleProfile = await _googleApi.Get<GoogleProfile>("https://www.googleapis.com/plus/v1/people/me");
                }
            }
            catch (TaskCanceledException e)
            {
                Debug.Write(e.Message);
                return false;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return false;
            }
            return _googleApi.HasAuthenticated;
        }

        public bool TrySignOut()
        {
            _googleApi.Logout();
            if (!_googleApi.HasAuthenticated)
            {
                GoogleProfile = new GoogleProfile();
            }
            return !_googleApi.HasAuthenticated;
        }
    }
}
