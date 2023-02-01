namespace NLox
{
    public interface IScanner
    {
        string Code { get; }
        List<Token> Tokens { get; set; }

        List<Token> ScanTokens();
    }
}