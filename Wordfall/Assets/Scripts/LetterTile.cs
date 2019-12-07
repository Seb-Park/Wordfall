using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterTile : MonoBehaviour
{
    public string letter;
    public TextMeshPro textMesh;
    public GameManager gm;

    public Animator anim;
    public GameObject glow;

    public bool isSelected;
    [SerializeField]
    int indexInSelection;
    [SerializeField]
    bool hasExited;

    Vector2 tilesAway;

    public bool randomLetter = true;

    public int column, row;
    //TODO: Change color of tiles, dark mode, upper/lower case
    //long id;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        if (randomLetter) { GenerateLetter(); }
        textMesh.text = letter;
        hasExited = true;
        column = (int)Mathf.Round(transform.position.x) + 2;
    }

    void GenerateLetter(){
        letter = gm.RandomLetter();
    }

    public void SubtractLetter(){
        //if(isSelected){
        TurnOffGlow();
            isSelected = false;
            for (int i = indexInSelection; i < gm.selectedTiles.Count; i++){
                gm.selectedTiles[gm.selectedTiles.Count - 1].TurnOffGlow();
                gm.selectedTiles[gm.selectedTiles.Count - 1].isSelected = false;
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

        //if (gm.selectedTiles.Count < 1)
        //{
        //    Debug.Log("Starting at " + letter + " and the is selecting bool is " + gm.isSelecting);
        //}

        if (gm.isSelecting && !isSelected)
        {
            isSelected = true;
            TurnOnGlow();
            indexInSelection = gm.selectedTiles.Count;
            gm.selectedTiles.Add(this);
            gm.selectedTilePoints.Add(transform.position);
            gm.UpdateWord();
        }

        anim.SetTrigger("Expand");
        //if (gm.selectedTiles.Count == 1)
        //{
        //    Debug.Log("Starting at " + letter + " and the isselected bool is " + isSelected);
        //}
    }

    public void TurnOnGlow(){
        glow.SetActive(true);
    }

    public void TurnOffGlow(){
        glow.SetActive(false);
    }

    public void OnColliderMouseDown()
    {
        if (row < 7)
        {
            AddLetter();
        }
        hasExited = false;
    }

    public void OnColliderMouseOver()
    {
        if (gm.isSelecting)
        {
            if (hasExited)
            {
                tilesAway = new Vector2(Mathf.Abs(gm.selectedTiles[gm.selectedTiles.Count - 1].column - column), Mathf.Abs(gm.selectedTiles[gm.selectedTiles.Count - 1].row - row));
                if (!isSelected&&row<7&&tilesAway.x<2&&tilesAway.y<2)//make sure it's not already selected and it's on screen//TODO: Change later if column system changed
                {
                    AddLetter();
                }
                else if(indexInSelection != 0&&indexInSelection>gm.selectedTiles.Count-3)//if it's not the first one
                {
                    if (indexInSelection < gm.selectedTiles.Count - 1)
                    {
                        SubtractLetter();
                        isSelected = true;//This fixes the double selection glitch
                        TurnOnGlow();
                    }
                    else{
                        SubtractLetter();
                    }
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
