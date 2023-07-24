INCLUDE globals.ink


{freyainit== false: ->first}
{freyacomp== false: ->second}
{dockdone==true: -> dockdone2}
{freyacomp== true: ->comeback}


==first===
I'm tired of your shit Freya! Where's my money?!#speaker: Maurice #portrait:maurice 

Just the guy I wanted to see! My good friend robocop here has your money #speaker:Freya #portrait:freya

Wait what?? #speaker:Jett #portrait:mc
    
This little twerp has my money???#speaker: Maurice #portrait:maurice 

Bro I have no idea who any of you are #speaker:Jett #portrait:mc

Yep! He was a member of my crew, but I caught him stealing from the pot.#speaker:Freya #portrait:freya

The deviant made off with the money I was going to give you 

Why you gotta lie though??#speaker:Jett #portrait:mc

Oh your ass is mine now robocop!#speaker: Maurice #portrait:maurice

Why is no one listening to me???? #speaker:Jett #portrait:mc

Get his ass Maurice! #speaker:Freya #portrait:freya
~freyainit = true

->END



===second===

Wow thanks kid. Maurice has been up my ass for weeks about that money#speaker:Freya #portrait:freya

*[WHAT THE HELL WAS THAT?]

I've been able to avoid his crew for weeks, but he finally caught me. 

I don't know what I would've done without my hero <3

**[YOU WERE JUST TELLING HIM TO KILL ME.]

He would have done unspeakable things to me if I didn't come up with a diversion

I knew all along that a strong guy like you could handle yourself

****[Oh... uhh. You think I'm strong, huh??]

Yeaaaaahhhh. Sure. 

You looking to make some cash? I need all the help I can get after this SkyCorp Takeover

*****[SkyCorp? Takeover? Huh?]

->questions

****[Maybe you should stop doing deals with guys like him.]
->preques2

*[Now that I've fought your battle for you, you owe me]

Ha! Don't get ahead of yourself. I don't owe you a thing.
->questions


->DONE



===preques===
**[Maybe you should stop doing deals with guys like him.]
->preques2
===preques2===
Ever since the SkyCorp takeover, you either do deals with guys like him, or you starve.
***[SkyCorp? Taken Over? Huh?]
->questions






==questions===
You're clearly not from around here. Maybe I have some answers for you?#speaker:Freya #portrait:freya
*[What is SkyCorp?]

Oh god, what ISN'T SkyCorp at this point?

Huge tech conglomerate with an imperialist army. 

They set up a base here a little over a year ago, and have been a pain in everyone's ass ever since.

They own literally everything valuable here now. We're all just doing what we can to make it and defend ourselves.

->questions

*[What kind of deals do you do?]
Not that it's it's any of your business... But I'm an arms dealer. 

You got any problems you need to solve? haha
->questions

*[My ship crashed here and I need fuel. Are you able to help?]

Damn kid. SkyCorp has stranglehold over all the fuel here. They keep that shit deep in their base.
->q2
===q2===
**[How can I get in the base?]
You must have a death wish! haha.

***[I'm serious, I'll do anything to make it off this planet]

I tell you what. I can give you a SkyCorp credential card that will get you in the base. 

From there it's gonna be a shootout between you and their endless soldiers.

If you help me with a job at the docks, I can get you some more powerful guns.

You'll probably die, but my blasters will at least give you a fighting chance
->q3

**[Well surely there has to be fuel somewhere else?]

Anyone with access to fuel and a functional ship is long gone from here.
->q2

===q3===

****[What do I need to do?]

Go down to the docks and talk to my guy Tommy. He will fill you in. 
->q4


===q4===

*****[Sounds good. I'll do it]
~freyacomp = true
~skycred = true
->END
*****[What does he look like?]

You're asking too many questions dude. You'll know him when you see him 
->q4




===comeback===

What are you waiting for? Go to the docks and help Tommy with that job!#speaker:Freya #portrait:freya
->END



===dockdone2===
Thanks for the help at the docks! Good luck taking down SkyCorp! #speaker:Freya #portrait:freya
->END







===ending===

->END
