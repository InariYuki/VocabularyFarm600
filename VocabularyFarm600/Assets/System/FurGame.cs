using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurGame : MonoBehaviour
{
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position , new Vector2(16 , 9));
    }
}
