using NLox.Grammer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLox.Visitors
{
    public class AstPrinter : IVisitor<string>
    {
        public string VisitBinary(Binary expr)
        {
            return Paranthesize(expr.Operator.Lexeme, expr.Left, expr.Right);
        }

        public string VisitGrouping(Grouping r)
        {
            return Paranthesize("group", r.Expression);
        }

        public string VisitLiteral(Literal r)
        {
            if (r.Value == null) 
                return "nil";
            else
                return r.Value.ToString();
        }

        internal string Print(Expr expr)
        {
            return expr.Accept(this);
        }

        public string VisitUnary(Unary r)
        {
            return Paranthesize(r.Operator.Lexeme, r.Right);
        }

        private string Paranthesize(string? name, params Expr[] expressions)
        {
            StringBuilder builder = new StringBuilder();
            builder
                .Append("(").Append(name);
            foreach(var expr in expressions)
            {
                builder.Append(" ");
                builder.Append(expr.Accept(this));
            }
            builder.Append(")");
            return builder.ToString();
        }

    }
}
