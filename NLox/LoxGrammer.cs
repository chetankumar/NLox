namespace NLox
{
    internal class LoxGrammer
    {
        public readonly static Dictionary<char, TokenType> ReserveKeywordDictionary = new Dictionary<char, TokenType> 
        {
            {'(',TokenType.LEFT_PARAN },
            {')',TokenType.RIGHT_PARAN },
            {'{',TokenType.LEFT_BRACE },
            {'}',TokenType.RIGHT_BRACE },

            {',',TokenType.COMMA },
            {'+',TokenType.PLUS },
            {'-',TokenType.MINUS },
            {';',TokenType.SEMICOLON },
            {'*',TokenType.STAR },
        };
    }
}