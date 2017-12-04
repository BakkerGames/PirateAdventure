// Program.cs - 12/03/2017

using System;

namespace PirateAdventure
{
    partial class Program
    {

        private static bool gameOver = false;
        private static string TPS_commandLine = "";
        private static bool F_commandNotOK = false;
        private static int R_currRoom;
        private static int LX_lightRemaining;
        private static bool DF_darkFlag;

        private static int[] NV = new int[2]; // NV(1)

        // 41 Z$="I'VE TOO MUCH TOO CARRY. TRY -TAKE INVENTORY-"
        private const string ZS_inventoryFullMsg = "I'VE TOO MUCH TOO CARRY. TRY -TAKE INVENTORY-";

        private static Random sysRand = new Random();

        static void Main(string[] args)
        {
            //TestData();
            RunGame();
        }

        static void RunGame()
        {
            Initialize();
            // 130 GOSUB 50
            ShowIntroduction();
            while (!gameOver)
            {
                // 131 GOSUB 240
                // 132 GOTO 160
                ShowLocation();
                // 160 NV(0)=0
                // 161 GOSUB 360
                // 162 GOTO 140
                RunEngine(0, 0);
                F_commandNotOK = false;
                while (!gameOver && !F_commandNotOK)
                {
                    GetCommand();
                    F_commandNotOK = ParseCommand();
                    // 142 GOSUB 170
                    // 143 IF F THEN PRINT "YOU USE WORD(S) I DON'T KNOW":GOTO 140
                    if (F_commandNotOK)
                    {
                        Console.WriteLine("YOU USE WORD(S) I DON'T KNOW");
                        Console.WriteLine();
                    }
                }
                // 150 GOSUB 360
                RunEngine(NV[0], NV[1]);
                if (!gameOver)
                {
                    // 151 IF IA(9)=-1 THEN 
                    if (IA[9] == -1)
                    {
                        //LX=LX-1
                        LX_lightRemaining--;
                        //:IF LX<0 THEN 
                        if (LX_lightRemaining < 0)
                        {
                            //PRINT "LIGHT HAS RUN OUT"
                            Console.WriteLine("LIGHT HAS RUN OUT");
                            //:IA(9)=0 
                            IA[9] = 0;
                        }
                        //ELSE IF LX<25 THEN 
                        else if (LX_lightRemaining < 25)
                        {
                            //PRINT "LIGHT RUNS OUT IN";LX;"TURNS!"
                            Console.WriteLine($"LIGHT RUNS OUT IN {LX_lightRemaining} TURNS!");
                        }
                    }
                }
            }
        }

        private static void ShowLocation()
        {
            // 240 IF DF THEN IF IA(9)<>-1 AND IA(9)<>R THEN 
            if (DF_darkFlag && (IA[9] != -1) && (IA[9] != R_currRoom))
            {
                //PRINT "I CAN'T SEE, ITS TOO DARK."
                Console.WriteLine("I CAN'T SEE, ITS TOO DARK.");
                //:RETURN
                return;
            }
            // 251 IF LEFT$(RS$(R),1)="*" THEN 
            if (RSS[R_currRoom].StartsWith("*"))
            {
                //PRINT MID$(RS$(R),2); 
                Console.Write(RSS[R_currRoom].Substring(1));
            }
            //ELSE 
            else
            {
                //PRINT "I'M IN A ";RS$(R);
                Console.Write("I'M IN A ");
                Console.Write(RSS[R_currRoom]);
            }
            // 250 K=-1
            bool K_firstItem = true;
            // 260 FOR Z=0 TO IL
            for (int z = 0; z <= IL_itemCount; z++)
            {
                // 261   IF K THEN IF IA(Z)=R THEN PRINT ". VISIBLE ITEMS HERE:":K=0
                if (K_firstItem && IA[z] == R_currRoom)
                {
                    Console.WriteLine(". VISIBLE ITEMS HERE:");
                    K_firstItem = false;
                }
                // 300   IF IA(Z)<>R THEN 320
                if (IA[z] == R_currRoom)
                {
                    string TPS_itemName;
                    // 280   TP$=IA$(Z)
                    TPS_itemName = IAS_itemDescriptions[z];
                    // 281   IF RIGHT$(TP$,1)="/" THEN 282 ELSE 290
                    // 282   FOR W=LEN(TP$)-1 TO 1 STEP -1
                    // 283     IF MID$(TP$,W,1)="/" THEN TP$=LEFT$(TP$,W-1):GOTO 290
                    // 284   NEXT W
                    if (TPS_itemName.EndsWith("/"))
                    {
                        int posNextSlash = TPS_itemName.LastIndexOf("/", TPS_itemName.Length - 2);
                        if (posNextSlash > 0)
                        {
                            TPS_itemName = TPS_itemName.Substring(0, posNextSlash);
                        }
                    }
                    // 290 RETURN
                    // 300   IF IA(Z)<>R THEN 320
                    // 310   PRINT TP$;".  ";
                    Console.WriteLine($"{TPS_itemName}.");
                }
                // 320 NEXT X
            }
            // 321 PRINT
            Console.WriteLine();
            // 330 K=-1
            K_firstItem = false;
            // 331 FOR Z=0 TO 5
            for (int z = 0; z <= 5; z++)
            {
                // 332 IF K THEN IF RM(R,Z)<>0 THEN PRINT "OBVIOUS EXITS: ";:K=0
                if (K_firstItem && RM[R_currRoom, z] != 0)
                {
                    Console.Write("OBVIOUS EXITS: ");
                    K_firstItem = false;
                }
                // 340 IF RM(R,Z)<>0 THEN PRINT NV$(Z+1,1);" ";
                if (RM[R_currRoom, z] != 0)
                {
                    Console.Write(NVS[z + 1, 1]);
                    Console.Write(" ");
                }
                // 350 NEXT Z
            }
            // 351 PRINT:PRINT:RETURN
            Console.WriteLine();
            Console.WriteLine();
            return;
        }

        private static void Initialize()
        {
            // 100 R=AR:LX=LT:DF=0:DIM SF(15)
            R_currRoom = AR_startRoom;
            LX_lightRemaining = LT_lightTotal;
            DF_darkFlag = false;
            for (int i = 0; i <= 15; i++)
            {
                SF_systemFlags[i] = false;
            }
            // 101 REM INPUT "USE OLD 'SAVED' GAME";K$:IF LEFT$(K$,1)<>"Y" THEN 130
            Console.Write("USE OLD 'SAVED' GAME? ");
            string KS = Console.ReadLine().ToUpper();
            if (!KS.StartsWith("Y"))
            {
                return;
            }
            // 110 REM IF D<>-1 THEN CLOSE:OPEN"I",D,SV$ ELSE INPUT "READY SAVED TAPE";K$:PRINT INT(IL*5/60)+1;"MINUTES"
            // 120 REM INPUT #D,SF,LX,DF,R
            // 121 REM FOR X=0 TO IL
            // 122 REM   INPUT #D,IA(X)
            // 123 REM NEXT X
            // 124 REM IF D<>-1 CLOSE
            // todo load saved game
        }

        private static void ShowIntroduction()
        {
            // 50 CLS:PRINT "     ***   WELCOME TO ADVENTURE LAND. (#4.6) ***"
            // 51 PRINT
            // 52 PRINT " UNLESS TOLD DIFFERENTLY YOU MUST FIND *TREASURES*"
            // 53 PRINT "AND-RETURN-THEM-TO-THEIR-PROPER--PLACE!"
            // 60 PRINT
            // 61 PRINT "I'M YOUR PUPPET. GIVE ME ENGLISH COMMANDS THAT"
            // 70 PRINT "CONSIST OF A NOUN AND VERB. SOME EXAMPLES..."
            // 71 PRINT
            // 72 PRINT "TO FIND OUT WHAT YOU'RE CARRYING YOU MIGHT SAY: TAKE INVENTORY"
            // 73 PRINT "TO GO INTO A HOLE YOU MIGHT SAY: GO HOLE"
            // 74 PRINT "TO SAVE CURRENT GAME: SAVE GAME"
            // 80 PRINT
            // 81 PRINT "YOU WILL AT TIMES NEED SPECIAL ITEMS TO DO THINGS, BUT I'M"
            // 82 PRINT "SURE YOU'LL BE A GOOD ADVENTURER AND FIGURE THESE THINGS OUT."
            // 90 PRINT
            // 91 INPUT "     HAPPY ADVENTURING... HIT ENTER TO START";K$:CLS:RETURN
            Console.WriteLine("*** WELCOME TO ADVENTURE LAND. (#4.6) ***");
            Console.WriteLine();
            Console.WriteLine("UNLESS TOLD DIFFERENTLY YOU MUST FIND *TREASURES*");
            Console.WriteLine("AND-RETURN-THEM-TO-THEIR-PROPER--PLACE!");
            Console.WriteLine();
            Console.WriteLine("I'M YOUR PUPPET. GIVE ME ENGLISH COMMANDS THAT");
            Console.WriteLine("CONSIST OF A NOUN AND VERB. SOME EXAMPLES...");
            Console.WriteLine();
            Console.WriteLine("TO FIND OUT WHAT YOU'RE CARRYING YOU MIGHT SAY: TAKE INVENTORY");
            Console.WriteLine("TO GO INTO A HOLE YOU MIGHT SAY: GO HOLE");
            Console.WriteLine("TO SAVE CURRENT GAME: SAVE GAME");
            Console.WriteLine();
            Console.WriteLine("YOU WILL AT TIMES NEED SPECIAL ITEMS TO DO THINGS, BUT I'M");
            Console.WriteLine("SURE YOU'LL BE A GOOD ADVENTURER AND FIGURE THESE THINGS OUT.");
            Console.WriteLine();
            Console.Write("HAPPY ADVENTURING... HIT ENTER TO START");
            Console.ReadLine();
            Console.WriteLine();
        }

        private static void GetCommand()
        {
            // 140 INPUT "TELL ME WHAT TO DO";TP$
            // 141 PRINT
            Console.Write("TELL ME WHAT TO DO > ");
            TPS_commandLine = Console.ReadLine().ToUpper();
            Console.WriteLine();
        }

        private static bool ParseCommand()
        {
            string[] NTS_currCommand = new string[2]; // NT$(1)

            // 170 K=0:NT$(0)="":NT$(1)=""
            NTS_currCommand[0] = "";
            NTS_currCommand[1] = "";
            int K_currWord = 0;

            // 180 FOR X=1 TO LEN(TP$)
            // 181   K$=MID$(TP$,X,1)
            // 182   IF K$=" " THEN K=1 ELSE NT$(K)=LEFT$(NT$(K)+K$,LN)
            // 190 NEXT X
            bool lastWasSpace = true; // handle leading and multiple spaces gracefully
            for (int x = 0; x < TPS_commandLine.Length; x++)
            {
                if (TPS_commandLine[x] == ' ')
                {
                    if (!lastWasSpace)
                    {
                        K_currWord++;
                        if (K_currWord >= 2)
                        {
                            break;
                        }
                    }
                    lastWasSpace = true;
                }
                else
                {
                    lastWasSpace = false;
                    if (NTS_currCommand[K_currWord].Length < LN_wordSize) // only use up to LN chars
                    {
                        NTS_currCommand[K_currWord] += TPS_commandLine[x];
                    }
                }
            }

            // 191 FOR X=0 TO 1
            // 192   NV(X)=0
            // 193   IF NT$(X)="" THEN 230
            // 194   FOR Y=0 TO NL
            // 195     K$=NV$(Y,X):IF LEFT$(K$,1)="*" THEN K$=MID$(K$,2)
            // 200     IF X=1 IF Y<7 THEN K$=LEFT$(K$,LN)
            // 210     IF NT$(X)=K$ THEN NV(X)=Y:GOTO 220
            // 211   NEXT Y:GOTO 230
            // 220   IF LEFT$(NV$(NV(X),X),1)="*" THEN NV(X)=NV(X)-1:GOTO 220
            // 230 NEXT X
            for (int X = 0; X < 2; X++)
            {
                NV[X] = 0;
                if (NTS_currCommand[X] == "")
                {
                    continue;
                }
                for (int Y = 0; Y < NL_wordCount; Y++)
                {
                    string KS = NVS[Y, X];
                    if (KS.StartsWith("*"))
                    {
                        KS = KS.Substring(1); // remove *
                    }
                    if (KS.Length > LN_wordSize)
                    {
                        KS = KS.Substring(0, LN_wordSize);
                    }
                    if (NTS_currCommand[X].Equals(KS))
                    {
                        NV[X] = Y;
                        while (NVS[NV[X], X].StartsWith("*")) // search for prior word without *
                        {
                            NV[X] -= 1;
                        }
                        break;
                    }
                }
            }

            // 231 F=NV(0)<1 OR LEN(NT$(1))>0 AND NV(1)<1:RETURN
            bool F = (NV[0] < 1 || (NTS_currCommand[1].Length > 0 && NV[1] < 1));
            return F;
        }
    }
}
