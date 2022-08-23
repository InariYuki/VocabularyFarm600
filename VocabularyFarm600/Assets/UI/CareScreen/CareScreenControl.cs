using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CareScreenControl : MonoBehaviour
{
    [SerializeField] GameObject hugPopup, hugResultPopup;
    [SerializeField] TextMeshProUGUI hugPopupMsg, hugResultMsg;
    [SerializeField] HugMessage hugMsgCtl;
    public void OnHugButtonClicked()
    {
        ShowHugPopup();
    }
    public void OnHugPopupYesButtonClicked()
    {
        hugPopup.SetActive(false);
        ShowHugResult();
    }
    public void OnHugResultYesButtonClicked()
    {
        hugResultPopup.SetActive(false);
    }
    void ShowHugPopup()
    {
        hugPopupMsg.text = hugMsgCtl.GetHugMsg();
        hugPopup.SetActive(true);
    }
    void ShowHugResult()
    {
        hugResultMsg.text = hugMsgCtl.GetHugResult();
        hugResultPopup.SetActive(true);
    }
}
