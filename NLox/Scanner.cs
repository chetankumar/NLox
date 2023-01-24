namespace NLox
{
    public class Scanner
    {
        public Scanner(string code)
        {
            Code = code;
        }

        public string Code { get; }
        public List<Token> Tokens { get; set; }

        private int _current;
        private int _start;
        private int _line;

        public bool IsAtEnd => _current >= Code.Length;

        internal List<Token> ScanTokens()
        {
            while (!IsAtEnd)
            {
                _start = _current;
                ScanToken();
            }
            Tokens.Add(new Token(TokenType.EOF, "", null, _line));
            return Tokens;
        }

        private void ScanToken()
        {
            char c = Advance();
            if (LoxGrammer.ReserveKeywordDictionary.ContainsKey(c))
            {
                TokenType tokenType = LoxGrammer.ReserveKeywordDictionary[c];
                AddToken(tokenType);
            }
        }

        private void AddToken(TokenType tokenType)
        {
            AddToken(tokenType, null);
        }

        private void AddToken(TokenType tokenType, object? literal)
        {
            string text = Code.Substring(_start, _current);
            Tokens.Add(new Token(tokenType, text, literal, _line));
        }

        private char Advance()
        {
            throw new NotImplementedException();
        }
    }
}