

namespace Unic.Bob.Scrambler.Core.PipelineProcessors
{
    using System;
    using System.Web;

    using Sitecore.Data.Serialization;
    using Sitecore.Pipelines.HttpRequest;
    using Sitecore.SecurityModel;

    using SecurityState = Unic.Bob.Scrambler.Core.Security.SecurityState;

    public class UpdateDatabase : BasePipelineProcessor
    {
        public UpdateDatabase(string activationUrl)
            : base(activationUrl)
        {
        }


        protected override void ProcessRequest(HttpContext context)
        {
            var options = new LoadOptions();
            options.DisableEvents = true;
            Manager.LoadTree(PathUtils.Root, options);
            context.Response.Write("<pre>Updated database.</pre>");

        }
    }
}
