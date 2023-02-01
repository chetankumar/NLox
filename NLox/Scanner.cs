namespace NLox
{
    public class Scanner : IScanner
    {
        
        public string Code { get; set; }
        public List<Token> Tokens { get; set; }

        private int _lineNumber;
        private int _start;
        private int _current;
        private readonly int _eof;
        public bool IsAtEnd => _current >= _eof;
        private char CurrentChar => IsAtEnd ? '\0'  : this.Code[_current];

        public Scanner(string code)
        {
            this.Code = code;
            this.Tokens = new();
            _start = 0;
            _current = 0;
            _eof = code.Length;
            _lineNumber = 0;
        }

        
        /**
            * 1. For the current character
            * 2. Check if character is one of the single char reserved chars. 
            * 3.   If yes, add that token
            * 4.   If no, then continue on
            * 5. Check if the character is the first of 2-part character keyword
            * 6.   If yes, invoke the corresponding handler.
            * 7.   If no, then continue.
            * 8. Check if the character is a digit
            * 9.   If yes, invoke the number handler.
            * 10. Check if the character is a alphabet.
            * 11.  If yes, invoke the identifier handler.
        */
        public List<Token> ScanTokens()
        {
            while (!IsAtEnd)
            {
                ScanToken();
            }
            AddEOF();
            
            return Tokens;
        }

        private void AddEOF()
        {
            _start = _current;
            AddToken(TokenType.EOF);
        }

        private void ScanToken()
        {
            _start = _current;
            char c = CurrentChar;
            if (LoxGrammer.SingleCharReservedKeywordDictionary.ContainsKey(c))
            {
                AddToken(LoxGrammer.SingleCharReservedKeywordDictionary[c]);
            }
            else if (LoxGrammer.CouldBeATwoCharKeyword(c))
            {
                HandlePossibleTwoCharKeyword(c);
            }
            else if (LoxGrammer.IsWhiteSpace(c))
            {
                if (c == '\n') 
                    _lineNumber++;
                // else
                //  skip and continue.
            }
            else if (LoxGrammer.IsStringQuote(c))
            {
                AdvanceTillStopChar('"');
                AddToken(TokenType.STRING, GetCodeSnippet(_start+1,_current-1));
            }
            else if (LoxGrammer.IsDigit(c))
            {
                HandleNumber();
            }
            else if (LoxGrammer.IsAlphabet(c))
            {
                HandleAlphaNumeric();
            }
            Advance(); // Move to the next character
        }

        private void HandleAlphaNumeric()
        {
            while (LoxGrammer.IsAlphaNumeric(CurrentChar))
                Advance();
            Advance(-1); // Move back 1 char to the end of the alpha numeric string
            string word = GetCodeSnippet();
            if (LoxGrammer.IsAReservedWord(word))
            {
                AddToken(LoxGrammer.TextReservedKeywordDictionary[word]);
            }
            else
                AddToken(TokenType.IDENTIFIER);
        }

        private void HandleNumber()
        {
            while(LoxGrammer.IsDigit(CurrentChar) || CurrentChar == '.')
            {
                Advance();
            }
            string? codeSnippet = GetCodeSnippet(_start,_current-1);
            if (codeSnippet!=null)
                AddToken(TokenType.NUMBER, float.Parse(codeSnippet));
        }

        private void AdvanceTillStopChar(char stopChar)
        {
            Advance();
            while (CurrentChar != stopChar) Advance();
        }

        private void HandlePossibleTwoCharKeyword(char c)
        {
            switch (c)
            {
                case '!': CheckSecondCharAndAddToken('=', TokenType.BANG_EQUAL, TokenType.BANG); break;
                case '=': CheckSecondCharAndAddToken('=', TokenType.EQUAL_EQUAL, TokenType.EQUAL); break;
                case '>': CheckSecondCharAndAddToken('=', TokenType.GREATER_EQUAL, TokenType.GREATER); break;
                case '<': CheckSecondCharAndAddToken('=', TokenType.LESS_EQUAL, TokenType.LESS); break;
                case '/': HandleSlash(); break;
            }
        }

        private void HandleSlash()
        {
            if (PeekNext() == '/')
            {
                HandleSingleLineComment();
            }
            else if (PeekNext() == '*')
            {
                HandleMultilineComment();
            }
            else
                AddToken(TokenType.SLASH);
        }

        private void HandleSingleLineComment()
        {
            while (CurrentChar != '\n')
                Advance();
            _lineNumber++;
        }

        private void HandleMultilineComment()
        {
            Advance(2);
            while (!AreNextTwoChars('*','/'))
            {
                if (CurrentChar == '\n') _lineNumber++;
                Advance();
            }
            Advance();
        }

        private bool AreNextTwoChars(char first, char second)
        {
            return (CurrentChar == first && PeekNext() == second);
        }

        /// <summary>
        /// Peeks at the next character and checks if it matches the second char of the 2 char keyword.
        /// If matches, add the 2 char token type
        /// else add the 1 char token type
        /// </summary>
        /// <param name="secondChar"></param>
        /// <param name="ifSecondCharMatches"></param>
        /// <param name="ifSecondCharDoesntMatch"></param>
        private void CheckSecondCharAndAddToken(char secondChar, TokenType ifSecondCharMatches, TokenType ifSecondCharDoesntMatch)
        {
            if (PeekNext() == secondChar)
            {
                Advance(); // this needs to be called first so that the 'current' idx is incremented to the second char before adding token.
                AddToken(ifSecondCharMatches); 
            }
            else
                AddToken(ifSecondCharDoesntMatch);
        }

        private void Advance(int increment=1)
        {
            _current += increment;
        }

        private char PeekNext()
        {
            if (!IsIndexWithinBounds(_current+1)) return '\0';
            else return Code[_current + 1];
        }

        private bool IsIndexWithinBounds(int index)
        {
            return index < _eof;
        }

        private void AddToken(TokenType tokenType,object? literal = null)
        {
            this.Tokens.Add(new Token(tokenType, GetCodeSnippet(), literal, _lineNumber));
        }

        private string GetCodeSnippet(int start, int stop)
        {
            if (start >= _eof) return "\0";
            return Code.Substring(start, stop - start + 1); // We want to include the current char, so +1.
        }

        private string GetCodeSnippet()
        {
            if (_start >= _eof) return "\0";
            if (IsIndexWithinBounds(_current + 1))
                return Code.Substring(_start, _current - _start + 1); // We want to include the current char, so +1.
            else
                return Code.Substring(_start, _eof - _start );
        }
    }
}
