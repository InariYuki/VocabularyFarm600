using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAlphabet : MonoBehaviour
{
    Rigidbody2D body;
    private void Awake() {
        body = GetComponent<Rigidbody2D>();
    }
    bool is_active = true;
    private void FixedUpdate() {
        if(is_active == false) return;
        random_roam();
        border_approach_control();
        move();
    }
    Vector2 _direction , _velocity;
    public Vector2 velocity{
        set{
            _velocity = value;
        }
    }
    float speed = 0.1f , acceleration = 1f;
    bool direction_choosed = false , waited = false;
    void random_roam(){
        if(direction_choosed == false){
            direction_choosed = true;
            _direction = new Vector2(Random.Range(-1f , 1f) , Random.Range(-1f , 1f));
            if(waited == false){
                waited = true;
                StartCoroutine(wait_to_choose_next_direction());
            }
        }
    }
    IEnumerator wait_to_choose_next_direction(){
        yield return new WaitForSeconds(0.5f);
        direction_choosed = false;
        waited = false;
    }
    void border_approach_control(){
        if(transform.position.y > 2f) _direction.y = -1;
        else if(transform.position.y < -1f) _direction.y = 1;
        if(transform.position.x > 5f) _direction.x = -1;
        else if(transform.position.x < -5f) _direction.x = 1;
    }
    void move(){
        velocity = Vector2.Lerp(_velocity , _direction * speed , acceleration * Time.deltaTime);
        body.MovePosition(body.position + _velocity);
    }
    public void stop(){
        is_active = false;
        velocity = Vector2.zero;
    }
}
