namespace Unic.Bob.Scrambler.Core.Pipelines.HttpRequest
{
    using System.Web;

    public class ReEnableIndexing : BasePipelineProcessor
    {
        /// <summary>
        /// When Sitecore is starting this is set to the original value configured in the settings file.
        /// So when the service is called we only reenable indexing, if it was set to true. 
        /// </summary>
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
