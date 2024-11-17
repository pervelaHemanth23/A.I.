# Enemies

**A repository where I'm going to upload all types of Enemy A.I and Pathing/Pathfinding techniques I learn**

## Enemy Patrol

This this the simplest of A.I. where the enemy will patrol from point **A** to point **B** in a linear space at any speed the player wants

I have added a slider which can be used to adjust the speed of the enemy.

***How does it work***

* Point A ---> PointA.transform
* Point B ---> PointB.transform
* CurrentPoint (OR Target Point)
* Enemy Position ---> transform.position

* Initialise currentPoint as Point B and apply velocity in +ve direction.
* As soon as the distance between transform.position and currentPoint < constant c(e.g. 0.5f), change currentPoint to A and apply velocity in -ve direction.
* Do the same when enemy reaches A.

## Following Enemy

This kind of AI can be used for enemies that follow the player. 
**BONUS**: We an also add such that the enemy follows the player only when within a certain distance.

The enemy also rotates towards the player when following.

***How does it work***

* *Speed*
* Position of the enemy ---> *transform.position*
* Position of the player ---> *player.transform.position*
* Distance ---> *Vector2.Distance(transform.position, player.transform.position)*

* Using *vector2.MoveTowards()* function, we can make the enemy move towards the player at certian Speed
* *if(Distance < constant C){(move)}*

**Rotating the player**

* Direction ---> *player.transform.position - transform.position*
* Angle ---> Tan @

* To get unity vector in the direction of enemy to payer, we use *Direction.Normalize()*
* @ = *Mathf.Atan(direction.y, directon.x)*. We are using Tan function cause we know distance from player to enemy and it can be split into x and y components. Tan of x and y components gives the angle
* Convert Rad to Deg
* Apply this angle to rotation of the enemy -> *transform.rotation = Quarteriion.Euler(Vector3.forward * Angle)*


### Improvements

1. Used basic sprites to distinguish enemies and player
2. Used animations for walking and Idle in Linear Path enemy
3. Custom Backgrounds


## AutoPilot

This A.I. will rotate and move the player towards a specified object. The code is not very flexible and I have added the comment *// Repeated* in the places where the code has been repeated for a different GameObject
I have attempted to use coroutines in Driver.cs Script but there are a few issues with coroutine management and interruptions. Right now, the OldDriver.cs script is working as intended.

https://github.com/pervelaHemanth23/AI/blob/main/VIDEOS/AI.mp4

*Downlaod and extract Enemies.rar and run Enemies.exe*

*I will upload more enemy varieties soon.*

