using System;
using System.Data;
using System.Linq;
using Ayehu.Sdk.ActivityCreation.Interfaces;
using Ayehu.Sdk.ActivityCreation.Extension;
using Microsoft.Azure.Management.Compute.Fluent;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace Ayehu.Sdk.ActivityCreation
{
    public class StartVMInstance : IActivity
    {
        /// <summary>
        /// APPLICATION (CLIENT) ID
        /// </summary>
        public string appId;

        /// <summary>
        /// Directory (tenant) ID
        /// </summary>
        public string tenantId;

        /// <summary>
        /// Client secret
        /// </summary>
        /// <remarks>
        /// A secret string that the application uses to prove its identity when requesting a token. 
        /// Also can be referred to as application password.
        /// </remarks>
        public string secret;

        public string subscriptionId;
        public string vmName;

        ICustomActivityResult IActivity.Execute()
        {
            DataTable dt = new DataTable("resultSet");
            dt.Columns.Add("Result");

            var azure = this.GetAzure();
            var subscription = azure.GetCurrentSubscription();
            var vm = azure.VirtualMachines.List().Where(x => x.Name.ToLower() == vmName.ToLower()).FirstOrDefault();

            if (vm.PowerState == PowerState.Deallocated || vm.PowerState == PowerState.Stopped)
            {
                vm.Start();
            }

            dt.Rows.Add("Success");
            return this.GenerateActivityResult(dt);
        }

        private IAzure GetAzure()
        {
            var credentials = SdkContext.AzureCredentialsFactory.FromServicePrincipal(appId, secret, tenantId, AzureEnvironment.AzureGlobalCloud);
            var azure = Azure
                .Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(credentials)
                .WithSubscription(subscriptionId);
            
            return azure;
        }
    }
}
