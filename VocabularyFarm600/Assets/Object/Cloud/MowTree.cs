using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MowTree : MonoBehaviour
{
    [HideInInspector] public Cloud cloud;
    public void GenerateFood(){
        cloud.MoveFoodToPosition();
    }
}
