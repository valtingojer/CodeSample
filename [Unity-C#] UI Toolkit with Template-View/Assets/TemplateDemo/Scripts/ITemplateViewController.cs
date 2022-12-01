using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface ITemplateViewController
{
    GameObject GobackObject { get; }
    string Title { get; }
    string ConfirmText { get; }
    TemplateDemoManager TemplateController { get; }
    void OnEnable();
    void OnDisable();
    void OnBeforeDestroy();
    void AfterEnable();
    void BeforeDisable();
    void ConfigureHeader();
    void ConfigureFooter();
    void OnCloseEvent();
    void OnConfirmEvent();
    void OnBackEvent();
}
