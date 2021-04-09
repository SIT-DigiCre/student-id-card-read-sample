using System;
using System.IO;
using System.Timers;

namespace SITStudentIDLoggerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SIT Student ID Logger C# Console");
            Console.WriteLine("Copyright (c) 2021 cordx56");
            Reader r = new Reader();
            Timer t = new Timer(100);
            t.Elapsed += r.read;
            t.Enabled = true;
            while (true)
            {
            }
        }
    }

    class Reader
    {
        private string beforeReadStudentID;
        public void read(object sender, System.Timers.ElapsedEventArgs e)
        {
            Timer t = (Timer)sender;
            t.Enabled = false;
            try
            {
                using (SITStudentID.Reader r = new SITStudentID.Reader())
                {
                    try
                    {
                        SITStudentID.Card card = r.Read();
                        if (card.ID != beforeReadStudentID)
                        {
                            Console.WriteLine(card.ID);
                            using (StreamWriter outputFile = File.AppendText("ids.txt"))
                            {
                                outputFile.WriteLine(card.ID);
                            }
                            beforeReadStudentID = card.ID;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            t.Enabled = true;
        }
    }
}
