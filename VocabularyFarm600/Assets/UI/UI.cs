using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] List<GiantAnimal> g_animals = new List<GiantAnimal>();
    private void Awake() {
        animal_layer = LayerMask.GetMask("Animal");
        building_layer = LayerMask.GetMask("Building");
    }
    private void Start() {
        add_building_button(carrot_house_button);
        add_g_animal(g_animals[0]);
    }
    private void Update() {
        input_control();
        main_camera.transform.position = new Vector3(Mathf.Clamp(main_camera.transform.position.x , -camera_clamp.x + main_camera.orthographicSize * main_camera.aspect , camera_clamp.x - main_camera.orthographicSize * main_camera.aspect) , Mathf.Clamp(main_camera.transform.position.y , -camera_clamp.y + main_camera.orthographicSize , camera_clamp.y - main_camera.orthographicSize) , -10);
    }
    public Camera main_camera;
    Vector2 camera_clamp = new Vector2(48f , 27f);
    Vector3 touch_point;
    GiantAnimal currently_interacting_animal;
    public int control_mode = 0;
    LayerMask animal_layer , building_layer;
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
            case 4:
                build_mode();
                break;
            default:
                break;
        }
    }
    [SerializeField] BuildingButton carrot_house_button;
    [SerializeField] Transform building_button_container;
    public void add_building_button(BuildingButton button){
        BuildingButton button_instanced = Instantiate(button  , building_button_container.position , Quaternion.identity , building_button_container);
        button_instanced.init(this);
    }
    public void add_g_animal(GiantAnimal animal){
        Instantiate(animal , Vector3.zero , Quaternion.identity , animal_container);
    }
    public void game_ui_exit_pressed()
    {
        close_fur_game(null , null);
        close_ballon_game();
    }
    void navigation_mode(){
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            touch_point = main_camera.ScreenToWorldPoint(Input.mousePosition);
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0)){
            RaycastHit2D hit = Physics2D.Raycast(main_camera.ScreenToWorldPoint(Input.mousePosition) , Vector2.up , 0.001f , animal_layer);
            if(hit){
                currently_interacting_animal = hit.transform.GetComponent<GiantAnimal>();
                if(currently_interacting_animal != null){
                    currently_interacting_animal.interact(this);
                    if(currently_interacting_animal._button_show == false){
                        currently_interacting_animal.toggle_buttons();
                    }
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
    Vector3 ballon_touch_point;
    Vector3 move_vector;
    void ballon_game_mode()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            ballon_touch_point = main_camera.ScreenToWorldPoint(Input.mousePosition);
            ballon_game_screen.set_ballon_origin();
        }
        if(Input.GetKey(KeyCode.Mouse0)){
            move_vector = main_camera.ScreenToWorldPoint(Input.mousePosition) - ballon_touch_point;
            ballon_game_screen.move_ballon(move_vector);
        }
    }
    int build_mode_substate = 0;
    Building current_building;
    void build_mode(){
        switch(build_mode_substate){
            case 0:
                if(Input.GetKeyDown(KeyCode.Mouse0)){
                    RaycastHit2D hit = Physics2D.Raycast(main_camera.ScreenToWorldPoint(Input.mousePosition) , Vector2.up , 0.001f , building_layer);
                    if(!hit && building != null && Input.mousePosition.y > 150f){
                        Vector3 mouse_pos = main_camera.ScreenToWorldPoint(Input.mousePosition);
                        instantiate_building(new Vector3(mouse_pos.x , mouse_pos.y , 0));
                        build_mode_substate = 1;
                    }
                }
                break;
            case 1:
                if(Input.GetKeyDown(KeyCode.Mouse0)){
                    RaycastHit2D hit = Physics2D.Raycast(main_camera.ScreenToWorldPoint(Input.mousePosition) , Vector2.up , 0.001f , building_layer);
                    if(hit){
                        current_building = hit.transform.GetComponent<Building>();
                        if(!current_building.can_be_moved){
                            current_building = null;
                            return;
                        }
                        current_building.show_button();
                    }
                    else{
                        if(current_building != null){
                            current_building.hide_button();
                            current_building = null;
                        }
                    }
                }
                if(Input.GetKey(KeyCode.Mouse0) && current_building != null){
                    Vector3 vec = main_camera.ScreenToWorldPoint(Input.mousePosition);
                    current_building.transform.position = new Vector3(Mathf.RoundToInt(vec.x * 100 / 32) * 0.32f , Mathf.RoundToInt(vec.y * 100 / 32) * 0.32f);
                }
                break;
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
    [SerializeField] Transform translation_score , spell_score;
    public void toggle_care_screen(GiantAnimal _game_animal){
        care_screen.SetActive(true);
        main_screen_ui.SetActive(false);
        game_animal = _game_animal;
        control_mode = 99;
        display_score(translation_score , game_animal);
        display_score(spell_score , game_animal);
    }
    public void close_care_screen(){
        care_screen.SetActive(false);
        main_screen_ui.SetActive(true);
        control_mode = 0;
    }
    void display_score(Transform score_board , GiantAnimal animal)
    {
        for(int i = 0; i < animal.vocabulary_eng.Count; i++)
        {
            Transform word_display = score_board.GetChild(i);
            if(score_board == translation_score)
            {
                word_display.GetChild(0).GetComponent<TextMeshProUGUI>().text = animal.vocabulary_eng[i].ToString();
                word_display.GetChild(1).GetComponent<TextMeshProUGUI>().text = animal.word_to_times_translation[animal.vocabulary_eng[i]].ToString();
                word_display.GetChild(2).GetComponent<TextMeshProUGUI>().text = animal.word_to_wrong_translation[animal.vocabulary_eng[i]] * 100 / ((animal.word_to_times_translation[animal.vocabulary_eng[i]] == 0) ? 1 : animal.word_to_times_translation[animal.vocabulary_eng[i]])  + "%";
            }
            else
            {
                word_display.GetChild(0).GetComponent<TextMeshProUGUI>().text = animal.vocabulary_eng[i].ToString();
                word_display.GetChild(1).GetComponent<TextMeshProUGUI>().text = animal.word_to_times_spell[animal.vocabulary_eng[i]].ToString();
                word_display.GetChild(2).GetComponent<TextMeshProUGUI>().text = animal.word_to_wrong_spell[animal.vocabulary_eng[i]] * 100 / ((animal.word_to_times_spell[animal.vocabulary_eng[i]] == 0) ? 1 : animal.word_to_times_spell[animal.vocabulary_eng[i]]) + "%";
            }
        }
    }
    [SerializeField] GameObject spell_score_panel;
    public void toggle_spell_score_panel()
    {
        spell_score_panel.SetActive(true);
    }
    public void close_spell_score_panel()
    {
        spell_score_panel.SetActive(false);
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
        game_ctl.set_library(this , (game_animal.unfinished_eng_spell.Count == 0) ? game_animal.vocabulary_eng : game_animal.unfinished_eng_spell , game_animal.eng_to_cht);
    }
    public void close_fur_game(List<string> words_finished , List<string> words_failed){
        fur_game_screen.SetActive(false);
        game_screen_ui.SetActive(false);
        start_main_screen();
        control_mode = 0;
        fur_game_screen.GetComponent<FurGame>()._games_finished = 0;
        if(words_finished != null)
        {
            for(int i = 0; i < words_finished.Count; i++)
            {
                if(!words_failed.Contains(words_finished[i]))
                {
                    game_animal.unfinished_eng_spell.Remove(words_finished[i]);
                }
                game_animal.word_to_times_spell[words_finished[i]] += 1;
            }
            for(int i = 0; i < words_failed.Count; i++)
            {
                game_animal.word_to_wrong_spell[words_failed[i]] += 1;
            }
        }
    }
    [SerializeField] BallonGame ballon_game_screen;
    public void start_ballon_game()
    {
        close_care_screen();
        close_main_screen();
        ballon_game_screen.gameObject.SetActive(true);
        game_screen_ui.SetActive(true);
        main_camera.transform.position = new Vector3(0, 0, -10);
        main_camera.orthographicSize = 5;
        control_mode = 3;
        ballon_game_screen.init(this , game_animal.vocabulary_eng , game_animal.eng_to_cht , game_animal.unfinished_eng_translation);
    }
    public void close_ballon_game()
    {
        ballon_game_screen.gameObject.SetActive(false);
        game_screen_ui.SetActive(false);
        start_main_screen();
        control_mode = 0;
    }
    [SerializeField] GameObject build_screen_ui;
    [SerializeField] GameObject Grid_display;
    [SerializeField] Transform building_container;
    public void open_build_screen(){
        main_screen_ui.SetActive(false);
        build_screen_ui.SetActive(true);
        Grid_display.SetActive(true);
        for(int i = 0; i < building_container.childCount; i++){
            building_container.GetChild(i).GetComponent<Building>().show_building_area();
        }
        control_mode = 4;
        build_mode_substate = 1;
    }
    public void close_build_screen(){
        main_screen_ui.SetActive(true);
        build_screen_ui.SetActive(false);
        Grid_display.SetActive(false);
        for(int i = 0; i < building_container.childCount; i++){
            building_container.GetChild(i).GetComponent<Building>().hide_building_area();
        }
        control_mode = 0;
    }
    GameObject building;
    public void set_building(GameObject _building){
        building = _building;
        build_mode_substate = 0;
    }
    void instantiate_building(Vector2 pos){
        Instantiate(building , pos , Quaternion.identity , building_container);
    }
}
