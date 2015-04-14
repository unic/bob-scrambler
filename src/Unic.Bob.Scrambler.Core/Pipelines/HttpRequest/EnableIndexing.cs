namespace Unic.Bob.Scrambler.Core.Pipelines.HttpRequest
{
    using System.Web;

    public class ReEnableIndexing : BasePipelineProcessor
    {
        public static bool OrignalIndexingEnabled = Sitecore.Configuration.Settings.Indexing.Enabled;

        public ReEnableIndexing(string activationUrl)
            : base(activationUrl)
        {
        }

        protected override void ProcessRequest(HttpContext context)
        {
            if (OrignalIndexingEnabled)
            {
                Sitecore.Configuration.Settings.Indexing.Enabled = true;
                context.Response.Write("Re-enabled indexing.");
            }
            else
            {
                context.Response.Write("Indexing was already disabled at start, so no action is required.");
            }
        }
    }
}
