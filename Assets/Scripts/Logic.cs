using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct MakeMove
{
    public int row;
    public int col;
}

public class Logic : MonoBehaviour
{
    [SerializeField] private Image[] xImages = new Image[9];
    [SerializeField] private Image[] oImages = new Image[9];
    [SerializeField] private Text comWinText;
    [SerializeField] private Text drawText;

    [Header ("Buttons")]
    [SerializeField] private Button slot0;
    [SerializeField] private Button slot1;
    [SerializeField] private Button slot2;
    [SerializeField] private Button slot3;
    [SerializeField] private Button slot4;
    [SerializeField] private Button slot5;
    [SerializeField] private Button slot6;
    [SerializeField] private Button slot7;
    [SerializeField] private Button slot8;
    [SerializeField] private Button resetButton;

    [Header("GameplayVariables")]
    [SerializeField] bool playerTurn;
    [SerializeField] bool comWin = false;
    [SerializeField] bool draw = false;
    [SerializeField] static char computer = 'x';
    [SerializeField] static char player = 'o';
    private char[,] board = new char[3, 3]
    {
        {'_', '_', '_'},
        {'_', '_', '_'},
        {'_', '_', '_'}
    };

    void Start()
    {
        if (slot0 && slot1 && slot2 && slot3 && slot4 && slot5 && slot6 && slot7 && slot8)
        {
            slot0.onClick.AddListener(() => SlotSelected(0, 0));
            slot1.onClick.AddListener(() => SlotSelected(0, 1));
            slot2.onClick.AddListener(() => SlotSelected(0, 2));
            slot3.onClick.AddListener(() => SlotSelected(1, 0));
            slot4.onClick.AddListener(() => SlotSelected(1, 1));
            slot5.onClick.AddListener(() => SlotSelected(1, 2));
            slot6.onClick.AddListener(() => SlotSelected(2, 0));
            slot7.onClick.AddListener(() => SlotSelected(2, 1));
            slot8.onClick.AddListener(() => SlotSelected(2, 2));
        }

        if (resetButton)
        {
            resetButton.onClick.AddListener(() => ResetGame());
        }

        for (int i = 0; i < xImages.Length; i++)
        {
            xImages[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < oImages.Length; i++)
        {
            oImages[i].gameObject.SetActive(false);
        }

        comWinText.gameObject.SetActive(false);

        drawText.gameObject.SetActive(false);

        playerTurn = false;
    }

    private void Update()
    {
        if (playerTurn == false && !draw && !comWin)
        {
            MakeMove bestMove = FindBestMove(board);
            ImplementMove(bestMove);
        }
    }

    static bool IsMovesLeft(char[,] board)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == '_')
                {
                    return true;
                }
            }
        }

        return false;
    }

    static int Evaluate(char[,] board)
    {
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
            {
                if (board[i, 0] == computer)
                {
                    return +10;
                }
                else if (board[i, 0] == player)
                {
                    return -10;
                }
            }
        }

        for (int j = 0; j < 3; j++)
        {
            if (board[0, j] == board[1, j] && board[1, j] == board[2, j])
            {
                if (board[0, j] == computer)
                {
                    return +10;
                }
                else if (board[0, j] == player)
                {
                    return -10;
                }
            }
        }

        if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
        {
            if (board[0, 0] == computer)
            {
                return +10;
            }
            else if (board[0, 0] == player)
            {
                return -10;
            }
        }

        if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
        {
            if (board[0, 2] == computer)
            {
                return +10;
            }
            else if (board[0, 2] == player)
            {
                return -10;
            }
        }

        return 0;
    }

    static int MiniMax(char[,] board, int depth, bool isMax)
    {
        int score = Evaluate(board);

        if (score == 10)
        {
            return score;
        }
        if (score == -10)
        {
            return score;
        }
        if (!IsMovesLeft(board))
        {
            return 0;
        }

        if (isMax)
        {
            int best = -9999;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == '_')
                    {
                        board[i, j] = computer;
                        best = Mathf.Max(best, MiniMax(board, depth + 1, !isMax));
                        board[i, j] = '_';
                    }
                }
            }

            return best;
        }
        else
        {
            int best = 9999;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == '_')
                    {
                        board[i, j] = player;
                        best = Mathf.Min(best, MiniMax(board, depth + 1, !isMax));
                        board[i, j] = '_';
                    }
                }
            }

            return best;
        }
    }

    static MakeMove FindBestMove(char[,] board)
    {
        int bestMoveValue = -9999;
        MakeMove bestMove;
        bestMove.row = -1;
        bestMove.col = -1;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == '_')
                {
                    board[i, j] = computer;
                    int moveValue = MiniMax(board, 0, false);
                    board[i, j] = '_';

                    if (moveValue > bestMoveValue)
                    {
                        bestMove.row = i;
                        bestMove.col = j;
                        bestMoveValue = moveValue;
                    }
                }
            }
        }

        return bestMove;
    }

    void UpdateBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == 'o')
                {
                    oImages[i * 3 + j].gameObject.SetActive(true);
                }
                else
                {
                    oImages[i * 3 + j].gameObject.SetActive(false);
                }

                if (board[i, j] == 'x')
                {
                    xImages[i * 3 + j].gameObject.SetActive(true);
                }
                else
                {
                    xImages[i * 3 + j].gameObject.SetActive(false);
                }
            }
        }
    }

    void SlotSelected(int rowSelected, int colSelected)
    {
        if (board[rowSelected, colSelected] == '_' && !draw && !comWin)
        {
            board[rowSelected, colSelected] = player;
            UpdateBoard();
            playerTurn = false;
        }
    }

    void ImplementMove(MakeMove bestMove)
    {
        board[bestMove.row, bestMove.col] = computer;
        UpdateBoard();

        bool movesLeft = IsMovesLeft(board);
        int evaluation = Evaluate(board);

        if (movesLeft && evaluation == 0)
        {
            playerTurn = true;
        }
        else if (!movesLeft)
        {
            drawText.gameObject.SetActive(true);
            draw = true;
        }
        else if (evaluation != 0)
        {
            comWinText.gameObject.SetActive(true);
            comWin = true;
        }
    }

    void ResetGame()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = '_';
            }
        }

        comWinText.gameObject.SetActive(false);
        drawText.gameObject.SetActive(false);

        playerTurn = false;
        comWin = false;
        draw = false;

        UpdateBoard();
    }
}
