# UI Toolkit sample

This sample shows how to use the Unity UI toolkit with template structure

You can see the screen working by running the project and pressing "esc"

It uses the "TemplateDemo" as base folder

The Uxml and Uss folders are for base template and render files

ViewScripts will act as mvc controllers, it is inherited from TemplateViewController class,
with will handle the majority of the opening and closing actions,
Where the view will implement as override its own needs.

The TemplateDemoManager will handle the UIDocument and its accessibility to controllers.

The prefab will act as View, containing the info about the template base, the render uxml,
custom title and expects to receive specific script controller to render and bind actions to
its specific view.
