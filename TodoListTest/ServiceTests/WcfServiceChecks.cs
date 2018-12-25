using ApprovalTests;
using ApprovalTests.Reporters;
using SoapService;
using System;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;
#pragma warning disable IDE1006 // Naming Styles
namespace ServiceTests
{
    public class WcfServiceChecks
    {
        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void ensure_wsdl_is_stable()
        {
            const string address = "http://localhost:11111/hello";
            using (var host = CreateAndOpenHost(address))
            {
                var wsdl = DownloadXml($"{address}?wsdl");
                var sb = new StringBuilder().AppendLine("<content>").AppendLine(wsdl);

                sb = Regex.Matches(wsdl, @"schemaLocation=""(?<url>[^""]+)""")
                                        .Cast<Match>()
                                        .Select(_ => _.Groups["url"].Value)
                                        .Aggregate(sb, (b, c) => b.AppendLine($"<c text=\"{c}\"></c>").AppendLine(DownloadXml(c)));

                sb.Append("</content>");

                Approvals.VerifyXml(sb.ToString());
            }
        }

        private static ServiceHost CreateAndOpenHost(string address)
        {
            var baseAddress = new Uri(address);
            var host = new ServiceHost(typeof(MyService), baseAddress);
            AddMetadata(host);
            host.Open();
            return host;
        }

        private static void AddMetadata(ServiceHost host)
        {
            var smb = new ServiceMetadataBehavior
            {
                HttpGetEnabled = true
            };
            smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
            host.Description.Behaviors.Add(smb);
        }

        private static string DownloadXml(string address)
        {
            var client = new HttpClient();
            return client.GetStringAsync(address).Result.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
        }
    }
}
#pragma warning restore IDE1006 // Naming Styles
