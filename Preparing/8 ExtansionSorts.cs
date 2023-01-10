using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparing
{
    public class DirectMergeSort
    {
        private string PathA = Path.Combine(Environment.CurrentDirectory, "A.txt");
        private string PathB = Path.Combine(Environment.CurrentDirectory, "B.txt");
        private string PathC = Path.Combine(Environment.CurrentDirectory, "C.txt");

        private bool IsLastRewrite = false;
        private int Chunk = 1;

        public void StartSort()
        {
            while (!IsLastRewrite)
            {
                RewriteToTwoFiles();
                RewriteToMainFile();
                Chunk *= 2;
            }

            File.Delete(PathB);
            File.Delete(PathC);
        }

        private void RewriteToTwoFiles()
        {
            StreamReader srA = new StreamReader(PathA);
            StreamWriter swB = new StreamWriter(PathB);
            StreamWriter swC = new StreamWriter(PathC);

            bool flagB = true;
            int step = 0;

            while (true)
            {
                string str = srA.ReadLine();
                if (str == null) break;

                step++;

                if (flagB) swB.WriteLine(str);
                else swC.WriteLine(str);

                if (step == Chunk)
                {
                    HelpUtils.ChangeFlag(ref flagB);
                    step = 0;
                }
            }
            srA.Close(); swB.Close(); swC.Close();
        }

        private void RewriteToMainFile()
        {
            StreamWriter swA = new StreamWriter(PathA);
            StreamReader srB = new StreamReader(PathB);
            StreamReader srC = new StreamReader(PathC);

            IsLastRewrite = true;

            while (true)
            {
                string strB = srB.ReadLine();
                string strC = srC.ReadLine();

                if (strB == null && strC == null) break;

                int indexB = 1;
                int indexC = 1;

                for (int i = 0; i < Chunk * 2; i++)
                {
                    if (strB == null && strC != null)
                    {
                        WriteAndSetNextPointer(ref swA, ref srC, ref strC, ref indexC);
                    }
                    else if (strB != null && strC == null)
                    {
                        WriteAndSetNextPointer(ref swA, ref srB, ref strB, ref indexB);
                    }
                    else if (strB != null && strC != null)
                    {
                        if (int.Parse(strB) <= int.Parse(strC))
                        {
                            WriteAndSetNextPointer(ref swA, ref srB, ref strB, ref indexB);
                        }
                        else
                        {
                            WriteAndSetNextPointer(ref swA, ref srC, ref strC, ref indexC);
                        }
                    }
                }
            }
            swA.Close(); srB.Close(); srC.Close();
        }

        private void WriteAndSetNextPointer(ref StreamWriter sw, ref StreamReader sr, ref string str, ref int index)//, ref bool isNewChunk)
        {
            sw.WriteLine(str);

            if (index == Chunk)
            {
                IsLastRewrite = false;
                str = null;
            }
            else
            {
                str = sr.ReadLine();
                index++;
            }
        }
    }

    public class NaturalMergeSort
    {
        private string PathA = Path.Combine(Environment.CurrentDirectory, "A.txt");
        private string PathB = Path.Combine(Environment.CurrentDirectory, "B.txt");
        private string PathC = Path.Combine(Environment.CurrentDirectory, "C.txt");

        private bool IsLastRewrite = false;

        public void StartSort()
        {
            while (!IsLastRewrite)
            {
                RewriteToTwoFiles();
                RewriteToMainFile();
            }

            File.Delete(PathB);
            File.Delete(PathC);
        }

        private void RewriteToTwoFiles()
        {
            StreamReader srA = new StreamReader(PathA);
            StreamWriter swB = new StreamWriter(PathB);
            StreamWriter swC = new StreamWriter(PathC);

            bool flagB = true;
            string cur = srA.ReadLine();
            string next = srA.ReadLine();

            if (cur != null) swB.WriteLine(cur);

            while (true)
            {
                if (next == null) break;

                if (int.Parse(cur) > int.Parse(next))
                {
                    HelpUtils.ChangeFlag(ref flagB);
                }

                if (flagB) swB.WriteLine(next);
                else swC.WriteLine(next);

                cur = next;
                next = srA.ReadLine();
            }

            srA.Close(); swB.Close(); swC.Close();
        }

        private void RewriteToMainFile()
        {
            StreamWriter swA = new StreamWriter(PathA);
            StreamReader srB = new StreamReader(PathB);
            StreamReader srC = new StreamReader(PathC);

            IsLastRewrite = true;

            string curB = srB.ReadLine();
            string curC = srC.ReadLine();

            string nextB = srB.ReadLine();
            string nextC = srC.ReadLine();

            while (true)
            {
                if (curB == null && curC == null && nextB == null && nextC == null) break;
                if (curB == null && curC == null)
                {
                    InitNextSeriesPointers(ref srB, ref srC, ref curB, ref curC, ref nextB, ref nextC);
                }

                if (curB == null)
                {
                    WriteAndSetNextPointers(ref swA, ref srC, ref curC, ref nextC);
                }
                else if (curC == null)
                {
                    WriteAndSetNextPointers(ref swA, ref srB, ref curB, ref nextB);
                }
                else
                {
                    if (int.Parse(curB) <= int.Parse(curC))
                    {
                        WriteAndSetNextPointers(ref swA, ref srB, ref curB, ref nextB);
                    }
                    else
                    {
                        WriteAndSetNextPointers(ref swA, ref srC, ref curC, ref nextC);
                    }
                }
            }
            swA.Close(); srB.Close(); srC.Close();
        }

        private void InitNextSeriesPointers(ref StreamReader srB, ref StreamReader srC,
            ref string curB, ref string curC, ref string nextB, ref string nextC)
        {
            if (nextB != null)
            {
                curB = nextB;
                nextB = srB.ReadLine();
            }
            if (nextC != null)
            {
                curC = nextC;
                nextC = srC.ReadLine();
            }
        }

        private void WriteAndSetNextPointers(ref StreamWriter sw, ref StreamReader sr, ref string cur, ref string next)//, ref bool isNewSerias)
        {
            sw.WriteLine(cur);

            if (next != null && int.Parse(cur) > int.Parse(next))
            {
                cur = null;
                IsLastRewrite = false;
            }
            else
            {
                cur = next;
                next = sr.ReadLine();
            }
        }
    }

    public class MultiPathMergeSort
    {
        private string PathA = Path.Combine(Environment.CurrentDirectory, "A.txt");
        private string PathB = Path.Combine(Environment.CurrentDirectory, "B.txt");
        private string PathC = Path.Combine(Environment.CurrentDirectory, "C.txt");
        private string PathD = Path.Combine(Environment.CurrentDirectory, "D.txt");

        private bool IsLastRewrite = false;

        public void StartSort()
        {
            while (!IsLastRewrite)
            {
                RewriteToThreeFiles();
                RewriteToMainFile();
            }

            File.Delete(PathB);
            File.Delete(PathC);
            File.Delete(PathD);
        }

        private void RewriteToThreeFiles()
        {
            StreamReader srA = new StreamReader(PathA);
            StreamWriter swB = new StreamWriter(PathB);
            StreamWriter swC = new StreamWriter(PathC);
            StreamWriter swD = new StreamWriter(PathD);

            int step = 0;
            int choice = 0;

            string cur = srA.ReadLine();
            string next = srA.ReadLine();

            if (cur != null) swB.WriteLine(cur);

            while (true)
            {
                if (next == null) break;

                if (int.Parse(cur) > int.Parse(next)) choice = ++step % 3;

                if (choice == 0)      swB.WriteLine(next);
                else if (choice == 1) swC.WriteLine(next);
                else if (choice == 2) swD.WriteLine(next);

                cur = next;
                next = srA.ReadLine();
            }

            srA.Close(); swB.Close(); swC.Close(); swD.Close();
        }

        private void RewriteToMainFile()
        {
            StreamWriter swA = new StreamWriter(PathA);
            StreamReader srB = new StreamReader(PathB);
            StreamReader srC = new StreamReader(PathC);
            StreamReader srD = new StreamReader(PathD);

            IsLastRewrite = true;

            string curB = srB.ReadLine();
            string curC = srC.ReadLine();
            string curD = srD.ReadLine();

            string nextB = srB.ReadLine();
            string nextC = srC.ReadLine();
            string nextD = srD.ReadLine();

            while (true)
            {
                if (curB == null && curC == null && curD == null &&
                    nextB == null && nextC == null && nextD == null) break;

                if (curB == null && curC == null && curD == null)
                {
                    InitNextSeriesPointers(ref srB, ref srC, ref srD, ref curB,
                        ref curC, ref curD, ref nextB, ref nextC, ref nextD); 
                }

                if (curB == null && curC == null)
                {
                    WriteAndSetNextPointers(ref swA, ref srD, ref curD, ref nextD);
                }
                else if (curB == null && curD == null)
                {
                    WriteAndSetNextPointers(ref swA, ref srC, ref curC, ref nextC);
                }
                else if (curC == null && curD == null)
                {
                    WriteAndSetNextPointers(ref swA, ref srB, ref curB, ref nextB);
                }
                else if (curB == null)
                {
                    if (int.Parse(curC) <= int.Parse(curD))
                    {
                        WriteAndSetNextPointers(ref swA, ref srC, ref curC, ref nextC);
                    }
                    else
                    {
                        WriteAndSetNextPointers(ref swA, ref srD, ref curD, ref nextD);
                    }
                }
                else if (curC == null)
                {
                    if (int.Parse(curB) <= int.Parse(curD))
                    {
                        WriteAndSetNextPointers(ref swA, ref srB, ref curB, ref nextB);
                    }
                    else
                    {
                        WriteAndSetNextPointers(ref swA, ref srD, ref curD, ref nextD);
                    }
                }
                else if (curD == null)
                {
                    if (int.Parse(curB) <= int.Parse(curC))
                    {
                        WriteAndSetNextPointers(ref swA, ref srB, ref curB, ref nextB);
                    }
                    else
                    {
                        WriteAndSetNextPointers(ref swA, ref srC, ref curC, ref nextC);
                    }
                }
                else
                {
                    if (int.Parse(curB) <= int.Parse(curC))
                    {
                        if (int.Parse(curB) <= int.Parse(curD))
                        {
                            WriteAndSetNextPointers(ref swA, ref srB, ref curB, ref nextB);
                        }
                        else
                        {
                            WriteAndSetNextPointers(ref swA, ref srD, ref curD, ref nextD);
                        }
                    }
                    else
                    {
                        if (int.Parse(curC) <= int.Parse(curD))
                        {
                            WriteAndSetNextPointers(ref swA, ref srC, ref curC, ref nextC);
                        }
                        else
                        {
                            WriteAndSetNextPointers(ref swA, ref srD, ref curD, ref nextD);
                        }
                    }
                }
            }
            swA.Close(); srB.Close(); srC.Close(); srD.Close();
        }

        private void InitNextSeriesPointers(ref StreamReader srB, ref StreamReader srC,
            ref StreamReader srD, ref string curB, ref string curC, ref string curD,
            ref string nextB, ref string nextC, ref string nextD)
        {
            if (curB == null && curC == null && curD == null)
            {
                if (nextB != null)
                {
                    curB = nextB;
                    nextB = srB.ReadLine();
                }
                if (nextC != null)
                {
                    curC = nextC;
                    nextC = srC.ReadLine();
                }
                if (nextD != null)
                {
                    curD = nextD;
                    nextD = srD.ReadLine();
                }
            }
        }

        private void WriteAndSetNextPointers(ref StreamWriter sw, ref StreamReader sr, ref string cur, ref string next)
        {
            sw.WriteLine(cur);

            if (next != null && int.Parse(cur) > int.Parse(next))
            {
                cur = null;
                IsLastRewrite = false;
            }
            else
            {
                cur = next;
                next = sr.ReadLine();
            }
        }
    }

    public static class HelpUtils
    {
        public static void ChangeFlag(ref bool flag)
        {
            if (flag) flag = false;
            else flag = true;
        }
    }
}
