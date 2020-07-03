using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BlogNetCore.DataServices.Interfaces
{
    public interface IFileHandler
    {
        string SaveFile(IFormFile file, IEnumerable<string> paths);
        IEnumerable<string> GetFiles(IEnumerable<string> paths);
        IEnumerable<string> GetFiles(string paths);
    }
}
