using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessPiece
{
	public Queen() : base()
	{

	}

	protected Queen(Queen k) : base(k)
	{

	}

	public override ChessPiece Clone()
	{
		var piece = gameObject.AddComponent<Queen>();
		piece.SetValues(this);
		return piece;
	}

	public override bool[,] IsLegalMove()
	{
		bool[,] move = new bool[8, 8];
		ChessPiece piece;
		int i;
		int j;


		#region rook
		i = PositionX;
		while (true)
		{
			i++;
			if (i >= 8)
			{
				break;
			}

			piece = ChessBoardManager.Instance.Pieces[i, PositionY];
			if (piece == null)
			{
				move[i, PositionY] = true;
			}
			else
			{
				if (piece.isWhite != isWhite)
				{
					move[i, PositionY] = true;
				}
				break;
			}

		}

		i = PositionX;
		while (true)
		{
			i--;
			if (i < 0)
			{
				break;
			}

			piece = ChessBoardManager.Instance.Pieces[i, PositionY];
			if (piece == null)
			{
				move[i, PositionY] = true;
			}
			else
			{
				if (piece.isWhite != isWhite)
				{
					move[i, PositionY] = true;
				}
				break;
			}

		}

		i = PositionY;
		while (true)
		{
			i++;
			if (i >= 8)
			{
				break;
			}

			piece = ChessBoardManager.Instance.Pieces[PositionX, i];
			if (piece == null)
			{
				move[PositionX, i] = true;
			}
			else
			{
				if (piece.isWhite != isWhite)
				{
					move[PositionX, i] = true;
				}
				break;
			}

		}

		i = PositionY;
		while (true)
		{
			i--;
			if (i < 0)
			{
				break;
			}

			piece = ChessBoardManager.Instance.Pieces[PositionX, i];
			if (piece == null)
			{
				move[PositionX, i] = true;
			}
			else
			{
				if (piece.isWhite != isWhite)
				{
					move[PositionX, i] = true;
				}
				break;
			}

		}
		#endregion
		#region bishop
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

		#endregion

		return move;
	}

	public override bool[,] IsPieceDefended()
	{
		bool[,] defend = new bool[8, 8];
		ChessPiece piece;
		int i;
		int j;

		#region rook
		i = PositionX;
		while (true)
		{
			i++;
			if (i >= 8)
			{
				break;
			}

			piece = ChessBoardManager.Instance.Pieces[i, PositionY];
			if (piece != null)
			{
				if(isWhite == piece.isWhite)
				{
					defend[i, PositionY] = true;
					break;
				}
				else
				{
					break;
				}
			}
		}

		i = PositionX;
		while (true)
		{
			i--;
			if (i < 0)
			{
				break;
			}

			piece = ChessBoardManager.Instance.Pieces[i, PositionY];
			if (piece != null)
			{
				if(isWhite == piece.isWhite)
				{
					defend[i, PositionY] = true;
					break;
				}
				else
				{
					break;
				}
			}
		}

		i = PositionY;
		while (true)
		{
			i++;
			if (i >= 8)
			{
				break;
			}

			piece = ChessBoardManager.Instance.Pieces[PositionX, i];
			if (piece != null)
			{
				if(isWhite == piece.isWhite)
				{
					defend[PositionX, i] = true;
					break;
				}
				else
				{
					break;
				}

			}
		}

		i = PositionY;
		while (true)
		{
			i--;
			if (i < 0)
			{
				break;
			}

			piece = ChessBoardManager.Instance.Pieces[PositionX, i];
			if (piece != null)
			{
				if (isWhite == piece.isWhite)
				{
					defend[PositionX, i] = true;
					break;
				}
				else
				{
					break;
				}

			}
		}
		#endregion
		#region bishop
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
			if(piece != null)
			{
				if (isWhite == piece.isWhite)
				{
					defend[i, j] = true;
					break;
				}
				else
				{
					break;
				}
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
			if(piece != null)
			{
				if(isWhite == piece.isWhite)
				{
					defend[i, j] = true;
					break;
				}
				else
				{
					break;
				}
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
			if (piece != null)
			{
				if (isWhite == piece.isWhite)
				{
					defend[i, j] = true;
					break;
				}
				else
				{
					break;
				}
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
			if (piece != null)
			{
				if (isWhite == piece.isWhite)
				{
					defend[i, j] = true;
					break;
				}
				else
				{
					break;
				}
			}
		}
		#endregion
		return defend;
	}
}
