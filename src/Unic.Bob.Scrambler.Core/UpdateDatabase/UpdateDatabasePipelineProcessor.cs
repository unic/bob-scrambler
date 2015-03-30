

namespace Unic.Bob.Scrambler.Core.UpdateDatabase
{
    using Sitecore.Pipelines.HttpRequest;
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Web;

    using Sitecore.Configuration;
    using Sitecore.Data.Serialization;
    using Sitecore.SecurityModel;

    using SecurityState = Unic.Bob.Scrambler.Core.Security.SecurityState;

    public class UpdateDatabasePipelineProcessor : HttpRequestProcessor
    {
        private readonly string activationUrl;
       

        public UpdateDatabasePipelineProcessor(string activationUrl)
        {
            this.activationUrl = activationUrl;
        }

        public override void Process(HttpRequestArgs args)
        {
            if (string.IsNullOrWhiteSpace(activationUrl)) return;

            if (args.Context.Request.RawUrl.StartsWith(activationUrl, StringComparison.OrdinalIgnoreCase))
            {
                ProcessRequest(args.Context);
                args.Context.Response.End();
            }
        }

        protected virtual void  ProcessRequest(HttpContext context)
        {
            context.Server.ScriptTimeout = 86400;

            var security = new SecurityState();

            if (security.IsAllowed)
            {
                try
                {
                    using (new SecurityDisabler())
                    {
                        var options = new LoadOptions();
                        options.DisableEvents = true;
                        Manager.LoadTree(PathUtils.Root, options);
                        context.Response.Write("<pre>Updated database.</pre>");
                    }
                }
                catch (Exception ex)
                {
                    context.Response.Write(ex.Message);
                }
            }
            else
            {
                context.Response.Write("<h4>Access Denied</h4>");
                context.Response.Write(
                    string.Format(
                        "<p>You need to <a href=\"/sitecore/admin/login.aspx?ReturnUrl={0}\">sign in to Sitecore as an administrator</a> to use the Unicorn control panel.</p>",
                        HttpUtility.UrlEncode(HttpContext.Current.Request.Url.PathAndQuery)));

                context.Response.TrySkipIisCustomErrors = true;
                context.Response.StatusCode = 401;
            }
        }
    }
}
