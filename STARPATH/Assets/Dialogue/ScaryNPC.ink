INCLUDE globals.ink

{ts1 == false:  -> start | ->taken}

===start===
Hey, I don't think I've ever seen you around here? #speaker:Citizen #portrait:scared

*[I am an undercover SkyCorp agent. Give me all your valuables now.]

I don't want any trouble man. Here, take my scraps.
~ts1 = true
->END

*[Yep, I'm new to town.]

Cool, have a nice day!
->END 

===taken===
I already gave you everything. Please just leave me alone!#speaker: Citizen #portrait:scared
->END 