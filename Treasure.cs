using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GCJ_Treasure
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = @"C:\Users\Xwizard\Downloads\D-small-practice";
            if (File.Exists(file + ".out")) File.Delete(file + ".out");
            string[] lines = File.ReadAllLines(file + ".in");
            ushort tcases = ushort.Parse(lines[0].Trim());
            ushort caseline = 1;

            for (int i = 0; i < tcases; i++)
            {
                string caseStatement = "Case #" + (i + 1) + ":";
                string[] casedata = lines[caseline].Split(' ');
                ushort nkeys = ushort.Parse(casedata[0]);
                ushort nchests = ushort.Parse(casedata[1]);
                caseline++;
                string[] keysdata = lines[caseline].Split(' ');
                ushort[] keys;
                if(keysdata.Length == 0) keys = new[]{ushort.Parse(keysdata[0])};
                keys = Array.ConvertAll(keysdata, Convert.ToUInt16);
                
                caseline++;
                Chest[] chests = new Chest[nchests];

                for (ushort u = 0; u < nchests; u++)
                {
                    string[] chestdata = lines[caseline].Split(' ');
                    string[] chestkeysdata = chestdata.Skip(2).Take(chestdata.Length).ToArray();
                    ushort[] chestkeys = Array.ConvertAll(chestkeysdata, Convert.ToUInt16);
                    Chest c = new Chest(ushort.Parse(chestdata[0]), chestkeys);
                    chests[u] = c;
                    caseline++;
                }
                
                List<Chest> chestList = chests.ToList();
                List<Chest> chestListBack = new List<Chest>();
                chestListBack.AddRange(chestList);
                Dictionary<int, Chest> opableChests = new Dictionary<int, Chest>();
                List<Chest> emptyChests = new List<Chest>();
                Queue<ushort> usableKeys = new Queue<ushort>(keys);
                List<int> openedChests = new List<int>();

                while(usableKeys.Count > 0 && openedChests.Count < chests.Length)
                {
                    ushort k = usableKeys.Dequeue();
                    opableChests.Clear();
                    Chest richest = null;
                    int richestIndex = 0;
                    int chestIndex = 0;

                    //Iterate over chests to find the ones that are openable/empty and store
                    for (int c = 0; c < chestList.Count; c++)
                    {
                        Chest cs = chestList[c];
                        if (richest == null)
                        {
                            if (cs.keyType == k)
                            {
                                richest = cs;
                                richestIndex = GetIndex(chestListBack, cs);
                                if (richestIndex == -1) { richest = new Chest(0); }
                                chestIndex = c;
                            }
                        }
                        else if (cs.keyType == k)
                        {
                            if (cs.numKeys >= richest.numKeys)
                            {
                                if(richestIndex < GetIndex(chestListBack, cs))
                                richest = cs;
                                richestIndex = GetIndex(chestListBack, cs);
                                chestIndex = c;
                            }
                            else if (cs.numKeys == richest.numKeys)
                            {
                            }
                        }
                    }

                    if(richest != null){
                        openedChests.Add(richestIndex + 1);
                        chestList.RemoveAt(chestIndex);
                        foreach (ushort ck in richest.keys)
                        {
                            usableKeys.Enqueue(ck);
                        }
                        chestListBack[richestIndex] = null;
                    }
                }

                if (openedChests.Count != chests.Length)
                {
                    File.AppendAllText(file + ".out", caseStatement + " IMPOSSIBLE" + Environment.NewLine);
                }
                else
                {
                    string result = "";
                    foreach (int io in openedChests)
                    {
                        result += " " + io;
                    }
                    File.AppendAllText(file + ".out", caseStatement + result + Environment.NewLine);
                }
            }
        }

        static int GetIndex(List<Chest> chests, Chest c)
        {
            for (int i = 0; i < chests.Count; i++)
            {
                if (chests[i] == null) continue;
                if (Enumerable.SequenceEqual(chests[i].keys, c.keys) && chests[i].keyType == c.keyType)
                {
                    return i;
                }
            }
            return -1;
        }
    }

    class Chest
    {
        public Chest(ushort keyType, ushort[] keys)
        {
            this.keys = keys;
            this.keyType = keyType;
            this.numKeys = keys.Length;
        }

        public Chest(ushort keyType)
        {
            this.keys = null;
            this.keyType = keyType;
            this.numKeys = -1;
        }

        public ushort[] keys;
        public int numKeys = 0;
        public ushort keyType;
    }
}
