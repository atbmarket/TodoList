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

[assembly: FrontLoadedReporter(typeof(BeyondCompare4Reporter))]
namespace ServiceTests
{
    public class WcfServiceChecks
    {
        [Fact]
        [UseReporter(typeof(BeyondCompare4Reporter))]
        public void ensure_wsdl_is_stable()
        {
            const string address = "http://localhost:11111/hello";
            using (var host = CreateAndOpenHost(address))
            {
                var wsdl = new HttpClient().GetStringAsync($"{address}?singleWsdl").Result;

                Approvals.VerifyXml(wsdl);
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
    }
}
#pragma warning restore IDE1006 // Naming Styles
