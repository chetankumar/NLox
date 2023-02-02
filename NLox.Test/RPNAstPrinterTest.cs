using NLox.Grammer;
using NLox.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLox.Test
{
    public class RPNAstPrinterTest
    {
        [Fact]
        public void TestExpression1()
        {
            Expr expr = new Binary
            (
                new Binary
                (
                    new Literal(1),
                    new Token(TokenType.PLUS, "+", null, 1),
                    new Literal(2)
                ),
                new Token(TokenType.STAR, "*", null, 1),
                new Binary
                (
                    new Literal(4),
                    new Token(TokenType.MINUS, "-", null, 1),
                    new Literal(3)
                )
            ) ;
            var printOutput = new RPNAstPrinter().Print(expr);
            Assert.Equal("1 2 + 4 3 - *",printOutput);
        }
    }
}
