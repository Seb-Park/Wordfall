using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterTile : MonoBehaviour
{
    public string letter;
    public TextMeshPro textMesh;
    public GameManager gm;

    public bool isSelected;
    [SerializeField]
    int indexInSelection;
    [SerializeField]
    bool hasExited;

    //long id;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        GenerateLetter();
        textMesh.text = letter;
        hasExited = true;
    }

    void GenerateLetter(){
        letter = gm.RandomLetter();
    }

    public void SubtractLetter(){
        //if(isSelected){
            isSelected = false;
            for (int i = indexInSelection; i < gm.selectedTiles.Count; i++){
                gm.selectedTiles.RemoveAt(gm.selectedTiles.Count-1);
                gm.selectedTilePoints.RemoveAt(gm.selectedTilePoints.Count - 1);
            }
            gm.UpdateWord();
        //}
    }

    //TODO: Fix the glitch where the letter does not start correctly when clicked on for the second or third round of selecting
    //TODO: Fix the glitch where you can occasionally go over letters twice
    //TODO: Fix the glitch where the letters get selected or deselected when mouse goes over the collider
    public void AddLetter(){

        if (gm.state.Equals(GameState.PLAYERTURN))
        {
            gm.isSelecting = true;
        }

        if (gm.selectedTiles.Count < 1)
        {
            Debug.Log("Starting at " + letter + " and the is selecting bool is " + gm.isSelecting);
        }

        if (gm.isSelecting && !isSelected)
        {
            isSelected = true;
            indexInSelection = gm.selectedTiles.Count;
            gm.selectedTiles.Add(this);
            gm.selectedTilePoints.Add(transform.position);
            gm.UpdateWord();
        }
        if (gm.selectedTiles.Count == 1)
        {
            Debug.Log("Starting at " + letter + " and the isselected bool is " + isSelected);
        }
    }



    public void OnColliderMouseDown()
    {
        AddLetter();
        hasExited = false;
    }

    public void OnColliderMouseOver()
    {
        if (gm.isSelecting)
        {
            if (hasExited)
            {
                if (!isSelected)
                {
                    AddLetter();
                }
                else if(indexInSelection != 0&&indexInSelection>gm.selectedTiles.Count-3)//if it's not the first one
                {
                    SubtractLetter();
                }
                hasExited = false;
            }
        }
    }

    public void OnColliderMouseExit()
    {
        hasExited = true;
    }

    private void OnMouseDown()
    {
        OnColliderMouseDown();
    }
    private void OnMouseOver()
    {
        OnColliderMouseOver();
    }
    private void OnMouseExit()
    {
        OnColliderMouseExit();
    }
}
