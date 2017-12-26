using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreAPI.Lib.HttpUti
{
    public class FileContent : MultipartFormDataContent
    {
        public FileContent(string filePath, string apiParamName)
        {
            var fileStream = File.Open(filePath, FileMode.Open);
            var filename = Path.GetFileName(filePath);
            Add(new StreamContent(fileStream), apiParamName, filename);
        }
    }
}
