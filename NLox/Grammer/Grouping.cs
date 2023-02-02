using NLox.Visitors;
using System;
namespace NLox.Grammer;
public class Grouping : Expr
{
	public Grouping(Expr Expression)
	{
		this.Expression = Expression;
	}
	public Expr Expression {get; set;}

    public override T Accept<T>(IVisitor<T> visitor)
    {
		return visitor.VisitGrouping(this);
    }
}
