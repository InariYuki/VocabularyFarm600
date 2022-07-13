using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cloud : MonoBehaviour
{
    [HideInInspector] public bool is_answer;
    [SerializeField] TextMeshProUGUI text;
    CloudController master_ctl;
    public void set_answer(string ans , bool _is_answer , CloudController parent){
        text.text = ans;
        is_answer = _is_answer;
        master_ctl = parent;
    }
    public void you_got_me(){
        master_ctl.ans_correct();
    }
}
