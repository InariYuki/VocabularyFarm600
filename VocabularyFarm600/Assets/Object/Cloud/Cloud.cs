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
    BallonGame game;
    bool is_answer;
    private void Awake() {
        sprite = GetComponent<SpriteRenderer>();
        ballon_layer = LayerMask.GetMask("Ballon");
        mow_tree.GetComponent<MowTree>().cloud = this;
    }
    private void FixedUpdate() {
        detect_answer();
        move_down();
    }
    public void set_answer(BallonGame _game , string ans , bool _is_answer){
        game = _game;
        sprite.sprite = cloud_sprites[Random.Range(0 , cloud_sprites.Count)];
        text.text = ans;
        is_answer = _is_answer;
    }
    float radius = 0.5f;
    [SerializeField] GameObject mow_tree;
    void detect_answer(){
        if(transform.position.y < -3f || !is_answer) return;
        Collider2D ballon = Physics2D.OverlapCircle(transform.position , radius , ballon_layer);
        if(ballon != null){
            is_answer = false;
            mow_tree.SetActive(true);
            game.set_answer_right();
        }
    }
    [HideInInspector] public bool can_move = false;
    void move_down(){
        if(!can_move) return;
        if(transform.position.y < -12f){
            mow_tree.SetActive(false);
            game.check_all_cloud_bottom();
            can_move = false;
        }
        transform.position -= new Vector3(0 , 0.08f , 0);
    }
    public void MoveFoodToPosition(){
        game.foods[0].transform.position = transform.position + new Vector3(-1.588f , 2.87f , 0);
        game.foods[1].transform.position = transform.position + new Vector3(-0.65f , 4.37f , 0);
        game.foods[2].transform.position = transform.position + new Vector3(1.25f , 4.1f , 0);
        game.foods[3].transform.position = transform.position + new Vector3(2.21f , 2.81f , 0);
        for(int i = 0; i < game.foods.Count; i++){
            game.foods[i].gameObject.SetActive(true);
            game.foods[i].SetRandomSprite();
        }
    }
}
