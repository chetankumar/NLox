using NLox.Grammer;
using NLox.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLox.Test
{
    public class AstPrinterTest
    {
        [Fact]
        public void TestExpression1()
        {
            Expr expr = new Binary
            (
                new Unary(new Token(TokenType.MINUS, "-", null, 1), new Literal(123)),
                new Token(TokenType.STAR, "*", null, 1),
                new Grouping(new Literal(45.67))
            ) ;
            var printOutput = new AstPrinter().Print(expr);
            Assert.Equal("(* (- 123) (group 45.67))",printOutput);
        }
    }
}
