using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyObject : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    SpriteRenderer sprite;
    float deviation;
    private void Awake() {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Start() {
        deviation = Random.Range(0.01f , 0.08f);
        sprite.sprite = sprites[Random.Range(0 , sprites.Count)];
    }
    private void FixedUpdate() {
        move_down();
    }
    void move_down(){
        transform.position -= new Vector3(0 , deviation , 0);
        if(transform.position.y < -8f){
            transform.position = new Vector3(transform.position.x , 8 , 0);
            deviation = Random.Range(0.01f , 0.08f);
            sprite.sprite = sprites[Random.Range(0 , sprites.Count)];
        }
    }
}
