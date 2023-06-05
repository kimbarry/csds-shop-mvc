using Minio;

namespace CsdsShop.Services
{
    public class MinioService
    {
        public async Task<List<string>> GetBuckets()
        {
            string endpoint = "minio:9000";
            string accessKey = "admin";
            string secretKey = "D@nceL1fE";
            bool secure = false;

            // Initialize the client with access credentials.
            MinioClient minio = new MinioClient()
                    .WithEndpoint(endpoint)
                    .WithCredentials(accessKey, secretKey)
                    .WithSSL(secure)
                    .Build();

            // Create an async task for listing buckets.
            var getListBucketsTask = await minio.ListBucketsAsync().ConfigureAwait(false);
            var buckets = new List<string>();
            // Iterate over the list of buckets.
            foreach (var bucket in getListBucketsTask.Buckets)
            {
                buckets.Add(bucket.Name);
            }
            return buckets;
        }
    }
}
