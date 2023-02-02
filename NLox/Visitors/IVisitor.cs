using NLox.Grammer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLox.Visitors
{
    public interface IVisitor<R>
    {
        R VisitBinary(Binary r);
        R VisitUnary(Unary r);
        R VisitGrouping(Grouping r);
        R VisitLiteral(Literal r);
    }
}
