using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [HideInInspector] public BallonGame game;
    SpriteRenderer sprite;
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    private void Awake() {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate() {
        MoveToBallonPosition();
    }
    void MoveToBallonPosition(){
        Vector3 vec = game.hot_ballon.transform.position - transform.position;
        if(vec.magnitude > 2f){
            transform.position += vec.normalized * 0.2f;
        }
        else{
            gameObject.SetActive(false);
        }
    }
    public void SetRandomSprite(){
        sprite.sprite = sprites[Random.Range(0 , sprites.Count)];
    }
}
