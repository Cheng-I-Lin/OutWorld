using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


namespace Array2DEditor
{
    public class OptionDialogue : MonoBehaviour
    {

        private StoryMode storyScript;
        public GameObject talkingNPC;
        private dialogue NPCDialogue;
        //Sets which lines should option buttons come out
        public int[] optionIndex;
        public string[] optionTitle;
        private List<string> titleList=new List<string>();
        public int[] titleCutOff;
        /*
        public Array2DString speakerName;
        public Array2DString sentence;
        private List<string> sentenceList = new List<string>();
        private List<string> speakerList = new List<string>();
        */
        private List<string> optionList=new List<string>();
        private List<string> speakerList=new List<string>();
        //Starts conversation
        public string[] option0;
        public string[] speaker0;

        public string[] option1;
        public string[] speaker1;
        public int[] cutOff1;

        public string[] option2;
        public string[] speaker2;
        public int[] cutOff2;

        public string[] option3;
        public string[] speaker3;
        public int[] cutOff3;

        public string[] option4;
        public string[] speaker4;
        public int[] cutOff4;
        
        public int index;
        public GameObject NPC;
        public Sprite[] speakerImg;
        public Image oldImg;
        private bool canTalk=false;
        public TextMeshProUGUI signText;
        public GameObject sign;
        public string message;
        private Camera cam;
        //Offset sign postion to make it on top of npc head
        private Vector3 offset=new Vector3(0,0.75f,0);
        private LevelLoader button;
        public GameObject[] optionButtons;
        public int[] buttonCutOff;
        public TextMeshProUGUI[] buttonText;
        public int buttonPressedNum;
        private int previousIndex;
        public bool shouldTransitionAllButton=false;
        public bool shouldTransitionOneButton=false;
        public int transition;
        public int transitionLevel;
        public GameObject textBubble;

        // Start is called before the first frame update
        void Start()
        {
            titleList=new List<string>();
            optionList=new List<string>();
            speakerList=new List<string>();
            button=GameObject.Find("Function").GetComponent<LevelLoader>();
            storyScript=GameObject.Find("Player").GetComponent<StoryMode>();
            NPCDialogue=talkingNPC.GetComponent<dialogue>();
            signText.text=string.Empty;
            sign.SetActive(false);
            textBubble.SetActive(false);
            cam=Camera.main;
            oldImg.enabled=false;
            index=0;
            previousIndex=-1;
            for(int i=0;i<option0.Length;i++)
            {
                optionList.Add(option0[i]);
                speakerList.Add(speaker0[i]);
            }
            buttonPressedNum=-1;
        }

        void Update()
        {
            if(!storyScript.enabled){
                //Update each text options
                var num=0;
                for(int i=titleCutOff[buttonPressedNum+1];i<titleCutOff[buttonPressedNum+2];i++)
                {
                    buttonText[num].text=optionTitle[i];
                    num++;
                }
                if(button.pressButton)
                {   
                    titleList=new List<string>();
                    optionList=new List<string>();
                    speakerList=new List<string>();
                    buttonPressedNum++;
                    foreach(GameObject obj in optionButtons)
                    {
                        obj.SetActive(false);
                    }
                    index=button.buttonNum;
                    switch(index)
                    {
                        case 1:
                            for(int i=cutOff1[buttonPressedNum];i<cutOff1[buttonPressedNum+1];i++)
                            {
                                optionList.Add(option1[i]);
                                speakerList.Add(speaker1[i]);
                            }
                            break;
                        case 2:
                            for(int i=cutOff2[buttonPressedNum];i<cutOff2[buttonPressedNum+1];i++)
                            {
                                optionList.Add(option2[i]);
                                speakerList.Add(speaker2[i]);
                            }
                            break;
                        case 3:
                            for(int i=cutOff3[buttonPressedNum];i<cutOff3[buttonPressedNum+1];i++)
                            {
                                optionList.Add(option3[i]);
                                speakerList.Add(speaker3[i]);
                            }
                            break;
                        case 4:
                            for(int i=cutOff4[buttonPressedNum];i<cutOff4[buttonPressedNum+1];i++)
                            {
                                optionList.Add(option4[i]);
                                speakerList.Add(speaker4[i]);
                            }
                            break;
                        default:
                            break;
                    }
                    if(optionList.Count==0)
                    {
                        index=0;
                        previousIndex=-1;
                        titleList=new List<string>();
                        optionList=new List<string>();
                        speakerList=new List<string>();
                        for(int i=0;i<option0.Length;i++)
                        {
                            optionList.Add(option0[i]);
                            speakerList.Add(speaker0[i]);
                        }
                        buttonPressedNum-=1;
                        foreach(GameObject obj in optionButtons)
                        {
                            obj.SetActive(false);
                        }
                        NPCDialogue.quitConversation();
                    }
                    else
                    {
                        NPCDialogue.Restart(optionList.ToArray());
                    }
                    button.pressButton=false;
                }
                //Show buttons when time to make decision
                for(int i=0;i<optionIndex.Length;i++)
                {
                    if(optionIndex[i]==index&&index!=previousIndex&&!NPCDialogue.hidden&&NPCDialogue.finishLine)
                    {    
                        switch(buttonCutOff[buttonPressedNum+1])
                        {
                            case 1:
                                optionButtons[0].SetActive(true);
                                break;
                            case 2:
                                optionButtons[0].SetActive(true);
                                optionButtons[1].SetActive(true);
                                break;
                            case 3:
                                optionButtons[0].SetActive(true);
                                optionButtons[1].SetActive(true);
                                optionButtons[2].SetActive(true);
                                break;
                            case 4:
                                optionButtons[0].SetActive(true);
                                optionButtons[1].SetActive(true);
                                optionButtons[2].SetActive(true);
                                optionButtons[3].SetActive(true);
                                break;
                            default:
                                break;
                        }
                        previousIndex=index;
                    }
                }
                //Enable this only when triggered so multiple npc work
                if(canTalk)
                {
                    if(((shouldTransitionAllButton&&buttonPressedNum==transition)||(shouldTransitionOneButton&&button.buttonNum==transition))&&NPCDialogue.hidden)
                    {
                        GameObject.Find("Player").GetComponent<LevelLoader>().LoadLevel(transitionLevel);
                    }
                    //Show/Hide speaker image with dialogue box
                    if(NPCDialogue.hidden)
                    {
                        index=0;
                        previousIndex=-1;
                        buttonPressedNum=-1;
                        oldImg.enabled=false;
                        titleList=new List<string>();
                        optionList=new List<string>();
                        speakerList=new List<string>();
                        for(int i=0;i<option0.Length;i++)
                        {
                            optionList.Add(option0[i]);
                            speakerList.Add(speaker0[i]);
                        }
                        for(int i=titleCutOff[0];i<titleCutOff[1];i++)
                        {
                            buttonText[i].text=optionTitle[i];
                        }
                        foreach(GameObject obj in optionButtons)
                        {
                            obj.SetActive(false);
                        }
                    }
                    else
                    {
                        oldImg.enabled=true;
                    }
                    //Changes image of speaker with different lines
                    if(NPCDialogue.index>=speakerList.Count)
                    {
                        NPCDialogue.index=0;
                    }
                    switch(speakerList[NPCDialogue.index])
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
                    signText.text=message;
                    //Changes ui position to that of npc
                    Vector3 pos=cam.WorldToScreenPoint(NPC.transform.position+offset);
                    if(sign.transform.position!=pos)
                    {
                        sign.transform.position=pos;
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision){
            if(collision.gameObject.name=="Player"){
                sign.SetActive(true);
                textBubble.SetActive(true);
                canTalk=true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision){
            if(collision.gameObject.name=="Player"){
                sign.SetActive(false);
                textBubble.SetActive(false);
                canTalk=false;
            }
        }

        void OnMouseDown()    
        {
            if(NPCDialogue.hidden&&canTalk)
            {
                NPCDialogue.Restart(optionList.ToArray());
            }
        }
        /*
        public void createCell()
        {
            sentenceList = new List<string>();
            speakerList = new List<string>();
            var sentenceCell=sentence.GetCells();
            var speakerCell=speakerName.GetCells();
            for(int i=0;i<sentence.GridSize.y;i++)
            {
                sentenceList.Add(sentenceCell[i,index]);
                speakerList.Add(speakerCell[i,index]);
            }
        }*/
    }
}        

/*Using 2d array
public class OptionDialogue : MonoBehaviour
{
    public int index;
    public string[] option1;
    public string[] option2;
    public string[] option3;
    public string[] option4;
    public string[] buttonTitle;
    
    void Start()
    {
        index=1;
    }

    void Update()
    {
        foreach(string title in buttonTitle)
        {

        }
    }
}*/   

//Fix out of bounds in title line 90 due to +2
//Fix going back to normal after each click of NPC