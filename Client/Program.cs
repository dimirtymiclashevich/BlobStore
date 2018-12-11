using System;
using System.Threading.Tasks;
using Client.Azure;
using Client.Login;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Client
{
    class Program
    {
        public static DiContainer DiContainer;

        static void Main()
        {
            DiContainer = new DiContainer();

            Do().GetAwaiter().GetResult();
        }

        static async Task Do()
        {
            // Get SAS URI for the user container from the login server

            var loginClient = new LoginClient();
            var sasUri = await loginClient.GetContainerSasUri("HARRY");

            // Connect to cloud and get the user container

            UserBlobContainer blobContainer = new UserBlobContainer();
            blobContainer.ConnectToCloud(sasUri);

            var blob = blobContainer.GetBlobReference("test.txt");

            //  Upload a string of text to a blob

            await blobContainer.WriteBlob(blob, "Gain a thorough understanding of the components of Azure and how you can take advantage of them as a developer.");

            // Download the blob's contents as a string

            string text = await blobContainer.ReadBlob(blob);

            Console.WriteLine($"Received from the server: {text}");

            // List the blobs in the user container

            Console.WriteLine($"\nDirectory of container\n");

            var list = await blobContainer.GetContainerContent();

            foreach (var item in list)
            {
                if (item is CloudBlob cblob)
                {
                    Console.WriteLine($"Name: {cblob.Name}, Uri: {cblob.Uri}");
                }
            }

            Console.ReadLine();
        }
    }
}
