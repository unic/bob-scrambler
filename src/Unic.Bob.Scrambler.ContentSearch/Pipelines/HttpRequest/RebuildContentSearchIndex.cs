namespace Unic.Bob.Scrambler.ContentSearch.Pipelines.HttpRequest
{
    using System;
    using System.Linq;
    using System.Web;

    using Sitecore.ContentSearch;

    using Unic.Bob.Scrambler.Core.Pipelines.HttpRequest;

    /// <summary>
    /// Pipeline processor for exposing a HTTP service for rebuilding the search index.
    /// </summary>
    public class RebuildContentSearchIndex : RebuildIndex
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="RebuildContentSearchIndex" /> class.
        /// </summary>
        /// <param name="activationUrl">The url where the pipline processor responds to. "/bob/" will always be prefixed to the passed url.</param>
        public RebuildContentSearchIndex(string activationUrl)
            : base(activationUrl)
        {
        }

        /// <summary>
        /// Processes the HTTP request by rebuilding the passed indexes.
        /// </summary>
        /// <param name="context">The HTTP context of the request.</param>
        protected override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);

            foreach (var index in this.GetIndexNames(context))
            {
                ContentSearchManager.Indexes.Where(_ => _.Name.Equals(index, StringComparison.OrdinalIgnoreCase))
                    .ToList()
                    .ForEach(
                        _ =>
                        {
                            _.Rebuild();
                            context.Response.Write(string.Format("Rebuilt index {0}\n", index));
                        });
            }
        }
    }
}
