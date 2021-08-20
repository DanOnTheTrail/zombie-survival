# zombie-survival
An implementation of https://github.com/ardalis/kata-catalog/blob/main/katas/Zombie%20Survivors.md


# Instructions #

This kata constructs a model for a zombie boardgame's survivors. If you enjoy the kata, you may find the Zombicide series of boardgames fun as well. Complete each step before reading ahead to the next one. Revise your design to react to new requirements as they appear.

## Step One: Survivors

The zombie apocalypse has occurred. You must model a Survivor in this harsh world. Sometimes, they get hurt, and even die.

- Each **Survivor** has a **Name**.
- Each Survivor begins with 0 **Wounds**.
- A Survivor who receives 2 Wounds dies immediately; additional Wounds are ignored.
- Each Survivor starts with the ability to perform 3 Actions per turn.

## Step Two : Equipment

Survivors can use equipment to help them in their mission.

- Each Survivor can carry up to 5 pieces of **Equipment**. 
	- Up to 2 pieces of carried Equipment are "In Hand"; the rest are "In Reserve".
	- Examples of Equipment: "Baseball bat", "Frying pan", "Katana", "Pistol", "Bottled Water", "Molotov"
- Each Wound a Survivor receives reduces the number of pieces of Equipment they can carry by 1.
	- If the Survivor has more Equipment than their new capacity, choose a piece to discard (implement however you like).

## Step Three : The Game

A Game includes one or more Survivors, as well as other Game elements that are outside the scope of this kata.

- A **Game** begins with 0 Survivors.
- A Game can have Survivors added to it at any time.
	- Survivor Names within a Game must be unique.
- A Game ends immediately if all of its Survivors have died.

## Step Four : Experience and Levels

As Survivors overcome zombies, they gain experience.

- Each Survivor begins with 0 **Experience**.
- Each Survivor has a current **Level**.
- Each Survivor begins at Level Blue.
- Each time the Survivor kills a zombie, they can 1 Experience.
- Levels consist of (in order): Blue, Yellow, Orange, Red.
	- When a Survivor exceeds 6 Experience, they advance ("level up") to level Yellow.
	- When a Survivor exceeds 18 Experience, they advance to level Orange.
	- When a Survivor exceeds 42 Experience, they advance to level Red.
- A Game has a Level (Level here is identical to Level for a Survivor).
- A Game begins at Level Blue.
- A Game Level is always equal to the level of the highest living Survivor's Level.

## Step Five : Output

The Game includes a running history of events that have taken place as it has been played. Managing game history is a Game responsibility.

- [x] A Game's **History** begins by recording the time the Game began.
- [x] A Game's History notes that a Survivor has been added to the Game.
- [x] A Game's History notes that a Survivor acquires a piece of Equipment.
- [x] A Game's History notes that a Survivor is wounded.
- [x] A Game's History notes that a Survivor dies.
- [x] A Game's History notes that a Survivor levels up.
- [x] A Game's History notes that the Game Level changes.
- [x] A Game's History notes that the Game has ended when the last Survivor dies.