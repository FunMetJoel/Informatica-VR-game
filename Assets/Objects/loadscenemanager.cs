using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadscenemanager : MonoBehaviour
{
    public loadscenemanager Instance;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void LoadSceneRoom(int scene)
    {

        //load the scene "Room generator" in the background
        SceneManager.LoadSceneAsync(scene);
    }
}