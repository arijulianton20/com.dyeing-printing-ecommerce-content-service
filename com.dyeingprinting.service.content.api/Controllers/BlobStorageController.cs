using com.dyeingprinting.service.content.data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace com.dyeingprinting.service.content.api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/bloob-storage")]
    public class BlobStorageController : ControllerBase
    {
        private readonly IOptions<Storage> _config;
        private readonly ILogger<WebContentController> _logger;

        public BlobStorageController(IOptions<Storage> config, ILogger<WebContentController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ListFiles()
        {
            List<string> blobs = new List<string>();
            try
            {
                if (CloudStorageAccount.TryParse(_config.Value.StorageConnection, out CloudStorageAccount storageAccount))
                {
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                    CloudBlobContainer container = blobClient.GetContainerReference(_config.Value.Container);

                    BlobResultSegment resultSegment = await container.ListBlobsSegmentedAsync(null);
                    foreach (IListBlobItem item in resultSegment.Results)
                    {
                        if (item.GetType() == typeof(CloudBlockBlob))
                        {
                            CloudBlockBlob blob = (CloudBlockBlob)item;
                            blobs.Add(blob.Name);
                        }
                        else if (item.GetType() == typeof(CloudPageBlob))
                        {
                            CloudPageBlob blob = (CloudPageBlob)item;
                            blobs.Add(blob.Name);
                        }
                        else if (item.GetType() == typeof(CloudBlobDirectory))
                        {
                            CloudBlobDirectory dir = (CloudBlobDirectory)item;
                            blobs.Add(dir.Uri.ToString());
                        }
                    }
                }

                return Ok(new { StatusCode = (int)HttpStatusCode.OK, Data = blobs });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> InsertFile(IFormFile stream)
        {
            try
            {
                if (CloudStorageAccount.TryParse(_config.Value.StorageConnection, out CloudStorageAccount storageAccount))
                {
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                    CloudBlobContainer container = blobClient.GetContainerReference(_config.Value.Container);

                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    var stringChars = new char[8];
                    var random = new Random();
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }

                    var finalString = new String(stringChars);
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(finalString + stream.FileName);

                    await blockBlob.UploadFromStreamAsync(stream.OpenReadStream());

                    var result = new { ImageUri = blockBlob?.Uri.ToString() };

                    return Ok(new { StatusCode = (int)HttpStatusCode.OK, Data = result }); 
                }

                throw new Exception("Failure upload image!");
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet("Download/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                if (CloudStorageAccount.TryParse(_config.Value.StorageConnection, out CloudStorageAccount storageAccount))
                {
                    CloudBlobClient BlobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = BlobClient.GetContainerReference(_config.Value.Container);

                    if (await container.ExistsAsync())
                    {
                        CloudBlob file = container.GetBlobReference(fileName);

                        if (await file.ExistsAsync())
                        {
                            await file.DownloadToStreamAsync(ms);
                            Stream blobStream = file.OpenReadAsync().Result;
                            return File(blobStream, file.Properties.ContentType, file.Name);
                        }
                        else
                            throw new Exception("File does not exist");
                        //else
                        //{
                        //    return Content("File does not exist");
                        //}
                    }
                    else
                        throw new Exception("Container does not exist");
                    //else
                    //{
                    //    return Content("Container does not exist");
                    //}
                }
                else
                    throw new Exception("Error opening storage");
                //else
                //{
                //    return Content("Error opening storage");
                //}
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("Delete/{fileName}")]
        [HttpGet]
        public async Task<IActionResult> DeleteFile(string fileName)
        {
            try
            {
                if (CloudStorageAccount.TryParse(_config.Value.StorageConnection, out CloudStorageAccount storageAccount))
                {
                    CloudBlobClient BlobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = BlobClient.GetContainerReference(_config.Value.Container);

                    if (await container.ExistsAsync())
                    {
                        CloudBlob file = container.GetBlobReference(fileName);

                        if (await file.ExistsAsync())
                        {
                            await file.DeleteAsync();
                        }
                    }
                }
                //return true;
                return Ok(new { StatusCode = (int)HttpStatusCode.OK, Data = new { } , Message = HttpStatusCode.OK.ToString() });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
                //return false;
            }
        }
    }
}