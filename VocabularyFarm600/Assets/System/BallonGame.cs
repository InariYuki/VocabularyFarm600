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
        words_done.Clear();
        answer_wrong.Clear();
        if(vocabulary_library.Count < 5){
            target = vocabulary_library.Count;
        }
        progress_display.text = "完成 : 0 / " + target;
        progress = 0;
        reset_ballon_position();
        set_question_and_clouds();
        release_clouds();
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
    [SerializeField] TextMeshProUGUI question;
    string current_word;
    void set_question_and_clouds(){
        answer_is_right = false;
        List<Cloud> all_clouds = new List<Cloud>();
        List<string> wrong_answers = new List<string>();
        all_clouds.AddRange(answer_clouds);
        wrong_answers.AddRange(total_words);
        int random_num = Random.Range(0 , vocabulary_library.Count);
        current_word = vocabulary_library[random_num];
        vocabulary_library.Remove(current_word);
        question.text = current_word;
        int random_cloud = Random.Range(0 , 4);
        all_clouds[random_cloud].set_answer(this , eng_to_cht_dict[current_word] , true);
        all_clouds.Remove(all_clouds[random_cloud]);
        wrong_answers.Remove(current_word);
        for(int i = 0; i < all_clouds.Count; i++){
            int random_wrong = Random.Range(0 , wrong_answers.Count);
            all_clouds[i].set_answer(this , eng_to_cht_dict[wrong_answers[random_wrong]] , false);
            wrong_answers.Remove(wrong_answers[random_wrong]);
        }
    }
    void release_clouds(){
        answer_clouds[0].transform.position = left_most_spawn_point;
        answer_clouds[1].transform.position = left_spawn_point;
        answer_clouds[2].transform.position = right_spawn_point;
        answer_clouds[3].transform.position = right_most_spawn_point;
        StartCoroutine(wait_for_clouds());
    }
    IEnumerator wait_for_clouds(){
        yield return new WaitForSeconds(0.5f);
        for(int i = 0; i < 4; i++){
            answer_clouds[i].transform.position += new Vector3(Random.Range(-0.5f , 0.5f) , Random.Range(-3f , 3f) , 0);
            answer_clouds[i].can_move = true;
        }
    }
    public void check_all_cloud_bottom(){
        for(int i = 0; i < 4; i++){
            if(answer_clouds[i].transform.position.y < -12f){
                continue;
            }
            else{
                return;
            }
        }
        set_answer_wrong();
        set_progress();
        if(progress == target){
            ui.close_ballon_game(words_done , answer_wrong);
            return;
        }
        set_question_and_clouds();
        release_clouds();
    }
    List<string> words_done = new List<string>();
    bool answer_is_right = false;
    public void set_answer_right(){
        words_done.Add(current_word);
        answer_is_right = true;
        print("right");
    }
    List<string> answer_wrong = new List<string>();
    void set_answer_wrong(){
        if(answer_is_right) return;
        words_done.Add(current_word);
        answer_wrong.Add(current_word);
        print("wrong");
    }
    [SerializeField] TextMeshProUGUI progress_display;
    int progress = 0;
    int target = 5;
    void set_progress(){
        progress++;
        progress_display.text = "完成 : " + progress + " / " + target;
    }
}
