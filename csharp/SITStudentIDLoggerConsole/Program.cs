using System;
using System.IO;
using FelicaLib;

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
                using (Felica f = new Felica())
                {
                    string beforeReadStudentID = "";
                    while (true)
                    {
                        try
                        {
                            string studentID = read(f);
                            if (studentID != beforeReadStudentID)
                            {
                                Console.WriteLine(studentID);
                                using (StreamWriter outputFile = File.AppendText("ids.txt"))
                                {
                                    outputFile.WriteLine(studentID);
                                }
                                beforeReadStudentID = studentID;
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

        private static string read(Felica f)
        {
            string studentID = "";
            f.Polling((int)0x8277);
            byte[] data = f.ReadWithoutEncryption(0x010b, 0);
            if (data == null)
            {
                throw new Exception("学生証が読み取れません");
            }
            for (int i = 3; i < 10; i++)
            {
                studentID += (char)data[i];
            }
            return studentID;
        }
    }
}
