Name: Smartphone with a flashlight and a camera mode.

Author: Michał Klekowicki

Content: see "filelist.pdf"

Description:

Thank toy for purchasing the "Smartphone - Flashlight/Camera - Interface" asset from the Unity Asset Store.

The package contains 3d models, animations, textures and UI elements that will allow you to implement the UI system - an arm holding a smartphone which replaces a typical flashlight and adds a camera mode with a full built in phone camera interface. You are, of course, allowed to modify the models, textures, animation and UI elements to fit your design (however if you base one of the elements directly on one of my models, an annotation "based on  <asset type here> by Michał Klekowicki" would be appreciated). Blend files and basic textures are provided with the package (see "filelist.pdf").

The animations are:
-turn on flashlight
-turn on camera
-turn on camera when the flashlight is on
All other variations of those animations (reversed versions) are handled script wise.

The asset also contains a free addition of an example script written by my friend Sara Jujeczka. It controls the animations and the UI behavior. Every function is commented on, so figuring out how does it work shouldn't be a problem. As with the models, you are free to modify the script as you like it, however if you base you use parts of code directly from the script elements made by my friend, or with minor changes, an annotation "based on a script by Sara Jujeczka" would be appreciated.


Implementation:

-The two script files attached to the asset pack, are meant to be attached to the Arm Object (for example with a drag and drop method)

-The spotlight object used as the flashlight is meant to be a child of the smartArmature (smartArmature is the child of metacarpal1 - a bone in the hand section of the Arm_Armature)

-The spotlight has to be tagged as "CameraFlashlight" as it is referenced as such in the script.

-The Asset Pack has 4 prefabs:
	-Camera UI (contains sprites)
	-The Player object with an example left hand and the camera (which is optional)
	-The Left Arm models/animations
	-The Right Arm models/animations (both are mostly the same, however mirrored)

-While implementing the above prefabs, you have to acknowledge that they're connected to certain scripts. You will have to drag and drop elements of the camera UI from the hierarchy window to the Instance UI (part of the arm script) category in the inspector window of the arm. See: "LOOKATME.png"

- To achieve the same effect as on the promo video on the asset store, adding the first person controller from the standard assets to the player character is needed.
