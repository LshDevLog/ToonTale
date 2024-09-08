using UnityEngine;

public class ToonWorldMapManager : MonoBehaviour
{
    void Start()
    {
        OnOffSavePointLights();
    }


    void Update()
    {
        
    }

    public void OnOffSavePointLights()
    {
        SavePoint[] savepoints = FindObjectsOfType<SavePoint>();

        for (int i = 0; i < savepoints.Length; i++)
        {
            savepoints[i].LightOnOff();
        }
    }
}
