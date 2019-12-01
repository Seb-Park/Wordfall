using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{PLAYERTURN, PHYSICS, WIN, LOSE}

public class GameManager : MonoBehaviour
{
    public GameState state;
    public string currentWord;
    public bool isSelecting;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
