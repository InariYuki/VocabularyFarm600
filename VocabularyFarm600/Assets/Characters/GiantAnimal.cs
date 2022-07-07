using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantAnimal : MonoBehaviour
{
    [SerializeField] GameObject button_holder;
    [SerializeField] Transform feet;
    public int first_word , last_word;
    UI interacter;
    Rigidbody2D rigid_body;
    Animator animator;
    SpriteRenderer sprite;
    Vector2 direction;
    public float speed;
    private void Awake() {
        rigid_body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Start() {
        start_ai_activity();
    }
    int ai_state = 2; //0 = idle , 1 = move_mode 2 = stop
    private void FixedUpdate() {
        animation_ctl();
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
    public void start_ai_activity(){
        StopAllCoroutines();
        StartCoroutine(ai_start());
    }
    IEnumerator ai_start(){
        yield return new WaitForSeconds(1f);
        idle_mode_init();
    }
    bool button_show;
    public bool _button_show{
        get{
            return button_show;
        }
    }
    public void toggle_buttons(){
        button_holder.SetActive(!button_holder.activeSelf);
        button_show = button_holder.activeSelf;
    }
    public void interact(UI ui){
        interacter = ui;
    }
    public void care_button_clicked(){
        interacter.toggle_care_screen(this);
        idle_mode_init();
        toggle_buttons();
    }
    public void walk_button_clicked(){
        interacter.control_mode = 1;
        ai_state = 2;
        toggle_buttons();
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
    Vector3 _target_position;
    public Vector3 target_position{
        set{
            _target_position = new Vector3(value.x , value.y , 0);
            ai_state = 1;
        }
    }
    bool enough_rest = false;
    void move_mode(){
        if((_target_position - feet.position).magnitude < 0.2f){
            direction = Vector3.zero;
            if(enough_rest == false){
                enough_rest = true;
                StartCoroutine(wait_for_random_walk());
            }
            return;
        }
        direction = (_target_position - feet.position).normalized;
        rigid_body.MovePosition(rigid_body.position + direction * speed * Time.deltaTime);
    }
    void animation_ctl(){
        sprite.sortingOrder = Mathf.RoundToInt(-feet.position.y * 100);
        if(direction != Vector2.zero){
            if(Mathf.Abs(direction.x) >= 0.3f){
                animator.SetFloat("Motion" , 0);
                if(direction.x > 0) sprite.flipX = false;
                else sprite.flipX = true;
            }
            else{
                if(direction.y > 0) animator.SetFloat("Motion" , 1);
                else animator.SetFloat("Motion" , 2);
            }
        }
        else animator.SetFloat("Motion" , 2);
    }
    IEnumerator wait_for_random_walk(){
        yield return new WaitForSeconds(3f);
        idle_mode_init();
        enough_rest = false;
    }
}
