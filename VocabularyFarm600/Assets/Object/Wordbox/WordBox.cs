using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordBox : MonoBehaviour
{
    private void FixedUpdate() {
        Collider2D[] touched_alphabets = Physics2D.OverlapCircleAll(transform.position , 0.1f);
        foreach(Collider2D alphabet in touched_alphabets){
            if(true){
                alphabet.GetComponent<Alphabet>().disable_physics();
                alphabet.transform.position = Vector3.Lerp(alphabet.transform.position , transform.position , 10f * Time.deltaTime);
            }
        }
    }
}
