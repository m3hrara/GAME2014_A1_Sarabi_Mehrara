
/*
DestroyOnExit.cs
Author: Mehrara Sarabi 
Student ID: 101247463
Last modified: 2021-10-24
Description: This code is used to destroy enemey object after their death animation is played.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnExit : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject, stateInfo.length);
    }
}

