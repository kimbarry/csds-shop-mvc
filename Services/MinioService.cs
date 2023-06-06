using Minio;
using System.IO;

namespace CsdsShop.Services
{
    public class MinioService
    {
        private readonly MinioClient _client;
        public MinioService()
        {
                _client = new MinioClient()
                    .WithEndpoint("minio:9000")
                    .WithCredentials("admin", "D@nceL1fE")
                    .WithSSL(false)
                    .Build();
        }
        const string BUCKET = "images";
        public string PutObj(IFormFile imgData, int sellerId, int itemId)
        {
            // Check Exists bucket
            bool found =  _client.BucketExistsAsync(new BucketExistsArgs()
                .WithBucket(BUCKET)).Result;
            if (!found)
            {
                // if bucket not Exists,make bucket
                _client.MakeBucketAsync(new MakeBucketArgs().WithBucket(BUCKET));
            }
            var filename = sellerId.ToString() +"-"+ itemId.ToString()+".jpeg";
            PutObjectArgs putObjectArgs = new PutObjectArgs()
                                      .WithBucket(BUCKET)
                                      .WithObject(filename)
                                      .WithStreamData(imgData.OpenReadStream())
                                      .WithObjectSize(imgData.Length)
                                      .WithContentType("image/jpg");
            var result = _client.PutObjectAsync(putObjectArgs).Result;
            
            // upload object
            return filename;
        }
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
