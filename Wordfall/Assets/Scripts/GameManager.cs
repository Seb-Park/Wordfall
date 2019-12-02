using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using System.Linq;

public enum GameState{PLAYERTURN, PHYSICS, WIN, LOSE}

public class GameManager : MonoBehaviour
{
    //TODO: Generate the letters to make more fall down.
    public GameState state;
    public string currentWord;
    public bool isSelecting;

    public Camera mainCamera;

    public TileSpawner ts;

    public TextMeshProUGUI currentWordText;
    public LineRenderer line;

    public List<LetterTile> selectedTiles;
    public List<Vector2> selectedTilePoints;

    Dictionary<string, bool> wordDictionary;

    Dictionary<string, Color> letterColorPairs = new Dictionary<string, Color>();

    string alphabet = "abcdefghijklmnopqrstuvwxyz";

    // Start is called before the first frame update
    void Start()
    {
        wordDictionary = new Dictionary<string, bool>();
        //letterColorPairs.Add("a", Color.red);
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
        line.SetPosition(line.positionCount - 1, (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition));
    }

    void ClearWord(){
        state = GameState.PHYSICS;
        ts.unfreezeBlocks();
        //if it's not a word
        for (int i = 0; i < selectedTiles.Count; i++)
        {
            selectedTiles[i].isSelected = false;
        }

        //if it is a word
        for (int i = 0; i < selectedTiles.Count; i++)
        {
            Destroy(selectedTiles[i].gameObject);
        }

        line.positionCount = 1;
        selectedTilePoints.Clear();
        selectedTiles.Clear();
        //Debug.Log("Clearing the lists... The length of selected Tile Points is " + selectedTilePoints.Count + " and the length of selected tiles is " + selectedTiles.Count);
        UpdateWord();
        currentWordText.text = currentWord;
        //int [] temp = ts.countColumns();
        //Debug.Log("Before spawning accordingly the count is " + string.Join(", ", temp.Select(i => i.ToString()).ToArray()));
        StartCoroutine(ts.spawnAccording());

        //temp = ts.countColumns();
        //Debug.Log("After spawning accordingly the count is " + string.Join(", ", temp.Select(i => i.ToString()).ToArray()));
        //TODO: Word length = life, word = more time, scrabble probability, some timed modes, some fixed modes, one way valves on top of letter, 

    }

    public string RandomLetter()
    {
        char c = alphabet[Random.Range(0,alphabet.Length)];
        return c.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelecting)
        {
            currentWordText.text = currentWord;

        }
        line.SetPosition(line.positionCount - 1, (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition));
        if(Input.GetMouseButtonUp(0)&&isSelecting){
            isSelecting = false;
            ClearWord();
        }
    }
}
