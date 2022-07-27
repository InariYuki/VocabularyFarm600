using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallonGame : MonoBehaviour
{
    UI ui;
    Vector2 left_most_spawn_point = new Vector2(-8f , 10f) , left_spawn_point = new Vector2(-3f , 10f) , right_spawn_point = new Vector2(3f , 10f) , right_most_spawn_point = new Vector2(8f , 10f);
    List<string> vocabulary_library = new List<string>();
    List<string> total_words; //do not modify
    Dictionary<string , string> eng_to_cht_dict;
    public void set_parameters(UI _ui , List<string> vocabulary , Dictionary<string , string> eng_to_cht , List<string> total_vocabulary){
        ui = _ui;
        vocabulary_library.AddRange(vocabulary);
        eng_to_cht_dict = eng_to_cht;
        total_words = total_vocabulary;
        reset_ballon_position();
        set_question_and_clouds();
    }
    Vector2 ballon_origin;
    public void set_ballon_origin(){
        ballon_origin = hot_ballon.transform.position;
    }
    [SerializeField] HotBallon hot_ballon;
    public void reset_ballon_position(){
        hot_ballon.transform.position = new Vector2(0 , -3f);
    }
    public void move_ballon(Vector2 vec){
        hot_ballon.move_to(ballon_origin + vec);
    }
    [SerializeField] List<Cloud> answer_clouds = new List<Cloud>(); //do not modify
    List<Cloud> all_clouds = new List<Cloud>();
    List<string> wrong_answers = new List<string>();
    [SerializeField] TextMeshProUGUI question;
    string current_word;
    void set_question_and_clouds(){
        all_clouds.AddRange(answer_clouds);
        wrong_answers.AddRange(total_words);
        int random_num = Random.Range(0 , vocabulary_library.Count);
        current_word = vocabulary_library[random_num];
        question.text = current_word;
        int random_cloud = Random.Range(0 , 4);
        all_clouds[random_cloud].set_answer(eng_to_cht_dict[current_word] , true);
        all_clouds.Remove(all_clouds[random_cloud]);
        wrong_answers.Remove(current_word);
        for(int i = 0; i < all_clouds.Count; i++){
            int random_wrong = Random.Range(0 , wrong_answers.Count);
            all_clouds[i].set_answer(eng_to_cht_dict[wrong_answers[random_wrong]] , false);
            wrong_answers.Remove(wrong_answers[random_wrong]);
        }
    }
}
