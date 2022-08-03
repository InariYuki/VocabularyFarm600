using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalCamera : MonoBehaviour
{
    private void FixedUpdate() {
        Follow();
    }
    bool is_following = false;
    GiantAnimal target_animal;
    void Follow(){
        if(!is_following) return;
        transform.position = Vector3.Lerp(transform.position , target_animal.transform.position , 2f);
    }
    public void FollowTarget(GiantAnimal ani){
        target_animal = ani;
        is_following = true;
    }
    public void UnFollow(){
        is_following = false;
    }
}
