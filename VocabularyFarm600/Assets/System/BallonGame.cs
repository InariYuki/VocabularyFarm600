using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonGame : MonoBehaviour
{
    UI ui;
    List<string> word_library;
    public void set_library(UI _ui , int first , int last){
        ui = _ui;
        word_library = ui.pull_words_from_dict(first , last);
        set_four_words();
    }
    [SerializeField] HotBallon ballon;
    [SerializeField] CloudController cloud_ctl;
    HotBallon ballon_instanced;
    CloudController cloud_ctl_instanced;
    void instance_ballon_and_cloud(){
        for(int i = 0 ; i < transform.childCount; i++){
            Destroy(transform.GetChild(i).gameObject);
        }
        ballon_instanced = Instantiate(ballon , new Vector3(0f , -2f , 0f) , Quaternion.identity , transform);
        cloud_ctl_instanced = Instantiate(cloud_ctl , new Vector3(1f , 3f , 0) , Quaternion.identity , transform);
    }
    public void move_ballon(Vector3 position)
    {
        ballon_instanced.set_position(position);
    }
    void set_four_words(){
        instance_ballon_and_cloud();
        string word = word_library[Random.Range(0 , word_library.Count)];
        string word_cht = ui.look_up_in_the_dictionary(word);
        word_library.Remove(word);
        ballon_instanced.set_ballon_text(word);
        List<string> cache_wordlist = word_library;
        List<string> random_words = new List<string>();
        for(int i = 0; i < 3; i++){
            int random_int = Random.Range(0 , cache_wordlist.Count);
            random_words.Add(ui.look_up_in_the_dictionary(cache_wordlist[random_int]));
            cache_wordlist.Remove(cache_wordlist[random_int]);
        }
        random_words.Insert(Random.Range(0 , random_words.Count) , ui.look_up_in_the_dictionary(word));
        cloud_ctl_instanced.set_answer(word_cht , random_words);
    }
}
