using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Hugmsg")]
public class HugMessage : ScriptableObject
{
    [SerializeField] List<string> hugMsg = new List<string>();
    [SerializeField] List<string> hugResult = new List<string>();
    public string GetHugMsg()
    {
        return hugMsg[Random.Range(0, hugMsg.Count)];
    }
    public string GetHugResult()
    {
        return hugResult[Random.Range(0, hugResult.Count)];
    }
}
