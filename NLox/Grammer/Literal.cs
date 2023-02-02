using NLox.Visitors;
using System;
namespace NLox.Grammer;
public class Literal : Expr
{
	public Literal(Object Value)
	{
		this.Value = Value;
	}
	public Object Value {get; set;}

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.VisitLiteral(this);	
    }
}
