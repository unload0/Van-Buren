# Builders Guide 🛠️

this document lists and explains how to implement specific features in the game. It will also give a brief explanation on the tools implemented and how to effectively use them. This is not a Unity guide document, it only provides specific details about the game's framework that could be helpful.

# Table Of Contents

[Main Menu UI](#MainMenuUI)

## Main Menu UI

### Information

The main menu UI uses a single canvas that holds multiple panels (menus), each panel consists of either buttons or other UI elements that all together represent a specific menu. The canvas GameObject contains a custom component script `UIPanelNavigation.cs` that includes the functionality to handle panel switching and scene loading. Buttons can call these functions using their OnClick() Event.

### How to create a new menu

Menus are handled as UI Panels. To create a new Menu, right-click on the main canvas GameObject "Canvas" then go to UI -> Panel, This panel is now your new menu.

You can create buttons and more panels within the menu panel to represent the menu you want to create. It's also better to rename your menu to not cause confusion, an example would be *"pnl_credits"*.

**Make sure that it's only directly a child of "Canvas" or else it will not function properly.**

### How to change menus using Buttons

First make sure you have your panel menu ready. Select a button you wish to switch menus with, scroll down it's inspector until you see the OnClick() event, click on the plus sign (+) to add a new event. Then drag the main canvas GameObject "Canvas" into the slot with the circle sign, this will then fill the drop-down "No Function" with some options. Click on the "No Function" drop-down, then find and hover over **UIPanelNavigation**, this will then open another menu that shows all functions from `UIPanelNavigation.cs` that the button can call. Select **goToMenu** then drag and drop your panel GameObject into the open slot.
