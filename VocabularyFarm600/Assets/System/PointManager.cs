using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointDisplay;
    int point = 0;
    private void Awake() {
        LoadPoint();
    }
    public void AddPoint(int pt){
        point += pt;
        UpdatePointDisplay();
    }
    void UpdatePointDisplay(){
        pointDisplay.text = $"本日積分:{point}/200";
    }
    void LoadPoint(){
        point = 0;
        UpdatePointDisplay();
    }
}
