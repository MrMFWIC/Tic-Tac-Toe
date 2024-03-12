using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logic : MonoBehaviour
{
    [SerializeField] private Image[] xImages = new Image[9];
    [SerializeField] private Image[] oImages = new Image[9];
    [SerializeField] private Image[] lineImages = new Image[8];

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

    [Header("GameplayVariables")]
    [SerializeField] private char player = 'o';
    [SerializeField] private char opponent = 'x';
    private char[,] board = new char[3, 3]
    {
        {'_', '_', '_'},
        {'_', '_', '_'},
        {'_', '_', '_'}
    };

    // Start is called before the first frame update
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

        for (int i = 0; i < xImages.Length; i++)
        {
            xImages[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < oImages.Length; i++)
        {
            oImages[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < lineImages.Length; i++)
        {
            lineImages[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == 'o')
                {
                    oImages[i + j].gameObject.SetActive(true);
                }

                if (board[i, j] == 'x')
                {
                    xImages[i + j].gameObject.SetActive(true);
                }
            }
        }
    }

    private void SlotSelected(int colSelected, int rowSelected)
    {
        board[colSelected, rowSelected] = 'o';
        UpdateBoard();
    }

    bool IsMovesLeft()
    {
        return false;
    }

    int Evaluate()
    {
        return 0;
    }

    int MiniMax()
    {
        return 0;
    }
}
