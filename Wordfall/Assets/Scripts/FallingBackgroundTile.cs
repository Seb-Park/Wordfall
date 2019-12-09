using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FallingBackgroundTile : MonoBehaviour
{
    float speed;

    public Image thisImage;

    public TextMeshProUGUI myText;

    string alphabet = "abcdefghijklmnopqrstuvwxyz" + "abcdefghijklmnopqrstuvwxyz".ToUpper();

    // Start is called before the first frame update
    void Start()
    {
        thisImage.color = Random.ColorHSV(0,1.0f,0,0.5f);
        speed = Random.Range(1.0f, 7.0f);
        transform.localScale = new Vector2(speed/5,speed/5);
        myText.text = alphabet[(int)Mathf.Round(Random.Range(0, 52))].ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector2(0, -speed));
        if(transform.localPosition.y<-1000){
            myText.text = alphabet[(int)Mathf.Round(Random.Range(0, 52))].ToString();
            transform.localPosition = new Vector2(transform.localPosition.x, 1000);
            thisImage.color = Random.ColorHSV(0, 1.0f, 0, 0.5f);
        }
    }
}
