using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CloudController : MonoBehaviour
{
    [SerializeField] List<Cloud> clouds = new List<Cloud>();
    BallonGame game;
    public void init(BallonGame ctl){
        game = ctl;
    }
    public void set_cloud_text(string answer , List<string> strings){
        for(int i = 0; i < clouds.Count; i++){
            clouds[i].set_answer(strings[i] , answer == strings[i] , this);
        }
    }
    public void ans_correct(){
        game.round_finished();
        Destroy(gameObject);
    }
}
