using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
//using UnityEditor;
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
    public TMP_FontAsset transparentFont, greenFont;
    public LineRenderer line;

    public List<LetterTile> selectedTiles;
    public List<Vector2> selectedTilePoints;

    Dictionary<string, bool> wordDictionary;

    Dictionary<string, Color> letterColorPairs = new Dictionary<string, Color>();

    public TextAsset dictionaryFile;

    string alphabet = "abcdefghijklmnopqrstuvwxyz";

    // Start is called before the first frame update

    void Start()
    {
        UpdateDictionary();
        //letterColorPairs.Add("a", Color.red);
        line.positionCount = 0;
        line.positionCount = 1;
        mainCamera = Camera.main;   
    }

    //[MenuItem("Tools/Read file")]
    public void UpdateDictionaryFromRead(){
        wordDictionary = new Dictionary<string, bool>();
        string path = "Assets/Resources/Word Dictionaries/scrabble.txt";
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }


    public void UpdateDictionary(){
        wordDictionary = new Dictionary<string, bool>();
        string[] words = dictionaryFile.text.Split('\n');
        foreach(string w in words){
            wordDictionary.Add(w, true);
            //Debug.Log(w);
        }
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

        currentWordText.font = wordDictionary.ContainsKey(currentWord.ToUpper()) ? greenFont : transparentFont;
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
        if(c == 'q'){
            return "qu";
        }
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
            if (wordDictionary.ContainsKey(currentWord.ToUpper())||currentWord == "go")
            {
                ClearWord();
            }
            else{
                for (int i = 0; i < selectedTiles.Count; i++)
                {
                    selectedTiles[i].isSelected = false;
                }
                line.positionCount = 1;
                selectedTilePoints.Clear();
                selectedTiles.Clear();
                UpdateWord();
                currentWordText.text = currentWord;
            }
        }
    }
}
