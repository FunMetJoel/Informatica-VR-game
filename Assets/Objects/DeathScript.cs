using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{
    public void Death()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
