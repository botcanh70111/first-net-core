using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BlogNetCore.DataServices.Interfaces
{
    public interface IFileHandler
    {
        string SaveFile(IFormFile file, List<string> paths);
    }
}
