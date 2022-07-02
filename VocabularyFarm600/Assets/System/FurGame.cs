using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FurGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI word_display;
    public Sprite[] alphabet_sprite = new Sprite[26];
    public char[] alphabet = new char[26];
    Dictionary<char , Sprite> char_to_instance_dict = new Dictionary<char, Sprite>();
    private void Awake() {
        for(int i = 0; i < alphabet_sprite.Length; i++){
            char_to_instance_dict[alphabet[i]] = alphabet_sprite[i];
        }
    }
    UI ui;
    public void set_current_word(UI _ui){
        remove_all_props();
        ui = _ui;
        string[] word_set = ui.pull_random_word_from_dict();
        word_display.text = word_set[1];
        int i = 0;
        for(i = 0; i < word_set[0].Length; i++){
            instantiate_alphabet(word_set[0][i] , i);
        }
        word_length = i;
        for(int j = 0 ; j < word_set[0].Length; j++){
            instantiate_alphabet_container(j , new Vector2(-(i - 1) * 0.64f + j * 1.28f , -3));
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
            ui.start_fur_game();
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position , new Vector2(16 , 9));
    }
}
