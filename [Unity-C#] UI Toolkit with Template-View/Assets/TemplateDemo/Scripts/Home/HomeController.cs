using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HomeController : TemplateViewController
{
    [SerializeField] GameObject OtherTabTarget;
    public override void AfterEnable()
    {
        Debug.Log("HomeController.AfterEnable");
        TemplateController.body.Q<Button>("goToOtherTab").clicked += goToOtherTabEvent;
        base.AfterEnable();
    }
    //public override void BeforeDisable()
    //{
    //    Debug.Log("HomeController.BeforeDisable");
    //    base.BeforeDisable();
    //}
    //public override void OnDisable()
    //{
    //    Debug.Log("HomeController.OnDisable");
    //    base.OnDisable();
    //}
    //public override void OnEnable()
    //{
    //    Debug.Log("HomeController.OnEnable");
    //    base.OnEnable();
    //}
    //public override void ConfigureFooter()
    //{
    //    Debug.Log("HomeController.ConfigureFooter");
    //    base.ConfigureFooter();
    //}
    //public override void OnCloseEvent()
    //{
    //    Debug.Log("HomeController.OnCloseEvent");
    //    base.OnCloseEvent();
    //}
    public override void OnConfirmEvent()
    {
        Debug.Log("HomeController.OnConfirmEvent ... I am saving some data");
        base.OnConfirmEvent();
    }
    //public override void OnBackEvent()
    //{
    //    Debug.Log("HomeController.OnBackEvent");
    //    base.OnBackEvent();
    //}

    public override void OnBeforeDestroy()
    {
        Debug.Log("I am the pause screen, its better to restore timescale before close me");
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        
        base.OnBeforeDestroy();
    }

    private void goToOtherTabEvent()
    {
        TemplateController.CreateNewWindow(OtherTabTarget); 
    }
}
