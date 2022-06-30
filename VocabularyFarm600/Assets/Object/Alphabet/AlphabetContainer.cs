using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphabetContainer : MonoBehaviour
{
    private void FixedUpdate() {
        detect_alphabet();
    }
    float radius = 0.5f;
    void detect_alphabet(){
        Collider2D[] alphabets = Physics2D.OverlapCircleAll(transform.position , radius);
        for(int i = 0 ; i < alphabets.Length; i++){
            alphabets[i].GetComponent<BubbleAlphabet>().stop();
            alphabets[i].transform.position = Vector2.Lerp(alphabets[i].transform.position , transform.position , 0.1f);
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position , radius);
    }
}
