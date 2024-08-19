using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRAG.Common.Options
{
    public class TextChunkerOption
    {
        public static int LinesToken { get; set; } = 100;

        public static int ParagraphsToken { get; set; } = 1000;
    }
}
