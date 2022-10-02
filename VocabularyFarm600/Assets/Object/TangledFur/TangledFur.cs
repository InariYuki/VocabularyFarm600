using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangledFur : MonoBehaviour
{
    SpriteRenderer sprite;
    Animator anim;
    private void Awake() {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    float opacity = 1;
    public void reset_opacity(){
        opacity = 1;
        sprite.color = new Vector4(1 , 1 , 1 , 1);
    }
    public void fade_out(){
        StartCoroutine(fade_process());
        anim.Play("FurUntangle");
    }
    IEnumerator fade_process(){
        while(opacity >= 0){
            opacity -= 0.02f;
            sprite.color = new Vector4(1 , 1 , 1 , opacity);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
