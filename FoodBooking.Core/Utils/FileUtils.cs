using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodBooking.Core.Utils
{
    public static class FileUtils
    {
        public static async Task<bool> CreateFile(IFormFile formFile, string filePath)
        {
            if (formFile != null || formFile?.Length>0)
            {
                if (!Directory.Exists(filePath))
                {
                    new FileInfo(filePath).Directory?.Create();
                 
                }
                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                return true;
            }
            return false;
        }
    }
}
