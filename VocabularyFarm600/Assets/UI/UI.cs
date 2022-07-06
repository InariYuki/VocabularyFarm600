using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    Dictionary<string , string> eng_to_cht_dict = new Dictionary<string, string>();
    public string[] vocabulary_eng = new string[0];
    public string[] vocabulary_cht = new string[0];
    private void Awake() {
        UI_canvas = GetComponent<Canvas>();
        for(int i = 0; i < vocabulary_eng.Length ; i++){
            eng_to_cht_dict[vocabulary_eng[i]] = vocabulary_cht[i];
        }
    }
    private void Update() {
        input_control();
        main_camera.transform.position = new Vector3(Mathf.Clamp(main_camera.transform.position.x , -camera_clamp.x + main_camera.orthographicSize * main_camera.aspect , camera_clamp.x - main_camera.orthographicSize * main_camera.aspect) , Mathf.Clamp(main_camera.transform.position.y , -camera_clamp.y + main_camera.orthographicSize , camera_clamp.y - main_camera.orthographicSize) , -10);
    }
    public Camera main_camera;
    Vector2 camera_clamp = new Vector2(48f , 27f);
    Canvas UI_canvas;
    Vector3 touch_point;
    GiantAnimal currently_interacting_animal;
    public int control_mode = 0;
    void input_control(){
        switch(control_mode){
            case 0:
                navigation_mode();
                break;
            case 1:
                object_control_mode();
                break;
            case 2:
                fur_game_mode();
                break;
            case 3:
                ballon_game_mode();
                break;
        }
    }
    public void game_ui_exit_pressed()
    {
        close_fur_game();
        close_ballon_game();
    }
    void navigation_mode(){
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            touch_point = main_camera.ScreenToWorldPoint(Input.mousePosition);
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0)){
            RaycastHit2D hit = Physics2D.Raycast(main_camera.ScreenToWorldPoint(Input.mousePosition) , Vector2.up , 0.001f);
            if(hit){
                currently_interacting_animal = hit.transform.GetComponent<GiantAnimal>();
                currently_interacting_animal.interact(this);
                if(currently_interacting_animal._button_show == false){
                    currently_interacting_animal.toggle_buttons();
                }
            }
            else{
                if(currently_interacting_animal != null && currently_interacting_animal._button_show){
                    currently_interacting_animal.toggle_buttons();
                }
                currently_interacting_animal = null;
            }
        }
        if(Input.GetKey(KeyCode.Mouse0)){
            main_camera.transform.position += touch_point - main_camera.ScreenToWorldPoint(Input.mousePosition);
        }
        main_camera.orthographicSize = Mathf.Clamp(main_camera.orthographicSize - Input.GetAxisRaw("Mouse ScrollWheel") , 1 , 16);
    }
    void object_control_mode(){
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            currently_interacting_animal.target_position = main_camera.ScreenToWorldPoint(Input.mousePosition);
            touch_point = main_camera.ScreenToWorldPoint(Input.mousePosition);
            control_mode = 0;
        }
    }
    float radius = 0.5f;
    int fur_game_substate = 0;
    void fur_game_mode(){
        switch(fur_game_substate){
            case 0:
                if(Input.GetKey(KeyCode.Mouse0)){
                    Collider2D[] alphabets = Physics2D.OverlapCircleAll(main_camera.ScreenToWorldPoint(Input.mousePosition) , radius);
                    for(int i = 0; i < alphabets.Length; i++){
                        BubbleAlphabet alphabet = alphabets[i].GetComponent<BubbleAlphabet>();
                        if(alphabet != null) alphabet.velocity = (alphabets[i].transform.position - main_camera.ScreenToWorldPoint(Input.mousePosition)).normalized;
                    }
                }
                break;
            case 1:
                if(Input.GetKey(KeyCode.Mouse0)){
                    Collider2D[] alphabet_containers = Physics2D.OverlapCircleAll(main_camera.ScreenToWorldPoint(Input.mousePosition) , radius);
                    for(int i = 0; i < alphabet_containers.Length; i++){
                        AlphabetContainer alphabet_container = alphabet_containers[i].GetComponent<AlphabetContainer>();
                        if(alphabet_container != null) alphabet_container.destroy_container();
                    }
                }
                break;
        }
    }
    Vector3 ballon_original_pos;
    void ballon_game_mode()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ballon_original_pos = main_camera.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            ballon_game_screen.GetComponent<BallonGame>().move_ballon(main_camera.ScreenToWorldPoint(Input.mousePosition));
        }
    }
    public void set_fur_game_substate(int state){
        fur_game_substate = state;
    }
    [SerializeField] GameObject main_screen_ui , game_screen_ui;
    [SerializeField] Transform animal_container;
    void start_main_screen(){
        close_care_screen();
        main_screen.SetActive(true);
        main_screen_ui.SetActive(true);
        for(int i = 0; i < animal_container.childCount; i++){
            animal_container.GetChild(i).GetComponent<GiantAnimal>().start_ai_activity();
        }
    }
    void close_main_screen(){
        main_screen.SetActive(false);
        main_screen_ui.SetActive(false);
    }
    [SerializeField] GameObject care_screen;
    public void toggle_care_screen(GiantAnimal _game_animal){
        care_screen.SetActive(true);
        game_animal = _game_animal;
    }
    public void close_care_screen(){
        care_screen.SetActive(false);
    }
    [SerializeField] GameObject main_screen;
    [SerializeField] GameObject fur_game_screen;
    GiantAnimal game_animal;
    public void start_fur_game(){
        close_care_screen();
        close_main_screen();
        fur_game_screen.SetActive(true);
        game_screen_ui.SetActive(true);
        main_camera.transform.position = new Vector3(0 , 0 , -10);
        main_camera.orthographicSize = 6;
        control_mode = 2;
        fur_game_substate = 0;
        FurGame game_ctl = fur_game_screen.GetComponent<FurGame>();
        game_ctl.set_library(this , game_animal.first_word , game_animal.last_word);
    }
    public void close_fur_game(){
        fur_game_screen.SetActive(false);
        game_screen_ui.SetActive(false);
        start_main_screen();
        control_mode = 0;
        fur_game_screen.GetComponent<FurGame>()._games_finished = 0;
    }
    [SerializeField] GameObject ballon_game_screen;
    public void start_ballon_game()
    {
        close_care_screen();
        close_main_screen();
        ballon_game_screen.SetActive(true);
        game_screen_ui.SetActive(true);
        main_camera.transform.position = new Vector3(0, 0, -10);
        main_camera.orthographicSize = 5;
        control_mode = 3;
        BallonGame game_ctl = ballon_game_screen.GetComponent<BallonGame>();
        game_ctl.set_library(this , game_animal.first_word , game_animal.last_word);
    }
    void close_ballon_game()
    {
        ballon_game_screen.SetActive(false);
        game_screen_ui.SetActive(false);
        start_main_screen();
        control_mode = 0;
    }
    public List<string> pull_words_from_dict(int start , int end){
        List<string> vocabulary_library = new List<string>();
        for(int i = start; i < end; i++){
            vocabulary_library.Add(vocabulary_eng[i]);
        }
        return vocabulary_library;
    }
    public string look_up_in_the_dictionary(string eng){
        return eng_to_cht_dict[eng];
    }
}
