namespace Unic.Bob.Scrambler.Core.Pipelines.HttpRequest
{
    using System;
    using System.Configuration;
    using System.Web;

    using Sitecore.Pipelines.HttpRequest;
    using Sitecore.Security.Authentication;
    using Sitecore.SecurityModel;

    public abstract class BasePipelineProcessor : HttpRequestProcessor
    {
        private readonly string activationUrl;

        protected BasePipelineProcessor(string activationUrl)
        {
            this.activationUrl = "/bob/" + activationUrl;
        }

        public override void Process(HttpRequestArgs args)
        {
            if (string.IsNullOrWhiteSpace(this.activationUrl)) return;

            var context = args.Context;

            if (args.Context.Request.RawUrl.StartsWith(this.activationUrl, StringComparison.OrdinalIgnoreCase))
            {
                context.Server.ScriptTimeout = 86400;

                if (this.IsUserAllowed())
                {
                    using (new SecurityDisabler())
                    {
                        args.Context.Response.ContentType = "text/plain";
                        this.ProcessRequest(args.Context);
                    }
                }
                else
                {
                    context.Response.Write("<h4>Access Denied</h4>");
                    context.Response.Write(
                        string.Format(
                            "<p>You need to <a href=\"/sitecore/admin/login.aspx?ReturnUrl={0}\">sign in to Sitecore as an administrator</a> to use the Bob Scrambler tools.</p>",
                            HttpUtility.UrlEncode(HttpContext.Current.Request.Url.PathAndQuery)));

                    context.Response.TrySkipIisCustomErrors = true;
                    context.Response.StatusCode = 401;
                }

                args.Context.Response.End();
            }
        }

        protected virtual bool IsUserAllowed()
        {
            var user = AuthenticationManager.GetActiveUser();

            if (user.IsAdministrator)
            {
                return true;
            }
            
            var authToken = HttpContext.Current.Request.Headers["Authenticate"];
            var correctAuthToken = ConfigurationManager.AppSettings["DeploymentToolAuthToken"];

            if (!string.IsNullOrWhiteSpace(correctAuthToken) && !string.IsNullOrWhiteSpace(authToken)
                && authToken.Equals(correctAuthToken, StringComparison.Ordinal))
            {
                return true;
            }
            
            return false;
        }

        protected abstract void ProcessRequest(HttpContext context);
    }
}
