using NLox.Visitors;
using System;
namespace NLox.Grammer;
public class Unary : Expr
{
	public Unary(Token Operator, Expr Right)
	{
		this.Operator = Operator;
		this.Right = Right;
	}
	public Token Operator {get; set;}
	public Expr Right {get; set;}

    public override T Accept<T>(IVisitor<T> visitor)
    {
        return visitor.VisitUnary(this);
    }
}
