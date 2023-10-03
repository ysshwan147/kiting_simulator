using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [HideInInspector] public bool animating = false;
    [HideInInspector] public bool hitting = false;


    public void setAnimating(bool flag)
    {
        animating = flag;
    }

    public void setHitting(bool flag)
    {
        hitting = flag;
    }
}
