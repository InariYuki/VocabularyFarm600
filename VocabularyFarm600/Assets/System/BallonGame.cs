using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallonGame : MonoBehaviour
{
    [SerializeField] HotBallon ballon;
    [SerializeField] CloudController cloud_cluster;
    [SerializeField] Transform ballon_container , cloud_container;
    HotBallon ballon_instanced;
    CloudController cloud_cluster_instanced;
    UI ui;
    List<string> word_library = new List<string>();
    List<string> unfinished = new List<string>();
    Dictionary<string , string> dictionary = new Dictionary<string, string>();
    public void init(UI _ui , List<string> words , Dictionary<string , string> dict , List<string> unfinished_eng){
        word_library.Clear();
        unfinished.Clear();
        dictionary.Clear();
        progress.text = "完成 : 0/5";
        ui = _ui;
        word_library.AddRange(words);
        for(int i = 0; i < words.Count; i++){
            dictionary[words[i]] = dict[words[i]];
        }
        unfinished.AddRange(unfinished_eng);
        instance_ballon_and_cloud();
        set_answer();
    }
    void instance_ballon_and_cloud(){
        if(ballon_container.childCount == 0){
            ballon_instanced = Instantiate(ballon , new Vector3(1.25f , -2f , 0) , Quaternion.identity , ballon_container);
            ballon_instanced.init(this);
        }
        for(int i = 0; i < cloud_container.childCount; i++){
            Destroy(cloud_container.GetChild(i).gameObject);
        }
        cloud_cluster_instanced = Instantiate(cloud_cluster , new Vector3(1.25f , 3.35f , 0) , Quaternion.identity , cloud_container);
        cloud_cluster_instanced.init(this);
    }
    string eng_ans;
    void set_answer(){
        List<string> unfinished_cache = new List<string>();
        List<string> word_library_cache = new List<string>();
        unfinished_cache.AddRange(unfinished);
        word_library_cache.AddRange(word_library);
        int random_num = Random.Range(0 , unfinished_cache.Count);
        eng_ans = unfinished_cache[random_num];
        ballon_instanced.set_ballon_text(eng_ans);
        string answer_cht = dictionary[eng_ans];
        word_library_cache.Remove(eng_ans);
        List<string> answers = new List<string>();
        for(int i = 0; i < 3; i++){
            int random_ans = Random.Range(0 , word_library_cache.Count);
            answers.Add(dictionary[word_library_cache[random_ans]]);
            word_library_cache.Remove(word_library_cache[random_ans]);
        }
        answers.Insert(Random.Range(0 , 2) , answer_cht);
        cloud_cluster_instanced.set_cloud_text(answer_cht , answers);
    }
    [SerializeField] TextMeshProUGUI progress;
    int finished = 0;
    public void round_finished(){
        unfinished.Remove(eng_ans);
        finished++;
        progress.text = $"完成 : {finished}/5";
    }
    public void game_failed(){
        ballon_instanced = null;
        print("LOL you lose");
    }
    Vector3 ballon_origin;
    public void set_ballon_origin(){
        if(ballon_instanced == null) return;
        ballon_origin = ballon_instanced.transform.position;
    }
    public void move_ballon(Vector3 vec){
        if(ballon_instanced == null) return;
        ballon_instanced.move_to(ballon_origin + vec);
    }
}
