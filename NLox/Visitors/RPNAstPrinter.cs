using NLox.Grammer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLox.Visitors
{
    public class RPNAstPrinter : IVisitor<string>
    {
        public string VisitBinary(Binary r)
        {
            return r.Left.Accept(this) + " " + r.Right.Accept(this) + " " + r.Operator.Lexeme;
        }

        public string VisitGrouping(Grouping r)
        {
            return r.Expression.Accept(this);
        }

        public string VisitLiteral(Literal r)
        {
            if (r.Value == null) return "nil";
            return r.Value.ToString();
        }

        public string Print(Expr expr)
        {
            return expr.Accept(this);
        }

        public string VisitUnary(Unary r)
        {
            return r.Right.Accept(this) + " " + r.Operator.Lexeme;
        }
    }
}
