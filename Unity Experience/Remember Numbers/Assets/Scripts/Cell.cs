using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public GameObject Border;
    int RememberedNumber = -1;

    public void RememberNumber(int number)
    {
        RememberedNumber = number;
    }

    public int GetRememberedNumber()
    {
        return RememberedNumber;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
