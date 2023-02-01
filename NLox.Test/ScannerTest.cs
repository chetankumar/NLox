namespace NLox.Test
{
    public class ScannerTest
    {
        private Token _equals = new Token(TokenType.EQUAL, "=", null, 0);
        private Token _eof = new Token(TokenType.EOF, "\0", null, 0);
        private Token _var = new Token(TokenType.VAR, "var", null, 0);

        [Fact]
        public void TestSingleCharKeywords()
        {
            string expression = "(){},+-;*";
            List<Token> expectedTokens = new List<Token>
            {
                new Token(TokenType.LEFT_PARAN, "(",null,0),
                new Token(TokenType.RIGHT_PARAN, ")", null, 0),
                new Token(TokenType.LEFT_BRACE , "{", null, 0),
                new Token(TokenType.RIGHT_BRACE, "}", null, 0),
                new Token(TokenType.COMMA, ",", null, 0),
                new Token(TokenType.PLUS, "+", null, 0),
                new Token(TokenType.MINUS, "-", null, 0),
                new Token(TokenType.SEMICOLON, ";", null, 0),
                new Token(TokenType.STAR, "*", null, 0)
            };
            TestExpression(expression, expectedTokens);
        }

        [Fact]
        public void TestTwoCharKeywords()
        {
            string expression = "=!<>!=<=>===";
            List<Token> expectedTokens = new List<Token>
            {
                new Token(TokenType.EQUAL, "=", null, 0),
                new Token(TokenType.BANG, "!",null,0),
                new Token(TokenType.LESS, "<", null, 0),
                new Token(TokenType.GREATER , ">", null, 0),
                new Token(TokenType.BANG_EQUAL, "!=", null, 0),
                new Token(TokenType.LESS_EQUAL, "<=", null, 0),
                new Token(TokenType.GREATER_EQUAL, ">=", null, 0),
                new Token(TokenType.EQUAL_EQUAL, "==", null, 0),
            };
            TestExpression(expression, expectedTokens);
        }

        [Fact]
        public void TestSingleLineComment()
        {
            string expression = "!\n//this is a test\n!";
            List<Token> expectedTokens = new List<Token>
            {
                new Token(TokenType.BANG, "!", null, 0),
                new Token(TokenType.BANG, "!", null, 2),
            };
            TestExpression(expression, expectedTokens);
        }

        [Fact]
        public void TestMultiLineComment()
        {
            string expression = "!/* this is a comment \n * this is a comment 2 \n */!";
            List<Token> expectedTokens = new List<Token>
            {
                new Token(TokenType.BANG, "!", null, 0),
                new Token(TokenType.BANG, "!", null, 3),
            };
            TestExpression(expression, expectedTokens);
        }

        [Fact]
        public void TestString()
        {
            string expression = "\"This is a text\"";
            List<Token> expectedTokens = new List<Token>
            {
                new Token(TokenType.STRING, "\"This is a text\"", "This is a text", 0),
            };
            TestExpression(expression, expectedTokens);
        }

        [Fact]
        public void TestNumber()
        {
            string expression = "123.4324";
            List<Token> expectedTokens = new List<Token>
            {
                new Token(TokenType.NUMBER, "123.4324" , 123.4324f, 0),
            };
            TestExpression(expression, expectedTokens);
        }

        [Fact]
        public void TestWhiteSpaces()
        {
            string expression = "!   !\n!";
            List<Token> expectedTokens = new List<Token>
            {
                new Token(TokenType.BANG, "!", null, 0),
                new Token(TokenType.BANG, "!", null, 0),
                new Token(TokenType.BANG, "!", null, 1),
            };
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
                GetNumber(1f)
            };
            TestExpression(expression, expectedTokens);
        }

        [Fact]
        public void TestComment()
        {
            string expression = "//This is a comment \nvar a = 1";
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

        private Token GetNumber(float number, int lineNumber = 0)
        {
            return new Token(TokenType.NUMBER, number.ToString(), number, lineNumber);
        }

        private Token GetString(string text, int lineNumber = 0)
        {
            return new Token(TokenType.STRING, $"\"{text}\"",text, lineNumber);
        }

        private static Token GetIdentifier(string lexeme, int lineNumber = 0)
        {
            return new Token(TokenType.IDENTIFIER, lexeme, null, lineNumber);
        }

        private void TestExpression
            (string expression, List<Token> expectedTokens)
        {
            IScanner scanner = new Scanner(expression);
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
                //Assert.Equal(expected.LineNumber, returned.LineNumber);
            }
        }
    }
}