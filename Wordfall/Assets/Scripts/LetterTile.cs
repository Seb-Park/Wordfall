using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterTile : MonoBehaviour
{
    public string letter;
    public TextMeshPro textMesh;
    public GameManager gm;

    [SerializeField]
    int indexInSelection;
    bool isSelected;
    bool hasExited;
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

    public void SubtractLetter(){
        if(isSelected){
            isSelected = false;
            for (int i = indexInSelection; i < gm.selectedTiles.Count; i++){
                gm.selectedTiles.RemoveAt(indexInSelection);
                gm.selectedTilePoints.RemoveAt(indexInSelection);
            }
            gm.UpdateWord();
        }
    }

    public void AddLetter(){
        if (gm.state.Equals(GameState.PLAYERTURN))
        {
            gm.isSelecting = true;
        }
        if (gm.isSelecting && !isSelected)
        {
            isSelected = true;
            indexInSelection = gm.selectedTiles.Count;
            gm.selectedTiles.Add(this);
            gm.selectedTilePoints.Add(transform.position);
            gm.UpdateWord();
        }
    }

    private void OnMouseDown()
    {
        AddLetter();
    }

    private void OnMouseOver()
    {
        if(gm.isSelecting){
            if (!isSelected)
            {
                AddLetter();
            }
        }
        if(hasExited){
            if (!isSelected)
            {
                AddLetter();
            }
            else{
                SubtractLetter();
            }
            hasExited = false;
        }
    }

    private void OnMouseExit()
    {
        hasExited = true;
    }
}
