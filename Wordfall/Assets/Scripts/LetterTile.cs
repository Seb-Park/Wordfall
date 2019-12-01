using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterTile : MonoBehaviour
{
    public string letter;
    public TextMeshPro textMesh;
    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        textMesh.text = letter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        if (gm.state.Equals(GameState.PLAYERTURN))
        {
            gm.isSelecting = true;
        }
    }
}
