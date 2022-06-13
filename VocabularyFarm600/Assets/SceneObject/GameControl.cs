using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    Camera cam;
    [SerializeField] LayerMask effect_by_mouse;
    private void Awake() {
        cam = GetComponent<Camera>();
    }
    private void FixedUpdate() {
        Vector3 mouse_position = cam.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetKey(KeyCode.Mouse0)){
            Collider2D[] touched_objects = Physics2D.OverlapCircleAll(mouse_position , 0.5f , effect_by_mouse);
            foreach(Collider2D obj in touched_objects){
                Rigidbody2D obj_body = obj.GetComponent<Rigidbody2D>();
                obj_body.AddForce((obj.transform.position - mouse_position).normalized * 100f);
            }
        }
    }
}
