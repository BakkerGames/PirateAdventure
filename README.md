# PirateAdventure - 12/04/2018

Back in December 1980, BYTE magazine printed the complete TRS-80 source code to a Scott Adams text adventure game called
"Pirate's Adventure".

For years I've played around with the code, making notes, trying it out in various ways. In 1984 I translated it into
Timex Sinclair 1000 Basic and got it completely working! Unfortunately that old code was lost long ago, but it was pretty cool.

This is another attempt to get the entire game working. It is a C# port using the exact same data and very similar code from that
original magazine article. This time I retyped the entire source code into text documents (OCR couldn't handle it, but it wasn't
that long). I'm pasting the text code in as comments as I work, to make sure I get it all right.

The only visible changes I'm making are some line wrapping differences in multiline text, and one or two tiny typos I just *had*
to fix. I still can't call them "SNEAKES", sorry.

There could also be some data issues in that block of numbers. I remember having to change some of them to make the game work.
This time I'll start with the exact data given, but also make notes if there are problems. Maybe have two versions playable,
original and corrected.

If anyone wants to take the typed text and run it through, say, an old TRS-80 emulator, go for it! Let me know the results.

For debugging, I have added a "/test" option which will print the entire data block as readable text. It shows the VERB NOUN
or percent chance of running. The VERB NOUN handles when the adventurer types a command, the percent chance is for the background
processes. This listing was critical to making sure that the data block processing was correct within the game.