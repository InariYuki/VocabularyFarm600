using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantAnimal : MonoBehaviour
{
    [SerializeField] GameObject button_holder;
    public bool button_show;
    public void toggle_buttons(){
        button_holder.SetActive(!button_holder.activeSelf);
        button_show = button_holder.activeSelf;
    }
    void idle_mode_init(){

    }
    void idle_mode(){

    }
    public void move_mode_init(){

    }
    void move_mode(){

    }
    int ai_state = 0; //0 = idle , 1 = move_mode
    private void FixedUpdate() {
        switch(ai_state){
            case 0:
                idle_mode();
                break;
            case 1:
                move_mode();
                break;
        }
    }
}
