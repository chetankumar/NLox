// See https://aka.ms/new-console-template for more information

using NLox.Tools;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
            Console.Error.WriteLine("Usage generate-ast <output-directory>");
        new GenerateAst().GenerateClasses(args[0]);
    }
}