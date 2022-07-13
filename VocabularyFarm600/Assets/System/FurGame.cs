using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FurGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI word_display , progress_bar;
    public Sprite[] alphabet_sprite = new Sprite[26];
    public char[] alphabet = new char[26];
    Dictionary<char , Sprite> char_to_instance_dict = new Dictionary<char, Sprite>();
    Dictionary<string , string> dictionary = new Dictionary<string, string>();
    private void Awake() {
        for(int i = 0; i < alphabet_sprite.Length; i++){
            char_to_instance_dict[alphabet[i]] = alphabet_sprite[i];
        }
    }
    UI ui;
    bool vocabulary_library_set = false;
    List<string> vocabulary_library = new List<string>();
    public void set_library(UI _ui , List<string> words , Dictionary<string , string> dict){
        if(vocabulary_library_set == false){
            vocabulary_library_set = true;
            ui = _ui;
            vocabulary_library.AddRange(words);
            for(int i = 0; i < dict.Count; i++){
                dictionary[words[i]] = dict[words[i]];
            }
        }
        set_current_word();
    }
    int games_finished = 0 , games_needs_to_be_finished = 5;
    public int _games_finished{
        set{
            games_finished = value;
        }
    }
    void set_current_word(){
        progress_bar.text = $"{games_finished}/{games_needs_to_be_finished}";
        if(vocabulary_library.Count == 0){
            //get next animal
            vocabulary_library_set = false;
            games_needs_to_be_finished = 30;
            ui.close_fur_game();
            return;
        }
        if(games_finished == games_needs_to_be_finished){
            ui.close_fur_game();
            return;
        }
        remove_all_props();
        int random_number = Random.Range(0 , vocabulary_library.Count);
        string[] word_set = {vocabulary_library[random_number] , dictionary[vocabulary_library[random_number]]};
        vocabulary_library.Remove(vocabulary_library[random_number]);
        word_display.text = word_set[1];
        word_length = word_set[0].Length;
        for(int i = 0; i < word_set[0].Length; i++){
            instantiate_alphabet(word_set[0][i] , i);
            instantiate_alphabet_container(i , new Vector2(-(word_length - 1) * 0.64f + i * 1.28f , -3));
        }
    }
    [SerializeField] GameObject bubble_alphabet;
    [SerializeField] Transform game_container;
    void instantiate_alphabet(char word , int id){
        BubbleAlphabet alphabet_instanced = Instantiate(bubble_alphabet , new Vector3(Random.Range(-5f , 5f) , Random.Range(-0.5f , 2f)) , Quaternion.identity , game_container).GetComponent<BubbleAlphabet>();
        alphabet_instanced.set_sprite(char_to_instance_dict[word] , id);
    }
    [SerializeField] GameObject alphabet_container;
    void instantiate_alphabet_container(int word , Vector2 pos){
        AlphabetContainer alphabet_container_instanced = Instantiate(alphabet_container , pos , Quaternion.identity , game_container).GetComponent<AlphabetContainer>();
        alphabet_container_instanced.set_expecting_char(word , this);
    }
    void remove_all_props(){
        finished_count = 0;
        for(int i = 0; i < game_container.childCount; i++){
            Destroy(game_container.GetChild(i).gameObject);
        }
    }
    int word_length;
    int finished_count = 0;
    public void check_word_finished(){
        finished_count++;
        if(word_length == finished_count){
            ui.set_fur_game_substate(1);
        }
    }
    public void brush(){
        word_length--;
        if(word_length == 0){
            games_finished++;
            set_current_word();
            ui.set_fur_game_substate(0);
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position , new Vector2(16 , 9));
    }
}
