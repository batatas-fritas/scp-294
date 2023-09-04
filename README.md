# SCP-294 The Vending Machine <a name="introduction"></a>
### SCP: Secret Laboratory plugin made with [EXILED](https://github.com/Exiled-Team/EXILED) and [MER](https://github.com/Michal78900/MapEditorReborn)
This plugin adds SCP-294 to the game. SCP-294 is a vending machine that will give you a drink based on the prompt you give it.
You will also be able to create and add your drinks to this repository! Learn how in this [section](#help) of the README.
## Table of contents
1. [Introduction](#introduction)
2. [Prompts and results](#prompts)
3. [How can you help?](#help)
4. [Setup your coding environment](#coding)
## This is a list with every prompt and its respective drink <a name="prompts"></a>
| Prompt   |  Result drink  |
| :------: | :------------: |
| drink of blue candy | gives the player a boosted blue candy effect (the boost can be specified in the config) |
| drink of green candy | gives the player a boosted green candy effect (the boost can be specified in the config) | 
| drink of pink candy | weirdly pink green, I wonder what happens when you drink it |
| drink of purple candy | gives the player a boosted purple candy effect (the boost can be specified in the config) |
| drink of rainbow candy | gives the player a boosted rainbow candy effect (the boost can be specified in the config) |
| drink of red candy | gives the player a boosted red candy effect (the boost can be specified in the config) |
| drink of yellow candy | gives the player a boosted yellow candy effect (the boost can be specified in the config) |
| drink of candy | gives the player a random combination of candy effects |
| drink of cum | tantrum spawns beneath the player |
## How can you help and write your own drink? <a name="help"></a>
1. You need to setup your coding environment. Here's the [guide](#coding) on how to do that.
2. Create a new branch with the name related to what you will be developing.
3. Code your drink. You can use see other drinks' code as reference.
4. Test your drink in your own server. You will have your own if you have read the [coding environment guide](#coding).
5. Create a pull-request so we can look at your drink and determine if it does not have any errors. 
We will also be testing it and will notify you if we encounter any errors. But please **test your drink**.
6. If everything goes ok we will merge your drink into the main repository!
## Environment Setup <a name="coding"></a>
1. Install SCP: Secret Laboratory Dedicated Server on steam. This will be your private server for you to test.
2. You will need to install [EXILED](https://github.com/Exiled-Team/EXILED) on your server and [MER](https://github.com/Michal78900/MapEditorReborn) which is an EXILED plugin.
Follow their instructions on how to install them.
3. Install Visual Studio (Any other IDE is fine as long as you have all the references however we recommend Visual Studio)
4. Create and clone a new branch in the repository.
5. Open up the solution.
6. Every reference from Exiled can be found in [NuGet Exiled](https://www.nuget.org/packages/EXILED) and everything else is in [here](https://exiled.host/build_deps/Dev.zip).
7. Add the missing references and start coding.
8. To test you will have to build your project, get the resulting dll and place it in the EXILED plugins folder, load your server, join and test!

### That is everything. We recommend you join [EXILED'S discord server](https://discord.gg/PyUkWTg) if you have any question about developing a plugin.

