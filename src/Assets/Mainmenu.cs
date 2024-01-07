using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Mainmenu : MonoBehaviour
{
    // Start is called before the first frame update
   public void PlayGame(){
    SceneManager.LoadSceneAsync(1);
   }
}
