using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    //Choose which objects get triggered
    public List<GameObject> target=new List<GameObject>();
    //Choose what method would be implemented once triggered
    public int methodIndex=1;
    public float alpha=0.5f;

    private void OnTriggerEnter2D(Collider2D collision){
        if(target.Count>0)
        {
            foreach (GameObject obj in target)
            {
                if(collision.gameObject==obj){
                    switch(methodIndex)
                    {
                        case 1:
                            target.Remove(obj);
                            collision.gameObject.SetActive(false);
                            break;
                        case 2:
                            target.Remove(obj);
                            changeAlpha(collision.gameObject.GetComponent<Renderer>().material,alpha);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    private void changeAlpha(Material mat,float alphaVal){
        Color oldColor=mat.color;
        Color newColor=new Color(oldColor.r,oldColor.g,oldColor.b,alphaVal);
        mat.SetColor("_Color",newColor);
    }
}
