using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiece
{
	public Knight() : base()
	{

	}

	protected Knight(Knight k) : base(k)
	{

	}

	public override ChessPiece Clone()
	{
		var piece = gameObject.AddComponent<Knight>();
		piece.SetValues(this);
		return piece;
	}

	public override bool[,] IsLegalMove()
	{
		bool[,] move = new bool[8, 8];

		#region up
		Move(PositionX - 1, PositionY + 2, ref move);
		Move(PositionX + 1, PositionY + 2, ref move);
		Move(PositionX + 2, PositionY + 1, ref move);
		Move(PositionX + 2, PositionY - 1, ref move);
		#endregion
		#region down
		Move(PositionX - 1, PositionY - 2, ref move);
		Move(PositionX + 1, PositionY - 2, ref move);
		Move(PositionX - 2, PositionY + 1, ref move);
		Move(PositionX - 2, PositionY - 1, ref move);
		#endregion
		return move;
	}

	public override bool[,] IsPieceDefended()
	{
		bool[,] defend = new bool[8, 8];
		Defend(PositionX - 1, PositionY + 2, ref defend);
		Defend(PositionX + 1, PositionY + 2, ref defend);
		Defend(PositionX + 2, PositionY + 1, ref defend);
		Defend(PositionX + 2, PositionY - 1, ref defend);
		Defend(PositionX - 1, PositionY - 2, ref defend);
		Defend(PositionX + 1, PositionY - 2, ref defend);
		Defend(PositionX - 2, PositionY + 1, ref defend);
		Defend(PositionX - 2, PositionY - 1, ref defend);
		return defend;
	}

	public void Move(int x, int y, ref bool [,] arr)
	{
		ChessPiece piece;
		if(x >= 0 && x < 8 && y >= 0 && y < 8 )
		{
			piece = ChessBoardManager.Instance.Pieces[x, y];
			if(piece == null)
			{
				arr[x, y] = true;
			}
			else if(isWhite != piece.isWhite)
			{
				arr[x, y] = true;
			}
		}
	}

	public void Defend(int x, int y, ref bool[,] arr)
	{
		ChessPiece piece;
		if (x >= 0 && x < 8 && y >= 0 && y < 8)
		{
			piece = ChessBoardManager.Instance.Pieces[x, y];
			if (piece != null && isWhite == piece.isWhite)
			{
				arr[x, y] = true;
			}
		}
	}
}
