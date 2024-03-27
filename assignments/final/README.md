# Final Project Design Document

## Gameplay Description & Core Mechanic

The main gameplay wil be a classic JRPG-style turn-based combat system. However, every attack, spell, action, etc. will have a required series of inputs. When an attack is selected, a UI element at the top of the screen will have nodes move right to left towards a target. When the nodes are above the target, a specific input will need to be pressed. The nodes will be have like a 1-track rhythm game a la Guitar Hero or Rock Band. Instead of various tracks, the nodes will have different key presses associated with them. The math & numbers of the JRPG will be fixed, and little to no RNG will be used. Said numbers will hover in the single and low double-digit numbers. Attack and Defense stats will affect each other directly (think of Paper Mario), making calculations of damage consistent *if* the Player can do the input track correctly. Inititive will be the only randomized aspect of the combat system. Inititive for the current and next turns will be displayed (like Octopath Traveller), with each character/enemy in combat having one action per turn.

## Inputs

With respect to the combat system, I think either mouse controls for the combat menu or WASD will work. The input track will work on either WASD or ASDF, as they will be the easiest for a single hand to control. If/when I create a space for the player to run around in, I will map the movement controls to WASD. In all liklihood, WASD can also be mapped to the arrow keys without loss of funcitonality.

## Visual Style

Haven't decided, but I suspect I'll just stick to a "traditional" sword and sorcery style. Something that is akin to a classic Final Fantasy or Dungeons and Dragons aesthetic. Characters should be distinguishable, if not by their personality then at least their combat role and ability. The game will take place in a 3D environment, though the characters may be 2D art (a al Octopath Traveller). If the 3D environment becomes to complicated, then a 2D battle environment could work as well. For the input track, the target and required inputs should be visually distinct from both the track and each other. If there are bars used for keeping time, they should be mostly transparent and unobtrusive.

## Audio Style

A large portion of the audio will be combat sounds: swords slashing, sounds of getting hit, enemies dying, etc. There won't be voice acting. Combat music will likely end up being free/royalty free music found off of the internet. It should be upbeat but not otherwise distracting to the player. The input track should have sound effects anytime an input is taken correctly (and a "wrong" sound for incorrect inputs). There may also be a background base to give a sense of timing to the player (Crypt of the Necrodancer and Hi-Fi Rush style). While it would be incredible to make the inputs synchronize to the music, it would take more effort than I have time for, so I don't think I'll do that.

## Interface "Sketches"

It's a little difficult to describe the interface in text alone, but I'll try. First off, the vast majority of the screen will be of the combat environment. The Player's units will be on the right and the enemies will be on the left. The enemies might have floating HP bars directly above them or they might not depending on how difficult it is to make them. The input track will be at the top of the screen. It will be transparent over the combat environment (though it might move to the bottom). Most of the player's inputs and action menu will be at the bottom of the screen in an opaque box. Again, it will greatly resemble a classic Final Fantasy with respect to how it functions. HP bars and numbers will be on the right while the command menu will be on the left. This bottom section should take up no more than 30% of the screen in order to avoid obstructing the battle environment.

## "Story" and Theme

I doubt that this game will have a deep story at all. However, the basic plot will involve exploring a mountain, taking out monsters and collecting McGuffins to proceed onto a final boss of sorts. What exactly those McGuffins are is yet to be determined. The enemies will be a mix of classic JRPG monsters (orcs, slimes, etc) and real-world animals (bears, wolves, mountain goats, etc) that could inhabit the mountain. The final boss is going to be a giant bird of some kind. The reason for fighting all of these things can be nebulous at best. Perhaps some concept of finding treasure or defeating a terror/corruption on the mountain that threatens the area. It could be noble or neutral, but it won't be for evil.

## Targets of Completion

The bare minimum required to get this game working will be the JRPG combat system and the input track at the top of the screen. In all liklihood, the input track will only have a single set of inputs for each type (attack, magic, etc) of action the Player can do in combat. <br>
The next goal is to expand the JRPG combat system to include what I'm calling "backline" characters. That is, characters that are not in the active Party but are still able to do various actions during combat (healing or weaker attacks/statuses). I will also want to create different input sets for every different action a player can do. I.e. each spell has a different set of inputs, every attack by each character has a different set of inputs. <br>
If things go well, I would want to create a type of "overworld" for the Player to run around in. I think it might end up being something along the lines of an RPG Maker grid-based overworld system. Thus, the player will have some control over what battles they get into and in what order.

## Rough Timeline

*Subject to change based on how smooth development proceeds*
- Week 1: April 3rd
    - Prototype input track system created. This includes the UI elements and the ability to generate different tracks. Inputs include the full array of WASD/ASDF inputs, and the target circle works as expected. *Art is not final.*
    - Combat environment is blocked out, literally with simple blocks the rough size of the characters and enemies. *Art is not final*. 
    - Blocked out HUD is created. The spacing of all UI elements is decided upon. *Art is not final*.
- Week 2: April 10th
    - Combat system is realized for varying enemy groups and 4 characters for the player to control. If it can be figured out, also include the backline characters and variations of Party size.
    - Includes things like attack names, "classes", spells, etc etc. Animations probably won't be created, and *art is not final*.
- Week 3: April 17th **Core Playtest Milestone**
    - Connect the systems from Weeks 1 & 2 into a playable experience.
    - If possible, have at least 3 fights prepared and create a very simple way to transition between them all (UI element: continue?).
    - If things have been going very smoothly so far, have a basic overworld prepared, including basic movement too.
- Week 4: April 24th
    - Most of the mechanics should be complete at this point. One of the targets for completion is decided upon and worked towards.
    - Start looking for soundFX, music, and art assets for Party & enemies.
    - All UI elements should be made by me. If time allows, some enemy or Party members can also be created by me, but this is less likely given the required work involved.
- Weeks 5 & 6 (Up to **Final Submission**)
    - Polish the heck out of everything.
    - Finalize art assets.
    - Prepare submission.

