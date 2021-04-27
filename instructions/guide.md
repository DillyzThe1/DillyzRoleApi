# Welcome!
Welcome to DillyzThe1's Role Api!
This API allows for an easy & documented way off adding a role that works accross mods using the same API!
To use this api, please add either of these links in your readme.md file:
https://github.com/DillyzThe1/DillyzRoleApi
https://discord.gg/49NFTwcYgZ

# Setup
Assuming you have over 6 braincells, you should probably know how to add a reference in vs.
If not, here's how.

Step 1: Right click "Dependencies" and click "Add Project Reference"
![GitHub Logo](/instructions/pics/addref.png)

Step 2: Click "Browse" and go to "C:\Program Files (x86)\Steam\steamapps\common\Among Us\BepInEx\plugins" where DillyzRoleAPI-2021.4.12s.dll should be at.
![Github Logo](/instructions/pics/browse.png)

Step 3: Click "Add" like a normal person and start creating! 
(adding it to deobfuscate task works too, but this is easier for new people.)

(Your dependencies dropdown should look like this now)
![Github Logo](/instructions/pics/depend.png)

# Adding a role 
In your HarmonyMain.cs file, you need to add a new static RoleGenerator to the game.
![Github Logo](/instructions/pics/rolegen.png)

(Don't forget to add "using DillyzRolesAPI.Roles;")

Next, set every variable in the role you want.
There are a limited amount, but there is a way to patch stuff yourself.
![Github Logo](/instructions/pics/roleenable.png)
(This is a screenshot from the sensei mod.)

"What if I want a setting to determine if my role is enabled?"
Keep reading to the next section and then patch PlayerControl.RpcSetInfected.
![Github Logo](/instructions/pics/rpcsetinfected.png)
(isEnabled is a bool within my custom mono called "RoleGenerator", so make sure to always set true or false.)

# Making a setting
Custom options: We all need it, but its uncommon to find.
(NOTE: These settings will only work with other mods that use this api or mods without settings at all.)
(Use any settings api at your compatibility own risk)
First, you need to do something similar to roles:
![Github Logo](/instructions/pics/options.png)

Next, you'll need to set the values you need for them.
![Github Logo](/instructions/pics/boolandnumb.png)
TIP: Use .IncrementValue to set how much it goes up or down.

Boom! Settings!
![Github Logo](/instructions/pics/lmaoileakmyownmods.png)
(Note: "Have Dictator" doesn't appear here due to being scrapped for dictator, as it was only a testing feature)

# Reading roles
The way to read if someone is a role is simple.
Here is the 1 updated way on checking a role.
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
