using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSquad : MonoBehaviour
{
    [SerializeField] bool fly_left = false;
    Vector3 fly_speed;
    private void Start() {
        fly_speed = new Vector3(Random.Range(0.05f , 0.08f) , Random.Range(-0.04f , -0.01f) , 0);
        if(fly_left){
            fly_speed.x = -fly_speed.x;
        }
    }
    private void FixedUpdate() {
        Fly();
    }
    void Fly(){
        if(fly_left){
            if(transform.position.x < -14){
                transform.position = new Vector3(12 , 7 , 0);
                fly_speed = new Vector3(Random.Range(0.05f , 0.08f) , Random.Range(-0.01f , -0.08f) , 0);
                fly_speed.x = -fly_speed.x;
            }
        }
        else{
            if(transform.position.x > 14){
                transform.position = new Vector3(-12 , 7 , 0);
                fly_speed = new Vector3(Random.Range(0.05f , 0.08f) , Random.Range(-0.01f , -0.08f) , 0);
            }
        }
        transform.position += fly_speed;
    }
}
