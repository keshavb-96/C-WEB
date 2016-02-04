using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mTangle
{
    class BlockOfCode
    {
        public string Name;
        public bool Used;
        public List<LinesOfCode> lines = new List<LinesOfCode>();
        public BlockOfCode()
        {
            Used = false;
        }
    }

    class LinesOfCode
    {
        public string Text;
        public string File;
        public int Line;
    }
}
