using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace mTangle
{
    class Program
    {
        List<string> LinesOfCode = new List<string>();
        int pos = 0;
        string path;
        string[] lines;

        public Program(string[] Lines, string Path)
        {
            lines = Lines;
            path = Path;
        }

        public bool DoesContainBlockStarter()
        {
            for (int i = 0; i < LinesOfCode.Count; i++)
            {
                if (LinesOfCode[i].Trim().StartsWith("<<"))
                {
                    pos = i;
                    return true;
                }
            }
            return false;
        }

        public void CodeExpander()
        {
                int tmp = pos;
                bool isExtracting = false;
                string BlockContent = LinesOfCode[pos] + "=";
                string AddBlockContent = LinesOfCode[pos] + "+=";
                for (int i = 0; i < lines.Length; i++)
                {
                    if(lines[i].StartsWith("@"))
                        isExtracting = false;
                    if (isExtracting)
                    {
                        LinesOfCode.Insert(pos + 1, lines[i]);
                        pos++;
                    }

                    if (lines[i] == BlockContent.Trim() || lines[i] == AddBlockContent.Trim())
                    {
                        isExtracting = true;
                    }
                }
                LinesOfCode.RemoveAt(tmp);
        }

        public void Caller()
        {
            while (DoesContainBlockStarter())
            {
                CodeExpander();
            }
        }

        public void CodeExtractor()
        {
            bool CurrentlyReadingCode = true;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("@"))
                    CurrentlyReadingCode = false;
                else if (lines[i].StartsWith("<<"))
                    CurrentlyReadingCode = true;
                if (CurrentlyReadingCode)
                {
                    if (lines[i] == "<<Program>>=")
                        continue;
                    else if (lines[i].StartsWith("<<") && (lines[i].EndsWith("=") || lines[i].EndsWith("+=")))  
                    {
                        while(!(lines[i+1].StartsWith("@")))
                            i++;
                    }   
                    else
                        LinesOfCode.Add(lines[i]);
                }
            }
        }

        void GenerateCsProgram()
        {
            int i=0;
            string ActPath = " ";
            while (true)
            {
                if (path[i] == 'm')
                    break;
                else
                {
                    ActPath = ActPath + path[i].ToString();
                    i++;
                }
            }
            ActPath.Trim();
            ActPath = ActPath + "cs";
            StreamWriter sw = new StreamWriter(File.Create(ActPath));
            foreach (var v in LinesOfCode)
            {
                sw.WriteLine(v);
            }
            sw.Close();
        }

        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(args[0]);
            Program ob = new Program(lines, args[0]);
            ob.CodeExtractor();
            ob.Caller();
            ob.GenerateCsProgram();
        }
    }
}
