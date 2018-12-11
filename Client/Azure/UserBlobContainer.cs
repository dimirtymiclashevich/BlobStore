using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Client.Azure
{
    class UserBlobContainer
    {
        private CloudBlobContainer _container;

        public void ConnectToCloud(string containerSasUri)
        {
            _container = new CloudBlobContainer(new Uri(containerSasUri));
        }

        public CloudBlockBlob GetBlobReference(string blobName)
        {
            CloudBlockBlob blob = _container.GetBlockBlobReference(blobName);
            return blob;
        }

        public async Task WriteBlob(CloudBlockBlob blob, string content)
        {
            await blob.UploadTextAsync(content);
        }

        public async Task<string> ReadBlob(CloudBlockBlob blob)
        {
            var result = await blob.DownloadTextAsync();
            return result;
        }

        public async Task<List<IListBlobItem>> GetContainerContent()
        {
            List<IListBlobItem> items = new List<IListBlobItem>();
            BlobContinuationToken continuationToken = null;

            do
            {
                var listingResult = await _container.ListBlobsSegmentedAsync(currentToken: continuationToken);
                continuationToken = listingResult.ContinuationToken;
                items.AddRange(listingResult.Results);
            }
            while (continuationToken != null);

            return items;
        }
    }
}
