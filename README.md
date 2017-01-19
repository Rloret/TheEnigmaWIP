# TheEnigmaWIP
This is a project made by four people as a prototype for an artificial Intelligence class.
In this pproject i took care of the steering behaviours, Fog of war and flocking.
Also, Made the communication system which is still under trial.

	The game is placed in a closed room complex inside of which there are a group of humans, and an another world creature too… this creature is able to adopt human appearance.

This way, two teams are created, Humans and Creatures, each one with different goals. As creature, the goal is to isolate each human in order to infect him and increase the number of creatures. It is important to isolate humans, because the creature is superior in single combat, but if there are two or more humans together the creature will be at a disadvantage. The creatures’ final goal is the total annihilation of the human team.

The other team, the humans, must attempt to create groups as soon as possible and collect information from the environment and the teammates. This information should be used in order to find all the hidden creatures and kill them. During the game, a human could be infected, so his goal will change to the creatures’ goal.

About gameplay, is a 2D top-down game with point’n click mechanic to interact and move around. The scene is covered by a fog of war, so that is not possible to see beyond the character’s visibility cone. In another way, the scene parts already traversed will be visible.

When a game agent visualizes other, either AI or Player, exists the possibility of initiate a dialogue with several options (to offer an object, to ask him to join our group or to attack). The fact that AI will attempt to initiate a dialogue and the action it will choose depending on the personality of the AI and the confidence level that it has with the receiver.

Throughout the scene there are objects which agents can pick up in order to get a power-up:
Boots: Increase movement speed
Axe: Increase damage
Flashlight: Increase the vision range
Bulletproof vest: Increase defense

	Each agent can only carry one object and the power-up will be active will the object is carried. In addition, the are a first-aid kit in a fixed place of the scene where the agents can recover health by getting close. However, the position of each object will be randomly selected in each game. 
