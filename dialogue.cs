using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialogue : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    private string[] lines;
    public float textSpeed;
    public int index;
    public TextMeshProUGUI continueText;
    public bool hidden;
    public GameObject player;
    public bool finishLine;

    // Start is called before the first frame update
    void Start()
    {
        continueText.enabled=false;
        textComponent.text=string.Empty;
        //StartDialogue();
        hidden=true;
        finishLine=false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(index==lines.Length-1&&textComponent.text==lines[index])
        {
            finishLine=true;
        }
        else
        {
            finishLine=false;
        }
        if(textComponent.text==lines[index])
        {
            continueText.enabled=true;
        }
        else
        {
            continueText.enabled=false;
        }
        if(Input.GetKeyDown("space"))
        {
            if(textComponent.text==lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text=lines[index];
            }
        }
        //Skip dialogue
        if(Input.GetKeyDown("q"))
        {
            quitConversation();
        }
    }

    public void quitConversation()
    {       
        index=lines.Length-1;
        NextLine();
    }

    public void Restart(string[] sentence)
    {
        lines=sentence;
        gameObject.SetActive(true);
        continueText.enabled=false;
        textComponent.text=string.Empty;
        StartDialogue();
        hidden=false;
        finishLine=false;
        player.GetComponent<Movements>().enabled=false;
    }

    void StartDialogue()
    {
        index=0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        //Type each character one by one
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text+=c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index<lines.Length-1)
        {
            index++;
            textComponent.text=string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            hidden=true;
            //Starts player movement
            if(!player.GetComponent<StoryMode>().enabled)
            {
                player.GetComponent<Movements>().enabled=true;
            }
        }
    }
}
