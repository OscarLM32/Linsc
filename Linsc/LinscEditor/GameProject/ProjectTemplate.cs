using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LinscEditor.GameProject
{
    [DataContract]
    public class ProjectTemplate
    {
        [DataMember]
        public string ProjectType { get; set; }
        [DataMember]
        public string ProjectFile { get; set; }
        [DataMember]
        public List<string> Folders { get; set; }

        public string IconFilePath { get; set; }
        public byte[] Icon { get; set; }
        public string ThumbnailFilePath { get; set; }
        public byte[] Thumbnail { get; set; }

        public string ProjectFilePath { get; set; }
    }
}
