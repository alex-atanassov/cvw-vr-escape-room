# Aalto University
# Coding Virtual Worlds - Assignments 4/5

Implementation of the fourth and fifth assignments of Coding Virtual Worlds, consisting in the creation of an Escape Room, using Pro Builder for the models, XR Interaction Toolkit, Animation Rigging and, for the multi-player, Normcore.

In detail, my implementation covers the following working functionalities:

## Assignment 4 (Single player)

-  **Escape room world with at least 3 objects** (0.5 point):
The Escape Room was created following the tutorials provided, which include a light switch, a lamp, a sliding door, a drawer, a mirror, two hint cards and a monitor for the audio settings. Additionally, I have added two extra elements to introduce more "puzzles" into the game: a wardrobe with another sliding door, and a pin code reader.

-  **At least one canvas UI** (0.5 point):
The game includes several canvases across the scene.<br>
The first canvas is for the audio settings, and it was created following the tutorial, including a slider for the volume and a toggle for the background music. <br>
Two canvases are used just to display the hints as a text onto other two card objects, that are hidden in the room. <br>
Another canvas is used to implement the pin reader, and includes 12 input buttons (0-9 digits, plus a cancel and a confirm button), plus two texts, one of which pops up only after entering the right input. 
Two more canvases display the countdown of seconds left to escape, one above the door, another on the robot's right hand. <br>
The last type of canvases are pop-up menus, which prompt the player whether to restart the game; one is shown after death and the other after winning, but both include two buttons Yes/No, which respectively restart the scene and quit the game. These two canvases have their Render Mode set to Screen Space - Camera, so that the UI will stay in front of the player, even when moving around.

-  **At least three diegetic user interactions** (1 point):
The interactions that are related to the game flow include opening/closing the drawer, pressing the switch to turn on/off the light, sliding the entrance door and the wardrobe door, entering an input using a pin reader made of UI buttons, and knocking on the door using sphere objects.

-  **Add game logic** (1 point):
The game features the logic required to win or lose the game. The base game flow from the tutorial includes a task where you need to pick up a bunch of spheres from inside the drawer, and then knock three times on the main door using them; this unlocks the door, which can now be opened by grabbing it from the handle. When the player exits the room, an invisible trigger collider will raise a Win event, consisting in a feedback sound and a UI canvas prompting the player to restart or quit the game. In case the player does not escape before the countdown reaches 0, another event - called Death - is used, which pops up another canvas with a different text, but prompting the same actions.

-  **Add robot avatar** (1 point):
The robot was added using Robot Kyle as base model. The legs were removed from the model, since it was not the focus of this assignment; the hands were instead animated, closing the first two fingers on trigger press, and the other three on grip press. The robot follows all the movements and rotation of head and arms through the XR Origin, using the VR Constraints from the Animation Rigging toolkit. The mirror in the room allows to see your own avatar, and was built using a Render Texture as a target of another fixed camera facing towards the player. Also notice that the robot's head was hidden using the Multi-Parent Constraint component: this effect can be easily reverted, but it was applied due to a minor visual glitch that allowed to see your own head when leaning back.

### Extra features

-  **Add sound design to table drawers and door** (0.5 point):<br>
All of the sliding objects, i.e. drawer, door and wardrobe, have an AudioSource component with a looping sound that resembles a sliding wooden door. Inside of the DrawerSetup script, which is attached to the respective parent objects, there is some added code that takes care of playing the sound when the handle is grabbed, and stopping it once released. Another script called SlidingSound will change the volume and pitch of this sound, based on the velocity of the respective Rigidbodies, meaning that sliding a drawer/door faster will result in a louder sound.

-  **Add more puzzles** (1-2 points):<br>
One more puzzle was added into the game, intended as an additional required task to progress the game. The completion of this added task unlocks the second task, which is the basic one from the tutorial.
The task consists in entering a secret pin code, by interacting with the pin reader canvas; in order to guess the code, the player can open the wardrobe on the right to find a hint card, which asks an arbitrary question, such as "What year are we in?" - and if the player enters "2022" as pin code, the pin reader will display the message "Drawer unlocked!". After this, the rest of the game is the same as the tutorial.


Lastly, some implicit features that were not required in this assignment were the movement controls, in which I have re-implemented the snap turn and continuous turn/movement; I have also kept the tunneling vignette from last week to prevent motion sickness. The other extra feature was the Menu UI after death and after winning, which prevents from getting stuck at the game over: before this, the death was only signaled by the camera fading to black, while the winning only had an "accomplishment" sound triggered by entering an invisible box collider.

## Assignment 5 (Multi-player)

-  **Add multiplayer connection** (1 point):<br>
The Escape Room is capable of hosting multiple players, who can join the room as soon as they run the apk on their own VR headset. This is made possible by the "Realtime + VR Player" component, which requires to insert an app key generated from the Normcore website.

-  **Synchronize light and timer** (1 point):<br>
The light is synchronized among the players, so that either they all see dark, or they see bright. The switch button in the scene allows to turn on the light for all players, on a single press. One of the two sliders on the canvas by the door (the one on the bottom) can adjust the light, binding the light intensity to a synced FloatVariable (the slider on the top adjust the volume in a similar way, using the same mechanic). Regarding the same canvas, the synchronization concerns not only the light and volume, but also the actual slider values on the UI.
Regarding the timer, the countdown is reset and started automatically whenever a player joins the game, so that the "Death" event occurs on both sides at the same time. The timer is set to start from 90 seconds.

-  **Synchronize at least one user interactions** (1 point):<br>
Multiple interactions have been synchronized. First, every player movement is tracked, including teleportation, snap turn, continuous movement and any other movement with the arms; all of this is made possible by the RealtimeTransform component applied to the Avatar.
As mentioned before, all the interactions with the light and audio UI canvas sliders are synchronized as well.
Other synchronized interactions include opening the drawer and doors, and carrying around the colored balls, still by using the Realtime Transform. In the case of drawer and doors, the synchronization also includes the lock or unlock, meaning that no player can open the drawer before one player has solved the pin code task, and similarly the exit door can be opened by both players only after solving the respective task. 

### Extra features

-  **Add voice modification for multiplayer** (1 point):<br>
An AudioMixer has been added, so that every player speaking will be heard with a higher pitch from the other players in the room.
Related to this feature, the microphone input animates the player's mouth, proportionally to the loudness of the respective speaker.

-  **Add multiplayer drawing mechanic** (1 point)<br>
The drawing mechanic was added following the normcore tutorials, and then fixed by changing the way the input is handled in the Brush script, due to compatibility problems of the InputSystem used.
Still, the final outcome is a fully working drawing mechanic, where the player's drawings are synchronized in the network. To draw a line, hold the right controller's trigger button.

---------------------
## Game flow:

1) Turn on the light pressing the switch on the left.
2) Open the wardrobe from the handle on the right to find a hint card inside.
3) Use the pin reader to input the answer to the first hint card. The correct pin will unlock the drawer (Answer = 2022).
4) Slide the drawer open, to find a second hint card, and three shperes. Pick up the spheres.
5) Go to the exit door, and knock on the door using the spheres three times. You can tell when a knock is valid when a short pottery sound plays.
6) After unlocking the door doing the above, slide to open it using the handle.
7) Go outside to win the game!
