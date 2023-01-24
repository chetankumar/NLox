namespace NLox
{
    internal class LoxGrammer
    {
        public readonly static Dictionary<char, TokenType> SingleCharReservedKeywordDictionary = new Dictionary<char, TokenType> 
        {
            { '(', TokenType.LEFT_PARAN },
            { ')', TokenType.RIGHT_PARAN },
            { '{', TokenType.LEFT_BRACE },
            { '}', TokenType.RIGHT_BRACE },
            { ',', TokenType.COMMA },
            { '+', TokenType.PLUS },
            { '-', TokenType.MINUS },
            { ';', TokenType.SEMICOLON },
            { '*', TokenType.STAR },
        };

        public readonly static Dictionary<string, TokenType> TextReservedKeywordDictionary = new Dictionary<string, TokenType>
        {
            { "or", TokenType.OR },
            { "and", TokenType.AND },
            { "super", TokenType.SUPER },
            { "this", TokenType.THIS },
            { "if", TokenType.IF },
            { "else", TokenType.ELSE },
            { "fun", TokenType.FUN },
            { "class", TokenType.CLASS },
            { "false", TokenType.FALSE },
            { "true", TokenType.TRUE },
            { "nil", TokenType.NIL },
            { "return", TokenType.RETURN },
            { "var", TokenType.VAR },
            { "while", TokenType.WHILE },
            { "for", TokenType.FOR },
            { "print", TokenType.PRINT },
        };

        internal class TokenMatch
        {
            public bool IsMatch => TokenType != null;
            public bool IsTwoChar { get; set; }
            public TokenType? TokenType;

            public TokenMatch( bool isTwoChar, TokenType? tokenType)
            {
                IsTwoChar = isTwoChar;
                TokenType = tokenType;
            }
        }
    }

}