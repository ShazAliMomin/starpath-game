INCLUDE globals.ink



{dockdone == true: -> thanks}
{dockmish == true: ->jfin}
{tommymet == true : ->job}
{skycred == false : ->getout | ->areyou}

===getout===

Get out of my face #speaker:Tommy #portrait:tommy 
->END

===areyou===
What do you want? #speaker:??? #portrait:tommy
*[Are you Tommy?]
I am, you must be the guy Freya sent. #speaker:Tommy #portrait:tommy
~tommymet = true
->job

===job===
Are you ready to this job? #speaker:Tommy #portrait:tommy 
**[Let's do it!]

Ight cool. I'm gonna take you to this SkyCorp container ship.
Word around town is one of the containers has some laser accelerators.
Those things can make any blaster 10 times as lethal.
You need to get in, find those accelerators, and get out!
~dockstart = true

-> END

**[Sorry, not quite.]
Stop wasting my time man.

->END

===jfin===

Can't believe you made it out of there alive. #speaker:Tommy #portrait:tommy 
Freya told me to let you keep one of the accelerators if you were able to steal them.
*[Wow! Awesome! Thank you guys!] ->jfind

*[I think I deserve a bit more]

How about you walk away before I knock your ass out
->jfind

===jfind===
~dockdone = true
->END

===thanks===

Thanks for the help man.  #speaker:Tommy #portrait:tommy
*[Thanks for letting me keep one of the accelerators!]
No sweat. Don't go getting any in trouble now  
->thanks
*[Do you have any scraps I can have?]
Sorry man, fresh outta scraps
->thanks
*[Gotta Blast!]
->END
