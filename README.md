# WarpTag

This game prototype is for Trijam #152, which involves creating a game prototype in 3 hours.

The final product will be listed here << link to itch.io >>.

A video of the prototype will go here << link to video >>.

## Design

### Basic Scene Description

#### Start Scene

In this scene, we see the game title, Warp Tag, preferably in a pixelated text. There is text on the screen that says press any key to play.

A script will use [Input.anyKeyDown](https://docs.unity3d.com/ScriptReference/Input.html), and if this is true, the Play Scene will load.

#### Play Scene

In this scene, the player will actually play the game. This is the most logically complex aspect of the game, but the premise is that the younger brother to a wizard is being annoying. The player will operate a character and tag the wizard who will be visibly annoyed. When tagged by the player, the wizard will warp a distance away from the player. The player then has a limited amount of time to tag the wizard. As wizard's patience wanes, warps further from the player. The player gains a small amount of time after tagging the wizard, but if time runs out, it's game over.

##### Warping

A base radius will be used as the intial warping distance. Afterward, the radius will increase logarithmically. A random factor ranging between 0.9 and 1.1 will be applied to this distance. Both the initial radius and logarithmic base are configurable. Extra care when warping needs to be taken with the play area's boundaries. In order to handle this, ten warp coordinates will be generated at a time, and only those that are in bounds will be used for the random warping.

To determine the radius, the following equation will be used r = d * f * log<sub>b</sub>(b + n)
  - r is the radius for the warping distance.
  - d is the base warping distance.
  - b is the logarithmic base
  - n is the number of tags
  - f is the random element, and is defined to be a random value between 0.9 and 1.1.

A maximum warping distance / cap will also exist, but this is configurable as well.

To create potential warping points, a simple loop will be used. An exposed variable will determine the number of potential warp points.

```
int numWarpPoints = 10;
int degreeIncrement = 360 / numWarpPoints;
for (int i = 0; i < numWarpPoints; i++){
  int degreesAroundCenter = i * degreeIncrement;
  Vector2 newWarpPoint = calculatePosition(currentRadius, degreesAroundCenter);
  addPositionToList(newWarpPoint);
}
```

To calculate potential warp points, a simple circle coordinate metod will be used.

```
Vector2 calculateNewPosition(float distance, float degreesAroundCenter){
  float radiansAroundCenter = degreesAroundCenter * Mathf.Deg2Rad;
  float xCoordinate = Mathf.Cos(radiansAroundCenter);
  float yCoordinate = Mathf.Sin(radiansAroundCenter);
  return new Vector2(xCoordinate, yCoordinate);
}

Vector2 calculateNewCharacterPosition(Vector2 basePosition){
  Vector2 newWarpPoint = new Vector2(basePosition.x, basePosition.y);
  newWarpPoint.x += transform.Position.x;
  newWarpPoint.y += transform.Position.y;
}
```

All points in this list will be compared to the boundary limits. All points that are not greater than maximum bounds or less than minimum bounds will be added to a new list. From these, a random point will be selected. These boundary points will be stored on the room.

##### Item drops

There will be a 10% chance of an item appearing after each tag:
  - Feather increases the player speed by 1.1
  - Clock increases the players time by 1 second

Item drops will be instantiated from prefabs when the player tags the wizard. These prefrabs will have colliders, and when a player interacts collides with them, they will affect the players stats. These prefabs will be added to the game at random coordinates within the boundary using [Instantiate](https://docs.unity3d.com/Manual/InstantiatingPrefabs.html).


##### Player Timer

While the player is chasing the wizard, he will have a timer/cooldown bar that will show the amount of time remaining. The maximum time cannot be exceeded, and the base time is exposed and configurable. The bar will be a slider component and will follow the design in this [video](https://www.youtube.com/watch?v=BLfNP4Sc_iA). The time can increase in two scenarios:
  - Player tags wizard results in +0.5s
  - Player gets clock icon results in +1s

Time increases are configurable and exposed in the Unity editor.

The timer is controlled by a script with similar code to [here](https://answers.unity.com/questions/225213/c-countdown-timer.html):
```
baseTime = 5f;
remainingTime = baseTime;
void Update(){
  updatePlayerTimer();
}

void updatePlayerTimer(){
  remainingTime -= Time.deltaTime;
  Slider.value = remainingTime / baseTime; //for the fill's slider component
  if (remainingTime <= 0){
    StartCoroutine(goToGameover());
  }
}
```

##### Player Controls

Player movement will be handled by Input.GetAxisRaw as described in [this tutorial](https://stuartspixelgames.com/2018/06/24/simple-2d-top-down-movement-unity-c/). This will allow vertical, horizontal, and diagonal movement. There will be some slight adjustments to account for player speed bonuses. The only controls in the game will be WASD or Arrow Keys.

##### Player Scoring
Scoring will be handled based on the how long the player survives the game. Additional points are awards for the following: 
  - Tagging the wizard, +100 points
  - Picking up bonus, +50 points
  - Total time is tracked using Time.deltaTime, and per second, 10 points is awared.

The score is calculated in a separated game object and is tagged with [Object.DontDestroyOnLoad](https://docs.unity3d.com/2017.3/Documentation/ScriptReference/Object.DontDestroyOnLoad.html). On the Gameover Scene, the score data is accessed and calculated from the raw data. The score is composed of it's raw components until then:
  - int numOfWizardTags
  - int numOfBonuses
  - float totalTime

Score = 100 * numOfWizardTags + numOfBonues * 50 + (int)(totalTime % 60f) * 10

#### Gameover Scene

A game over title is displayed with the same title text font from the Start Scene. Underneath a message saying 'You scored: XXXX' displays based on the raw score components from the Play Scene.

A script will use [Input.anyKeyDown](https://docs.unity3d.com/ScriptReference/Input.html), and if this is true, the Start Scene will load.

### Basic User Flow

#### Start Scene

Any key pressed or mouse down goes to the Play Scene.

#### Play Scene

Player used WASD or Arrow Key controls. If the player runs out of time, they will go to the Gameover Scene. To not run out of time, the player much tag or collide with the wizard.

#### Gameover Scene

Any key pressed or mouse down goes to the Start Scene.

## Implementation

### Start Scene
  - MusicManager (if time)
  - ControlManager
  - SceneManager
  - SceneUI
    - TitleText
    - InstructionMessage

### Play Scene
  - MusicManager (if time)
  - PlayerUI
    - TimerBar
      - TimerFill
      - TimerOutline
      - TimerSprite
  - PlayerManger
    - PlayerController
    - PlayerObject
      - PlayerSprite
      - PlayerAnimation (if time)
  - WizardManger
    - WizardObject
      - WizardSprite
      - WizardAnimation (if time)
    - WizardWarpBehaviour 
  - ScoreManager
  - RoomObject
    - Walls
      - NorthWall
      - SouthWall
      - WestWall
      - EastWall
    - Floor
    - BoundaryBehaviour
  - ClockObject
    - ClockCollisionBehaviour
    - ClockArt
    - ClockAnimation (if time)
  - FeatherObject
    - FeatherCollisionBehaviour
    - FeatherArt
    - FeatherAnimation (if time)

## Artwork
