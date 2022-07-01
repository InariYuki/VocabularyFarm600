using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FurGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI word_display;
    UI _ui;
    public UI ui{
        set{
            _ui = value;
            set_current_word();
        }
    }
    string current_word;
    void set_current_word(){
        string[] word_set = _ui.pull_random_word_from_dict();
        word_display.text = word_set[1];
        for(int i = 0; i < word_set[0].Length; i++){
            //instantiate bubble alphabet
            //place alphabet container
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position , new Vector2(16 , 9));
    }
}
