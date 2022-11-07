using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{

    public float moveSpeed=3f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;

    double ButtonCooler=0.5; // Half a second before reset
    int ButtonCount=0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("d")||Input.GetKeyDown("w")||Input.GetKeyDown("a")||Input.GetKeyDown("s"))
        {
            if(ButtonCooler>0&&ButtonCount==1/*Number of Taps you want Minus One*/){ 
                //Has double tapped
                moveSpeed=5;
            }
            else{
                ButtonCooler=0.5; 
                ButtonCount+=1;
                moveSpeed=3;
            }
        }
        if(ButtonCooler>0)
        {
            ButtonCooler-=1*Time.deltaTime ;
        }
        else{
            ButtonCount=0;
        }
        movement.x=Input.GetAxisRaw("Horizontal");
        movement.y=Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal",movement.x);
        animator.SetFloat("Vertical",movement.y);
        animator.SetFloat("Speed",movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position+movement*moveSpeed*Time.fixedDeltaTime);
    }    
}
