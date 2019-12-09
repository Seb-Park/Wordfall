using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject FadeInTransition;

    public GameObject MainCanvas;
    public Animator MainCanvasAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WaitBeforeSettingActiveFalse(GameObject item){
        StartCoroutine(WaitBeforeActive(item, 3f, false));
    }

    public void OpenMainFromSettings(GameObject mainMenu){
        mainMenu.SetActive(true);
        mainMenu.GetComponent<Animator>().SetTrigger("appear");
    }

    public IEnumerator WaitBeforeActive(GameObject item, float loadTime, bool activityState){
        yield return new WaitForSeconds(loadTime);
        Debug.Log("Setting " + item.name + " to active (" + activityState + ")");
        item.SetActive(activityState);
    }

    public void OpenTimed(){
        FadeInTransition.SetActive(true);
        StartCoroutine(WaitBeforeLoadingScene(1, 1));
    }

    public IEnumerator WaitBeforeLoadingScene(int scene, float loadTime){
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene(scene);
    }

    public void HideMainCanvas(){
        MainCanvasAnimator.SetTrigger("disappear");
        StartCoroutine(WaitBeforeActive(MainCanvas, 2f, false));
    }

    public void ShowMainCanvas()
    {
        MainCanvasAnimator.SetTrigger("appear");
        MainCanvas.SetActive(true);
        //StartCoroutine(WaitBeforeActive(MainCanvas, 3f, true));
    }
}
