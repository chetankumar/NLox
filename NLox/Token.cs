namespace NLox
{
    public class Token
    {
        public TokenType TokenType { get; set; }
        public string? Lexeme { get; set; }
        public object? Literal { get; set; }
        public int LineNumber { get; set; }

        public Token(TokenType tokenType, string? lexeme, object? literal, int lineNumber)
        {
            TokenType = tokenType;
            Lexeme = lexeme;
            Literal = literal;
            LineNumber = lineNumber;
        }

        public override string ToString()
        {
            return $"{TokenType} {Lexeme} {Literal}"; 
        }
    }
}