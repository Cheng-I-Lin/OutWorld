using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private LevelLoader level;
    public GameObject door;
    //Offset player position after entering door to not trigger go back transition
    private Vector3 offset=new Vector3(0,0.65f,0);

    // Start is called before the first frame update
    void Start()
    {
        //Use this if want to transition between scenes
        level=GameObject.Find("Player").GetComponent<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.name=="Player"){
            StartCoroutine(doorTransition(collision.gameObject));  
        }
    }

    IEnumerator doorTransition(GameObject obj)
    {
        yield return new WaitForSeconds(0.5f);
        obj.transform.position=door.transform.position-offset;
        //level.LoadLevel(sceneIndex);
    }
}
