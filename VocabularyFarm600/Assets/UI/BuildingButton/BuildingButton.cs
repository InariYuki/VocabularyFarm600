using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    Vector2 size;
    Image image;
    UI ui;
    [SerializeField] GameObject building;
    private void Awake() {
        image = GetComponent<Image>();
    }
    public void init(UI _ui){
        ui = _ui;
    }
    public void clicked(){
        ui.set_building(building);
    }
}
