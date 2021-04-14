# Welcome!
Welcome to DillyzThe1's Role Api!
This API allows for an easy & documented way off adding a role that works accross mods using the same API!
To use this api, please add either of these links in your readme.md file:
https://github.com/DillyzThe1/DillyzRoleApi
https://discord.gg/Xfk4FUep

# Setup
Assuming you have over 6 braincells, you should probably know how to add a reference in vs.
If not, here's how.

Step 1: Right click "Dependencies" and click "Add Project Reference"
![GitHub Logo](/instructions/pics/addref.png)

Step 2: Click "Browse" and go to "C:\Program Files (x86)\Steam\steamapps\common\Among Us\BepInEx\plugins" where DillyzRoleAPI-2021.4.12s.dll should be at.
![Github Logo](/instructions/pics/browse.png)

Step 3: Click "Add" like a normal person and start creating!

(Your dependencies dropdown should look like this now)
![Github Logo](/instructions/pics/browse.png)

# Adding a role 
In your HarmonyMain.cs file, you need to add a new static RoleGenerator to the game.
![Github Logo](/instructions/pics/rolegen.png)

(Don't forget to add "using DillyzRolesAPI.Roles;")

Next, set every variable in the role you want.
There are a limited amount, but there is a way to patch stuff yourself.
![Github Logo](/instructions/pics/roleenable.png)

"What if I want a setting to determine if my role is enabled?"
Well, I can't help you with creating settings, but if you know how to do custom settings you prefix/postfix HudManager.Update to make yourRole.isEnabled be the same as your setting.
(isEnabled is a bool within my custom mono called "RoleGenerator", so make sure to always set true or false.)

# Reading roles
The way to read if someone is a role is simple.
Here are 4 ways to read a players role.
![Github Logo](/instructions/pics/rolecheck.png)

# Adding your own credits
The way to add your own credit is simple.
All you have to do is add it to the list!
![Github Logo](/instructions/pics/credits.png)

# Disabling shared colors
In the mod, you might notice everybody can pick the same color.
I added a simple way to disable this as shown here.
![Github Logo](/instructions/pics/yoink.png)

When false:
![Github Logo](/instructions/pics/false.png)

When true:
![Github Logo](/instructions/pics/true.png)

# You did it?
Wow, you made a role!
Now it only takes 30 seconds rather than 10 minutes!
![Github Logo](/instructions/pics/crewpostor.png)
Enjoy making roles :)
NOTE: ow that new font hurts my eyes. i hope the font gets reverted back to the 12.9s version.