using BlogNetCore.DataServices.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
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

        public IEnumerable<string> GetFiles(IEnumerable<string> paths)
        {
            var rootPath = new List<string> { _hostingEnvironment.WebRootPath };
            var filePaths = Path.Combine(rootPath.Union(paths).ToArray());
            if (!Directory.Exists(filePaths))
            {
                return null;
            }

            var relativePath = $"/{string.Join("/", paths)}/";
            return Directory.GetFiles(filePaths).Select(x => new Uri(x)).Select(x => relativePath + x.Segments.Last());
        }

        public IEnumerable<string> GetFiles(string paths)
        {
            var rootPath = _hostingEnvironment.WebRootPath;
            var filePaths = rootPath + "/" + paths;
            if (!Directory.Exists(filePaths))
            {
                return null;
            }

            var relativePath = $"/{string.Join("/", paths)}/";
            return Directory.GetFiles(filePaths).Select(x => new Uri(x)).Select(x => relativePath + x.Segments.Last());
        }

        public string SaveFile(IFormFile file, IEnumerable<string> paths)
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
