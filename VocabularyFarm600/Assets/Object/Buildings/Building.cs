using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] Transform feet;
    [SerializeField] SpriteRenderer building_grid , sprite;
    [SerializeField] Vector2 size = new Vector2(2.56f , 2.56f);
    [SerializeField] Sprite green_grid , red_grid;
    [SerializeField] GameObject building_canvas;
    public bool can_be_moved = true;
    LayerMask building_layer;
    BoxCollider2D collision;
    private void Awake() {
        collision = GetComponent<BoxCollider2D>();
        building_layer = LayerMask.GetMask("Building");
    }
    private void Start() {
        collision.size = size;
        building_grid.size = size;
    }
    private void FixedUpdate() {
        sprite.sortingOrder = Mathf.FloorToInt(-feet.position.y * 100);
        detect_collision();
    }
    public void show_building_area(){
        building_grid.gameObject.SetActive(true);
    }
    public void hide_building_area(){
        building_grid.gameObject.SetActive(false);
    }
    void detect_collision(){
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position , new Vector2(size.x - 0.05f , size.y - 0.05f) , 0 , building_layer);
        if(colliders.Length != 1){
            building_grid.sprite = red_grid;
        }
        else{
            building_grid.sprite = green_grid;
        }
    }
    public void show_button(){
        building_canvas.SetActive(true);
    }
    public void hide_button(){
        StartCoroutine(hide());
    }
    IEnumerator hide(){
        yield return new WaitForSeconds(0.1f);
        building_canvas.SetActive(false);
    }
    public void destroy_building(){
        Destroy(gameObject);
    }
}
