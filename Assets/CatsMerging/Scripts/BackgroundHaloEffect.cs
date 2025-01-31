using UnityEngine;

public class BackgroundHaloEffect : MonoBehaviour
{
    public GameObject InnerHalo;
    public GameObject OuterHalo;        

    // Update is called once per frame
    void Update()
    {
        var deltaTime = Time.deltaTime;

        InnerHalo.transform.Rotate(new Vector3(0, 0, 25f * deltaTime));
        OuterHalo.transform.Rotate(new Vector3(0, 0, -25f * deltaTime));
    }
}
