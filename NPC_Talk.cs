using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Array2DEditor
{
    public class NPC_Talk : MonoBehaviour
    {

        public GameObject box;
        private dialogue NPCDialogue;
        public string[] sentence;
        public string[] speakerName;
        public Sprite[] speakerImg;
        public Image oldImg;
        private bool canTalk=false;
        public TextMeshProUGUI signText;
        public GameObject sign;
        public string message;
        private Camera cam;
        //Offset sign postion to make it on top of npc head
        private Vector3 offset=new Vector3(0,0.75f,0);
        public GameObject NPC;
        public Image image;
        public int[] imageIndex;
        public Sprite[] ImageSprite;
        public GameObject textBubble;

        // Start is called before the first frame update
        void Start()
        {
            NPCDialogue=box.GetComponent<dialogue>();
            signText.text=string.Empty;
            sign.SetActive(false);
            textBubble.SetActive(false);
            cam=Camera.main;
            oldImg.enabled=false;
            image.enabled=false;
        }

        void Update()
        {
            //Enable this only when triggered so multiple npc work
            if(canTalk)
            {
                //Show image
                for(int i=0;i<imageIndex.Length;i++)
                {
                    if(NPCDialogue.index==imageIndex[i])
                    {
                        image.sprite=ImageSprite[i];
                        image.enabled=true;
                    }
                    else
                    {
                        image.enabled=false;
                    }
                }
                //Show/Hide speaker image with dialogue box
                if(NPCDialogue.hidden)
                {
                    oldImg.enabled=false;
                    image.enabled=false;
                }
                else
                {
                    oldImg.enabled=true;
                }
                //Changes image of speaker with different lines
                if(NPCDialogue.index>=speakerName.Length)
                {
                    NPCDialogue.index=0;
                }
                switch(speakerName[NPCDialogue.index])
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
                NPCDialogue.Restart(sentence);
            }
        }
    }
}           