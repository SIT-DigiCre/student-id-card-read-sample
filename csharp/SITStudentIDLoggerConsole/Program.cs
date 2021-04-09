using System;
using System.IO;

namespace SITStudentIDLoggerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SIT Student ID Logger C# Console");
            Console.WriteLine("Copyright (c) 2021 cordx56");
            try
            {
                using (SITStudentID.Reader r = new SITStudentID.Reader())
                {
                    string beforeReadStudentID = "";
                    while (true)
                    {
                        try
                        {
                            SITStudentID.Card card = r.Read();
                            if (card.ID != beforeReadStudentID)
                            {
                                Console.WriteLine(card.ID);
                                // Console.Write(card.ValidFrom.ToString() + " -> " + card.ValidTo.ToString());
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
