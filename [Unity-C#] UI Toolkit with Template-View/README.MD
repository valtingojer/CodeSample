# UI Toolkit sample

This sample shows how to use the Unity UI toolkit with a template structure.

You can see the screen working by running the project and pressing "Esc" key.

It uses the "TemplateDemo" as the base folder

The Uxml and Uss folders are for the base template and render files.

ViewScripts will act as MVC controllers, it is inherited from TemplateViewController class,
it will handle the majority of the opening and closing actions,
Where the view will implement as override all its own needs.

The TemplateDemoManager will handle the UIDocument and its accessibility to controllers.

The prefab will act as View, containing the info about the template base, the render uxml,
custom title and expects to receive a specific script controller to render and bind actions to
its specific view.
