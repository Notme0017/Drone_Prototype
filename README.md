# Drone_Prototype
A prototype with drone and enemy with basic behaviour such as patrolling and shooting when a player reaches the detection zone


#Controls

W & S for throttles
A & D for strafe

Q & E for Ascend & Descend

Space for Firing missile

#Features Implemented
- A basic drone prototype to move in all directions based on camera orientatiton and player input
- A missile firing system which fires a directed missile to the targeted direction
- The targeted direciton is controlled via a crosshair
- Orbital camera following the drone while also deciding where the missile fired will go

- A missile which explodes on contacting any surface
- During explosion it looks for any destructible object in a certain radius and destroys it along with too
- If somehow missile doesn't come in contact with anything it will be destroyed without any explosion after a certain time

- Basic enemy prefab who use navmesh and animator to patrol in patrol points
- if a drone enters the detection range it starts attacking it with a fixed fire rate which can be altered in inspector

- A basic explosion particle system which intiates after a missile explodes

#Known Limitations

- The drone and missile prefabs can be more visually attaractive
- The particle system for drone is not completely accurate and needs reforming
- There is no limitations on missile numbers
- The sounds for ship can be edited to make it more reformed
