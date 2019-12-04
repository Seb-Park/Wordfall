using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreNumber : MonoBehaviour
{
	public GameManager gm;
    public TextMeshProUGUI thisText;
    public float arrivalTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(scoreAnimate(gm.score));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator scoreAnimate(int endNumber)
    {
        float intervalTime = arrivalTime / endNumber;
        for (int i = 0; i <= endNumber; i++)
        {
            thisText.text = i.ToString();
            yield return new WaitForSeconds(intervalTime);
        }
    }
}
