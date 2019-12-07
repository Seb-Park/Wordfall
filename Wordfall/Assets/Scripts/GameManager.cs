using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;
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
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreNumberText;
    public TMP_FontAsset transparentFont, greenFont;
    public LineRenderer line;

    public List<LetterTile> selectedTiles;
    public List<Vector2> selectedTilePoints;

    public bool started;

    public float timeStarted;
    public float timeLeft;
    public float totalTime;

    public GameObject countdownCanvas, endCanvas;

    public int score;

    Dictionary<string, bool> wordDictionary;

    Dictionary<string, Color> letterColorPairs = new Dictionary<string, Color>();

    public TextAsset dictionaryFile;

    string alphabet = "abcdefghijklmnopqrstuvwxyz";
    public int[] letterWeights;

    // Start is called before the first frame update

    void Start()
    {
        UpdateDictionary();
        //letterColorPairs.Add("a", Color.red);
        line.positionCount = 0;
        line.positionCount = 1;
        mainCamera = Camera.main;
        score = 0;
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
        if (!started)
        {
            started = true;
            timeStarted = Time.time;
        }
        state = GameState.PHYSICS;
        ts.unfreezeBlocks();
        //if it's not a word
        for (int i = 0; i < selectedTiles.Count; i++)
        {
            selectedTiles[i].isSelected = false;
            selectedTiles[i].TurnOffGlow();
        }

        //if it is a word
        for (int i = 0; i < selectedTiles.Count; i++)
        {
            Destroy(selectedTiles[i].gameObject);
        }

        //addScore((int)(Mathf.Pow(currentWord.Length, 2)/2));
        addScore((int)Mathf.Pow(2, currentWord.Length-1));


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

    public void addScore(int increase){
        score += increase;
        scoreText.text = score.ToString();
    }

    public string RandomLetter()
    {
        char c = alphabet[CustomMath.WeightedRandomize(letterWeights)];
        if (c == 'q'){
            return "qu";
        }
        return c.ToString();
    }

    public void Deselect(){
        for (int i = 0; i < selectedTiles.Count; i++)
        {
            selectedTiles[i].isSelected = false;
            selectedTiles[i].TurnOffGlow();
        }
        line.positionCount = 1;
        selectedTilePoints.Clear();
        selectedTiles.Clear();
        UpdateWord();
        currentWordText.text = currentWord;
    }

    public IEnumerator scoreAnimate(int endNumber){
        float intervalTime = 2.0f / endNumber;
        //Debug.Log(intervalTime);
        for (int i = 0; i <= endNumber; i++){
            scoreNumberText.text = i.ToString();
            yield return new WaitForSeconds(intervalTime);
            //Debug.Log(i);
        }
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
            if (wordDictionary.ContainsKey(currentWord.ToUpper()))
            {
                ClearWord();
            }
            else{
                Deselect();
            }
        }
        if(started){
            timeLeft = totalTime - (Time.time - timeStarted);
            if (!(state == GameState.WIN))
            {
                timeText.text = timeLeft.ToString("f0");
            }
            if(timeLeft<=5){
                countdownCanvas.SetActive(true);
            }
            if(timeLeft<=0){
                isSelecting = false;
                Deselect();
                state = GameState.WIN;
                endCanvas.SetActive(true);
            }
            if(timeLeft<-2){
                countdownCanvas.SetActive(false);
                scoreNumberText.gameObject.SetActive(true);
            }
        }
    }

    public void loadScene(int sceneNumber){
        SceneManager.LoadScene(sceneNumber);
    }
}
