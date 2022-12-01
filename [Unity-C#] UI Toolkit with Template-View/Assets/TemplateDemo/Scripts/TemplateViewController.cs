using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class TemplateViewController : MonoBehaviour, ITemplateViewController
{
    public TemplateDemoManager TemplateController { get; private set; }

    [SerializeField] GameObject goBackObject;
    [SerializeField] string title = "Menu";
    [SerializeField] string confirmText = "Ok";
    public GameObject GobackObject { get => goBackObject; set => goBackObject = value; }
    public string Title { get => title; set => title = value; }

    public string ConfirmText { get => confirmText; set => confirmText = value; }
    

    public virtual void AfterEnable()
    {
        Debug.Log("TemplateViewController.AfterEnable");
    }
    public virtual void OnBeforeDestroy()
    {
        Debug.Log("TemplateViewController.OnBeforeDestroy");
    }
    public virtual void BeforeDisable()
    {
        Debug.Log("TemplateViewController.BeforeDisable");
    }
    public virtual void OnDisable()
    {
        BeforeDisable();
        Debug.Log("TemplateViewController.OnDisable");
        TemplateController.closeButton.clicked -= OnCloseEvent;
        TemplateController.confirmButton.clicked -= OnConfirmEvent;
        OnBeforeDestroy();
        Destroy(gameObject);
    }
    public virtual void OnEnable()
    {
        TemplateController = GetComponent<TemplateDemoManager>();
        ConfigureHeader();
        ConfigureFooter();
        Debug.Log("TemplateViewController.OnEnable");
        AfterEnable();
    }
    public virtual void ConfigureHeader()
    {
        TemplateController.title.text = title;
        TemplateController.closeButton.clicked += OnCloseEvent;
    }
    public virtual void ConfigureFooter()
    {
        TemplateController.confirmButton.text = confirmText;
        TemplateController.confirmButton.clicked += OnConfirmEvent;
    }
    public virtual void OnCloseEvent()
    {
        OnBackEvent();
        gameObject.SetActive(false);
    }
    public virtual void OnConfirmEvent()
    {
        OnBackEvent();
        gameObject.SetActive(false);
    }
    public virtual void OnBackEvent()
    {
        if (goBackObject == null)
            return;
        Instantiate(GobackObject, Vector3.zero, Quaternion.identity, transform.parent ?? transform.root);
    }
}
