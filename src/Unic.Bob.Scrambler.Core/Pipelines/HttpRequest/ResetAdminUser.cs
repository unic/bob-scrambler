namespace Unic.Bob.Scrambler.Core.Pipelines.HttpRequest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Security;

    using Sitecore.Search;

    /// <summary>
    /// Pipeline processor for exposing a HTTP service for rebuilding the search index.
    /// </summary>
    public class ResetAdminUser : BasePipelineProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResetAdminUser" /> class.
        /// </summary>
        /// <param name="activationUrl">The url where the pipline processor responds to. "/bob/" will always be prefixed to the passed url.</param>
        public ResetAdminUser(string activationUrl)
            : base(activationUrl)
        {
        }

        /// <summary>
        /// Processes the HTTP request by reseting the password of the admin user.
        /// </summary>
        /// <param name="context">The HTTP context of the request.</param>
        protected override void ProcessRequest(HttpContext context)
        {
            MembershipUser  user = Membership.GetUser("sitecore\\admin", true);
            var resetPassword = user.ResetPassword();
            user.ChangePassword(resetPassword, "b");
            context.Response.Write("Admin password reset");
        }
    }
}
