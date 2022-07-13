using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HotBallon : MonoBehaviour
{
    Vector3 move_pos;
    LayerMask cloud_layermask;
    BallonGame game;
    private void Awake() {
        cloud_layermask = LayerMask.GetMask("Building");
    }
    private void FixedUpdate() {
        transform.position = Vector3.Lerp(transform.position , move_pos , Time.deltaTime * 5f);
        detect_answer();
    }
    public void init(BallonGame ctl){
        game = ctl;
    }
    public void move_to(Vector2 pos){
        move_pos = pos;
    }
    [SerializeField] TextMeshProUGUI ballon_text;
    public void set_ballon_text(string question){
        ballon_text.text = question;
    }
    void detect_answer(){
        Collider2D col = Physics2D.OverlapCircle(transform.position , 1.28f , cloud_layermask);
        if(col){
            Cloud cloud_ramed = col.GetComponent<Cloud>();
            if(cloud_ramed.is_answer){
                cloud_ramed.you_got_me();
            }
            else{
                pop();
            }
        }
    }
    void pop(){
        game.game_failed();
        Destroy(gameObject);
    }
}
