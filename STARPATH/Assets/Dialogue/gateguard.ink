INCLUDE globals.ink

{skycred == false: ->buzz}
{gateopen == true: ->casual}
{skycred == true: ->credtrue}


===buzz===

Identification Please #speaker: Guard #portrait:gateguard

*[I don't have any Identification]

Get out of here ->END

*[I am undercover SkyCorp Agent. I do not need identification]

Get out of here ->END

===credtrue===

Identification Please #speaker: Guard #portrait:gateguard

*[*ACTION* Show Identification]

I guess this checks out. I'll open the gate sir. 
~gateopen = true

->END

===casual===
What do you want now? #speaker: Guard #portrait:gateguard

*[Nothing, sorry]

->END

*[I just wanted to thank you for your service]

Bootlicker...

->END






