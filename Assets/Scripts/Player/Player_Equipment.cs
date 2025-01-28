using System;
using UnityEngine;

public class Player_Equipment : MonoBehaviour
{
    public static Action swapWeapon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Temp until inventory is implemented
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            swapWeapon?.Invoke();
        }
    }
}
