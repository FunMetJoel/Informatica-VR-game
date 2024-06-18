using System.Collections;
using System.Collections.Generic;
using UnityEngine;

private bool Switcher;     //hier

public class switchScript : MonoBehaviour
{
    private void Update()
    {

        [SerializeField] GameObject MenuMain, MenuOptions;

        Switcher = !Switcher;
        //makes the bool reverse of what it was prior.As it will start as true, it will turn false and set the main menu on. 

        //Enable the Main menu when the boolean is false, disable when true
        MenuMain.SetActive(!Switcher);

        //Enable the Options page when the boolean is true, disable when false
        MenuOptions.SetActive(Switcher);


    }

}
