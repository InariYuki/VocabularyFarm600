using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CloudController : MonoBehaviour
{
    public void set_answer(string ans_cht , List<string> cht_list){
        for(int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).GetComponent<Cloud>().set_word(cht_list[i] , (ans_cht == cht_list[i]) , this);
        }
    }
    public void destroy_self(){
        Destroy(gameObject);
    }
}
