using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquircleTouchCollider : MonoBehaviour
{
    public LetterTile parentTile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Mouse down on collider!");
        parentTile.OnColliderMouseDown();
    }

    private void OnMouseOver()
    {
        Debug.Log("Mouse over on collider!");

        parentTile.OnColliderMouseOver();
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse exit on collider!");

        parentTile.OnColliderMouseExit();
    }
}
