# AstroPunk

A text-based, sandbox adventure that takes place in a sci-fi universe. Explore a procedurally galaxy with diverse worlds and alien life forms. Craft your base. Manage your crew. Make allies. Start wars. The galaxy is at your fingertips.

This game is currently in early development. At the current state of production I'm developing the core systems needed to make the game function and gradually adding entities into the game as they are needed. Once systems are in place, the game will allow for quick iteration based on the use of dictionaries in entity data bases.

# Current State of Gameplay Systems

**Basic Commands**
Basic text-based commands have been implemented, such as "go north", "attack x", "examine y", "get z". This will be further expanded to a [verb] [target] [modifier] system to improve gameplay. Such as "gather carbon 5" will gather 5 carbon in the current chunk rather than typing "gather carbon" 5 times. I also plan on adding "to x" functionality for improved player agency. For example if the player has two refiners, "add carbon 3 to crude refiner" will add 3 carbon to the first crude refiner on the entity list. Currently, "add carbon 3" adds carbon to the most advanced refiner in the area.

[b]World Generation[/b]
The current state of the world generation is very basic as there is only one biome and planet that randomly spawns entities based on probability. The system does feature a chunk based system where the player moves NSWE to different chunk coordinates. Future world generation will incorporate perlin noise to make the worlds more interesting with multiple biomes. Think Minecraft in a text-based game.

[b]Planet Generation[/b]
This will be implemented in the future. Planets will be procedurally generated based on mass and distance from the sun. This system will allow for further biome generation. For example, a large planet would have stronger gravity and create a very hot environment impacted by runaway greenhouse effect, while a small planet might have too little gravity to have an atmosphere, providing a very cold and oxygenless environment.

[b]Combat[/b]
The current combat system is simple with "attack x" command implemented and retaliation from the NPC. RPG-like stats are calculated to add modifiers to d12 dice rolls to determine damage, plus subtraction of armor that the player and NPC have. Future developments will incorporate dodging, fleeing, item usage(such as explosives), etc. The plan is to make this feel strategic and as if the player is playing a text-based TTRPG.

[b]Crafting[/b]
Basic crafting system is in place which uses a crafting recipe dictionary and gets required equipment and materials to craft that item. Further implentation will need to implement fuel usage in refiners, storying items in containers, and regathering placed furniture items (such as work bench or refiner).

[b]AI/NPCs[/b]
This has barely been touched. Currently, a temporary NPC exists for combat purposes. Futurue implementation will allow for procedurally generated life forms that will include dynamic dialogue, personalities, and have an impact on the world. This will likely be a complex project as I wish to make the gameplay feel alive and incredibly dynamic where player choices have an impact on the world.

[b]Layered Universe[/b]
Future implementations will the world and player position to work in layers such as Galaxy, Planet, Structure. These will all have an impact on commands within the game and the procedural generation. The galaxy layer will be something similar to Eve Online where there will be warp gates generated in each solar system. Passing through a warp gate will move the player to a new solar system. The player will also be able to land on a chosen planet which will use a perlin noise chunk generation system. This is partially implemented. Then upon entering structures, let's say a shelter, the system will become a room based system, whereas a room might have a door going north leading to another room.

The ultimate goal is to create a heavily procedurally generated sci-fi simulation and provide the player all the tools necessary for complete atonomy. The reason for choosing a text-based game is to achieve exactly that, for quick implementation of systems and items that can be more flexible than graphical games. Also for infinite possibilities of procedural generation, especially when creating alien life forms, since this is impossible in a graphical engine. With a text-based system, several characteristics can be predefined into multiple lists and the algorithm can randomly select characteristics to procedurally generate a species, planet, etc.
