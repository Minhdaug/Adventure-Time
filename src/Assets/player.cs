using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   public float speed = 5f;
   private Animator animator;
void Update()
{
    float moveHorizontal = Input.GetAxis("Horizontal");
    float moveVertical = Input.GetAxis("Vertical");
    animator= GetComponent<Animator>();

    Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);
    transform.Translate(movement * speed * Time.deltaTime);
    if(movement.x !=0 || movement.y  !=0){
        animator.SetFloat("X",movement.x);
        animator.SetFloat("Y",movement.y);
        animator.SetBool("IsWalking",true);
    }else {
            animator.SetBool("IsWalking",false);
    }
    
}
}
