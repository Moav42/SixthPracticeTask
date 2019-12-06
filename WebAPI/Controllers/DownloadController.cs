using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        [HttpGet("sync")]
        public IActionResult DownloudSync()
        {
            _logger.Trace("Sync dowloud");

            byte[] file = null;
            Uri uri = new Uri("https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-3.0");

            using (WebClient wc = new WebClient())
            {
                for (int i = 0; i < 10; i++)
                {
                    file = wc.DownloadData(uri);

                }
            }

            return File(file, "APPLICATION/octet-stream", "Dowloded File");
        }

        [HttpGet("async")]
        public async Task<IActionResult> DownloudAsync()
        {
            _logger.Trace("Async dowloud");

            Uri uri = new Uri("https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-3.0");
            byte[] file = null;

            using (WebClient wc = new WebClient())
            {
                for (int i = 0; i < 10; i++)
                {
                    file = await wc.DownloadDataTaskAsync(uri);
                }
            }

            return File(file, "APPLICATION/octet-stream");
        }

        [HttpGet("parallel")]
        public IActionResult DownloudParallel()
        {
            _logger.Trace("Parallel dowloud");

            Uri uri = new Uri("https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-3.0");
            byte[] file = null;

            Parallel.For(0, 10, s =>
            {
                using (WebClient wc = new WebClient())
                {
                    file = wc.DownloadData(uri);
                }

            });

            return File(file, "APPLICATION/octet-stream");
        }

    }
}