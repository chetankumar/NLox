using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLox
{
    internal class Lox
    {
        public bool HadError { get; set; }  

        internal void RunFile(string script)
        {
            string code = File.ReadAllText(script);
            Run(code);
        }

        internal  void RunPrompt()
        {
            string? line  = Console.ReadLine();
            while (line != null)
            {
                Run(line);
                HadError = false;
            }
        }

        private void Run(string code)
        {
            ScannerOld scanner = new ScannerOld(code);
            List<Token> tokens = scanner.ScanTokens();
            foreach(var token in tokens)
            {
                Console.WriteLine(token);
            }
        }

        public void HandleError(int lineNumber, string line, string message)
        {
            ReportError(lineNumber, line, message);
        }

        public void ReportError(int lineNumber, string line, string message)
        {
            Console.WriteLine($"{lineNumber} => Error: {message} while processing '${line}'");
        }
    }
}
