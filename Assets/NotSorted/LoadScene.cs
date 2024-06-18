using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene: MonoBehaviour
{
    public void LoadSceneRoom()
    {
        //load the scene "Room generator" in the background
        SceneManager.LoadSceneAsync(2);
    }
}