// Program.Engine.cs - 12/03/2017

using System;

namespace PirateAdventure
{
    partial class Program
    {
        private static void RunEngine(int currVerb, int currNoun)
        {
            // 360 F2=-1
            //     :F=-1
            //     :F3=0
            //     :{IF}NV(0)=1{AND}NV(1)<7{THEN}610{ELSE}{FOR}X=0{TO}CL
            //     :V=CA(X,0)/150
            //     :{IF}NV(0)=0{IF}V<>0{RETURN}
            // 370 {IF}NV(0)<>V{THEN}{NEXT}X
            //     :{GOTO}990{ELSE}N=CA(X,0)-V*150
            // 380 {IF}NV(0)=0{THEN}F=0
            //     :{IF}RND(100)<=N{THEN}400{ELSE}{NEXT}X
            //     :{GOTO}990
            // 390 {IF}N<>NV(1){AND}N<>0{THEN}{NEXT}X
            //     :{GOTO}990
            for (int X_dataLine = 0; X_dataLine <= CL_commandCount; X_dataLine++)
            {
                if (gameOver)
                {
                    return;
                }
                int verb = CA[X_dataLine, 0] / 150;
                int noun = CA[X_dataLine, 0] % 150;
                if (verb == 0) // background task
                {
                    if (currVerb != 0) // not running background tasks
                    {
                        continue;
                    }
                    if (noun < 100 && sysRand.Next(100) > noun)
                    {
                        continue;
                    }
                }
                else if (currVerb == 1 && currNoun < 7)
                {
                    GoDirection(currNoun);
                    return;
                }
                else if (verb != currVerb || noun != currNoun)
                {
                    continue;
                }
                // check all the conditions on this data line

                // 400 F2=-1
                //     :F=0
                //     :F3=-1
                //     :{FOR}Y=1{TO}5
                //     :W=CA(X,Y)
                //     :LL=W/20
                //     :K=W-LL*20
                //     :F1=-1
                //     :{ON}K+1{GOTO}550,430,450,470,490,500,510,520,530,540,410,420,440,460,480
                bool F1_conditionsMet = true;
                for (int conditionIndex = 1; conditionIndex <= 5; conditionIndex++)
                {
                    int LL_conditionData = CA[X_dataLine, conditionIndex] / 20;
                    int conditionNum = CA[X_dataLine, conditionIndex] % 20;
                    switch (conditionNum)
                    {
                        case 0:
                            // nothing
                            break;
                        case 1: // item carried
                                // 430 F1=IA(LL)=-1
                                //     :{GOTO}550
                            F1_conditionsMet = (IA[LL_conditionData] == -1);
                            break;
                        case 2: // item in room
                                // 450 F1=IA(LL)=R
                                //     :{GOTO}550
                            F1_conditionsMet = (IA[LL_conditionData] == R_currRoom);
                            break;
                        case 3: // item carried or in room
                                // 470 F1=IA(LL)=R{OR}IA(LL)=-1
                                //     :{GOTO}550
                            F1_conditionsMet = (IA[LL_conditionData] == -1 || IA[LL_conditionData] == R_currRoom);
                            break;
                        case 4: // room matches
                                // 490 F1=R=LL
                                //     :{GOTO}550
                            F1_conditionsMet = (R_currRoom == LL_conditionData);
                            break;
                        case 5: // item not in room
                                // 500 F1=IA(LL)<>R
                                //     :{GOTO}550
                            F1_conditionsMet = (IA[LL_conditionData] != R_currRoom);
                            break;
                        case 6: // item not carried
                                // 510 F1=IA(LL)<>-1
                                //     :{GOTO}550
                            F1_conditionsMet = (IA[LL_conditionData] != -1);
                            break;
                        case 7: // room doesn't match
                                // 520 F1=R<>LL
                                //     :{GOTO}550
                            F1_conditionsMet = (R_currRoom != LL_conditionData);
                            break;
                        case 8: // flag true
                                // 530 F1=SF{AND}{CINT}(2^LL+.5)
                                //     :F1=F1<>0
                                //     :{GOTO}550
                            F1_conditionsMet = (SF_systemFlags[LL_conditionData]);
                            break;
                        case 9: // flag false
                                // 540 F1=SF{AND}{CINT}(2^LL+.5)
                                //     :F1=F1=0
                                //     :{GOTO}550
                            F1_conditionsMet = (!SF_systemFlags[LL_conditionData]);
                            break;
                        case 10: // inventory not empty
                                 // 410 F1=-1
                                 //     :{FOR}Z=0{TO}IL
                                 //     :{IF}IA(Z)=-1{THEN}550{ELSE}{NEXT}
                                 //     :F1=0
                                 //     :{GOTO}550
                            F1_conditionsMet = false;
                            for (int Z = 0; Z <= IL_itemCount; Z++)
                            {
                                if (IA[LL_conditionData] == -1)
                                {
                                    F1_conditionsMet = true;
                                    break;
                                }
                            }
                            break;
                        case 11: // inventory is empty
                                 // 420 F1=0
                                 //     :{FOR}Z=0{TO}IL
                                 //     :{IF}IA(Z)=-1{THEN}550{ELSE}{NEXT}
                                 //     :F1=-1
                                 //     :{GOTO}550
                            F1_conditionsMet = true;
                            for (int Z = 0; Z <= IL_itemCount; Z++)
                            {
                                if (IA[LL_conditionData] == -1)
                                {
                                    F1_conditionsMet = false;
                                    break;
                                }
                            }
                            break;
                        case 12: // item not carried and not in room
                                 // 440 F1=IA(LL)<>-1{AND}IA(LL)<>R
                                 //     :{GOTO}550
                            F1_conditionsMet = (IA[LL_conditionData] != -1 && IA[LL_conditionData] != R_currRoom);
                            break;
                        case 13: // item is anywhere
                                 // 460 F1=IA(LL)<>0
                                 //     :{GOTO}550
                            F1_conditionsMet = (IA[LL_conditionData] != 0);
                            break;
                        case 14: // item is nowhere
                                 // 480 F1=IA(LL)=0
                                 //     :{GOTO}550
                            F1_conditionsMet = (IA[LL_conditionData] == 0);
                            break;
                        default:
                            Console.WriteLine($"#ERROR# Unknown condition: {conditionNum} {LL_conditionData}");
                            break;
                    }
                    if (!F1_conditionsMet)
                    {
                        break;
                    }
                }
                // 550 F2=F2{AND}F1
                //     :{IF}F2{THEN}{NEXT}Y{ELSE}{NEXT}X
                //     :{GOTO}990
                if (!F1_conditionsMet)
                {
                    continue;
                }

                // run actions
                // 560 IP=0
                //     :{FOR}Y=1{TO}4
                //     :K=(Y-1)/2+6
                //     :{ON}Y{GOTO}570,580,570,580
                // 570 AC=CA(X,K)/150
                //     :{GOTO}590
                // 580 AC=CA(X,K)-{CINT}(CA(X,K)/150)*150
                int IP_dataPointer = 0;
                for (int Y = 6; Y <= 7; Y++)
                {
                    int AC_action1 = CA[X_dataLine, Y] / 150;
                    int AC_action2 = CA[X_dataLine, Y] % 150;
                    RunAction(AC_action1, X_dataLine, ref IP_dataPointer);
                    if (gameOver)
                    {
                        return;
                    }
                    RunAction(AC_action2, X_dataLine, ref IP_dataPointer);
                }
            }
        }

        private static void RunAction(int AC_action, int X_dataLine, ref int IP_dataPointer)
        {
            // 590 {IF}AC>101{THEN}600{ELSE}{IF}AC=0{THEN}960{ELSE}{IF}AC<52{THEN}{PRINT}MS$(AC)
            //     :{GOTO}960
            //     :{ELSE}{ON}AC-51{GOTO}660,700,740,760,770,780,790,760,810,830,840,850,860,870,890,920,930,940,950,710,750
            // 600 {PRINT}MS$(AC-50)
            //     :{GOTO}960
            // 590 {IF}AC>101{THEN}600{ELSE}{IF}AC=0{THEN}960{ELSE}{IF}AC<52{THEN}{PRINT}MS$(AC)
            //     :{GOTO}960
            if (AC_action > 101)
            {
                // 600 {PRINT}MS$(AC-50)
                //     :{GOTO}960
                Console.WriteLine($"{MSS_messages[AC_action - 50]}");
                return;
            }
            if (AC_action == 0)
            {
                return;
            }
            if (AC_action < 52)
            {
                Console.WriteLine($"{MSS_messages[AC_action]}");
                return;
            }
            int P_dataValue = 0;
            int P2_dataValue2 = 0;
            //     :{ELSE}{ON}AC-51{GOTO}660,700,740,760,770,780,790,760,810,830,840,850,860,870,890,920,930,940,950,710,750
            switch (AC_action - 51)
            {
                case 0:
                    // do nothing
                    break;
                case 1: // take //1=660
                    // 660 L=0
                    //     :{FOR}Z=1{TO}IL
                    //     :{IF}IA(Z)=-1{LET}L=L+1
                    // 670 {NEXT}Z
                    // 680 {IF}L>=MX{PRINT}Z$
                    //     :{GOTO}970
                    // 690 {GOSUB}1050
                    //     :IA(P)=-1
                    //     :{GOTO}960
                    int L_invCount = 0;
                    for (int Z = 0; Z <= IL_itemCount; Z++)
                    {
                        if (IA[Z] == -1)
                        {
                            L_invCount++;
                        }
                    }
                    if (L_invCount >= MX_maxCarry) // inventory full
                    {
                        Console.WriteLine(ZS_inventoryFullMsg);
                        return;
                    }
                    P_dataValue = GetDataValue(X_dataLine, ref IP_dataPointer);
                    IA[P_dataValue] = -1;
                    break;
                case 2: // drop to current room //2=700
                    // 700 {GOSUB}1050
                    //     :IA(P)=R
                    //     :{GOTO}960
                    P_dataValue = GetDataValue(X_dataLine, ref IP_dataPointer);
                    IA[P_dataValue] = R_currRoom;
                    break;
                case 3: // teleport //3=740
                    // 740 {GOSUB}1050
                    //     :R=P
                    //     :{GOTO}960
                    P_dataValue = GetDataValue(X_dataLine, ref IP_dataPointer);
                    R_currRoom = P_dataValue;
                    break;
                case 4: // item to nowhere //4=760
                case 8: // item to nowhere //8=760
                    // 760 {GOSUB}1050
                    //     :IA(P)=0
                    //     :{GOTO}960
                    P_dataValue = GetDataValue(X_dataLine, ref IP_dataPointer);
                    IA[P_dataValue] = 0;
                    break;
                case 5: // turn on dark flag //5=770
                    // 770 DF=-1
                    //     :{GOTO}960
                    DF_darkFlag = true;
                    break;
                case 6: // turn off dark flag //6=780
                    // 780 DF=0
                    //     :{GOTO}960
                    DF_darkFlag = false;
                    break;
                case 7: // turn on flag //7=790
                    // 790 {GOSUB}1050
                    // 800 SF=SF {OR}{CINT}(.5+2^P)
                    //     :{GOTO}960
                    P_dataValue = GetDataValue(X_dataLine, ref IP_dataPointer);
                    SF_systemFlags[P_dataValue] = true;
                    break;
                case 9: // turn off flag //9=810
                    // 810 {GOSUB}1050
                    // 820 SF=SF{AND}{NOT}{CINT}(.5+2^P)
                    //     :{GOTO}960
                    P_dataValue = GetDataValue(X_dataLine, ref IP_dataPointer);
                    SF_systemFlags[P_dataValue] = false;
                    break;
                case 10: // dead //10=830
                    // 830 {PRINT}"I'M DEAD..."
                    //     :R=RL
                    //     :DF=0
                    //     :{GOTO}860
                    Console.WriteLine("I'M DEAD...");
                    R_currRoom = RL_roomCount;
                    DF_darkFlag = false;
                    // 860 {GOSUB}240
                    //     :{GOTO}960
                    Look();
                    break;
                case 11: // item goes to room //11=840
                    // 840 {GOSUB}1050
                    //     :L=P
                    //     :{GOSUB}1050
                    //     :IA(L)=P
                    //     :{GOTO}960
                    P_dataValue = GetDataValue(X_dataLine, ref IP_dataPointer);
                    P2_dataValue2 = GetDataValue(X_dataLine, ref IP_dataPointer);
                    IA[P_dataValue] = P2_dataValue2;
                    break;
                case 12: // game over //12=850
                    // 850 {INPUT}"THE GAME IS NOW OVER"
                    //     :{PRINT}"ANOTHER GAME";K$
                    //     :{IF}{LEFT$}(K$,1)="N"{THEN}{END}{ELSE}{FOR}X=0{TO}IL
                    //     :IA(X)=I2(X)
                    //     :{NEXT}
                    //     :{GOTO}100
                    GameIsOver();
                    break;
                case 13: // look //13=860
                    // 860 {GOSUB}240
                    //     :{GOTO}960
                    Look();
                    break;
                case 14: // check treasures //14=870
                         // 870 L=0
                         //     :{FOR}Z=1{TO}IL
                         //     :{IF}IA(Z)=TR{IF}{LEFT$}(IA$(Z),1)="*"{LET}L=L+1
                         // 880 {NEXT}Z
                         //     :{PRINT}"I'VE STORED";L;"TREASURES."
                         //     :{PRINT}"ON A SCALE OF 0 TO 100 THAT RATES A";{CINT}(L/TT*100)
                         //     :{IF}L=TT{THEN}{PRINT}"WELL DONE."
                         //     :{GOTO}850{ELSE}960
                    int L_treasureCount = 0;
                    for (int Z = 0; Z <= IL_itemCount; Z++)
                    {
                        if (IA[Z] == TR_treasureRoom && IAS_itemDescriptions[Z].StartsWith("*"))
                        {
                            L_treasureCount++;
                        }
                    }
                    Console.WriteLine($"I'VE STORED {L_treasureCount} TREASURES.");
                    Console.WriteLine($"ON A SCALE OF 0 TO 100 THAT RATES A {(L_treasureCount * 100) / TT_TotalTreasures}");
                    if (L_treasureCount < TT_TotalTreasures)
                    {
                        break;
                    }
                    Console.WriteLine("WELL DONE.");
                    GameIsOver();
                    break;
                case 15: // show inventory //15=890
                    Console.WriteLine("    : show inventory");
                    break;
                case 16: // flag 0 true //16=920
                    // 920 P=0
                    //     :{GOTO}800
                    // 800 SF=SF {OR}{CINT}(.5+2^P)
                    //     :{GOTO}960
                    SF_systemFlags[0] = true;
                    break;
                case 17: // flag 0 false //17=930
                    // 930 P=0
                    //     :{GOTO}820
                    // 820 SF=SF{AND}{NOT}{CINT}(.5+2^P)
                    //     :{GOTO}960
                    SF_systemFlags[0] = false;
                    break;
                case 18: //18=940
                    break;
                case 19: // clear screen //19=950
                    Console.WriteLine();
                    break;
                case 20: // save game //20=710
                    Console.WriteLine("    : save game");
                    break;
                case 21: // swap two items //21=750
                    // 750 {GOSUB}1050
                    //     :L=P
                    //     :{GOSUB}1050
                    //     :Z=IA(P)
                    //     :IA(P)=IA(L)
                    //     :IA(L)=Z
                    //     :{GOTO}960
                    P_dataValue = GetDataValue(X_dataLine, ref IP_dataPointer);
                    P2_dataValue2 = GetDataValue(X_dataLine, ref IP_dataPointer);
                    int P2 = IA[P_dataValue];
                    IA[P_dataValue] = IA[P2_dataValue2];
                    IA[P2_dataValue2] = P2;
                    break;
                default:
                    Console.WriteLine($"#ERROR# Unknown action: {AC_action - 51}");
                    break;
            }
        }

        private static int GetDataValue(int X_dataLine, ref int IP_dataPointer)
        {
            // 1050 IP=IP+1
            //     :W=CA(X,IP)
            //     :P=W/20
            //     :M=W-P*20
            //     :{IF}M<>0{THEN}1050{ELSE}{RETURN}
            int P_dataValue;
            int W_dataWord;
            int M_remainder;
            do
            {
                IP_dataPointer++;
                W_dataWord = CA[X_dataLine, IP_dataPointer];
                P_dataValue = W_dataWord / 20;
                M_remainder = W_dataWord % 20;
            } while (M_remainder != 0);
            return P_dataValue;
        }

        private static void GoDirection(int noun)
        {
            // 610 L=DF
            //     :{IF}L{THEN}L=DF{AND}IA(9)<>R {AND}IA(9)<>-1
            //     :{IF}L {PRINT}"DANGEROUS TO MOVE IN THE DARK!"
            bool L_isDark = DF_darkFlag;
            if (L_isDark)
            {
                if (IA[9] != R_currRoom && IA[9] != -1)
                {
                    Console.WriteLine("DANGEROUS TO MOVE IN THE DARK!");
                }
                else
                {
                    L_isDark = false;
                }
            }

            // 620 {IF}NV(1)<1{PRINT}"GIVE ME A DIRECTION TOO."
            //     :{GOTO}1040
            if (noun == 0)
            {
                Console.WriteLine("GIVE ME A DIRECTION TOO.");
                return;
            }

            // 630 K=RM(R,NV(1)-1)
            //     :{IF}K<1{IF}L{THEN}{PRINT}"I FELL DOWN AND BROKE MY NECK."
            //     :K=RL
            //     :DF=0
            //     :{ELSE}{PRINT}"I CAN'T GO IN THAT DIRECTION"
            //     :{GOTO}1040
            int K_toRoom = RM[R_currRoom, noun - 1];
            if (K_toRoom < 1 && L_isDark)
            {
                Console.WriteLine("I FELL DOWN AND BROKE MY NECK.");
                K_toRoom = RL_roomCount;
                DF_darkFlag = false;
            }
            else
            {
                Console.WriteLine("I CAN'T GO IN THAT DIRECTION");
                return;
            }

            // 640 {IF}{NOT}L{CLS}
            // 650 R=K
            //     :{GOSUB}240
            //     :{GOTO}1040
            Console.WriteLine();
            R_currRoom = K_toRoom;
            Look();
        }

        private static void GameIsOver()
        {
            // 850 {INPUT}"THE GAME IS NOW OVER"
            //     :{PRINT}"ANOTHER GAME";K$
            //     :{IF}{LEFT$}(K$,1)="N"{THEN}{END}{ELSE}{FOR}X=0{TO}IL
            //     :IA(X)=I2(X)
            //     :{NEXT}
            //     :{GOTO}100
            Console.WriteLine("THE GAME IS NOW OVER");
            Console.Write("ANOTHER GAME? ");
            string KS = Console.ReadLine();
            if (KS.ToUpper().StartsWith("N"))
            {
                gameOver = true;
            }
            Initialize();
        }

        //private static void DoActions()
        //{
        //    // 360 F2=-1:F=-1:F3=0
        //    bool F2_allCmds = true;
        //    F_commandNotOK = true;
        //    F3_commandPossible = false;
        //    // 362 FOR X=0 TO CL
        //    for (int X = 0; X <= CL_commandCount; X++)
        //    {
        //        // 363   V=CA(X,0)/150:IF NV(0)=0 THEN IF V<>0 THEN RETURN
        //        int V_verb = CA[X, 0] / 150;
        //        if (NV[0] == 0 && V_verb != 0)
        //        {
        //            return; // done doing background actions
        //        }
        //        // 370   IF NV(0)<>V THEN 980
        //        if (NV[0] != V_verb)
        //        {
        //            continue;
        //        }
        //        // 371   N=CA(X,0)-V*150
        //        int N_noun = CA[X, 0] - (V_verb * 150);
        //        // 380   IF NV(0)=0 THEN F=0:IF RND(100)<=N THEN 400 ELSE 980
        //        if (NV[0] == 0)
        //        {
        //            F_commandNotOK = false;
        //            if (sysRand.Next(100) > N_noun)
        //            {
        //                continue;
        //            }
        //        }
        //        // 390   IF N<>NV(1) AND N<>0 THEN 980
        //        else if (N_noun != NV[1] && N_noun != 0)
        //        {
        //            continue;
        //        }
        //        // 400   F2=-1:F=0:F3=-1
        //        F2_allCmds = true;
        //        F_commandNotOK = false;
        //        F3_commandPossible = true;
        //        // 401   FOR Y=1 TO 5
        //        for (int Y = 1; Y <= 5; Y++)
        //        {
        //            // 402     W=CA(X,Y):LL=W/20:K=W-LL*20:F1=-1
        //            int W = CA[X, Y];
        //            int LL = W / 20;
        //            int K = W - (LL * 20);
        //            bool F1_thisCmd = true;
        //            // 403     ON K+1 GOTO 550,430,450,470,490,500,510,520,530,540,410,420,440,460,480
        //            switch (K + 1)
        //            {
        //                case 1:
        //                    // 430     F1=IA(LL)=-1:GOTO 550
        //                    F1_thisCmd = (IA[LL] == -1); // item in inventory
        //                    break;
        //                case 2:
        //                    // 450     F1=IA(LL)=R:GOTO 550
        //                    F1_thisCmd = (IA[LL] == R_currRoom); // item in current room
        //                    break;
        //                case 3:
        //                    // 470     F1=IA(LL)=R OR IA(LL)=-1:GOTO 550
        //                    F1_thisCmd = (IA[LL] == R_currRoom || IA[LL] == -1); // item in current room or inventory
        //                    break;
        //                case 4:
        //                    // 490     F1=R=LL:GOTO 550
        //                    F1_thisCmd = (R_currRoom == LL); // current room is LL
        //                    break;
        //                case 5:
        //                    // 500     F1=IA(LL)<>R:GOTO 550
        //                    F1_thisCmd = (IA[LL] != R_currRoom); // item not in current room
        //                    break;
        //                case 6:
        //                    // 510     F1=IA(LL)<>-1:GOTO 550
        //                    F1_thisCmd = (IA[LL] != -1); // item not in inventory
        //                    break;
        //                case 7:
        //                    // 520     F1=R<>LL:GOTO 550
        //                    F1_thisCmd = (R_currRoom != LL); // current room not LL
        //                    break;
        //                case 8:
        //                    // 530     F1=SF(LL):F1=F1<>0:GOTO 550
        //                    F1_thisCmd = SF_systemFlags[LL]; // system flag is true
        //                    break;
        //                case 9:
        //                    // 540     F1=SF(LL):F1=F1=0:GOTO 550
        //                    F1_thisCmd = !SF_systemFlags[LL]; // system flag is false
        //                    break;
        //                case 10:
        //                    // 410     F1=-1
        //                    // 411     FOR Z=0 TO IL
        //                    // 412       IF IA(Z)=-1 THEN 550
        //                    // 413     NEXT Z
        //                    // 414     F1=0:GOTO 550
        //                    F1_thisCmd = false; // no items in inventory
        //                    for (int Z = 0; Z <= IL_itemCount; Z++)
        //                    {
        //                        if (IA[Z] == -1) // item in inventory
        //                        {
        //                            F1_thisCmd = true; // has some item in inventory
        //                            break;
        //                        }
        //                    }
        //                    break;
        //                case 11:
        //                    // 420     F1=0
        //                    // 421     FOR Z=0 TO IL
        //                    // 422       IF IA(Z)=-1 THEN 550
        //                    // 423     NEXT Z
        //                    // 424     F1=-1:GOTO 550
        //                    F1_thisCmd = true; // inventory is empty
        //                    for (int Z = 0; Z <= IL_itemCount; Z++)
        //                    {
        //                        if (IA[Z] == -1) // item in inventory
        //                        {
        //                            F1_thisCmd = false; // inventory not empty
        //                            break;
        //                        }
        //                    }
        //                    break;
        //                case 12:
        //                    // 440     F1=IA(LL)<>-1 AND IA(LL)<>R:GOTO 550
        //                    F1_thisCmd = (IA[LL] != -1 && IA[LL] != R_currRoom); // item not inventory or current room
        //                    break;
        //                case 13:
        //                    // 460     F1=IA(LL)<>0:GOTO 550
        //                    F1_thisCmd = (IA[LL] != 0); // item is somewhere
        //                    break;
        //                case 14:
        //                    // 480     F1=IA(LL)=0:GOTO 550
        //                    F1_thisCmd = (IA[LL] == 0); // item is nowhere
        //                    break;
        //            }
        //            // 550     F2=F2 AND F1:IF F2 THEN 551 ELSE 980
        //            F2_allCmds = (F2_allCmds && F1_thisCmd);
        //            if (!F2_allCmds)
        //            {
        //                break;
        //            }
        //            // 551   NEXT Y
        //        }
        //        if (!F2_allCmds)
        //        {
        //            break;
        //        }
        //        // 560   IP=0
        //        int IP = 0;
        //        // 561   FOR Y=1 TO 4
        //        for (int Y = 1; Y <= 4; Y++)
        //        {
        //            int K = ((Y - 1) / 2) + 6; // 6, 6, 7, 7
        //            int AC;
        //            // 562     K=(Y-1)/2+6:ON Y GOTO 570,580,570,580
        //            if (Y == 1 || Y == 3)
        //            {
        //                // 570    AC=CA(X,K)/150:GOTO 590
        //                AC = CA[X, K] / 150;
        //            }
        //            else
        //            {
        //                // 580     AC=CA(X,K)-CINT(CA(X,K)/150)*150
        //                AC = CA[X, K] - ((CA[X, K] / 150) * 150);
        //            }
        //            if (AC == 0)
        //            {
        //                // 591     IF AC=0 THEN 960
        //                continue;
        //            }
        //            if (AC > 101)
        //            {
        //                // 590     IF AC>101 THEN 600
        //                // 600     PRINT MS$(AC-50):GOTO 960
        //                Console.WriteLine(MSS_messages[AC - 50]);
        //                continue;
        //            }
        //            if (AC < 52)
        //            {
        //                // 592     IF AC<52 THEN PRINT MS$(AC):GOTO 960
        //                Console.WriteLine(MSS_messages[AC]);
        //                continue;
        //            }
        //            // vars for below
        //            int P;
        //            int L;
        //            int Z;
        //            // 593     ON AC-51 GOTO 660,700,740,760,770,780,790,760,810,830,840,850,860,870,890,920,930,940,950,710,750
        //            switch (AC - 51)
        //            {
        //                case 1: // 660
        //                        // 660     L=0
        //                    L = 0;
        //                    // 661     FOR Z=1 TO IL
        //                    for (int Z_temp = 1; Z_temp <= IL_itemCount; Z_temp++)
        //                    {
        //                        // 662       IF IA(Z)=-1
        //                        if (IA[Z_temp] == -1)
        //                        {
        //                            // LET L=L+1
        //                            L++; // count items carried
        //                        }
        //                        // 670     NEXT Z
        //                    }
        //                    // 680     IF L>=MX PRINT Z$:GOTO 970
        //                    if (L >= MX_maxCarry)
        //                    {
        //                        Console.WriteLine(ZS);
        //                        break;
        //                    }
        //                    // 690     GOSUB 1050
        //                    P = GetDataValue(ref IP, X);
        //                    //:IA(P)=-1
        //                    IA[P] = -1; // carry item
        //                    //:GOTO 960
        //                    break;
        //                case 2: // 700 - Move item to current room
        //                        // 700     GOSUB 1050
        //                    P = GetDataValue(ref IP, X);
        //                    //:IA(P)=R
        //                    IA[P] = R_currRoom;
        //                    //:GOTO 960
        //                    break;
        //                case 3: // 740 - Teleport to room P
        //                        // 740 GOSUB 1050
        //                    P = GetDataValue(ref IP, X);
        //                    //:R=P
        //                    R_currRoom = P;
        //                    //:GOTO 960
        //                    break;
        //                case 4: // 760
        //                case 8: // 760 - Item goes nowhere
        //                    // 760 GOSUB 1050
        //                    P = GetDataValue(ref IP, X);
        //                    //:IA(P)=0
        //                    IA[P] = 0;
        //                    //:GOTO 960
        //                    break;
        //                case 5: // 770
        //                        // 770     DF=-1
        //                    DF_roomIsDark = true;
        //                    //:GOTO 960
        //                    break;
        //                case 6: // 780
        //                        // 780     DF=0
        //                    DF_roomIsDark = false;
        //                    //:GOTO 960
        //                    break;
        //                case 7: // 790 - SF_systemFlags[P] = true
        //                        // 790     GOSUB 1050
        //                    P = GetDataValue(ref IP, X);
        //                    // 800     SF(P)=-1
        //                    SF_systemFlags[P] = true;
        //                    // 801 GOTO 960
        //                    break;
        //                case 9: // 810 - SF_systemFlags[P] = false
        //                        // 810     GOSUB 1050
        //                    P = GetDataValue(ref IP, X);
        //                    // 820 SF(P)=0
        //                    SF_systemFlags[P] = false;
        //                    // 821 GOTO 960
        //                    break;
        //                case 10: // 830
        //                         // 830     PRINT "I'M DEAD...":R=RL:DF=0:GOTO 860
        //                    Console.WriteLine("I'M DEAD...");
        //                    R_currRoom = RL_roomCount;
        //                    DF_roomIsDark = false;
        //                    // 860     GOSUB 240
        //                    ShowLocation();
        //                    //:GOTO 960
        //                    break;
        //                case 11: // 840
        //                         // 840     GOSUB 1050
        //                    P = GetDataValue(ref IP, X);
        //                    // 841 L=P
        //                    L = P;
        //                    // 842 GOSUB 1050
        //                    P = GetDataValue(ref IP, X);
        //                    // 843 IA(L)=P
        //                    IA[L] = P;
        //                    //GOTO 960
        //                    break;
        //                case 12: // 850
        //                         // 850     PRINT "THE GAME IS NOW OVER"
        //                    Console.WriteLine("THE GAME IS NOW OVER");
        //                    //:INPUT "ANOTHER GAME";K$
        //                    Console.Write("ANOTHER GAME? ");
        //                    string KS = Console.ReadLine();
        //                    //:IF LEFT$(K$,1)="N" THEN
        //                    if (KS.ToUpper().StartsWith("N"))
        //                    {
        //                        // END
        //                        gameOver = true;
        //                        // ???
        //                    }
        //                    // 851     FOR Z=0 TO IL
        //                    for (int Z_Temp = 0; Z_Temp <= IL_itemCount; Z_Temp++)
        //                    {
        //                        // 852       IA(Z)=I2(Z)
        //                        IA[Z_Temp] = I2[Z_Temp];
        //                        // 853     NEXT Z
        //                    }
        //                    // 854     GOTO 100
        //                    break;
        //                case 13: // 860
        //                         // 860     GOSUB 240
        //                    ShowLocation();
        //                    //:GOTO 960
        //                    break;
        //                case 14: // 870
        //                         // 870     L=0
        //                    int L_treasureCount = 0;
        //                    // 871     FOR Z=1 TO IL
        //                    for (int z = 1; z <= IL_itemCount; z++)
        //                    {
        //                        // 872       IF IA(Z)=TR THEN IF LEFT$(IA$(Z),1)="*" THEN 
        //                        if (IA[z] == TR && IAS_itemDescriptions[z].StartsWith("*"))
        //                        {
        //                            //LET L=L+1
        //                            L_treasureCount++;
        //                            // 880     NEXT Z
        //                        }
        //                    }
        //                    // 881     PRINT "I'VE STORED";L;"TREASURES."
        //                    Console.WriteLine($"I'VE STORED {L_treasureCount} TREASURES.");
        //                    //PRINT"ON A SCALE OF 0 TO 100 THAT RATES A";CINT(L/TT*100)
        //                    Console.WriteLine($"ON A SCALE OF 0 TO 100 THAT RATES A {(L_treasureCount / TT_TotalTreasures * 100)}");
        //                    // 882     IF L=TT THEN PRINT "WELL DONE.":GOTO 850 ELSE 960
        //                    if (L_treasureCount == TT_TotalTreasures)
        //                    {
        //                        Console.WriteLine("WELL DONE.");
        //                        // GOTO 850
        //                        gameOver = true;
        //                        // ???
        //                    }
        //                    break;
        //                case 15: // 890
        //                         // 890     PRINT "I'M CARRYING:"
        //                    Console.WriteLine("I'M CARRYING:");
        //                    //:K$="NOTHING"
        //                    string KS_temp = "NOTHING";
        //                    // 891     FOR Z=0 TO IL
        //                    for (int z = 0; z <= IL_itemCount; z++)
        //                    {
        //                        // 892       IF IA(Z)<>-1 THEN 910
        //                        if (!(IA[z] != -1))
        //                        {
        //                            // 893       GOSUB 280
        //                            ShowLocation();
        //                            //:IF LEN(TP$)+POS(0)>63 THEN PRINT
        //                            // ???
        //                            // 900       PRINT TP$;".",;:K$=""
        //                            KS_temp = "";
        //                        }
        //                        // 910     NEXT Z
        //                    }
        //                    // 911     PRINT K$
        //                    Console.WriteLine(KS_temp);
        //                    //:GOTO 960
        //                    break;
        //                case 16: // 920
        //                         // 920     P=0:GOTO 800
        //                    P = 0;
        //                    // 800     SF(P)=-1:GOTO 960
        //                    SF_systemFlags[P] = true;
        //                    break;
        //                case 17: // 930
        //                         // 930     P=0:GOTO 820
        //                    P = 0;
        //                    // 820     SF(P)=0:GOTO 960
        //                    SF_systemFlags[P] = false;
        //                    break;
        //                case 18: // 940
        //                         // 940     LX=LT:IA(9)=-1:GOTO 960
        //                    LX_lightRemaining = LT_lightTotal;
        //                    IA[9] = -1;
        //                    break;
        //                case 19: // 950
        //                         // 950     CLS:GOTO 960
        //                    Console.Clear();
        //                    break;
        //                case 20: // 710
        //                         // 710     PRINT "SAVING GAME"
        //                    Console.WriteLine("SAVING GAME");
        //                    // 711     REM IF D=-1 THEN INPUT "READY OUTPUT TAPE";K$:PRINT INT(IL*5/60)+1;"MINUTES" ELSE OPEN"O",D,SV$
        //                    // 720     REM PRINT #D,SF,LX,DF,R
        //                    // 721     REM FOR W=0 TO IL
        //                    // 722     REM   PRINT #D,IA(W)
        //                    // 723     REM NEXT W:IF D<>-1CLOSE
        //                    // 730     GOTO 960
        //                    break;
        //                case 21: // 750 - Swap two values
        //                         // 750     GOSUB 1050
        //                    P = GetDataValue(ref IP, X);
        //                    //:L=P
        //                    L = P;
        //                    //:GOSUB 1050
        //                    P = GetDataValue(ref IP, X);
        //                    //:Z=IA(P)
        //                    Z = IA[P];
        //                    //:IA(P)=IA(L)
        //                    IA[P] = IA[L];
        //                    //:IA(L)=Z
        //                    IA[L] = Z;
        //                    //:GOTO 960
        //                    break;
        //            }
        //            // 610     L=DF:IF L THEN L=DF AND IA(9)<>R AND IA(9)<>-1:IF L PRINT "DANGEROUS TO MOVE IN THE DARK!"
        //            // 620     IF NV(1)<1 PRINT "GIVE ME A DIRECTION TOO.":GOTO 1040
        //            // 630     K=RM(R,NV(1)-1):IF K<1 IF L THEN PRINT "I FELL DOWN AND BROKE MY NECK.":K=RL:DF=0:ELSE PRINT "I CAN'T GO IN THAT DIRECTION":GOTO 1040
        //            // 640     IF NOT L CLS
        //            // 650     R=K:GOSUB 240:GOTO 1040
        //            // 960   NEXT Y
        //        }
        //        // 970   IF NV(0)<>0 THEN 990
        //        if (NV[0] != 0)
        //        {
        //            break;
        //        }
        //        // 980 NEXT X
        //    }
        //    // 990 REM
        //    // 361 IF NV(0)=1 AND NV(1)<7 THEN 610
        //    if (!(NV[0] == 1 && NV[1] < 7))
        //    {
        //        // todo handle GO direction
        //    }
        //}
    }
}
