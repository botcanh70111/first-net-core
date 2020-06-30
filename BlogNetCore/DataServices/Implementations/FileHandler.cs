using BlogNetCore.DataServices.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BlogNetCore.DataServices.Implementations
{
    public class FileHandler : IFileHandler
    {
        private IWebHostEnvironment _hostingEnvironment;

        public FileHandler(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public string SaveFile(IFormFile file, List<string> paths)
        {
            var rootPath = new List<string> { _hostingEnvironment.WebRootPath };
            var savePath = Path.Combine(rootPath.Union(paths).ToArray());
            var webFolder = "/" + string.Join("/", paths) + "/";

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            var filePath = Path.Combine(savePath, file.FileName);

            using (var stream = File.Create(filePath))
            {
                file.CopyTo(stream);
            }

            return webFolder + file.FileName;
        }
    }
}
