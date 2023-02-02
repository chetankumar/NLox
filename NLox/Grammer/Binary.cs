using NLox.Visitors;
using System;
namespace NLox.Grammer;
public class Binary : Expr
{
	public Binary(Expr Left, Token Operator, Expr Right)
	{
		this.Left = Left;
		this.Operator = Operator;
		this.Right = Right ;
	}
	public Expr Left {get; set;}
	public Token Operator {get; set;}
	public Expr Right {get; set;}

    public override T Accept<T>(IVisitor<T> visitor)
    {
		return visitor.VisitBinary(this);
    }
}
