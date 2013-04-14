using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Numerics;
using Emil.GMP;

namespace GCJ_Palindrome4
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = @"C:\Users\Xwizard\Downloads\C-small-attempt0";
            string[] lines = File.ReadAllLines(file + ".in");
            int tcases = int.Parse(lines[0].Trim());
            sections = tcases / 4;

            dlines = new[] { lines.Skip(0).Take(sections), lines.Skip(sections * 1).Take(sections), lines.Skip(sections * 2).Take(sections), lines.Skip(sections * 3).Take(lines.Length - (sections * 3)) };

            Thread t1 = new Thread(solveSectionT);
            t1.Start(0);

            Thread t2 = new Thread(solveSectionT);
            t2.Start(1);

            Thread t3 = new Thread(solveSectionT);
            t3.Start(2);

            Thread t4 = new Thread(solveSectionT);
            t4.Start(3);

            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();

            File.WriteAllText(file + "_vok.out", dsections[0] + dsections[1] + dsections[2] + dsections[3]);
        }

        private static void solveSectionT(object i)
        {
            int sc = (int)i;
            solveSection(dlines[sc], ref dsections[sc], sections * sc);
        }

        private static string[] dsections = new string[] { "", "", "", "" };
        private static int sections;
        private static IEnumerable<string>[] dlines;

        private static void solveSection(IEnumerable<string> lines, ref string section, int offset)
        {
            for (int i = 0; i < lines.Count(); i++)
            {
                int count = 0;
                string[] data = lines.ElementAt(i).Split(' ');
                if (data.Length == 1) continue;
                BigInt start = new BigInt(data[0]);
                BigInt end = new BigInt(data[1]);
                BigInt closest;
                BigInt farthest;

                BigInt d = start.Sqrt();
                while (d.Square() < start) d++;
                while (!IsSymmetric(d.ToString()))
                {
                    d++;
                }
                closest = d;

                BigInt f = end.Sqrt();
                while (!IsSymmetric(f.ToString()))
                {
                    f--;
                }

                farthest = f;

                for (BigInt j = closest; j <= farthest; j++)
                {
                    if (IsSymmetric(j.ToString()))
                    {
                        BigInt sqr = j * j;
                        if (IsSymmetric(sqr.ToString()))
                        {
                            count++;
                            section += sqr + Environment.NewLine;
                        }
                    }
                }

                section += string.Format("Case #{0}: {1}{2}", i + offset, count, Environment.NewLine);
            }
        }

        static bool IsSymmetric(string s)
        {
            if (s.Length == 1) return true;
            Char[] schar = s.ToCharArray();
            for (int i = 0; i < (s.Length / 2); i++)
            {
                if (schar[i] != schar[schar.Length - (i + 1)])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
