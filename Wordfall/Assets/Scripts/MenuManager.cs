using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject FadeInTransition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenTimed(){
        FadeInTransition.SetActive(true);
        StartCoroutine(WaitBeforeLoadingScene(1, 1));
    }

    public IEnumerator WaitBeforeLoadingScene(int scene, float loadTime){
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene(scene);
    }
}
