using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class vacham : MonoBehaviour

{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision){
       if(collision.gameObject.tag == "Player")   {
           
             SceneManager.LoadScene("Combat");
       }     

    }
    
}
