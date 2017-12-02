// Program.cs - 12/01/2017

using System;

namespace PirateAdventure
{
    partial class Program
    {
        private static int IL_itemCount = 60;
        private static int CL_commandCount = 151;
        private static int NL_wordCountF = 59;
        private static int RL_roomCount = 33;
        private static int MX_maxCarry = 5;
        private static int AR_startRoom = 1;
        private static int TT_TotalTreasures = 2;
        private static int LN_wordSize = 3;
        private static int LT_lightTotal = 200;
        private static int ML = 71;
        private static int TR = 1;

        private static bool gameOver = false;
        private static string TPS_commandLine = "";
        private static bool F_commandNotOK = false;
        private static int R_currRoom;
        private static int LX_lightRemaining;
        private static bool DF_roomIsDark;

        // 41 Z$="I'VE TOO MUCH TOO CARRY. TRY -TAKE INVENTORY-"
        private const string ZS = "I'VE TOO MUCH TOO CARRY. TRY -TAKE INVENTORY-";

        private static Random sysRand = new Random();

        static void Main(string[] args)
        {
            TestData();
            //RunGame();
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
                NV[0] = 0;
                DoActions();
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
                DoActions();
                if (!gameOver)
                {
                    // 151 IF IA(9)=-1 THEN 
                    if (IA[9] == -1)
                    {
                        //LX=LX-1
                        LX_lightRemaining -= 1;
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
            if (DF_roomIsDark && (IA[9] != -1) && (IA[9] != R_currRoom))
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
                    // ???
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
                // 332   IF K THEN IF RM(R,Z)<>0 THEN PRINT "OBVIOUS EXITS: ";:K=0
                if (K_firstItem && RM[R_currRoom, z] != 0)
                {
                    Console.Write("OBVIOUS EXITS: ");
                    K_firstItem = false;
                }
                // 340   IF RM(R,Z)<>0 THEN PRINT NV$(Z+1,1);" ";
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
            DF_roomIsDark = false;
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

        private static void DoActions()
        {
            // 360 F2=-1:F=-1:F3=0
            bool F2_allCmds = true;
            bool F = true;
            bool F3 = false;
            // 362 FOR X=0 TO CL
            for (int X = 0; X < CL_commandCount; X++)
            {
                // 363   V=CA(X,0)/150:IF NV(0)=0 THEN IF V<>0 THEN RETURN
                int V_verb = CA[X, 0] / 150;
                if (NV[0] == 0 && V_verb != 0)
                {
                    return; // done doing background actions
                }
                // 370   IF NV(0)<>V THEN 980
                if (NV[0] != V_verb)
                {
                    continue;
                }
                // 371   N=CA(X,0)-V*150
                int N_noun = CA[X, 0] - (V_verb * 150);
                // 380   IF NV(0)=0 THEN F=0:IF RND(100)<=N THEN 400 ELSE 980
                if (NV[0] == 0)
                {
                    F = false;
                    if (sysRand.Next(100) > N_noun)
                    {
                        continue;
                    }
                }
                // 390   IF N<>NV(1) AND N<>0 THEN 980
                else if (N_noun != NV[1] && N_noun != 0)
                {
                    continue;
                }
                // 400   F2=-1:F=0:F3=-1
                F2_allCmds = true;
                F = false;
                F3 = true;
                // 401   FOR Y=1 TO 5
                for (int Y = 1; Y <= 5; Y++)
                {
                    // 402     W=CA(X,Y):LL=W/20:K=W-LL*20:F1=-1
                    int W = CA[X, Y];
                    int LL = W / 20;
                    int K = W - (LL * 20);
                    bool F1_thisCmd = true;
                    // 403     ON K+1 GOTO 550,430,450,470,490,500,510,520,530,540,410,420,440,460,480
                    switch (K + 1)
                    {
                        case 1:
                            // 430     F1=IA(LL)=-1:GOTO 550
                            F1_thisCmd = (IA[LL] == -1); // item in inventory
                            break;
                        case 2:
                            // 450     F1=IA(LL)=R:GOTO 550
                            F1_thisCmd = (IA[LL] == R_currRoom); // item in current room
                            break;
                        case 3:
                            // 470     F1=IA(LL)=R OR IA(LL)=-1:GOTO 550
                            F1_thisCmd = (IA[LL] == R_currRoom || IA[LL] == -1); // item in current room or inventory
                            break;
                        case 4:
                            // 490     F1=R=LL:GOTO 550
                            F1_thisCmd = (R_currRoom == LL); // current room is LL
                            break;
                        case 5:
                            // 500     F1=IA(LL)<>R:GOTO 550
                            F1_thisCmd = (IA[LL] != R_currRoom); // item not in current room
                            break;
                        case 6:
                            // 510     F1=IA(LL)<>-1:GOTO 550
                            F1_thisCmd = (IA[LL] != -1); // item not in inventory
                            break;
                        case 7:
                            // 520     F1=R<>LL:GOTO 550
                            F1_thisCmd = (R_currRoom != LL); // current room not LL
                            break;
                        case 8:
                            // 530     F1=SF(LL):F1=F1<>0:GOTO 550
                            F1_thisCmd = SF_systemFlags[LL]; // system flag is true
                            break;
                        case 9:
                            // 540     F1=SF(LL):F1=F1=0:GOTO 550
                            F1_thisCmd = !SF_systemFlags[LL]; // system flag is false
                            break;
                        case 10:
                            // 410     F1=-1
                            // 411     FOR Z=0 TO IL
                            // 412       IF IA(Z)=-1 THEN 550
                            // 413     NEXT Z
                            // 414     F1=0:GOTO 550
                            F1_thisCmd = false; // no items in inventory
                            for (int Z = 0; Z <= IL_itemCount; Z++)
                            {
                                if (IA[Z] == -1) // item in inventory
                                {
                                    F1_thisCmd = true; // has some item in inventory
                                    break;
                                }
                            }
                            break;
                        case 11:
                            // 420     F1=0
                            // 421     FOR Z=0 TO IL
                            // 422       IF IA(Z)=-1 THEN 550
                            // 423     NEXT Z
                            // 424     F1=-1:GOTO 550
                            F1_thisCmd = true; // inventory is empty
                            for (int Z = 0; Z <= IL_itemCount; Z++)
                            {
                                if (IA[Z] == -1) // item in inventory
                                {
                                    F1_thisCmd = false; // inventory not empty
                                    break;
                                }
                            }
                            break;
                        case 12:
                            // 440     F1=IA(LL)<>-1 AND IA(LL)<>R:GOTO 550
                            F1_thisCmd = (IA[LL] != -1 && IA[LL] != R_currRoom); // item not inventory or current room
                            break;
                        case 13:
                            // 460     F1=IA(LL)<>0:GOTO 550
                            F1_thisCmd = (IA[LL] != 0); // item is somewhere
                            break;
                        case 14:
                            // 480     F1=IA(LL)=0:GOTO 550
                            F1_thisCmd = (IA[LL] == 0); // item is nowhere
                            break;
                    }
                    // 550     F2=F2 AND F1:IF F2 THEN 551 ELSE 980
                    F2_allCmds = (F2_allCmds && F1_thisCmd);
                    if (!F2_allCmds)
                    {
                        break;
                    }
                    // 551   NEXT Y
                }
                if (!F2_allCmds)
                {
                    break;
                }
                // 560   IP=0
                int IP = 0;
                // 561   FOR Y=1 TO 4
                for (int Y = 1; Y <= 4; Y++)
                {
                    int K = ((Y - 1) / 2) + 6;
                    int AC;
                    // 562     K=(Y-1)/2+6:ON Y GOTO 570,580,570,580
                    if (Y == 1 || Y == 3)
                    {
                        // 570    AC=CA(X,K)/150:GOTO 590
                        AC = CA[X, K] / 150;
                    }
                    else
                    {
                        // 580     AC=CA(X,K)-CINT(CA(X,K)/150)*150
                        AC = CA[X, K] - ((CA[X, K] / 150) * 150);
                    }
                    if (AC == 0)
                    {
                        // 591     IF AC=0 THEN 960
                        continue;
                    }
                    if (AC > 101)
                    {
                        // 590     IF AC>101 THEN 600
                        // 600     PRINT MS$(AC-50):GOTO 960
                        Console.WriteLine(MSS_messages[AC - 50]);
                        continue;
                    }
                    if (AC < 52)
                    {
                        // 592     IF AC<52 THEN PRINT MS$(AC):GOTO 960
                        Console.WriteLine(MSS_messages[AC]);
                        continue;
                    }
                    // vars for below
                    int P;
                    int L;
                    int Z;
                    // 593     ON AC-51 GOTO 660,700,740,760,770,780,790,760,810,830,840,850,860,870,890,920,930,940,950,710,750
                    switch (AC - 51)
                    {
                        case 1: // 660
                                // 660     L=0
                            L = 0;
                            // 661     FOR Z=1 TO IL
                            for (int Z_temp = 1; Z_temp <= IL_itemCount; Z_temp++)
                            {
                                // 662       IF IA(Z)=-1
                                if (IA[Z_temp] == -1)
                                {
                                    // LET L=L+1
                                    L++; // count items carried
                                }
                                // 670     NEXT Z
                            }
                            // 680     IF L>=MX PRINT Z$:GOTO 970
                            if (L >= MX_maxCarry)
                            {
                                Console.WriteLine(ZS);
                                break;
                            }
                            // 690     GOSUB 1050
                            P = GOSUB1050(ref IP, X);
                            //:IA(P)=-1
                            IA[P] = -1; // carry item
                            //:GOTO 960
                            break;
                        case 2: // 700 - Move item to current room
                                // 700     GOSUB 1050
                            P = GOSUB1050(ref IP, X);
                            //:IA(P)=R
                            IA[P] = R_currRoom;
                            //:GOTO 960
                            break;
                        case 3: // 740 - Teleport to room P
                                // 740 GOSUB 1050
                            P = GOSUB1050(ref IP, X);
                            //:R=P
                            R_currRoom = P;
                            //:GOTO 960
                            break;
                        case 4: // 760
                        case 8: // 760 - Item goes nowhere
                            // 760 GOSUB 1050
                            P = GOSUB1050(ref IP, X);
                            //:IA(P)=0
                            IA[P] = 0;
                            //:GOTO 960
                            break;
                        case 5: // 770
                                // 770     DF=-1
                            DF_roomIsDark = true;
                            //:GOTO 960
                            break;
                        case 6: // 780
                                // 780     DF=0
                            DF_roomIsDark = false;
                            //:GOTO 960
                            break;
                        case 7: // 790 - SF_systemFlags[P] = true
                                // 790     GOSUB 1050
                            P = GOSUB1050(ref IP, X);
                            // 800     SF(P)=-1
                            SF_systemFlags[P] = true;
                            // 801 GOTO 960
                            break;
                        case 9: // 810 - SF_systemFlags[P] = false
                                // 810     GOSUB 1050
                            P = GOSUB1050(ref IP, X);
                            // 820 SF(P)=0
                            SF_systemFlags[P] = false;
                            // 821 GOTO 960
                            break;
                        case 10: // 830
                                 // 830     PRINT "I'M DEAD...":R=RL:DF=0:GOTO 860
                            Console.WriteLine("I'M DEAD...");
                            R_currRoom = RL_roomCount;
                            DF_roomIsDark = false;
                            // 860     GOSUB 240
                            GOSUB240();
                            //:GOTO 960
                            break;
                        case 11: // 840
                                 // 840     GOSUB 1050
                            P = GOSUB1050(ref IP, X);
                            // 841 L=P
                            L = P;
                            // 842 GOSUB 1050
                            P = GOSUB1050(ref IP, X);
                            // 843 IA(L)=P
                            IA[L] = P;
                            //GOTO 960
                            break;
                        case 12: // 850
                                 // 850     PRINT "THE GAME IS NOW OVER"
                            Console.WriteLine("THE GAME IS NOW OVER");
                            //:INPUT "ANOTHER GAME";K$
                            Console.Write("ANOTHER GAME? ");
                            string KS = Console.ReadLine();
                            //:IF LEFT$(K$,1)="N" THEN
                            if (KS.ToUpper().StartsWith("N"))
                            {
                                // END
                                gameOver = true;
                                // ???
                            }
                            // 851     FOR Z=0 TO IL
                            for (int Z_Temp = 0; Z_Temp <= IL_itemCount; Z_Temp++)
                            {
                                // 852       IA(Z)=I2(Z)
                                IA[Z_Temp] = I2[Z_Temp];
                                // 853     NEXT Z
                            }
                            // 854     GOTO 100
                            break;
                        case 13: // 860
                                 // 860     GOSUB 240
                            GOSUB240();
                            //:GOTO 960
                            break;
                        case 14: // 870
                                 // 870     L=0
                            int L_treasureCount = 0;
                            // 871     FOR Z=1 TO IL
                            for (int z = 1; z <= IL_itemCount; z++)
                            {
                                // 872       IF IA(Z)=TR THEN IF LEFT$(IA$(Z),1)="*" THEN 
                                if (IA[z] == TR && IAS_itemDescriptions[z].StartsWith("*"))
                                {
                                    //LET L=L+1
                                    L_treasureCount++;
                                    // 880     NEXT Z
                                }
                            }
                            // 881     PRINT "I'VE STORED";L;"TREASURES."
                            Console.WriteLine($"I'VE STORED {L_treasureCount} TREASURES.");
                            //PRINT"ON A SCALE OF 0 TO 100 THAT RATES A";CINT(L/TT*100)
                            Console.WriteLine($"ON A SCALE OF 0 TO 100 THAT RATES A {(L_treasureCount / TT_TotalTreasures * 100)}");
                            // 882     IF L=TT THEN PRINT "WELL DONE.":GOTO 850 ELSE 960
                            if (L_treasureCount == TT_TotalTreasures)
                            {
                                Console.WriteLine("WELL DONE.");
                                // GOTO 850
                                // ???
                            }
                            break;
                        case 15: // 890
                                 // 890     PRINT "I'M CARRYING:"
                            Console.WriteLine("I'M CARRYING:");
                            //:K$="NOTHING"
                            string KS_temp = "NOTHING";
                            // 891     FOR Z=0 TO IL
                            for (int z = 0; z <= IL_itemCount; z++)
                            {
                                // 892       IF IA(Z)<>-1 THEN 910
                                if (!(IA[z] != -1))
                                {
                                    // 893       GOSUB 280
                                    string TPS_temp = GOSUB280();
                                    //:IF LEN(TP$)+POS(0)>63 THEN PRINT
                                    // ???
                                    // 900       PRINT TP$;".",;:K$=""
                                    Console.WriteLine(TPS_temp);
                                    KS_temp = "";
                                }

                                // 910     NEXT Z
                            }
                            // 911     PRINT K$
                            Console.WriteLine(KS_temp);
                            //:GOTO 960
                            break;
                        case 16: // 920
                                 // 920     P=0:GOTO 800
                            P = 0;
                            // 800     SF(P)=-1:GOTO 960
                            SF_systemFlags[P] = true;
                            break;
                        case 17: // 930
                                 // 930     P=0:GOTO 820
                            P = 0;
                            // 820     SF(P)=0:GOTO 960
                            SF_systemFlags[P] = false;
                            break;
                        case 18: // 940
                                 // 940     LX=LT:IA(9)=-1:GOTO 960
                            LX_lightRemaining = LT_lightTotal;
                            IA[9] = -1;
                            break;
                        case 19: // 950
                                 // 950     CLS:GOTO 960
                            Console.Clear();
                            break;
                        case 20: // 710
                                 // 710     PRINT "SAVING GAME"
                            Console.WriteLine("SAVING GAME");
                                 // 711     REM IF D=-1 THEN INPUT "READY OUTPUT TAPE";K$:PRINT INT(IL*5/60)+1;"MINUTES" ELSE OPEN"O",D,SV$
                                 // 720     REM PRINT #D,SF,LX,DF,R
                                 // 721     REM FOR W=0 TO IL
                                 // 722     REM   PRINT #D,IA(W)
                                 // 723     REM NEXT W:IF D<>-1CLOSE
                                 // 730     GOTO 960
                            break;
                        case 21: // 750 - Swap two values
                                 // 750     GOSUB 1050
                            P = GOSUB1050(ref IP, X);
                            //:L=P
                            L = P;
                            //:GOSUB 1050
                            P = GOSUB1050(ref IP, X);
                            //:Z=IA(P)
                            Z = IA[P];
                            //:IA(P)=IA(L)
                            IA[P] = IA[L];
                            //:IA(L)=Z
                            IA[L] = Z;
                            //:GOTO 960
                            break;
                    }
                    // 610     L=DF:IF L THEN L=DF AND IA(9)<>R AND IA(9)<>-1:IF L PRINT "DANGEROUS TO MOVE IN THE DARK!"
                    // 620     IF NV(1)<1 PRINT "GIVE ME A DIRECTION TOO.":GOTO 1040
                    // 630     K=RM(R,NV(1)-1):IF K<1 IF L THEN PRINT "I FELL DOWN AND BROKE MY NECK.":K=RL:DF=0:ELSE PRINT "I CAN'T GO IN THAT DIRECTION":GOTO 1040
                    // 640     IF NOT L CLS
                    // 650     R=K:GOSUB 240:GOTO 1040







                    // 960   NEXT Y
                }

                // 970   IF NV(0)<>0 THEN 990
                if (NV[0] != 0)
                {
                    break;
                }
                // 980 NEXT X
            }
            // 990 REM





            // 361 IF NV(0)=1 AND NV(1)<7 THEN 610
            if (!(NV[0] == 1 && NV[1] < 7))
            {
                // todo handle GO direction
            }
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
            for (int x = 0; x < 2; x++)
            {
                NV[x] = 0;
                if (NTS_currCommand[x] == "")
                {
                    continue;
                }
                for (int y = 0; y < NL_wordCountF; y++)
                {
                    string KS = NVS[y, x];
                    if (KS.StartsWith("*"))
                    {
                        KS = KS.Substring(1); // remove *
                    }
                    if (KS.Length > LN_wordSize)
                    {
                        KS = KS.Substring(0, LN_wordSize);
                    }
                    if (NTS_currCommand[x].Equals(KS))
                    {
                        NV[x] = y;
                        while (NVS[NV[x], x].StartsWith("*")) // search for prior word without *
                        {
                            NV[x] -= 1;
                        }
                        break;
                    }
                }
            }

            // 231 F=NV(0)<1 OR LEN(NT$(1))>0 AND NV(1)<1:RETURN
            return (NV[0] < 1 || (NTS_currCommand[1].Length > 0 && NV[1] < 1));
        }

        private static void DoCommand()
        {
            //throw new NotImplementedException();
        }

        private static int GOSUB1050(ref int IP, int X)
        {
            int P = 0;
            int W = 0;
            int M = 0;
            do
            {
                // 1050 IP=IP+1
                IP++;
                // 1051 W=CA(X,IP)
                W = CA[X, IP];
                // 1052 P=W/20
                P = W / 20;
                // 1053 M=W-P*20
                M = W - (P * 20);
                // 1054 IF M<>0 THEN 1050 ELSE RETURN
            } while (M != 0);
            return P;
        }
    }
}
