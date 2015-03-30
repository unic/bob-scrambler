﻿namespace Unic.Bob.Scrambler.Core.PipelineProcessors
{
    using System;
    using System.Web;

    using Sitecore.Data.Serialization;
    using Sitecore.Pipelines.HttpRequest;
    using Sitecore.SecurityModel;

    using SecurityState = Unic.Bob.Scrambler.Core.Security.SecurityState;

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

                if (new SecurityState().IsAllowed)
                {
                    using (new SecurityDisabler())
                    {
                        args.Context.Response.ContentType = "text/plain";
                        ProcessRequest(args.Context);
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

        protected abstract void ProcessRequest(HttpContext context);
    }
}
