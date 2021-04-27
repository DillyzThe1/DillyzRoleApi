# Welcome!
Welcome to DillyzThe1's Role Api!
This API allows for an easy & documented way off adding a role that works accross mods using the same API!<br/>
To use this api, please add either of these links in your readme.md file:<br/>
https://github.com/DillyzThe1/DillyzRoleApi<br/>
https://discord.gg/49NFTwcYgZ<br/>

# Setup
Assuming you have over 6 braincells, you should probably know how to add a reference in vs.<br/>
If not, here's how.<br/>
<br/>
Step 1: Right click "Dependencies" and click "Add Project Reference"<br/>
![GitHub Logo](/instructions/pics/addref.png)<br/>
<br/>
Step 2: Click "Browse" and go to "C:\Program Files (x86)\Steam\steamapps\common\Among Us\BepInEx\plugins" where DillyzRoleAPI-2021.4.12s.dll should be at.<br/>
![Github Logo](/instructions/pics/browse.png)<br/>
<br/>
Step 3: Click "Add" like a normal person and start creating! <br/>
(adding it to deobfuscate task works too, but this is easier for new people.)<br/>
<br/>
(Your dependencies dropdown should look like this now)<br/>
![Github Logo](/instructions/pics/depend.png)<br/>
<br/>
# Adding a role
In your HarmonyMain.cs file, you need to add a new static RoleGenerator to the game.<br/>
![Github Logo](/instructions/pics/rolegen.png)<br/>
<br/>
(Don't forget to add "using DillyzRolesAPI.Roles;")<br/>
<br/>
Next, set every variable in the role you want.<br/>
There are a limited amount, but there is a way to patch stuff yourself.<br/>
![Github Logo](/instructions/pics/roleenable.png)<br/>
(This is a screenshot from the sensei mod.)<br/>
<br/>
"What if I want a setting to determine if my role is enabled?"<br/>
Keep reading to the next section and then patch PlayerControl.RpcSetInfected.<br/>
![Github Logo](/instructions/pics/rpcsetinfected.png)<br/>
(isEnabled is a bool within my custom mono called "RoleGenerator", so make sure to always set true or false.)<br/>
<br/>
# Making a setting
Custom options: We all need it, but its uncommon to find.<br/>
(NOTE: These settings will only work with other mods that use this api or mods without settings at all.)<br/>
(Use any settings api at your compatibility own risk)<br/>
First, you need to do something similar to roles:<br/>
![Github Logo](/instructions/pics/options.png)<br/>
<br/>
Next, you'll need to set the values you need for them.<br/>
![Github Logo](/instructions/pics/boolandnumb.png)<br/>
TIP: Use .IncrementValue to set how much it goes up or down.<br/>
<br/>
Boom! Settings!<br/>
![Github Logo](/instructions/pics/lmaoileakmyownmods.png)<br/>
(Note: "Have Dictator" doesn't appear here due to being scrapped for dictator, as it was only a testing feature)<br/>
<br/>
# Reading roles
The way to read if someone is a role is simple.<br/>
Here is the 1 updated way on checking a role.<br/>
![Github Logo](/instructions/pics/rolecheck.png)<br/>
<br/>
# Adding your own credits
The way to add your own credit is simple.<br/>
All you have to do is add it to the list!<br/>
![Github Logo](/instructions/pics/credits.png)<br/>
<br/>
# Disabling shared colors
In the mod, you might notice everybody can pick the same color.<br/>
I added a simple way to disable this as shown here.<br/>
![Github Logo](/instructions/pics/yoink.png)<br/>
<br/>
When false:<br/>
![Github Logo](/instructions/pics/false.png)<br/>
<br/>
When true:<br/>
![Github Logo](/instructions/pics/true.png)<br/>
<br/>
# You did it?
Wow, you made a role!<br/>
Now it only takes 30 seconds rather than 10 minutes!<br/>
![Github Logo](/instructions/pics/crewpostor.png)<br/>
Enjoy making roles :)<br/>
NOTE: ow that new font hurts my eyes. i hope the font gets reverted back to the 12.9s version.<br/>
