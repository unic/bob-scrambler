using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unic.Bob.Scrambler.Core.Pipelines.HttpRequest
{
    using System.Web;

    using Sitecore.Data;
    using Sitecore.Globalization;
    using Sitecore.Publishing;

    public class FullPublish : BasePipelineProcessor
    {
        public FullPublish(string activationUrl)
            : base(activationUrl)
        {
        }

        protected override void ProcessRequest(HttpContext context)
        {
            var webDb = Sitecore.Configuration.Factory.GetDatabase("web");
            var masterDb = Sitecore.Configuration.Factory.GetDatabase("master");
            foreach (Language language in masterDb.Languages)
            {
                //loops on the languages and do a full republish on the whole sitecore content tree
                var options = new PublishOptions(masterDb, webDb, PublishMode.Full, language, DateTime.Now)
                                  {
                                      RootItem
                                          =
                                          masterDb
                                          .Items
                                          [
                                              "/sitecore"
                                          ],
                                      RepublishAll
                                          =
                                          true,
                                      Deep =
                                          true
                                  };

                var myPublisher = new Publisher(options);
                myPublisher.Publish();
                context.Response.Write(string.Format("Published {0}\n", language.Name));
            }
        }
    }
}
