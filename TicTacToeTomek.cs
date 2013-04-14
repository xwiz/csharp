using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace GCJ_TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = @"C:\Users\Xwizard\Downloads\A-large";
            string[] lines = File.ReadAllLines(file + ".in");
            int tcases = int.Parse(lines[0].Trim());
            bool NotComplete = false, Xwin = false, Owin = false;
            
            for (int i = 0; i < tcases; i++)
            {                
                string[] linesdata = lines.Skip((i * 4) + i +1).Take(4).ToArray();
                string[] vlinedata = new string[4];
                string[] dlinedata = new string[] { linesdata[0][0].ToString() + linesdata[1][1].ToString() + linesdata[2][2].ToString() + linesdata[3][3].ToString(), linesdata[0][3].ToString() + linesdata[1][2].ToString() + linesdata[2][1].ToString() + linesdata[3][0].ToString() };

                //Check for horizontal symmetry and store vertical data
                foreach (string line in linesdata)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        vlinedata[j] += line[j];
                    }
                    if (count(line, 'O') > 3 || (count(line, 'O') > 2 && count(line, 'T') > 0))
                    {
                        Owin = true;
                    }
                    else if (count(line, 'X') > 3 || (count(line, 'X') > 2 && count(line, 'T') > 0))
                    {
                        Xwin = true;
                    }
                    else if (count(line, '.') > 0)
                    {
                        NotComplete = true;
                    }
                }

                foreach (string line in vlinedata)
                {
                    if (count(line, 'O') > 3 || (count(line, 'O') > 2 && count(line, 'T') > 0))
                    {
                        Owin = true;
                    }
                    else if (count(line, 'X') > 3 || (count(line, 'X') > 2 && count(line, 'T') > 0))
                    {
                        Xwin = true;
                    }
                    else if (count(line, '.') > 0)
                    {
                        NotComplete = true;
                    }
                }

                foreach (string line in dlinedata)
                {
                    if (count(line, 'O') > 3 || (count(line, 'O') > 2 && count(line, 'T') > 0))
                    {
                        Owin = true;
                    }
                    else if (count(line, 'X') > 3 || (count(line, 'X') > 2 && count(line, 'T') > 0))
                    {
                        Xwin = true;
                    }
                    else if (count(line, '.') > 0)
                    {
                        NotComplete = true;
                    }
                }

                string output = string.Format("Case #{0}: ", i + 1);
                
                if (Xwin)
                {
                    output += "X won";
                }
                else if (Owin)
                {
                    output += "O won";
                }
                else if (NotComplete)
                {
                    output += "Game has not completed";
                }
                else
                {
                    output += "Draw";
                }

                File.AppendAllText(file + ".out", output + Environment.NewLine);
                Console.WriteLine(output);
                NotComplete = false;
                Xwin = false;
                Owin = false;
            }
        }

        static int count(string text, Char c)
        {
            return text.Split(c).Length - 1; ;
        }
    }
}
