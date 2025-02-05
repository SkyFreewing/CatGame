using System.Linq;
using UnityEngine;

public class BackgroundHaloEffect : MonoBehaviour
{
    public GameObject[] EffectObjects;        

    void Update()
    {
        var deltaTime = Time.deltaTime;

        for (int i = 0; i < EffectObjects.Length; i++) 
        {
            if (i % 2 == 0) 
                EffectObjects[i].transform.Rotate(new Vector3(0, 0, 25f * deltaTime));
            else
                EffectObjects[i].transform.Rotate(new Vector3(0, 0, -25f * deltaTime));
        }
    }
}
