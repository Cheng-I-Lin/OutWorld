using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryMode : MonoBehaviour
{
    //Automatically let player walk to designated locations
    public GameObject[] movingPoints;
    //Other characters and npcs that can move with player or alone
    public GameObject[] NPC;
    public float speed=3f;
    private Movements player;
    private int index=0;
    public Animator animator;
    public GameObject AutoDialogue;
    private dialogue auto;
    public string[] sentence;
    public string[] speakerName;
    public Sprite[] speakerImg; 
    public Image oldImg;
    public bool shouldTalk=false;
    public bool shouldPlayerMove=true;
    private StoryMode script;

    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.Find("Player").GetComponent<Movements>();
        script=GameObject.Find("Player").GetComponent<StoryMode>();
        auto=AutoDialogue.GetComponent<dialogue>();
        player.enabled=false;
        oldImg.enabled=false;
        if(shouldTalk)
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
                if(index<movingPoints.Length)
                {
                    //Checks which direction the player should go
                    if(movingPoints.Length>0&&Vector3.Distance(transform.position,movingPoints[index].transform.position)<=0.1f){
                        if(index<movingPoints.Length)
                        {
                            index++;
                        }
                    }
                }
                if(movingPoints.Length>0&&index<movingPoints.Length)
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
                if(Vector3.Distance(transform.position,movingPoints[movingPoints.Length-1].transform.position)<=0.1f)
                {
                    script.enabled=false;
                    player.enabled=true;
                }
            }
            else
            {
                if(NPC.Length>0)
                {
                    if(index<movingPoints.Length)
                    {
                        //Checks which direction the NPC should go
                        if(movingPoints.Length>0&&Vector3.Distance(NPC[0].transform.position,movingPoints[index].transform.position)<=0.1f){
                            if(index<movingPoints.Length)
                            {
                                index++;
                            }
                        }
                    }
                    if(movingPoints.Length>0&&index<movingPoints.Length)
                    {
                        //Moves the NPC to the next point with a speed relative to the frames per second
                        foreach (GameObject obj in NPC)
                        {
                            obj.transform.position=Vector3.MoveTowards(obj.transform.position,movingPoints[index].transform.position,speed*Time.deltaTime);
                        }
                    }
                    if(Vector3.Distance(NPC[0].transform.position,movingPoints[movingPoints.Length-1].transform.position)<=0.1f||!NPC[NPC.Length-1].activeSelf)
                    {
                        script.enabled=false;
                        player.enabled=true;
                    }
                }
            }
        }
    }
}
