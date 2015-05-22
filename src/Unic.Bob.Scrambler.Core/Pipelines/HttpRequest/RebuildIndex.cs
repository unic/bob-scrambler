namespace Unic.Bob.Scrambler.Core.Pipelines.HttpRequest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Sitecore.Search;

    /// <summary>
    /// Pipeline processor for exposing a HTTP service for rebuilding the search index.
    /// </summary>
    public class RebuildIndex : BasePipelineProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RebuildIndex" /> class.
        /// </summary>
        /// <param name="activationUrl">The url where the pipline processor responds to. "/bob/" will always be prefixed to the passed url.</param>
        public RebuildIndex(string activationUrl)
            : base(activationUrl)
        {
        }

        /// <summary>
        /// Processes the HTTP request by rebuilding the passed indexes.
        /// </summary>
        /// <param name="context">The HTTP context of the request.</param>
        protected override void ProcessRequest(HttpContext context)
        {
            foreach (var index in this.GetIndexNames(context))
            {
                SearchManager.Indexes.Where(_ => _.Name.Equals(index, StringComparison.OrdinalIgnoreCase))
                    .ToList()
                    .ForEach(
                        _ =>
                            {
                                _.Rebuild();
                                context.Response.Write(string.Format("Rebuilt index {0}\n", index));
                            });
            }
        }

        /// <summary>
        /// Returns a list of the index names passed to the HTTP request.
        /// </summary>
        /// <param name="context">The HTTP request.</param>
        /// <returns>The index names.</returns>
        protected virtual IEnumerable<string> GetIndexNames(HttpContext context)
        {
            var indexes = context.Request.QueryString["indexes"];
            if (indexes == null)
            {
                return new List<string>();
            }
            
            return indexes.Split(',').Select(_ => _.Trim()).Where(_ => _ != string.Empty);
        }
    }
}
