using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonGame : MonoBehaviour
{
    [SerializeField] HotBallon ballon;
    public void move_ballon(Vector3 position)
    {
        ballon.set_position(position);
    }
}
