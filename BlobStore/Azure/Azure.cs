using System;
using System.Threading.Tasks;
using BlobStore.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BlobStore.Azure
{
    public class Azure
    {
        private static readonly Lazy<Azure> lazy = new Lazy<Azure>(() => new Azure());

        private const string ContainerPrefix = "user-";
        private CloudBlobClient _cloudBlobClient;

        #region Ctor
        private Azure()
        {
            LoginMainAccount();
        }
        #endregion

        public static Azure Instance()
        {
            return lazy.Value;
        }

        /// <summary>
        /// Return the URI string for the container, including the SAS token.
        /// </summary>
        public async Task<string> UserProcess(string userName)
        {
            Guid userId = UserList.GetId(userName.ToUpper());
            if (userId != Guid.Empty)
            {
                CloudBlobContainer userContainer = await GetUserContainer(userId);
                string sasToken = GetSasToken(userContainer.Name);
                return userContainer.Uri + sasToken;
            }
            return "";
        }

        /// <summary>
        /// Return shared access signature (SAS) token for the user container
        /// </summary>
        private string GetSasToken(string containerName)
        {
            CloudBlobContainer container = _cloudBlobClient.GetContainerReference(containerName);

            var sharedPolicy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read |
                SharedAccessBlobPermissions.Write |
                SharedAccessBlobPermissions.Delete |
                SharedAccessBlobPermissions.List |
                SharedAccessBlobPermissions.Add,

                SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-1), 
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24)
            };

            string key = container.GetSharedAccessSignature(sharedPolicy);
            return key;
        }

        /// <summary>
        /// Return (create if not exist) a user container
        /// </summary>
        private async Task<CloudBlobContainer> GetUserContainer(Guid userGuid)
        {
            string containerName = ContainerPrefix + userGuid;

            // Get a reference to a container.
            CloudBlobContainer container = _cloudBlobClient.GetContainerReference(containerName);

            try
            {
                // Create the container if it does not already exist.
                await container.CreateIfNotExistsAsync();
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return container;
        }

        private void LoginMainAccount()
        {
            string storageConnectionString = "UseDevelopmentStorage=true";

            // Check whether the connection string can be parsed.
            if (CloudStorageAccount.TryParse(storageConnectionString, out var storageAccount))
            {
                try
                {
                    // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
                    _cloudBlobClient = storageAccount.CreateCloudBlobClient();
                }
                catch (StorageException ex)
                {
                    Console.WriteLine("Error returned from the service: {0}", ex.Message);
                }
            }
        }
    }
}
