namespace NLox.Test
{
    public class ScannerTest
    {
        private Token _equals = new Token(TokenType.EQUAL, null, null, 0);
        private Token _eof = new Token(TokenType.EOF, null, null, 0);
        private Token _var = new Token(TokenType.VAR, null, null, 0);

        [Fact]
        public void TestVariableDeclaration()
        {
            string expression = "var a = 1";
            List<Token> expectedTokens = new List<Token> { _equals, new Token(TokenType.NUMBER, "1", 1, 0) };
            TestExpression(expression, expectedTokens);
        }

        [Fact]
        public void TestVariableAssignmentToNumber()
        {
            string expression = "var a = 1";
            List<Token> expectedTokens = new List<Token>
            {   _var,
                GetIdentifier("a"),
                _equals,
                GetNumber(1)
            };
            TestExpression(expression, expectedTokens);
        }

        [Fact]
        public void TestVariableAssignmentToString()
        {
            string expression = "var name = \"Chetan\"";
            List<Token> expectedTokens = new List<Token>
            {   _var,
                GetIdentifier("name"),
                _equals,
                GetString("Chetan")
            };
            TestExpression(expression, expectedTokens);
        }

        private Token GetNumber(int number, int lineNumber = 0)
        {
            return new Token(TokenType.NUMBER, number.ToString(), number, lineNumber);
        }

        private Token GetString(string text, int lineNumber = 0)
        {
            return new Token(TokenType.STRING, text,text, lineNumber);
        }

        private static Token GetIdentifier(string lexeme, int lineNumber = 0)
        {
            return new Token(TokenType.IDENTIFIER, lexeme, null, lineNumber);
        }

        private void TestExpression
            (string expression, List<Token> expectedTokens)
        {
            Scanner scanner = new(expression);
            List<Token> returnedTokens =  scanner.ScanTokens();

            AssertTokensMatch(
                expectedTokens.Append(_eof).ToList(),
                returnedTokens
            );
        }

        private void AssertTokensMatch(List<Token> expectedTokens, List<Token> returnedTokens)
        {
            Assert.Equal(expectedTokens.Count, returnedTokens.Count);   
            for(int i = 0; i < expectedTokens.Count; i++)
            {
                Token expected = expectedTokens[i];
                Token returned = returnedTokens[i];
                Assert.Equal(expected.TokenType, returned.TokenType);
                Assert.Equal(expected.Lexeme, returned.Lexeme);
                Assert.Equal(expected.Literal, returned.Literal);
                Assert.Equal(expected.LineNumber, returned.LineNumber);
            }
        }
    }
}