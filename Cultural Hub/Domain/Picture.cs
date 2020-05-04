using System;
using System.Collections.Generic;

namespace Domain
{
    /*public class Picture
    {
        public Uri Link { get; }

        public Picture(Uri link)
        {
            Link = link;
        }
    }*/

    public class PictureCollectin
    {
        private readonly List<Uri> picturesList;

        public string EventId { get; }
        public IReadOnlyList<Uri> PicturesList => picturesList;
        public void AddPicture(Uri link)
        {
            picturesList.Add(link);
        }
        public void RemovePicture(Uri link)
        {
            picturesList.Remove(link);
        }
    }
}
