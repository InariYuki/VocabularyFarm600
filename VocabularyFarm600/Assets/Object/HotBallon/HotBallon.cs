using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HotBallon : MonoBehaviour
{
    Vector3 move_pos;
    BallonGame game;
    private void FixedUpdate() {
        transform.position = Vector3.Lerp(transform.position , move_pos , Time.deltaTime * 5f);
    }
    public void init(BallonGame ctl){
        game = ctl;
    }
    public void move_to(Vector2 pos){
        move_pos = pos;
        move_pos.x = Mathf.Clamp(move_pos.x , -9f , 9f);
        move_pos.y = Mathf.Clamp(move_pos.y , -4f , 3f);
    }
}
