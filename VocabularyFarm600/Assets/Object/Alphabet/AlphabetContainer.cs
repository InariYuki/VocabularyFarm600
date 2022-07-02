using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphabetContainer : MonoBehaviour
{
    private void FixedUpdate() {
        detect_alphabet();
        pull_alphabet();
    }
    int expecting_char;
    public void set_expecting_char(int c , FurGame _controller){
        expecting_char = c;
        controller = _controller;
    }
    float radius = 0.5f;
    bool received = false;
    void detect_alphabet(){
        if(received) return;
        Collider2D[] alphabets = Physics2D.OverlapCircleAll(transform.position , radius);
        for(int i = 0 ; i < alphabets.Length; i++){
            BubbleAlphabet alphabet = alphabets[i].GetComponent<BubbleAlphabet>();
            if(alphabet != null && expecting_char == alphabet.me){
                alphabet_pulled = alphabet;
                received = true;
                controller.check_word_finished();
                break;
            }
        }
    }
    BubbleAlphabet alphabet_pulled;
    FurGame controller;
    void pull_alphabet(){
        if(alphabet_pulled == null || alphabet_pulled.transform.position == transform.position) return;
        if(alphabet_pulled.transform.parent != transform) alphabet_pulled.transform.SetParent(transform);
        alphabet_pulled.stop();
        alphabet_pulled.transform.position = Vector2.Lerp(alphabet_pulled.transform.position , transform.position , 0.1f);
    }
    public void destroy_container(){
        controller.brush();
        Destroy(gameObject);
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position , radius);
    }
}
