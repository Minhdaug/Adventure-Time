using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDSystem : MonoBehaviour
{
    [SerializeField] List<Transform> speedFrameTransforms;
    [SerializeField] List<Transform> buttonTransforms;
    private List<GameObject> speedFrames;
    // Start is called before the first frame update

    public void UpdateSpeedFrames(List<GameObject> goList)
    {
		for (int i = 0; i < speedFrameTransforms.Count; i++)
        {
            //SpriteRenderer tmpGO = goList[i].GetComponent<Unit>().SpeedFrame.GetComponent<SpriteRenderer>();
            GameObject tmpGO = Instantiate(goList[i].GetComponent<Unit>().SpeedFrame, speedFrameTransforms[i]);

            //GameObject tmpGO = goList[i].GetComponent<Unit>().SpeedFrame;
            //tmpGO.GetComponent<SpriteRenderer>().sortingLayerID = 1;
            //speedFrames.Add(tmpGO);
        }
    }

    public void GenerateButtons(GameObject go)
    {

    }
}
