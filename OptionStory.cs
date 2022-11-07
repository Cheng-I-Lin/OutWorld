using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionStory : MonoBehaviour
{
    private List<string> sentenceList=new List<string>();
    private List<string> speakerList=new List<string>();
    private List<GameObject> movingPathList=new List<GameObject>();
    private List<GameObject> NPCList=new List<GameObject>();
    public float speed=3f;
    private Movements player;
    private int index=0;
    public Animator animator;
    public GameObject AutoDialogue;
    private dialogue auto;
    public string[] sentence;
    public int[] sentenceCutOff;
    public string[] speakerName;
    public int[] nameCutOff;
    public Sprite[] speakerImg; 
    public GameObject[] movingPoints;
    public int[] pointCutOff;
    public GameObject[] NPC;
    public int[] NPCCutOff;
    public Image oldImg;
    public bool shouldTalk=false;
    public bool shouldPlayerMove=true;
    private OptionStory script;
    private int cutOffIndex=0;

    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.Find("Player").GetComponent<Movements>();
        script=GameObject.Find("Player").GetComponent<OptionStory>();
        auto=AutoDialogue.GetComponent<dialogue>();
        player.enabled=false;
        oldImg.enabled=false;
        if(sentence.Length<cutOffIndex){
            for(int i=0;i<sentenceCutOff[cutOffIndex];i++)
            {
                sentenceList.Add(sentence[i]);
                speakerList.Add(speakerName[i]);
                movingPathList.Add(movingPoints[i]);
                if(NPC.Length!=0){
                    NPCList.Add(NPC[i]);
                }
            }
        }
        if(shouldTalk&&sentenceList.Count!=0)
        {
            auto.Restart(sentence);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldTalk)
        {
            //Show/Hide speaker image with dialogue box
            if(auto.hidden)
            {
                oldImg.enabled=false;
                shouldTalk=false;
            }
            else
            {
                oldImg.enabled=true;
            }
            //Changes image of speaker with different lines
            switch(speakerName[auto.index])
            {
                case "Player":
                    oldImg.sprite=speakerImg[0];
                    break;
                case "NPC":
                    oldImg.sprite=speakerImg[1];
                    break;
                case "Villager 1":
                    oldImg.sprite=speakerImg[2];
                    break;
                case "Villager 2":
                    oldImg.sprite=speakerImg[3];
                    break;
                case "Mouse":
                    oldImg.sprite=speakerImg[4];
                    break;
                default:
                    break;
            }
        } 
        else
        {
            if(shouldPlayerMove)
            {
                if(index<pointCutOff[cutOffIndex])
                {
                    //Checks which direction the player should go
                    if(movingPoints.Length>0&&Vector3.Distance(transform.position,movingPoints[index].transform.position)<=0.1f){
                        if(index<movingPoints.Length)
                        {
                            index++;
                        }
                    }
                }
                if(movingPoints.Length>0&&index<pointCutOff[cutOffIndex])
                {
                    //Moves the player to the next point with a speed relative to the frames per second
                    transform.position=Vector3.MoveTowards(transform.position,movingPoints[index].transform.position,speed*Time.deltaTime);
                    foreach(GameObject obj in NPC)
                    {
                        obj.transform.position=Vector3.MoveTowards(obj.transform.position,movingPoints[index].transform.position,speed*Time.deltaTime);
                    }
                    animator.SetFloat("Horizontal",movingPoints[index].transform.position.x-transform.position.x);
                    animator.SetFloat("Vertical",movingPoints[index].transform.position.y-transform.position.y);
                    animator.SetFloat("Speed",speed);
                }
                if(Vector3.Distance(transform.position,movingPoints[pointCutOff[cutOffIndex]-1].transform.position)<=0.1f)
                {
                    if(cutOffIndex==pointCutOff.Length-1){
                        script.enabled=false;
                        player.enabled=true;
                    }
                    cutOffIndex++;
                    shouldTalk=true;
                    sentenceList=new List<string>();
                    speakerList=new List<string>();
                    movingPathList=new List<GameObject>();
                    NPCList=new List<GameObject>();
                    if(sentence.Length<cutOffIndex){
                        for(int i=0;i<sentenceCutOff[cutOffIndex];i++)
                        {
                            sentenceList.Add(sentence[i]);
                            speakerList.Add(speakerName[i]);
                            movingPathList.Add(movingPoints[i]);
                            if(NPC.Length!=0){
                                NPCList.Add(NPC[i]);
                            }
                        }
                    }
                    if(shouldTalk&&sentenceList.Count!=0)
                    {
                        auto.Restart(sentence);
                    }
                }
            }
            else
            {
                if(NPC.Length>0)
                {
                    if(index<pointCutOff[cutOffIndex])
                    {
                        //Checks which direction the NPC should go
                        if(movingPoints.Length>0&&Vector3.Distance(NPC[0].transform.position,movingPoints[index].transform.position)<=0.1f){
                            if(index<movingPoints.Length)
                            {
                                index++;
                            }
                        }
                    }
                    if(movingPoints.Length>0&&index<pointCutOff[cutOffIndex])
                    {
                        //Moves the NPC to the next point with a speed relative to the frames per second
                        foreach (GameObject obj in NPC)
                        {
                            obj.transform.position=Vector3.MoveTowards(obj.transform.position,movingPoints[index].transform.position,speed*Time.deltaTime);
                        }
                    }
                    if(Vector3.Distance(NPC[0].transform.position,movingPoints[pointCutOff[cutOffIndex]-1].transform.position)<=0.1f||!NPC[NPC.Length-1].activeSelf)
                    {
                        if(cutOffIndex==pointCutOff.Length-1){
                            script.enabled=false;
                            player.enabled=true;
                        }
                        cutOffIndex++;
                        shouldTalk=true;
                        sentenceList=new List<string>();
                        speakerList=new List<string>();
                        movingPathList=new List<GameObject>();
                        NPCList=new List<GameObject>();
                        if(sentence.Length<cutOffIndex){
                            for(int i=0;i<sentenceCutOff[cutOffIndex];i++)
                            {
                                sentenceList.Add(sentence[i]);
                                speakerList.Add(speakerName[i]);
                                movingPathList.Add(movingPoints[i]);
                                if(NPC.Length!=0){
                                    NPCList.Add(NPC[i]);
                                }
                            }
                        }
                        if(shouldTalk&&sentenceList.Count!=0)
                        {
                            auto.Restart(sentence);
                        }
                    }
                }
            }
        }
    }
}
//Think about how to make sure that some sentences may not be played
//Should check length of sentencelist or make bool shouldtalk for every sentence