using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece
{
	public Pawn() : base()
	{

	}

	protected Pawn(Pawn k) : base(k)
	{

	}

	public override ChessPiece Clone()
	{
		var piece = gameObject.AddComponent<Pawn>();
		piece.SetValues(this);
		return piece;
	}

	public override bool[,] IsLegalMove()
	{
		bool[,] move = new bool[8, 8];

		ChessPiece p1, p2, p3, p4;
		Array.Clear(kingCapture, 0, kingCapture.Length);
			
		if(isWhite)
		{
			if (PositionX != 0 && PositionY != 7)
			{
				p1 = ChessBoardManager.Instance.Pieces[PositionX - 1, PositionY + 1];
				if (p1 != null && !p1.isWhite)
				{
					move[PositionX - 1, PositionY + 1] = true;
				}
				if(p1 == null)
				{
					kingCapture[PositionX - 1, PositionY + 1] = true;
				}
			}

			if (PositionX != 7 && PositionY != 7)
			{
				p1 = ChessBoardManager.Instance.Pieces[PositionX + 1, PositionY + 1];
				if (p1 != null && !p1.isWhite)
				{
					move[PositionX + 1, PositionY + 1] = true;
				}
				if(p1 == null)
				{
					kingCapture[PositionX + 1, PositionY + 1] = true;
				}
			}

			if(PositionY != 7)
			{
				p1 = ChessBoardManager.Instance.Pieces[PositionX, PositionY + 1];
				if (p1 == null)
				{
					move[PositionX, PositionY + 1] = true;
				}
			}

			if(PositionY == 1)
			{
				p1 = ChessBoardManager.Instance.Pieces[PositionX, PositionY + 1];
				p2 = ChessBoardManager.Instance.Pieces[PositionX, PositionY + 2];
				if (p1 == null && p2 == null)
				{
					move[PositionX, PositionY + 2] = true;
					if(ChessBoardManager.Instance.doNotPerformCheckScanForEnPassant == false)
					{
						if (PositionX > 0 && PositionX < 7)
						{
							p3 = ChessBoardManager.Instance.Pieces[PositionX + 1, PositionY + 2];
							p4 = ChessBoardManager.Instance.Pieces[PositionX - 1, PositionY + 2];
							if (p3 != null && p3.GetType() == typeof(Pawn) && !p3.isWhite)
							{
								p3.isEnPassantEnabledRight = true;
							}
							if (p4 != null && p4.GetType() == typeof(Pawn) && !p4.isWhite)
							{
								p4.isEnPassantEnabledLeft = true;
							}

						}
						else if (PositionX == 0)
						{
							p3 = ChessBoardManager.Instance.Pieces[PositionX + 1, PositionY + 2];
							if (p3 != null && p3.GetType() == typeof(Pawn) && !p3.isWhite)
							{
								p3.isEnPassantEnabledRight = true;
							}

						}
						else if (PositionX == 7)
						{
							p4 = ChessBoardManager.Instance.Pieces[PositionX - 1, PositionY + 2];
							if (p4 != null && p4.GetType() == typeof(Pawn) && !p4.isWhite)
							{
								p4.isEnPassantEnabledLeft = true;

							}
						}
					}

				}			
			}

			if(PositionY == 4 && PositionX < 7)
			{
				if (isEnPassantEnabledRight)
				{
					move[PositionX + 1, PositionY + 1] = true;
				}				
			}

			if (PositionY == 4 && PositionX > 0)
			{
				if (isEnPassantEnabledLeft)
				{
					move[PositionX - 1, PositionY + 1] = true;
				}
			}

		}
		else 
		{
			if (PositionX != 7 && PositionY != 0)
			{
				p1 = ChessBoardManager.Instance.Pieces[PositionX + 1, PositionY - 1];
				if (p1 != null && p1.isWhite)
				{
					move[PositionX + 1, PositionY - 1] = true;
				}
				if(p1 == null)
				{
					kingCapture[PositionX + 1, PositionY - 1] = true;
				}
			}

			if (PositionX != 0 && PositionY != 0)
			{
				p1 = ChessBoardManager.Instance.Pieces[PositionX - 1, PositionY - 1];
				if (p1 != null && p1.isWhite)
				{
					move[PositionX - 1, PositionY - 1] = true;
				}
				if(p1 == null)
				{
					kingCapture[PositionX - 1, PositionY - 1] = true;

				}
			}

			if (PositionY != 7)
			{
				p1 = ChessBoardManager.Instance.Pieces[PositionX, PositionY - 1];
				if (p1 == null)
				{
					move[PositionX, PositionY - 1] = true;
				}
			}

			if (PositionY == 6)
			{
				p1 = ChessBoardManager.Instance.Pieces[PositionX, PositionY - 1];
				p2 = ChessBoardManager.Instance.Pieces[PositionX, PositionY - 2];
				if (p1 == null && p2 == null)
				{
					move[PositionX, PositionY - 2] = true;

					if(ChessBoardManager.Instance.doNotPerformCheckScanForEnPassant == false)
					{
						if (PositionX > 0 && PositionX < 7)
						{
							p3 = ChessBoardManager.Instance.Pieces[PositionX + 1, PositionY - 2];
							p4 = ChessBoardManager.Instance.Pieces[PositionX - 1, PositionY - 2];
							if (p3 != null && p3.GetType() == typeof(Pawn))
							{
								p3.isEnPassantEnabledLeft = true;
							}
							if (p4 != null && p4.GetType() == typeof(Pawn))
							{
								p4.isEnPassantEnabledRight = true;
							}

						}
						else if (PositionX == 0)
						{
							p3 = ChessBoardManager.Instance.Pieces[PositionX + 1, PositionY - 2];
							if (p3 != null && p3.GetType() == typeof(Pawn))
							{
								p3.isEnPassantEnabledLeft = true;
							}

						}
						else if (PositionX == 7)
						{
							p4 = ChessBoardManager.Instance.Pieces[PositionX - 1, PositionY - 2];
							if (p4 != null && p4.GetType() == typeof(Pawn))
							{
								p4.isEnPassantEnabledRight = true;
							}

						}
					}


				}
			}

			if (PositionY == 3 && PositionX > 0)
			{
				if (isEnPassantEnabledRight)
				{
					move[PositionX - 1, PositionY - 1] = true;
				}
			}

			if (PositionY == 3 && PositionX < 7)
			{
				if (isEnPassantEnabledLeft)
				{
					move[PositionX + 1, PositionY - 1] = true;
				}
			}
		}

		return move;
	}

	public override bool[,] IsPieceDefended()
	{
		bool[,] defend = new bool[8, 8];

		ChessPiece p1;;

		if (isWhite)
		{
			if (PositionX != 0 && PositionY != 7)
			{
				p1 = ChessBoardManager.Instance.Pieces[PositionX - 1, PositionY + 1];
				if (p1 != null && p1.isWhite)
				{
					defend[PositionX - 1, PositionY + 1] = true;
				}
			}

			if (PositionX != 7 && PositionY != 7)
			{
				p1 = ChessBoardManager.Instance.Pieces[PositionX + 1, PositionY + 1];
				if (p1 != null && p1.isWhite)
				{
					defend[PositionX + 1, PositionY + 1] = true;
				}
			}
		}
		else
		{
			if (PositionX != 7 && PositionY != 0)
			{
				p1 = ChessBoardManager.Instance.Pieces[PositionX + 1, PositionY - 1];
				if (p1 != null && !p1.isWhite)
				{
					defend[PositionX + 1, PositionY - 1] = true;
				}
			}

			if (PositionX != 0 && PositionY != 0)
			{
				p1 = ChessBoardManager.Instance.Pieces[PositionX - 1, PositionY - 1];
				if (p1 != null && !p1.isWhite)
				{
					defend[PositionX - 1, PositionY - 1] = true;
				}
			}
		}


		return defend;

	}
}
