using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot
{
    class DataSubscribers
    { 
        public List<FileInfo> Photos;
        public List<FileInfo> TikTok;
        public long Id;
        public string Name;

        public DataSubscribers(long id, string name, List<FileInfo> photos, List<FileInfo> tikTok)
        {
            Id = id;
            Name = name;
            Photos = photos;
            TikTok = tikTok;
        }

        public void Delete(int idFile, string typeFile)
        {
            if (typeFile == "photo")
            {
                Photos.RemoveAt(idFile);
            }
            else if (typeFile == "tiktok")
            {
                TikTok.RemoveAt(idFile);
            }
        }
    }
}
