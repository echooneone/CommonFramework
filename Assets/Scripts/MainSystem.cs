using System;
using System.Collections;
using System.Collections.Generic;
using SFB;
using UnityEngine;

public class MainSystem : MonoBehaviour
{
    private void Start()
    {
        int x = 1;

        TestValue(ref x);
        print(x);
        
    }

    private void TestValue(ref int a)
    {
        a = 10;
    }
}
