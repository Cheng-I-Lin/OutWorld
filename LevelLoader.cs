using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{

    public int buttonNum=0;
    public bool pressButton=false;

    public void LoadLevel(int sceneIndex) 
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }  

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation=SceneManager.LoadSceneAsync(sceneIndex);
        while(!operation.isDone)
        {
            //Debug.Log(operation.progress);
            yield return null;
        }
    }

    public void HidePage(GameObject page)
    {
        page.SetActive(false);
    }

    public void ShowPage(GameObject page)
    {
        page.SetActive(true);
    }

    public void Menu(GameObject menu)
    {
        if(menu.activeSelf)
        {
            menu.SetActive(false);
        }
        else
        {
            ShowMenu(menu);
        }
    }

    private void ShowMenu(GameObject menu)
    {
        menu.SetActive(true);
    }

    public void ChangeVolume()
    {
        //audioVolume=GameObject.Find("Volume").GetComponent<Slider>().value;
        //Debug.Log(audioVolume);
    }

    public void responseOption(int num)
    {
        buttonNum=num;
        pressButton=true;
    }
}
