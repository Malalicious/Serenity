# This project is no longer maintained and is kept purely for educational reasons

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

## Contact
You can post an issue in the repo and I will check them out. I will push changes I make when I feel like actually working on this project. I will see if I can write a wiki overtime.
