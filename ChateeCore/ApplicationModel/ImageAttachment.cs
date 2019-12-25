using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class ImageAttachment
    {
        #region Public Properties
        public string ImageID { get; set; }
        public string ImagePath { get; set; }
        public bool IsNewImage { get; set; }
        #endregion
        // TODO: Check in Database if image is new 
        #region Constructors
        public ImageAttachment()
        {

        }
        public ImageAttachment(string imagePath)
        {
            ImagePath = imagePath;
        }
        public ImageAttachment(string imageID, string imagePath)
        {
            ImageID = imageID;
            ImagePath = imagePath;
        }
        public ImageAttachment(ImageAttachment file)
        {
            ImageID = file.ImageID;
            ImagePath = file.ImagePath;
        }
        #endregion
    }
}
