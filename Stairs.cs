using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    private LevelLoader level;
    public int sceneIndex;
    public Animator transition;

    // Start is called before the first frame update
    void Start()
    {
        level=GameObject.Find("Player").GetComponent<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.name=="Player"){
            StartCoroutine(stairTransition());  
        }
    }

    IEnumerator stairTransition()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);
        level.LoadLevel(sceneIndex);
    }
}
