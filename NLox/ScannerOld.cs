using System.Text;

namespace NLox
{
    public class ScannerOld : IScanner
    {
        public ScannerOld(string code)
        {
            Code = code;
            Tokens = new List<Token>();
        }

        public string Code { get; }
        public List<Token> Tokens { get; set; }

        private int _current;
        private int _start;
        private int _line;

        private bool IsAtEnd => _current >= Code.Length;

        public List<Token> ScanTokens()
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
            if (LoxGrammer.SingleCharReservedKeywordDictionary.ContainsKey(c))
            {
                TokenType tokenType = LoxGrammer.SingleCharReservedKeywordDictionary[c];
                AddToken(tokenType);
                return;
            }

            switch (c)
            {
                case '!':
                    AddToken(MatchNextToken('=') ? TokenType.BANG_EQUAL : TokenType.BANG);
                    break;
                case '=':
                    AddToken(MatchNextToken('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL);
                    break;
                case '>':
                    AddToken(MatchNextToken('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER);
                    break;
                case '<':
                    AddToken(MatchNextToken('=') ? TokenType.LESS_EQUAL : TokenType.LESS);
                    break;

                case '/':
                    if (MatchNextToken('/'))
                    {
                        while (Peek() != '\n' && !IsAtEnd)
                        {
                            Advance();
                        }
                    }
                    else if (MatchNextToken('*'))
                    {
                        while (!IsAtEnd && Peek() != '*' && !MatchNextToken('/'))
                        {
                            Advance();
                        }
                        Advance();
                        Advance();
                    }
                    else
                    {
                        AddToken(TokenType.SLASH);
                    }
                    break;

                case '"':
                    while ((Peek()) != '"' && !IsAtEnd)
                    {
                        if (Peek() == '\n')
                            _line++;
                        Advance();
                    }
                    AddToken(TokenType.STRING, GetCodeSnippet(_start + 1, _current));
                    break;

                case ' ':
                case '\r':
                case '\t':
                    break;

                case '\n':
                    _line++;
                    break;
                default:
                    if (IsDigit(c))
                    {
                        ProcessNumber();
                    }
                    else if (IsAlpha(c))
                    {
                        ProcessIdentifierOrKeyword();
                    }
                    else
                    {
                        throw new SyntaxError(_line, $"Expected character {c}");
                    }
                    break;
            }


        }

        private void ProcessIdentifierOrKeyword()
        {
            while (IsAlphaNumeric(Peek())) Advance();

            string text = GetCodeSnippet(_start, _current);
            TokenType tokenType;
            bool reservedKeywordFound = LoxGrammer.TextReservedKeywordDictionary.TryGetValue(text, out tokenType);
            if (!reservedKeywordFound)
            {
                tokenType = TokenType.IDENTIFIER;
            }
            AddToken(tokenType);
        }

        private bool IsAlphaNumeric(char c)
        {
            return IsDigit(c) || IsAlpha(c);
        }

        private bool IsAlpha(char c)
        {
            return
                (c >= 'a' && c <= 'z') ||
                (c >= 'A' && c <= 'Z') ||
                (c == '_');
        }

        private bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }



        private void ProcessNumber()
        {
            while (IsDigit(Peek())) Advance();

            if (Peek() == '.' && IsDigit(PeekNext()))
                Advance();

            AddToken(TokenType.NUMBER, GetCodeSnippet(_start, _current));
        }

        private char PeekNext()
        {
            if (_current + 1 >= Code.Length) return '\0';
            return Code[_current + 1];
        }

        private char Peek()
        {
            if (IsAtEnd) return '\0';
            return Code[_current];
        }

        private bool MatchNextToken(char expected)
        {
            if (IsAtEnd) return false;
            if (Code[_current] != expected) return false;

            _current++;
            return true;
        }

        private void AddToken(TokenType tokenType)
        {
            AddToken(tokenType, null);
        }

        private void AddToken(TokenType tokenType, object? literal)
        {
            string text = GetCodeSnippet(_start, _current);
            Tokens.Add(new Token(tokenType, text, literal, _line));
        }

        private string GetCodeSnippet(int start, int end)
        {
            return Code.Substring(start, end - start);
        }

        private char Advance()
        {
            return Code[_current++];
        }
    }
}