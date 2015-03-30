namespace Unic.Bob.Scrambler.Core.Pipelines.HttpRequest
{
    using System.Web;

    using Sitecore.Data.Serialization;

    /// <summary>
    /// A pipeline processor for which performs an "update database" when the specified url is called.
    /// </summary>
    public class UpdateDatabase : BasePipelineProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDatabase" /> class.
        /// </summary>
        /// <param name="activationUrl">The url where the pipline processor responds to. "/bob/" will always be prefixed to the passed url.</param>
        public UpdateDatabase(string activationUrl)
            : base(activationUrl)
        {
        }

        /// <summary>
        /// Processes the HTTP request for updating the database.
        /// </summary>
        /// <param name="context">The HTTP contxt.</param>
        protected override void ProcessRequest(HttpContext context)
        {
            var options = new LoadOptions();
            options.DisableEvents = true;
            Manager.LoadTree(PathUtils.Root, options);
            context.Response.Write("Updated database.");
        }
    }
}
