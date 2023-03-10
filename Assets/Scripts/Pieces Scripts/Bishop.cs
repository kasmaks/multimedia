using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bishop : ChessPiece
{
	public Bishop() : base()
	{

	}

	protected Bishop(Bishop bishop) : base(bishop)
	{
		
	}

	public override ChessPiece Clone()
	{
		var piece = gameObject.AddComponent<Bishop>();
		piece.SetValues(this);
		return piece;
	}

	public override bool[,] IsPieceDefended()
	{
		bool[,] defend = new bool[8, 8];

		ChessPiece piece;

		int i;
		int j;

		i = PositionX;
		j = PositionY;
		while (true)
		{
			i--;
			j++;
			if (i < 0 || j >= 8)
			{
				break;
			}
			piece = ChessBoardManager.Instance.Pieces[i, j];
			if(piece != null && isWhite == piece.isWhite)
			{ 
				defend[i, j] = true;
				break;
			}
		}

		i = PositionX;
		j = PositionY;
		while (true)
		{
			i++;
			j++;
			if (i >= 8 || j >= 8)
			{
				break;
			}
			piece = ChessBoardManager.Instance.Pieces[i, j];
			if (piece != null && isWhite == piece.isWhite)
			{
				defend[i, j] = true;
				break;
			}
		}

		i = PositionX;
		j = PositionY;
		while (true)
		{
			i--;
			j--;
			if (i < 0 || j < 0)
			{
				break;
			}
			piece = ChessBoardManager.Instance.Pieces[i, j];
			if ( piece != null && isWhite == piece.isWhite)
			{
				defend[i, j] = true;
				break;
			}
		}

		i = PositionX;
		j = PositionY;
		while (true)
		{
			i++;
			j--;
			if (i >= 8 || j < 0)
			{
				break;
			}
			piece = ChessBoardManager.Instance.Pieces[i, j];
			if ((piece != null) && isWhite == piece.isWhite)
			{
				defend[i, j] = true;
				break;
			}

		}

		return defend;
	}

	public override bool[,] IsLegalMove()
	{
		bool[,] move = new bool[8, 8];

		ChessPiece piece;

		int i;
		int j;


		i = PositionX;
		j = PositionY;

		while (true)
		{
			i--;
			j++;
			if (i < 0 || j >= 8)
			{
				break;
			}
			piece = ChessBoardManager.Instance.Pieces[i, j];
			if (piece == null)
			{
				move[i, j] = true;
			}
			else
			{
				if (isWhite != piece.isWhite)
				{
					move[i, j] = true;
				}
				break;
			}

		}

		i = PositionX;
		j = PositionY;

		while (true)
		{
			i++;
			j++;
			if (i >= 8 || j >= 8)
			{
				break;
			}
			piece = ChessBoardManager.Instance.Pieces[i, j];
			if (piece == null)
			{
				move[i, j] = true;
			}
			else
			{
				if (isWhite != piece.isWhite)
				{
					move[i, j] = true;
				}
				break;
			}

		}

		i = PositionX;
		j = PositionY;

		while (true)
		{
			i--;
			j--;
			if (i < 0 || j < 0)
			{
				break;
			}
			piece = ChessBoardManager.Instance.Pieces[i, j];
			if (piece == null)
			{
				move[i, j] = true;
			}
			else
			{
				if (isWhite != piece.isWhite)
				{
					move[i, j] = true;
				}
				break;
			}

		}

		i = PositionX;
		j = PositionY;

		while (true)
		{
			i++;
			j--;
			if (i >= 8 || j < 0)
			{
				break;
			}
			piece = ChessBoardManager.Instance.Pieces[i, j];
			if (piece == null)
			{
				move[i, j] = true;
			}
			else
			{
				if (isWhite != piece.isWhite)
				{
					move[i, j] = true;
				}
				break;
			}

		}

		return move;

	}
}
