© 2021. This work is licensed under CC4.0 	https://creativecommons.org/licenses/by/4.0/
Credit to Blair Ceradsky (Twitter: @MookaTheCaveman), please clearly state that the work was edited if you alter the art.

THANK YOU FOR YOUR PURCHASE!







SUGGESTED COLLISION BOX:
Included in this folder is a mock-up image of what the player's hitbox proportions might be. Keeping at-least
the leftmost edge faithful to this example is recommended for the wall-slide animation 
(otherwise your player might not visually connect with wall-tiles correctly).


SUGGESTED GAME-OBJECT CENTER:
Also included in this folder is a suggestion image for where the center of the game-object should be. Using this 
should help ensure that the turn animation/game-object flipping doesn't awkwardly snap the player too far 
in one direction when turning.







FX RULES:
You'll find many "OVERLAY FX" animations in the folder "2 Hiro's Attacks And Spells".
Since the effects had to have larger borders than the player sprites; their proportions do not match up with the 
corresponding player animations (which they are meant to be played simultaneously on-top of). So to help you 
know exactly where to position them in relationship to the player; all "overlay FX" animations have
the first frame of player's idle animation in them. You should Use this frame to as reference to find the
correct position for the FX, but should NOT include it in the FX animation itself.


If an FX animation has an empty frame after the idle image; it's apart of the animation and should be played 
(It's just there to help keep proper timing with the associated player animation).


Some attacks have "contact FX" included with them. Play these on-top of enemies when they are struck by those
attacks. Not all attacks have their own contacts, but you can easily re-use some from other attacks as desired.







FPS GUIDES, ETC.:
The sub-folders also contain more text files like this to instruct you on exact FPS values for each animation, 
as well as other tips that might aid in design and sprite implementation.


All animations that are meant to be looped have [LOOP] written at the end of their filename. There are also some single-frame
""animations"" that are meant to be held for a moment; these have [HOLD] written at the end of their filename. 


The player has alternate air versions of some animations (so that his feet don't awkwardly look like they are planted
on the ground when he is performing actions in the air!). These animations have [AIR] written first on their filenames.


If the animation states should play in a set order in-game, they will often have number-ordered filenames.


SPAGHETTI STATE-MACHINE WARNING: there are many player actions and transitional animations included in this pack and 
therefore many possible conflicting states. I'm sorry if things get confusing. Just try to keep to clean practices.







Enjoy :)