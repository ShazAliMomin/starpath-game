INCLUDE globals.ink

{norhelp == true: -> help}
{slimek == false: ->buzz}
{slimek == true: ->thanks}

===buzz===
Buzz off kid. I'm trying to get rid of these damn slimes #speaker:Norman #portrait:norman
->END

===thanks===

Thank you so much. I've been trying to get those things off my property for weeks. #speaker:Norman #portrait:norman
Now what can I do for you?

*[Interesting outfit. Do you work for SkyCorp?]

Everyone asks me that damn question.#speaker:Norman #portrait:norman

Regretably, I did work for SkyCorp, but those assholes fired me for being a free thinker.

So now I'm stuck in this isolated house with a lifetime supply of their shitty uniforms

**[You sound like you want revenge. Will you help me get back at them?]

I'd love more than to stick it to them. What do you have in mind?#speaker:Norman #portrait:norman

***[I'm breaking into their base to steal some fuel. You worked there?]

Hell yeah! I can sync up with your transmitter and guide you through that place. I know exactly where to find what you need. #speaker:Norman #portrait:norman

****[Sounds great! Thanks for your help!]

Anything to put Simeon Sky in his place. #speaker:Norman #portrait:norman

~norhelp = true
->END

**[You need to let that hurt go my guy.]

Oh I don't need this from you. Buzz off!#speaker:Norman #portrait:norman



->END
===help===
I'll be ready for you when you enter that base.#speaker:Norman #portrait:norman

->END
