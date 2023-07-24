INCLUDE globals.ink

{shiprepaired == true: ->shiprep}
{jannatalk == false:->main}
{jannatalk == true:->zekeotw}


-> main

=== main ===
Hey. Cool suit you got there! What's up? #speaker:Janna #portrait:janna
    + {shiprepaired == false}[Is that your ship? What happened to it?]
        -> ship
    + {jannaagree == false}[Can you help me find fuel for my ship?]
        -> fuel
    + [Gotta blast!]
    
    Thanks for the talk!
        ->DONE
        
        

->DONE

===zekeotw===

Please tell me you brought your little robot friend to repair my ship? #speaker:Janna #portrait:janna
    +{zekeack == false} [Sorry, no]
    
        Well go get him!
        ->DONE
        
    
  + {jannaagree == false}[Can you help me find fuel for my ship?]
        -> fuel
        
 + {zekeack && shiprepaired == false} [I got him!]
        Thank you so much!!
        ++[*ACTION* Let Zeke repair Janna's Ship]
        ~shiprepaired = true
         {jannaagree == false:-> main}
        {shiprepaired && jannaagree: ->shiprep}
    
    Thanks for the talk!
        ->DONE
        
        

->DONE



===shiprep===
Hey! I'm ready to help take down SkyCorp. #speaker:Janna #portrait:janna

Just give me the call when you need me.

->END

=== ship === 

Yeah... I overloaded one of the circuits in my port side engine racing some goons. You wouldn't happen to know how to repair it, would ya?#speaker:Janna #portrait:janna
 + [Sorry, that's not really in my plans]
 
 Ah. Well thats too bad... 
       ->main
 + [I have a robot who could have it running in no time!]
 {zekeack ==true:->zekeotw}

Oh gosh! Really?? Can you bring him here? I'd do anything to get this ship back running again.
 ++ [Sike! You thought.... LOL]
  Ah. Well thats too bad... jerk... 
  ->END
 ++ [Sure! Let me go get him!]
 ~jannatalk = true
 ->END
      
->DONE


===fuel===
Unforunately I only got enough fuel for my own ship. I hear they keep stuff like that deep in the SkyCorp tower though.... #speaker:Janna #portrait:janna
    +[I'm going to break in there, will you help me?]
    
{shiprepaired == false: I would do anything to get back at those jerks! Help me get my ship running, and I can provide air support for your break-in.|Absolutely! Give me the call when you're in the base and I can provide air support.}
~jannaagree = true
   
   ++[Sounds like a deal]
   
{jannaagree && shiprepaired: ->END}   
{jannatalk == true: -> zekeotw}   
{shiprepaired != true: -> main}
~jannahelp = true
        

->main
    
    +[I wouldn't dare try to mess with SkyCorp!]
    
    That's very brave of you! /sarcasm
    {jannatalk == true: -> zekeotw}   
{shiprepaired != true: -> main}
    
    
    ->main
    






Thanks for the talk!
-> END