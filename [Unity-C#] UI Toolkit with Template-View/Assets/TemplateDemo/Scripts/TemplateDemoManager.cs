using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TemplateDemoManager : MonoBehaviour
{
    UIDocument Document;
    [SerializeField] VisualTreeAsset Template;
    [SerializeField] VisualTreeAsset RenderTemplate;
    public VisualElement root { get; private set; }
    public VisualElement body { get; private set; }
    public VisualElement header { get; private set; }
    public VisualElement footer { get; private set; }
    public Button closeButton { get; private set; }
    
    public Button confirmButton { get; private set; }
    public Label title { get; private set; }

    private void OnEnable()
    {
        AutoSetupView();
    }
    private void AutoSetupView()
    {
        if (Template == null && RenderTemplate == null)
        {
            return;
        }

        SetupView();
    }
    
    public void SetupView()
    {
        Document ??= GetComponent<UIDocument>();

        if (Template == null && RenderTemplate == null)
        {
            Debug.LogError("Template is not set");
            return;
        }

        Document.visualTreeAsset = Template ?? RenderTemplate;
        root = Document.rootVisualElement;
        header = root.Q<VisualElement>("header");
        footer = root.Q<VisualElement>("footer");
        closeButton = header.Q<Button>("closeButton");
        confirmButton = footer.Q<Button>("confirmButton");
        title = header.Q<Label>("title");

        if (Template != null && RenderTemplate != null)
        {
            body = root.Q<VisualElement>("body");
            var cloneRender = RenderTemplate.CloneTree();
            cloneRender.style.flexGrow = 1;
            body.Add(cloneRender);
        }
        else
        {
            body = root;
        }
    }

    public void CreateNewWindow(GameObject window, Transform parent = null)
    {
        Instantiate(window, Vector3.zero, Quaternion.identity, parent ?? transform.parent);
    }

}
