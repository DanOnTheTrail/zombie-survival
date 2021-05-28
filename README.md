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