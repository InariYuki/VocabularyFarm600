using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cloud : MonoBehaviour
{
    [HideInInspector] public bool is_answer;
    [SerializeField] TextMeshProUGUI cloud_text_container;
    CloudController cloud_ctl;
    public void set_word(string word , bool is_ans , CloudController ctl){
        cloud_text_container.text = word;
        is_answer = is_ans;
        cloud_ctl = ctl;
    }
    public void destroy_whole_clouds(){
        cloud_ctl.destroy_self();
    }
}
