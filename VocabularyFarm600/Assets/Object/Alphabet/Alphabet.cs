using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alphabet : MonoBehaviour
{
    Rigidbody2D body;
    private void Awake() {
        body = GetComponent<Rigidbody2D>();
    }
    public void disable_physics(){
        body.bodyType = RigidbodyType2D.Static;
    }
}
