using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] Transform feet;
    [SerializeField] SpriteRenderer building_grid;
    [SerializeField] Vector2 size = new Vector2(2.56f , 2.56f);
    [SerializeField] SpriteRenderer sprite;
    BoxCollider2D collision;
    private void Awake() {
        collision = GetComponent<BoxCollider2D>();
    }
    private void Start() {
        sprite.sortingOrder = Mathf.RoundToInt(-feet.position.y * 100);
        collision.size = size;
        building_grid.size = size;
    }
    public void show_building_area(){
        building_grid.gameObject.SetActive(true);
    }
    public void hide_building_area(){
        building_grid.gameObject.SetActive(false);
    }
}
