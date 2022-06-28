using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantAnimal : MonoBehaviour
{
    [SerializeField] GameObject button_holder;
    [SerializeField] Transform feet;
    UI interacter;
    Rigidbody2D rigid_body;
    Vector2 direction;
    public float speed;
    private void Awake() {
        rigid_body = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        StartCoroutine(ai_start());
    }
    IEnumerator ai_start(){
        yield return new WaitForSeconds(1f);
        ai_state = 0;
    }
    public bool button_show;
    public void toggle_buttons(){
        button_holder.SetActive(!button_holder.activeSelf);
        button_show = button_holder.activeSelf;
        if(button_show){
            ai_state = 2;
        }
        else{
            idle_mode_init();
        }
    }
    public void interact(UI ui){
        interacter = ui;
    }
    public void care_button_clicked(){
        interacter.toggle_care_screen();
    }
    public void walk_button_clicked(){

    }
    Vector3 random_position;
    bool position_decided = false , rested = false;
    void idle_mode_init(){
        position_decided = false;
        rested = false;
        ai_state = 0;
    }
    void idle_mode(){
        if(position_decided == false){
            random_position = new Vector2(Random.Range(-9f , 9f) , Random.Range(-5f , 5f));
            position_decided = true;
        }
        if((feet.position - random_position).magnitude < 0.2f){
            direction = Vector2.zero;
            if(rested == false){
                rested = true;
                StartCoroutine(rest_for_a_while());
            }
            return;
        }
        direction = (random_position - feet.position).normalized;
        rigid_body.MovePosition(rigid_body.position + direction * speed * Time.deltaTime);
    }
    IEnumerator rest_for_a_while(){
        yield return new WaitForSeconds(2f);
        position_decided = false;
        rested = false;
    }
    void move_mode(){

    }
    int ai_state = 2; //0 = idle , 1 = move_mode 2 = stop
    private void FixedUpdate() {
        switch(ai_state){
            case 0:
                idle_mode();
                break;
            case 1:
                move_mode();
                break;
            case 2:
                direction = Vector2.zero;
                break;
            default:
                break;
        }
    }
}
