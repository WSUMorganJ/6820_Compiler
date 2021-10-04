using System;
using System.Collections.Generic;
using System.IO;

namespace compiler
{
    public class Parser
    {
        private List<char> Input = new List<char>();

        public Parser(string inputFile)
        {
            // get text into Input
            StreamReader reader = new StreamReader(inputFile);
            do
            {
                Input.Add((char)reader.Read());
            } while (!reader.EndOfStream);
            reader.Close();
            reader.Dispose();

            FilterComments();
            foreach (var thing in Input)
            {
                Console.Write(thing);
            }


        }

        private void FilterComments()
        {
            for (int i = 0; i < Input.Count; i++)
            {
                if (Input[i] == '/')
                {
                    if (Input[i + 1] == '/')
                    {
                        int iter = i;
                        while (Input[iter] != '\n')
                        {
                            iter++;
                        }
                        Input.RemoveRange(i, iter - i);
                    }
                    else if (Input[i + 1] == '*')
                    {
                        int iter = i + 2; // jump past the *
                        while (true)
                        {
                            iter++;
                            if (Input[iter] == '*' && Input[iter + 1] == '/')
                            {
                                iter += 2; // jump past the '*/'
                                break;
                            }
                        }
                        Input.RemoveRange(i, iter - i);
                    }
                }
            }
        }
    }
}