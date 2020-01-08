﻿# Black Spirit Helper

<p align="center">
    <img src="Resources/logo_red_text_512.png" alt="Logo Black Spirit Helper" width="256" style="text-align:center;">
</p>

**Are you tired of forgetting important tasks in a game such as... daily rewards, world boss spawns, buff activation at the proper time and so on?**  
Or... are you finding hard to track all the tasks you want to do in-game?  
  
Black Spirit Helper can **solve your problem!** You can also set the application to start with your operating system to do not miss any task you want to do, accidentally.  
**Black Spirit Helper offers** several features that can manage these things with multiple types of adjustable timers.  
Oh... I almost forgot to tell you, the application has **own overlay!** So, you can easily track your timers in-game without needs to switch to your desktop.
### Overlay?!#
Yes! Black Spirit Helper offers overlay! So, you do not need to switch to your desktop and then back to your game anymore! Enjoy your game comfortably without switching to system desktop.
### Is it legal to use?
Of course! Black Spirit Helper does **not** affect any game files. It does **not** track any screen events. It is just an advanced timer. I ask you... is Windows OS build-in timer illegal? It is in the same category as other software like [Discord](https://discordapp.com/), [TeamSpeak](https://www.teamspeak.com), etc. - For more info you can check [this](https://github.com/Frixs/BlackSpiritHelper/wiki/LegalUseProof) page!

## Table of Contents
- **[Features](#features)**
- **[Installation (Download)](#installation)**
- **[Credits](#credits)**
- **[License](#license)**
- **[TODO List](#todo-list)**
- **[Release History](#release-history)**

## Features
### Timer Section
It offers to you to create multiple groups with multiple timers in each. You can control each timer independently, and you can also control multiple timers with group control.  
Custom overlay time to set, in-game overlay control and much more! You can find it useful in all kinds of games whre you need to track self-buffs/food-buffs/potion-buffs at proper time.
### Schedule Section
This feature allows you to build your own schedule. Set timezone where you are currently - it recalculates time when you are abroad. Sound alerts and 3rd party message alerts (e.g. Discord).  
Very useful in any kind of games where you need to track down whatever at proper time. The application can start with your operating system, so you will never miss any event again. Contains predefined templates of popular games, you do not need to create your own from scratch.  
Create your own templates with your own items which suits your needs!
### Watchdog Section
This feature checks your internet connection and it also checks your game's process to determine, if your game is running or sitting in login screen (stuck in black/disconnection screen). Set alerts to inform you what happend. Capable to set routine options to proceed on failure (e.g. shutdown your PC). It also offers manager to prioritize your game or kill unsuitable processes.  
Everything just to avoid game disconnections over night!  
Based on what happends you can set events like send a message to your favorite platform to inform you about the situation or just simply shutdown/restart your computer.

## Installation
#### Requirements
- **Windows**: .NET 4.7.2+

#### Installation process
Follow the instructions **[here](https://github.com/Frixs/BlackSpiritHelper/wiki/Installation)**!

## Credits
#### Author
[@Tomáš Frixs](https://github.com/Frixs)  
If you want, you can **[Donate Me!](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=QE2V3BNQJVG5W&source=url)**

## License
[GNU General Public License v3.0](https://github.com/Frixs/BlackSpiritHelper/blob/master/LICENSE)

## TODO List
- [ ] Daily Check Section
- [x] Optimalization
- [x] Loading info/notification screen

## Release History
```
[23/12/2019] [1.3.0.0] - Watchdog MVP, Import/Export settings + Website release.
[26/10/2019] [1.2.2.0, BETA] - Added Schedule Custom Templates + user settings revamp.
[15/08/2019] [1.2.1.1, BETA] - Added Schedule section (beta).
[29/07/2019] [1.1.2.4, BETA] - Added custom settings provider due to issues with locating settings generated by MS while you are in Administrator mode or not. Timer code optimalized/recode. Added Setup methods into ApplicationDataContent, we are loading everything at the same time from the settings, so we do not want relations to IoC.DataContent inside during creation. Instead of that, we use setup methods which runs after creation to do the rest of work.
[26/07/2019] [1.0.2.0, BETA] - Overlay bug fixes. Added keypress to activate interaction with the overlay. Added group control buttons into overlay.
[25/07/2019] [1.0.1.0, BETA] - The application launch!
```
