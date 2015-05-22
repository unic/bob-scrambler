namespace Unic.Bob.Scrambler.Core.Pipelines.HttpRequest
{
    using System.Web;

    public class DisableIndexing : BasePipelineProcessor
    {
        public DisableIndexing(string activationUrl)
            : base(activationUrl)
        {
        }

        protected override void ProcessRequest(HttpContext context)
        {
            Sitecore.Configuration.Settings.Indexing.Enabled = false;
            context.Response.Write("Disabled indexing.");
        }
    }
}
