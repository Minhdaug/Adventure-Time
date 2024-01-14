using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    public GameObject menuCanvas;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)) {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
        }
    }
}
