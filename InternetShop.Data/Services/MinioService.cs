using CSharpFunctionalExtensions;
using InternetShop.Application.Interfaces;
using InternetShop.Domain.ValueObjects;
using Minio;
using Minio.DataModel.Args;

namespace InternetShop.Data.Services
{
    public class MinioService : IMinioService
    {
        private readonly IMinioClient _minioClient;

        public MinioService(IMinioClient minioClient)
        {
            _minioClient = minioClient;
        }
        public async Task<Stream> GetImage(string fileName)
        {
            var imageStream = new MemoryStream();

            var getObjectArgs = new GetObjectArgs()
                .WithBucket(MainPhoto.BUCKET_NAME)
                .WithFile(fileName)
                .WithCallbackStream(stream => stream.CopyTo(imageStream));

            await _minioClient.GetObjectAsync(getObjectArgs);

            return imageStream;
        }

        public async Task<Result> UploadImage(Stream stream, MainPhoto mainPhoto)
        {
            var bucketArgs = new BucketExistsArgs()
            .WithBucket(MainPhoto.BUCKET_NAME);

            var bucketExists = await _minioClient.BucketExistsAsync(bucketArgs);
            if (bucketExists == false)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(MainPhoto.BUCKET_NAME);

                await _minioClient.MakeBucketAsync(makeBucketArgs);
            }

            var putObjectAtgs = new PutObjectArgs()
                .WithBucket(MainPhoto.BUCKET_NAME)
                .WithObject(mainPhoto.Path)
                .WithContentType("application/octet-stream")
                .WithStreamData(stream)
                .WithObjectSize(stream.Length);

            var response = await _minioClient.PutObjectAsync(putObjectAtgs);

            var statObjectArgs = new StatObjectArgs()
                .WithBucket(MainPhoto.BUCKET_NAME)
                .WithObject(mainPhoto.Path);

            var stat = await _minioClient.StatObjectAsync(statObjectArgs);

            var imageStream = new MemoryStream();

            var getObjectArgs = new GetObjectArgs()
                .WithBucket(MainPhoto.BUCKET_NAME)
                .WithObject(mainPhoto.Path)
                .WithCallbackStream(s =>
                {
                    s.CopyTo(imageStream);
                });

            var result = await _minioClient.GetObjectAsync(getObjectArgs);

            return Result.Success();
        }
    }
}
