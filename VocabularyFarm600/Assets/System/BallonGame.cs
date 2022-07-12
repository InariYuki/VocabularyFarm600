using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallonGame : MonoBehaviour
{
    UI ui;
    List<string> word_library;
    public void set_library(UI _ui , int first , int last){
        ui = _ui;
        if(word_library.Count == 0) word_library = ui.pull_words_from_dict(first , last);
        set_four_words();
    }
    [SerializeField] HotBallon ballon;
    HotBallon ballon_instanced;
    [SerializeField] CloudController cloud_ctl;
    [SerializeField] Transform game_container;
    CloudController cloud_ctl_instanced;
    void instance_cloud(){
        for(int i = 0 ; i < game_container.childCount; i++){
            Destroy(game_container.GetChild(i).gameObject);
        }
        cloud_ctl_instanced = Instantiate(cloud_ctl , new Vector3(1f , 3f , 0) , Quaternion.identity , game_container);
        ballon_instanced = Instantiate(ballon , new Vector3(1.5f , -1.4f , 0) , Quaternion.identity , game_container);
    }
    public void move_ballon(Vector3 position)
    {
        ballon_instanced.set_position(position);
    }
    void set_four_words(){
        instance_cloud();
        string word = word_library[Random.Range(0 , word_library.Count)];
        string word_cht = ui.look_up_in_the_dictionary(word);
        word_library.Remove(word);
        ballon_instanced.set_ballon_text(word , this);
        List<string> cache_wordlist = word_library;
        List<string> random_words = new List<string>();
        for(int i = 0; i < 3; i++){
            int random_int = Random.Range(0 , cache_wordlist.Count);
            random_words.Add(ui.look_up_in_the_dictionary(cache_wordlist[random_int]));
            cache_wordlist.Remove(cache_wordlist[random_int]);
        }
        random_words.Insert(Random.Range(0 , random_words.Count) , ui.look_up_in_the_dictionary(word));
        cloud_ctl_instanced.set_answer(word_cht , random_words , this);
    }
    [SerializeField] TextMeshProUGUI progress_display;
    int finished = 0;
    public void set_progress(){
        finished++;
        progress_display.text = $"完成 : {finished}/5";
        set_four_words();
        if(finished == 5){
            close_game();
        }
    }
    public void close_game(){
        finished = 0;
        progress_display.text = $"完成 : 0/5";
        ui.close_ballon_game();
    }
}
