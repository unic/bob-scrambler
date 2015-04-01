using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unic.Bob.Scrambler.ContentSearch.Pipelines.HttpRequest
{
    using System.Web;

    using Sitecore.ContentSearch;

    using Unic.Bob.Scrambler.Core.Pipelines.HttpRequest;

    public class RebuildContentSerachIndex : RebuildIndex
    {
        public RebuildContentSerachIndex(string activationUrl)
            : base(activationUrl)
        {
        }

        protected override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);

            context.Response.Write("New");
            foreach (var index in ContentSearchManager.Indexes)
            {
                context.Response.Write(index.Name);

            }
        }
    }
}
