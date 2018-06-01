﻿// Program.cs - 05/31/2018

using System;

namespace PirateAdventure
{
    partial class Program
    {

        private static bool gameOver = false;
        private static string TPS_commandLine = "";
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
            while (!gameOver)
            {
                Look();
                bool F_commandOK = false;
                while (!gameOver && !F_commandOK)
                {
                    GetCommand();
                    F_commandOK = ParseCommand();
                    if (!F_commandOK)
                    {
                        Console.WriteLine("YOU USE WORD(S) I DON'T KNOW");
                        Console.WriteLine();
                    }
                }
                RunEngine(NV[0], NV[1]);
            }
        }

        static void Initialize()
        {
            R_currRoom = AR_startRoom;
            LX_lightRemaining = LT_lightTotal;
            DF_darkFlag = false;
            for (int i = 0; i <= FC_flagCount; i++)
            {
                SF_systemFlags[i] = false;
            }
            // copy items into memory
            for (int i = 0; i <= IL_itemCount; i++)
            {
                IA[i] = I2[i];
            }
        }


        #region Old

        static void Old_RunGame()
        {
            // 130 {GOSUB}50
            Initialize();
            ShowIntroduction();
            while (!gameOver)
            {
                //     :{GOSUB}240
                //     :{GOTO}160
                Look();
                // 160 NV(0)=0
                //     :{GOSUB}360
                //     :{GOTO}140
                RunEngine(0, 0);
                bool F_commandOK = false;
                while (!gameOver && !F_commandOK)
                {
                    //     :{GOSUB}170
                    GetCommand();
                    F_commandOK = ParseCommand();
                    //     :{IF}F{PRINT}"YOU USE WORD(S) I DON'T KNOW"
                    //     :{GOTO}140
                    if (!F_commandOK)
                    {
                        Console.WriteLine("YOU USE WORD(S) I DON'T KNOW");
                        Console.WriteLine();
                    }
                }
                // 150 {GOSUB}360
                RunEngine(NV[0], NV[1]);
                if (!gameOver)
                {
                    //     :{IF}IA(9)=-1{THEN}LX=LX-1
                    //     :{IF}LX<0{THEN}{PRINT}"LIGHT HAS RUN OUT"
                    //     :IA(9)=0{ELSE}{IF}LX<25{PRINT}"LIGHT RUNS OUT IN";LX;"TURNS!"
                    if (IA[9] == -1)
                    {
                        LX_lightRemaining--;
                        if (LX_lightRemaining < 0)
                        {
                            Console.WriteLine("LIGHT HAS RUN OUT");
                            IA[9] = 0;
                        }
                        else if (LX_lightRemaining < 25)
                        {
                            Console.WriteLine($"LIGHT RUNS OUT IN {LX_lightRemaining} TURNS!");
                        }
                    }
                }
            }
        }

        private static void Look()
        {
            Console.WriteLine();
            // 240 {IF}DF{IF}IA(9)<>-1{AND}IA(9)<>R{PRINT}"I CAN'T SEE, ITS TOO DARK."
            //     :{RETURN}
            if (DF_darkFlag && (IA[9] != -1) && (IA[9] != R_currRoom))
            {
                Console.WriteLine("I CAN'T SEE, ITS TOO DARK.");
                return;
            }

            // 250 K=-1
            //     :{IF}{LEFT$}(RS$(R),1)="*"{THEN}{PRINT}{MID$}(RS$(R),2);{ELSE}{PRINT}"I'M IN A ";RS$(R);
            bool K_firstItem = true;
            if (RSS[R_currRoom].StartsWith("*"))
            {
                Console.Write(TrimDescription(RSS[R_currRoom].Substring(1)));
            }
            else
            {
                Console.Write("I'M IN A ");
                Console.Write(TrimDescription(RSS[R_currRoom]));
            }

            // 260 {FOR}Z=0{TO}IL
            //     :{IF}K{IF}IA(Z)=R{PRINT}". VISIBLE ITEMS HERE:"
            //     :K=0
            // 270 {GOTO}300
            // 280 TP$=IA$(Z)
            //     :{IF}{RIGHT$}(TP$,1)="/"{FOR}W={LEN}(TP$)-1{TO}1{STEP}-1
            //     :{IF}{MID$}(TP$,W,1)="/"{THEN}TP$={LEFT$}(TP$,W-1){ELSE}{NEXT}W
            // 290 {RETURN}
            // 300 {IF}IA(Z)<>R{THEN}320{ELSE}{GOSUB}280
            //     :{IF}POS(0)+{LEN}(TP$)+3>63{THEN}{PRINT}
            // 310 {PRINT}TP$;".  ";
            // 320 {NEXT}
            //     :{PRINT}
            for (int Z = 0; Z <= IL_itemCount; Z++)
            {
                if (K_firstItem && IA[Z] == R_currRoom)
                {
                    Console.WriteLine(". VISIBLE ITEMS HERE:");
                    K_firstItem = false;
                }
                if (IA[Z] == R_currRoom)
                {
                    string TPS_itemName;
                    TPS_itemName = IAS_itemDescriptions[Z];
                    Console.WriteLine($"{TrimDescription(TPS_itemName)}.");
                }
            }
            Console.WriteLine();

            // 330 K=-1
            //     :{FOR}Z=0{TO}5
            //     :{IF}K{IF}RM(R,Z)<>0{PRINT}""
            //     :{PRINT}"OBVIOUS EXITS: ";
            //     :K=0
            // 340 {IF}RM(R,Z)<>0{PRINT}NV$(Z+1,1);" ";
            // 350 {NEXT}
            //     :{PRINT}
            //     :{PRINT}
            //     :{RETURN}
            K_firstItem = false;
            for (int Z = 0; Z <= 5; Z++)
            {
                if (K_firstItem && RM[R_currRoom, Z] != 0)
                {
                    Console.Write("OBVIOUS EXITS: ");
                    K_firstItem = false;
                }
                if (RM[R_currRoom, Z] != 0)
                {
                    Console.Write(NVS[Z + 1, 1]);
                    Console.Write(" ");
                }
            }
            //Console.WriteLine();
            //Console.WriteLine();
            return;
        }

        private static void Old_Initialize()
        {
            // 100 R=AR
            //     :LX=LT
            //     :DF=0
            //     :SF=0
            R_currRoom = AR_startRoom;
            LX_lightRemaining = LT_lightTotal;
            DF_darkFlag = false;
            for (int i = 0; i <= FC_flagCount; i++)
            {
                SF_systemFlags[i] = false;
            }
            for (int i = 0; i <= IL_itemCount; i++)
            {
                IA[i] = I2[i];
            }
            //     :{INPUT}"USE OLD 'SAVED' GAME";K$
            //     :{IF}{LEFT$}(K$,1)<>"Y"{THEN}130
            Console.Write("USE OLD 'SAVED' GAME? ");
            string KS = Console.ReadLine().ToUpper();
            if (!KS.StartsWith("Y"))
            {
                return;
            }
            // Load saved game file
            Load();
        }

        private static void ShowIntroduction()
        {
            // 50 {CLS}
            //     :{PRINT}"     ***   WELCOME TO ADVENTURE LAND. (#4.6) ***"
            //     :{PRINT}
            //     :{PRINT}" UNLESS TOLD DIFFERENTLY YOU MUST FIND *TREASURES* AND-RETURN-THEM-TO-THEIR-PROPER--PLACE!"
            // 60 {PRINT}
            //     :{PRINT}"I'M YOUR PUPPET. GIVE ME ENGLISH COMMANDS THAT"
            // 70 {PRINT}"CONSIST OF A NOUN AND VERB. SOME EXAMPLES..."
            //     :{PRINT}
            //     :{PRINT}"TO FIND OUT WHAT YOU'RE CARRYING YOU MIGHT SAY: TAKE INVENTORY"
            //     :{PRINT}"TO GO INTO A HOLE YOU MIGHT SAY: GO HOLE"
            //     :{PRINT}"TO SAVE CURRENT GAME: SAVE GAME"
            // 80 {PRINT}
            //     :{PRINT}"YOU WILL AT TIMES NEED SPECIAL ITEMS TO DO THINGS, BUT I'M SURE YOU'LL BE A GOOD ADVENTURER AND FIGURE THESE THINGS OUT."
            // 90 {PRINT}
            //     :{INPUT}"     HAPPY ADVENTURING... HIT ENTER TO START";K$
            //     :{CLS}
            //     :{RETURN}
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
            // 140 {INPUT}"TELL ME WHAT TO DO";TP$
            //     :{PRINT}
            Console.Write("TELL ME WHAT TO DO > ");
            TPS_commandLine = Console.ReadLine().ToUpper().Trim();
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
            for (int X = 0; X < TPS_commandLine.Length; X++)
            {
                if (TPS_commandLine[X] == ' ')
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
                        NTS_currCommand[K_currWord] += TPS_commandLine[X];
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
            for (int X = 0; X <= 1; X++)
            {
                NV[X] = 0;
                if (NTS_currCommand[X] == "")
                {
                    continue;
                }
                for (int Y = 0; Y <= NL_wordCount; Y++)
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
            if (NV[0] == 0)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(NTS_currCommand[1]) && NV[1] == 0)
            {
                return false;
            }
            return true;
        }

        private static string TrimDescription(string value)
        {
            // trims trailing /SHORTDESC/
            if (value.EndsWith("/"))
            {
                value = value.Substring(0, value.IndexOf("/"));
            }
            return value;
        }

        #endregion

    }
}
