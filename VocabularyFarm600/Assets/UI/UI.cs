using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    Dictionary<string , string> eng_to_cht_dict = new Dictionary<string, string>();
    public string[] vocabulary_eng = new string[0];
    public string[] vocabulary_cht = new string[0];
    private void Awake() {
        UI_canvas = GetComponent<Canvas>();
        for(int i = 0; i < vocabulary_eng.Length ; i++){
            eng_to_cht_dict[vocabulary_eng[i]] = vocabulary_cht[i];
        }
    }
    private void Update() {
        input_control();
    }
    public Camera main_camera;
    Canvas UI_canvas;
    Vector3 touch_point;
    GiantAnimal currently_interacting_animal;
    void input_control(){
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            touch_point = main_camera.ScreenToWorldPoint(Input.mousePosition);
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0)){
            RaycastHit2D hit = Physics2D.Raycast(main_camera.ScreenToWorldPoint(Input.mousePosition) , Vector2.up , 0.001f);
            if(hit){
                currently_interacting_animal = hit.transform.GetComponent<GiantAnimal>();
                currently_interacting_animal.interact(this);
                currently_interacting_animal.toggle_buttons();
            }
            else{
                if(currently_interacting_animal != null && currently_interacting_animal.button_show){
                    currently_interacting_animal.toggle_buttons();
                }
                currently_interacting_animal = null;
            }
        }
        if(Input.GetKey(KeyCode.Mouse0)){
            main_camera.transform.position += touch_point - main_camera.ScreenToWorldPoint(Input.mousePosition);
        }
        main_camera.orthographicSize = Mathf.Clamp(main_camera.orthographicSize - Input.GetAxisRaw("Mouse ScrollWheel") , 1 , 16);
    }
    [SerializeField] GameObject care_screen;
    public void toggle_care_screen(){
        care_screen.SetActive(true);
    }
    public void close_care_screen(){
        care_screen.SetActive(false);
    }
}
