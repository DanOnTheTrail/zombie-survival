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