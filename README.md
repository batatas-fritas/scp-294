# SCP-294 The Vending Machine ![GitHub all releases](https://img.shields.io/github/downloads/batatas-fritas/scp-294/total)
### SCP: Secret Laboratory plugin made with [EXILED](https://github.com/Exiled-Team/EXILED) and [MER](https://github.com/Michal78900/MapEditorReborn) (latest releases)
This plugin adds SCP-294 to the game. SCP-294 is a vending machine that will give you a drink based on the prompt you give it.
Besides the release, you might also want to download the scp schematic and make sure it spawns in the map otherwise the plugin will not work.
You can have your own schematic of scp294, however make sure its name and the name in the config are the same.
## Setup - If you have used MER before you might want to skip this, just make sure to add the schematic to the map you're using.
1. Make sure you have MapEditorReborn Plugin in your server
2. Add scp294 schematic provided to the schematics folder of your server
3. Go in game and spawn the schematic in whichever room you want using the command: mp create scp294
4. Place scp294 wherever you want, you can set the position and rotation of the machine with the command:
    - mp pos set x y z (replace x  y z with the coordinates you want -- keep in mind these are relative to the center of the room)
    - mp rot set x y z (replace x y z with the values you want)
    - you can also use the gravity gun (not recommended) mp gg
5. Once you have the machine in a spot you're happy with you can save the map with: mp save {whatever name you'd like for your map}.
6. Go into MER config and add the map name to the load_map_on_event - on_generated/on_round_started (it can be either one, choose one).
In this screenshot the name i gave to the map was 'scp294'. But it can be whatever you'd like.
![alt text](https://github.com/batatas-fritas/scp-294/blob/main/scp294/assets/merconfig.png?raw=true)
7. Restart your server and check if the machine has automatically spawned in the room you placed it before. It should have if you followed the steps
8. Scp294 is now ready to be used! Have fun!
## Usage In-Game
Open your client game console in game close to the machine and type in '.scp294 whatever drink you'd like'. You need to be holding a coin
## This is a list with every prompt and its respective drink.
| Prompt   |  Result drink  |
| :------: | :------------: |
| list | not a drink. It lists every drink available |
| drink of blue candy | gives the player a boosted blue candy effect (the boost can be specified in the config) |
| drink of green candy | gives the player a boosted green candy effect (the boost can be specified in the config) | 
| drink of pink candy | weirdly pink green, I wonder what happens when you drink it |
| drink of purple candy | gives the player a boosted purple candy effect (the boost can be specified in the config) |
| drink of rainbow candy | gives the player a boosted rainbow candy effect (the boost can be specified in the config) |
| drink of red candy | gives the player a boosted red candy effect (the boost can be specified in the config) |
| drink of yellow candy | gives the player a boosted yellow candy effect (the boost can be specified in the config) |
| drink of candy | gives the player a random combination of candy effects |
| drink of cum | tantrum spawns beneath the player |
| cola / scp207 | gives the player a cola |
| anticola / scp207? / antiscp207 | gives the player an anti cola |
| drink of scp173 | gives the player a massive movement boost |
| drink of chorus fruit | teleports the player to a random room |
| scp drink | disguises the player as a random scp |
| drink of scp106 | teleports the player to the pocket dimension |


