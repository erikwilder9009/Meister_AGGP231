using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VirtualButton : MonoBehaviour
{
    public Animator animate;
    public VirtualButtonBehaviour VBB;

    // Start is called before the first frame update
    void Start()
    {
        VBB.RegisterOnButtonPressed(OnButtonPressed);
        VBB.RegisterOnButtonReleased(OnButtonReleased);
    }

    public void OnButtonPressed(VirtualButtonBehaviour VB)
    {
        animate.Play("Fall");
    }
    public void OnButtonReleased(VirtualButtonBehaviour VB)
    {
        animate.Play("None");
    }
}
