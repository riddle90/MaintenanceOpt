using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess
{
    public static class FileReader
    {
        public static async Task<TextReader> GetReader(string filename)
        {
            try
            {
                var folder = "Data";
                var path = Path.Combine(folder, filename );
                return await Task.FromResult<TextReader>(File.OpenText(path));
            }
            catch (FileNotFoundException e)
            {
                throw new Exception($"File associated with the run couldn't be found: {e.FileName}");
                
            }
        }
        
    }
}