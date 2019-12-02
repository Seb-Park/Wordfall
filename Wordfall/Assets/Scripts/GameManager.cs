using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameState{PLAYERTURN, PHYSICS, WIN, LOSE}

public class GameManager : MonoBehaviour
{
    public GameState state;
    public string currentWord;
    public bool isSelecting;

    public Camera mainCamera;

    public TextMeshProUGUI currentWordText;
    public LineRenderer line;

    public List<LetterTile> selectedTiles;
    public List<Vector2> selectedTilePoints;

    // Start is called before the first frame update
    void Start()
    {
        line.positionCount = 0;
        line.positionCount = 1;
        mainCamera = Camera.main;   
    }

    public void UpdateWord(){
        currentWord = "";
        for (int i = 0; i < selectedTiles.Count; i++)
        {
            currentWord = currentWord + selectedTiles[i].letter;
        }
        line.positionCount = selectedTilePoints.Count + 1;
        for (int i = 0; i < selectedTilePoints.Count; i++)
        {
            line.SetPosition(i, selectedTilePoints[i]);
        }
        line.SetPosition(line.positionCount - 1, mainCamera.ScreenToWorldPoint(Input.mousePosition));
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelecting)
        {
            currentWordText.text = currentWord;

        }
        line.SetPosition(line.positionCount - 1, mainCamera.ScreenToWorldPoint(Input.mousePosition));

    }
}
