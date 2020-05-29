using System;

namespace FL.Sendgrid.Implementation.Standard.Configuration.Models
{
    public class EmailItemConfiguration
    {
        public Guid LangaugeId { get; set; }

        public string TemplateId { get; set; }

        public string TypeEmail { get; set; }

        public string SupportName { get; set; }

        public string SupportEmail { get; set; }
    }
}
