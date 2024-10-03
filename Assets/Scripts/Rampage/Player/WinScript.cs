using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    [SerializeField] private GameObject trophy;
    [SerializeField] private Transform artLocation;
    [SerializeField] private GameObject enemy;

    [SerializeField] private int winLoop = 24;

    public void WinTrack()
    {
        winLoop--;

        if (winLoop <= 0)
        {
            Instantiate(trophy, artLocation.position, Quaternion.identity);
            Debug.Log($"Trophy instantiated! ");
            Destroy(enemy);
        }
    }
    
}
