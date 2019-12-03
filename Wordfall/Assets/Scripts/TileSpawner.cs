using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public Transform[] spawnpoints;
    public LetterTile[] allCurrentTiles;
    public GameObject tilePrefab;

    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator spawnAccording(){
        yield return new WaitForSeconds(0.2f);
        int[] tileInEach = countColumns();
        for (int i = 0; i < spawnpoints.Length; i++){
            StartCoroutine(spawnTilesInRow(tileInEach[i], spawnpoints[i]));
        }
    }

    public IEnumerator spawnTilesInRow(int tileInColumn, Transform spawnpoint){
        for (int k = 0; k < 14 - tileInColumn; k++)
        {
            Instantiate(tilePrefab, spawnpoint);
            yield return new WaitForSeconds(0.1f);

        }
        yield return new WaitForSeconds(1f);
        assignBlocks();
        yield return new WaitForSeconds(1f);
        gm.state = GameState.PLAYERTURN;
        freezeBlocks();
    }

    public void freezeBlocks(){
        GameObject[] tileGOs = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject go in tileGOs)
        {
            LetterTile letterTileComponent = go.GetComponent<LetterTile>();
            go.transform.position = new Vector2(letterTileComponent.column-2, letterTileComponent.row - 3);
            letterTileComponent.isSelected = false;
            letterTileComponent.TurnOffGlow();

            foreach (GameObject go2 in tileGOs){
                LetterTile letterTileComponent2 = go2.GetComponent<LetterTile>();
                if(go2!=go&&letterTileComponent.column == letterTileComponent2.column&&letterTileComponent.row == letterTileComponent2.row){
                    Destroy(go2);
                    //Debug.Log("Tile was destroyed because columns were " + letterTileComponent.column + letterTileComponent2.column
                              //+ " and rows were " + letterTileComponent.row + letterTileComponent2.row);
                }
            }

            go.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public void unfreezeBlocks()
    {
        GameObject[] tileGOs = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject go in tileGOs)
        {
            go.transform.position = new Vector2(go.GetComponent<LetterTile>().column - 2, go.GetComponent<LetterTile>().row - 3);
            go.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public void assignBlocks(){
        GameObject[] tileGOs = GameObject.FindGameObjectsWithTag("Tile");
        foreach(GameObject go in tileGOs){
            go.GetComponent<LetterTile>().row = (int)Mathf.Round(go.transform.position.y) + 3;
        }
        //Debug.Log("Freezing blocks");
    }

    public int[] countColumns(){
        GameObject[] tileGOs = GameObject.FindGameObjectsWithTag("Tile");
        int[] tileInEach = new int[5];
        for (int i = 0; i < tileInEach.Length; i++){
            tileInEach[i] = 0;
        }
        for (int i = 0; i < tileGOs.Length; i++){
            tileInEach[tileGOs[i].GetComponent<LetterTile>().column]+=1;
        }
        return tileInEach;
    }
}
