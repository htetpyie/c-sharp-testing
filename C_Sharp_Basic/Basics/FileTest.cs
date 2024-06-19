using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic.Basics
{
    public class FileTest
    {
        const string ImagePath = "D://Hppm/Images";
        const string base64Image = "";

        public void SaveImage()
        {

            var imageName = Guid.NewGuid().ToString() + ".jpg";

            if (!Directory.Exists(ImagePath))
                Directory.CreateDirectory(ImagePath);

            string fullPath = Path.Combine(ImagePath, imageName);
           
            var imageBytes = Encoding.UTF8.GetBytes(base64Image);
            var imagefile = new FileStream(fullPath, FileMode.Create);
            imagefile.Write(imageBytes, 0, imageBytes.Length);
            imagefile.Flush();
        }

        public void SaveImageV2()
        {
            var imageName = Guid.NewGuid().ToString() + ".jpg";

            byte[] imgBytes = Convert.FromBase64String(base64Image);
            if (!Directory.Exists(ImagePath))
                Directory.CreateDirectory(ImagePath);

            string fullPath = Path.Combine(ImagePath, imageName);

            File.WriteAllBytes(fullPath, imgBytes);
        }
    }
}
