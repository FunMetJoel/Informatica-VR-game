using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HightScene : MonoBehaviour
{

    void Update()
    {
        if(transform.position.y <= -20)
        {
            SceneManager.LoadSceneAsync(2);
        }
    }
}
