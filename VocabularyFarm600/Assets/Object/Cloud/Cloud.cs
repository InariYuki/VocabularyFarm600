using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cloud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] List<Sprite> cloud_sprites = new List<Sprite>();
    SpriteRenderer sprite;
    LayerMask ballon_layer;
    bool is_answer;
    private void Awake() {
        sprite = GetComponent<SpriteRenderer>();
        ballon_layer = LayerMask.GetMask("Ballon");
    }
    private void FixedUpdate() {
        detect_answer();
    }
    public void set_answer(string ans , bool _is_answer){
        sprite.sprite = cloud_sprites[Random.Range(0 , cloud_sprites.Count)];
        text.text = ans;
        is_answer = _is_answer;
    }
    float radius = 0.5f;
    void detect_answer(){
        if(transform.position.y < -1f) return;
        Collider2D ballon = Physics2D.OverlapCircle(transform.position , radius , ballon_layer);
        if(ballon != null && is_answer){
            print("correct !");
        }
    }
}
