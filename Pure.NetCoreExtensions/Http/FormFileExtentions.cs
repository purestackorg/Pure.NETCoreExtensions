using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Pure.NetCoreExtensions;
namespace Pure.NetCoreExtensions
{
    public static class FormFileExtentions
    {
        public static byte[] ReadAllBytes(this IFormFile self)
        {
            using (var reader = new BinaryReader(self.OpenReadStream()))
            {
                return reader.ReadBytes(Convert.ToInt32(self.Length));
            }
        }

        public static Task<byte[]> ReadAllBytesAsync(this IFormFile self)
        {
            return Task.Factory.StartNew<byte[]>(() =>
            {
                using (var reader = new BinaryReader(self.OpenReadStream()))
                {
                    return reader.ReadBytes(Convert.ToInt32(self.Length));
                }
            });
        }

        public static string GetFormFieldName(this IFormFile self)
        {
            try
            {
                var tmp = self.ContentDisposition.Split(';');
                foreach (var str in tmp)
                {
                    var tmp2 = str.Trim(' ');
                    var tmp3 = tmp2.Split('=');
                    if (tmp3.Count() == 2 && tmp3[0].ToLower() == "name")
                        return tmp3[1].PopFrontMatch("\"").PopBackMatch("\"");
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static string GetFileName(this IFormFile self)
        {
            try
            {
                var tmp = self.ContentDisposition.Split(';');
                foreach (var str in tmp)
                {
                    var tmp2 = str.Trim(' ');
                    var tmp3 = tmp2.Split('=');
                    if (tmp3.Count() == 2 && tmp3[0].ToLower() == "filename")
                        return tmp3[1].PopFrontMatch("\"").PopBackMatch("\"");
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static bool SaveFile(this IFormFile file)
        {
            try
            {
                var filename = ContentDispositionHeaderValue
                               .Parse(file.ContentDisposition)
                               .FileName
                               .Trim('"');
                string newFileName = Path.GetFileNameWithoutExtension(filename) + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(filename);
                filename = PathHelper.CombineWithRootPath(newFileName);

                using (FileStream fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
            return false;

        }


    }
}
