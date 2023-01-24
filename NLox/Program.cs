
using NLox;

public class Program
{
    public static void Main(string[] args)
    {
        Lox lox = new Lox();

        if (args.Length > 1)
        {
            Console.WriteLine("Usage: jlox Script");
        }
        else if (args.Length == 1)
        {
            lox.RunFile(args[0]);
        }
        else
        {
            lox.RunPrompt();
        }

    }
}
