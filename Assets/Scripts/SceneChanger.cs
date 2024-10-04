using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private float delay = 3f;

    private void Start()
    {
        StartCoroutine(SceneChange());
    }

    private IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("EnemyTest");
    }
}
