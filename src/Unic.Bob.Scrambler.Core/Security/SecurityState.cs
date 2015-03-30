using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unic.Bob.Scrambler.Core.Security
{
    using System.Configuration;
    using System.Web;

    using Sitecore.Security.Authentication;

    public class SecurityState
    {
        public SecurityState()
        {
            var user = AuthenticationManager.GetActiveUser();

            if (user.IsAdministrator)
            {
                IsAllowed = true;
                IsAutomatedTool = false;
            }
            else
            {
                var authToken = HttpContext.Current.Request.Headers["Authenticate"];
                var correctAuthToken = ConfigurationManager.AppSettings["DeploymentToolAuthToken"];

                if (!string.IsNullOrWhiteSpace(correctAuthToken) && !string.IsNullOrWhiteSpace(authToken)
                    && authToken.Equals(correctAuthToken, StringComparison.Ordinal))
                {
                    IsAllowed = true;
                    IsAutomatedTool = true;
                }
                else
                {
                    IsAllowed = false;
                    IsAutomatedTool = false;
                }
            }
        }

        public bool IsAllowed { get; private set; }
        public bool IsAutomatedTool { get; private set; }
    }
}
