# AmongUsUtility
This program involves reverse engineering and memory hooking a popular game, [Among Us](https://store.steampowered.com/app/945360/Among_Us/), to automatically track stats. This way, I am able to create a leaderboard to have some friendly competition.

### Account Registration
Due to the game not having any ID system, users must register through a username that they use in game. One a user is registered, the database adds an entry for them.

![](https://github.com/tkocher62/AmongUsUtility/blob/master/register.png)

Through reverse engineering and memory management via an internally injected library into the game, the assembly is able to detect when the game ends and who won. I pass this information to a bot over TCP networking and the bot increments the player's win count in the database.

![](https://github.com/tkocher62/AmongUsUtility/blob/master/database.png)

Finally, the player can check their stats at any time.

![](https://github.com/tkocher62/AmongUsUtility/blob/master/statslist.png)
