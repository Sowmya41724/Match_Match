\# Memory Match



A cute 2D memory card matching game for Android, made with Unity.

Flip the cards, find all the pairs, and earn up to 3 stars on every level!



<p align="center">

&#x20; <img src="screenshots/home\_screen.jpeg" width="250">

&#x20; <img src="screenshots/level\_screen.jpeg" width="250">

&#x20; <img src="screenshots/game\_screen.jpeg" width="250">

</p>



\## Features



\- 5 levels with growing difficulty (from 2 pairs up to 10 pairs)

\- Star rating (1 to 3) based on how many moves you take

\- Levels unlock as you progress, and your progress is saved on the device

\- Pause menu to resume, restart or return to level select

\- Sound effects and background music

\- Portrait layout designed for phones



\## How to play



1\. Tap Play, then pick a level.

2\. Tap a card to flip it, then tap a second card.

3\. If the two cards match, they stay face up. If not, they flip back.

4\. Match all the pairs to win. Fewer moves means more stars!



\## Download and play



Download the latest APK from the \[Releases](../../releases) page and install it on your Android phone.



> Android may show an "unknown source" or Play Protect warning because the app is not from the Play Store. Choose Install anyway.



\## Built with



\- Unity 6000.6.0f1 (2D)

\- C#

\- TextMeshPro and the Unity UI system



\## Run the project



1\. Install Unity 6000.6.0f1 with Android Build Support through Unity Hub.

2\. Clone this repo and open the folder in Unity Hub.

3\. Open the `Main\_Menu` scene and press Play.

4\. To build for Android, go to File > Build Profiles, select Android, and click Build.



\## Project structure



```

Assets/

&#x20; Scenes/    Main\_Menu, Level\_Select, Game\_Screen

&#x20; script/    GameManager, Card, LevelSelectManager, MenuManager, GameData, MusicPlayer

&#x20; Sprites/   Cards, buttons, backgrounds, icons

&#x20; Audio/     Sound effects and music

```



\## Credits



\- Art: created with AI image generation (ChatGPT) and edited by me

\- Sound effects: \[Pixabay](https://pixabay.com)

\- Background music: "Soft" by prettyjohn1 from \[Pixabay](https://pixabay.com)



\## What I learned



This is my first complete Unity mobile game. I practiced UI scaling for different phone sizes, scene management, saving progress with PlayerPrefs, spawning and shuffling objects with code, and building to Android.



\## Future ideas



\- Flip animation for the cards

\- Mute button for sound and music

\- More levels and card themes

\- Timer mode



\## Author



Sowmya N · \[GitHub](https://github.com/Sowmya41724)

