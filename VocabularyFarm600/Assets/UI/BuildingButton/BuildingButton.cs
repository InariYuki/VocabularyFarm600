using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    UI ui;
    [SerializeField] GameObject building;
    public void init(UI _ui){
        ui = _ui;
    }
    public void clicked(){
        ui.set_building(building);
    }
}
