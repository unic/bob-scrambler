namespace Unic.Bob.Scrambler.Core.Pipelines.HttpRequest
{
    using System;
    using System.Web;

    using Sitecore.Configuration;
    using Sitecore.Globalization;
    using Sitecore.Publishing;

    /// <summary>
    /// Pipeline processor for exposing a HTTP service to perform a full publish.
    /// </summary>
    public class FullPublish : BasePipelineProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FullPublish" /> class.
        /// </summary>
        /// <param name="activationUrl">The url where the pipline processor responds to. "/bob/" will always be prefixed to the passed url.</param>
        public FullPublish(string activationUrl)
            : base(activationUrl)
        {
        }

        /// <summary>
        /// Processes the HTTP request and performs a full publish.
        /// </summary>
        /// <param name="context">The context of the HTTP request.</param>
        protected override void ProcessRequest(HttpContext context)
        {
            var webDb = Factory.GetDatabase("web");
            var masterDb = Factory.GetDatabase("master");
            foreach (Language language in masterDb.Languages)
            {
                var options = new PublishOptions(masterDb, webDb, PublishMode.Full, language, DateTime.Now)
                                  {
                                      RootItem = masterDb.Items["/sitecore"],
                                      RepublishAll = true,
                                      Deep = true
                                  };

                var myPublisher = new Publisher(options);
                myPublisher.Publish();
                context.Response.Write(string.Format("Published {0}\n", language.Name));
            }
        }
    }
}
