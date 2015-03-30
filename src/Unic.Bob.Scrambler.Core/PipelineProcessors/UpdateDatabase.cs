

namespace Unic.Bob.Scrambler.Core.PipelineProcessors
{
    using System.Web;
    using Sitecore.Data.Serialization;

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
            context.Response.Write("Updated database.");
        }
    }
}
