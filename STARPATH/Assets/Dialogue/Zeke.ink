INCLUDE globals.ink

{scrap==true:->sfound}

{freyainit == false:->start}

{zstory ==false:->story}

{jannatalk == true:->questionsj |->questions}
~start1 = true


===start===


I was able to get the ship operable, but we are in dire need of fuel. #speaker:zeke #portrait:zeke

Maybe go check around the city and see if there is someone who can help us?

*[I'm on it]
->END
    
===story===

{zstory == true:-> questions}
    
Were you able to find some fuel? #speaker:zeke #portrait:zeke

Yep. We're gonna have to steal it though #speaker:Jett #portrait:mc

Steal it from whom?!? #speaker:zeke #portrait:zeke

There's this huge coorporation here that is hoarding all the planet's valuable resources. #speaker:Jett #portrait:mc

The fuel is kept deep in their base. 

Nothing sparks my circuits more than getting even with an evil coorporation! #speaker: Zeke #portrait:zeke

Let's go take down those bastards!

Who will watch the ship though? #speaker: Jett #portrait:mc

Ah yes, you're right. I can set up some defense beacons if you find me 6 Scrap items. #speaker: Zeke #portrait:zeke

Alright cool, I'll check around town for those. #speaker: Jett #portrait:mc

You should also check and see if there's anyone else who can help us infiltrate that base  #speaker: Zeke #portrai



~zstory = true

===questions===
{scrap == true:-> sfound}
Have you found anyone else who can help us get the fuel?#speaker:zeke #portrait:zeke

*[There's this shady woman named Freya who can get me some powerful guns]

That will be a huge help!#speaker:zeke #portrait:zeke
->questions
*[Gotta Blast!]


->END

===questionsj===
{scrap == true:-> sfound}
Have you found anyone else who can help us get the fuel?#speaker:zeke #portrait:zeke

*[I found a pilot who can help. She needs you to repair her ship though]

Once you get me those scraps, I'll be able to leave our ship and help fix hers#speaker:zeke #portrait:zeke

->questionsj
*[There's this shady woman named Freya who can get me some powerful guns]

That will be a huge help!#speaker:zeke #portrait:zeke
->questionsj
*[Gotta Blast!]

->END



===sfound===
I have the scraps we need! #speaker:Jett #portrait:mc

Delightful! I'll set up the beacons and hop in your suit's inventory.#speaker:zeke #portrait:zeke
I'll be a useful turret in a fight!

~zekeack = true
->END






