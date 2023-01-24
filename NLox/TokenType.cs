using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLox
{
    public enum TokenType
    {
        LEFT_PARAN, RIGHT_PARAN, LEFT_BRACE, RIGHT_BRACE, 
        COMMA, DOT, MINUS, PLUS, SEMICOLON, SLASH, STAR,

        BANG, BANG_EQUAL, EQUAL, EQUAL_EQUAL, GREATER, LESS, GREATER_EQUAL, LESS_EQUAL,

        IDENTIFIER, STRING, NUMBER, NIL, EOF,

        TRUE, FALSE,
        AND, OR, IF, ELSE, 
        WHILE, FOR,
        CLASS, FUN, VAR, RETURN,
        SUPER, THIS,

        PRINT

    }
}
