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
    List<string> vocabulary_library = new List<string>();
    Dictionary<string , string> dictionary = new Dictionary<string, string>();
    private void Awake() {
        for(int i = 0; i < alphabet_sprite.Length; i++){
            char_to_instance_dict[alphabet[i]] = alphabet_sprite[i];
        }
    }
    UI ui;
    public void set_library(UI _ui , List<string> words , Dictionary<string , string> eng_to_cht_dict){
        ui = _ui;
        vocabulary_library.Clear();
        vocabulary_library.AddRange(words);
        for(int i = 0; i < words.Count; i++){
            dictionary[words[i]] = eng_to_cht_dict[words[i]];
        }
        create_tangled_fur();
        set_current_word();
    }
    [HideInInspector] public int _games_finished = 0 , games_needs_to_be_finished = 5;
    List<string> words_finished = new List<string>();
    List<string> words_failed = new List<string>();
    string current_word;
    void set_current_word(){
        if(vocabulary_library.Count == 0){
            //current animal finished !
            ui.close_fur_game(words_finished , words_failed);
            words_finished.Clear();
            words_failed.Clear();
            return;
        }
        if(_games_finished == games_needs_to_be_finished){
            ui.close_fur_game(words_finished , words_failed);
            words_finished.Clear();
            words_finished.Clear();
            return;
        }
        start_timer();
        progress_bar.text = $"{_games_finished}/{games_needs_to_be_finished}";
        remove_all_props();
        int random_number = Random.Range(0 , vocabulary_library.Count);
        string[] word_set = {vocabulary_library[random_number] , dictionary[vocabulary_library[random_number]]};
        current_word = vocabulary_library[random_number];
        vocabulary_library.Remove(current_word);
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
            _games_finished++;
            words_finished.Add(current_word);
            resolve_tangled_furs();
            set_current_word();
            ui.set_fur_game_substate(0);
        }
    }
    [SerializeField] TextMeshProUGUI timer_display;
    float time = 30f;
    float time_interval = 0.02f;
    Coroutine current_coroutine;
    void timer()
    {
        if(time >= 0)
        {
            time -= time_interval;
            timer_display.text = "Time : " + time.ToString("0.00");
            current_coroutine = StartCoroutine(timer_interval());
        }
        else
        {
            _games_finished++;
            words_finished.Add(current_word);
            words_failed.Add(current_word);
            set_current_word();
            ui.set_fur_game_substate(0);
        }
    }
    IEnumerator timer_interval()
    {
        yield return new WaitForSeconds(time_interval);
        timer();
    }
    void start_timer()
    {
        if(current_coroutine != null) StopCoroutine(current_coroutine);
        time = 30f;
        timer();
    }
    [SerializeField] List<TangledFur> furs = new List<TangledFur>();
    List<TangledFur> tangled_furs = new List<TangledFur>();
    void create_tangled_fur(){
        for(int i = 0; i < vocabulary_library.Count; i++){
            furs[i].reset_opacity();
            tangled_furs.Add(furs[i]);
        }
    }
    void resolve_tangled_furs(){
        int random_number = Random.Range(0 , tangled_furs.Count);
        tangled_furs[random_number].fade_out();
        tangled_furs.Remove(tangled_furs[random_number]);
    }
    [SerializeField] Transform comb;
    public void set_comb_position(Vector2 pos){
        comb.position = pos;
    }
}
