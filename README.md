# Serenity
This is cheat software I have written a long time ago for Overwatch and used for quite a long time. It has been optimized for use with my sensitivity settings, I'll include those. Currently I do not give any support but I might push some updates when I return to Overwatch again. The cheat has been written in C# and the search algorithm is pretty fast. It's not the prettiest code and it was written over a weekend time.

The software compiles as Dropbox.exe. I used a custom-made obfuscator and packer to create unique copies for me and my friends to lower the chance of getting banned. So far nobody got banned using this, then again we don't really play Overwatch anymore. Got us to Top 500 2 seasons long though.

## Features
 * Aimbot
 * Anabot
 * Widowbot (experimental, functional but situational)
 * Triggerbot (experimental, not very useful).
 * Multiple resolutions supported (can be added easily)
 
### Aimbot
The aimbot works by using a custom pixel search function, looks for a specific color (with optional tolerance) and moves the mouse to the target. It is possible to enable force headshot for heroes like Widowmaker or Tracer in certain situations. To toggle force headshot, see the commands section below. By default the cheat only corrects the X-axis making it look a lot more legit.

### Anabot
The anabot works similar to the aimbot by looking for a specific color. When a teammate gets damaged the little arrow underneath their healthbar turns orange/yellow, this is the color the bot is looking for.

### Widowbot
The "widowbot" looks for the color of the outline around a hero. This only works on short to medium range since on long range the outline changes color to orange. With a few modifications you can probably detect multi-color. It is recommended to combine this with Force Headshot.

### Triggerbot
Nothing worth noting. Does not work properly but feel free to play around with it. It is disabled by default by having the thread commented out in `program.cs`.

## Commands
The software opens a console window. This window accepts input and a few commands were registered.

 * `aimbot.antishake` - this is an attempt to reduce shake caused on long range because of detecting multiple HP bars.
 * `anabot.toggle` - by default the Anabot is not enabled.

I've added a "quick toggle" to enable force headshot quickly. All you need to do is press `numpad1`.

## Keys
The components each use a different key to enable their features. By default the following is used:

 * Aimbot: Mouse 5
 * Anabot: Mouse 4 (It could have melee-attack binded to it)
 * Widowbot: Left Alt
 * Triggerbot: Left Alt
 
## My settings
The cheat was optimized for my settings and could therefor work strangely for you or not work at all.

 * In-game sensitivity: 10.00 ~ 15.00
 * DPI: 400
 * Windowed borderless
 * 1920x1080
 
## Customizing aimspeed
To change the speed at which the mouse moves towards the target: these values are currently hard-coded inside `Helpers/MouseHelper.cs`.
Inside this class, the method `Move()` calculates a step count for both the X and Y-axis. Play around with the `StepCount.X` and `StepCount.Y` to fiddle with the speed.

I preferred using this aim-method over the generic mouse_event aim-snippet you can find all over the internet. The main reason is that these generic algorithms move the mouse towards the target and slow down towards the end making the aim less accurate.

## Contact
You can post an issue in the repo and I will check them out. I will push changes I make when I feel like actually working on this project. I will see if I can write a wiki overtime.
