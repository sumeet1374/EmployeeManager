using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model
{
    public class Document
    {
        public int Id { get; set; }
        public string FileName { get; set; }

        public string Type { get; set; }

        public byte[] FileContent { get; set; }
    }
}
