// Data.cs - 05/21/2018

namespace PirateAdventure
{
    partial class Program
    {
        private const int IL_itemCount = 60;
        private const int CL_commandCount = 151;
        private const int NL_wordCount = 59;
        private const int RL_roomCount = 33;
        private const int MX_maxCarry = 5;
        private const int AR_startRoom = 1;
        private const int TT_TotalTreasures = 2;
        private const int LN_wordSize = 3;
        private const int LT_lightTotal = 200;
        private const int ML_messageCount = 71;
        private const int TR_treasureRoom = 1;
        private const int FC_flagCount = 16;

        private static bool[] SF_systemFlags = new bool[FC_flagCount + 1]; // DIM SF(15)

        private static int[,] CA = new int[CL_commandCount + 1, 8] // [CL,7]
            {
                // random events
                { 80, 422, 342, 420, 340, 0, 16559, 8850 },
                { 80, 462, 482, 460, 0, 0, 15712, 1705 },
                { 100, 521, 552, 540, 229, 220, 203, 8700 },
                { 3, 483, 0, 0, 0, 0, 15712, 0 },
                { 100, 284, 0, 0, 0, 0, 8550, 0 },
                { 100, 28, 663, 403, 40, 0, 8700, 0 },
                { 100, 48, 20, 660, 740, 220, 9055, 10902 },
                { 100, 28, 20, 0, 0, 0, 3810, 0 },
                { 100, 8, 700, 720, 0, 0, 10868, 0 },
                { 100, 48, 40, 640, 400, 300, 9055, 8305 },
                { 25, 664, 0, 0, 0, 0, 4263, 0 },
                { 40, 104, 886, 0, 0, 0, 4411, 0 },
                { 80, 242, 502, 820, 80, 240, 9321, 10109 },
                { 100, 8, 140, 80, 500, 0, 10262, 8850 },
                { 35, 421, 846, 420, 200, 0, 5162, 0 },
                { 100, 129, 120, 0, 0, 0, 6508, 0 },
                { 50, 242, 982, 820, 440, 240, 9321, 8850 },
                { 35, 483, 69, 0, 0, 0, 15705, 0 },
                { 10, 483, 249, 0, 0, 0, 15706, 0 },
                { 50, 484, 1073, 1086, 0, 0, 17661, 9150 },
                { 50, 204, 1086, 0, 0, 0, 16711, 0 },
                { 10, 209, 1040, 1060, 300, 1100, 10872, 10050 },
                { 10, 208, 1040, 1060, 89, 0, 10867, 0 },
                { 85, 483, 8, 0, 0, 0, 15719, 10200 },
                { 100, 8, 0, 0, 0, 0, 10200, 0 },
                { 100, 104, 0, 0, 0, 0, 8550, 0 },
                { 80, 462, 282, 280, 1160, 0, 1422, 0 },
                // verb-noun events
                { 158, 82, 60, 0, 0, 0, 8170, 9600 },
                { 4510, 61, 0, 0, 0, 0, 300, 0 },
                { 163, 22, 100, 0, 0, 0, 8170, 9600 },
                { 8100, 0, 0, 0, 0, 0, 16200, 0 },
                { 4800, 104, 120, 61, 0, 0, 10507, 8164 },
                { 4800, 107, 100, 61, 89, 0, 10507, 8164 },
                { 4063, 22, 0, 0, 0, 0, 647, 0 },
                { 5570, 161, 203, 160, 180, 0, 10870, 1264 },
                { 6170, 181, 180, 160, 0, 0, 8302, 0 },
                { 6300, 104, 0, 0, 0, 0, 900, 0 },
                { 1529, 442, 465, 440, 0, 0, 7800, 0 },
                { 1529, 442, 462, 0, 0, 0, 760, 9150 },
                { 183, 322, 180, 0, 0, 0, 8170, 9600 },
                { 1538, 262, 242, 0, 0, 0, 1800, 0 },
                { 1538, 262, 245, 260, 0, 0, 7800, 0 },
                { 5888, 262, 242, 0, 0, 0, 1800, 0 },
                { 5888, 262, 245, 0, 0, 0, 1950, 0 },
                { 6188, 262, 245, 541, 260, 560, 2155, 7950 },
                { 5888, 261, 0, 0, 0, 0, 2400, 0 },
                { 4088, 561, 0, 0, 0, 0, 2400, 0 },
                { 4088, 263, 0, 0, 0, 0, 2713, 0 },
                { 4088, 562, 580, 109, 100, 249, 2303, 8700 },
                { 4088, 249, 562, 108, 900, 240, 6203, 8700 },
                { 4088, 248, 562, 0, 0, 0, 6600, 0 },
                { 4068, 103, 69, 0, 0, 0, 646, 0 },
                { 4068, 103, 68, 0, 0, 0, 6600, 0 },
                { 5887, 342, 0, 0, 0, 0, 2550, 0 },
                { 5887, 362, 0, 0, 0, 0, 2713, 0 },
                { 5887, 382, 0, 0, 0, 0, 2100, 0 },
                { 159, 382, 320, 0, 0, 0, 8170, 9600 },
                { 6187, 342, 362, 0, 0, 0, 2550, 0 },
                { 6187, 345, 362, 541, 360, 380, 8303, 10050 },
                { 3461, 503, 0, 0, 0, 0, 172, 0 },
                { 3750, 0, 0, 0, 0, 0, 9900, 0 },
                { 1528, 0, 0, 0, 0, 0, 9900, 0 },
                { 4108, 1143, 1012, 0, 0, 0, 646, 0 },
                { 6450, 0, 0, 0, 0, 0, 2853, 0 },
                { 4510, 66, 0, 0, 0, 0, 2720, 0 },
                { 4950, 0, 0, 0, 0, 0, 9750, 0 },
                { 5114, 0, 0, 0, 0, 0, 10650, 0 },
                { 7092, 592, 0, 0, 0, 0, 2745, 0 },
                { 185, 284, 140, 0, 0, 0, 8156, 10564 },
                { 4098, 1054, 0, 0, 0, 0, 647, 17550 },
                { 4098, 1053, 0, 0, 0, 0, 647, 17400 },
                { 4083, 322, 0, 0, 0, 0, 647, 0 },
                { 4095, 762, 0, 0, 0, 0, 647, 0 },
                { 195, 782, 921, 0, 0, 0, 2727, 0 },
                { 195, 762, 261, 0, 0, 0, 2727, 0 },
                { 6900, 0, 0, 0, 0, 0, 9450, 0 },
                { 1526, 602, 0, 0, 0, 0, 2723, 0 }, // 76
                { 1541, 621, 602, 640, 520, 600, 7853, 8250 },
                { 195, 782, 661, 0, 0, 0, 2727, 0 },
                { 7092, 623, 583, 303, 643, 20, 8700, 0 },
                { 7092, 0, 0, 0, 0, 0, 3750, 0 },
                { 200, 722, 220, 0, 0, 0, 10554, 9600 },
                { 195, 762, 61, 0, 0, 0, 2727, 0 },
                { 4050, 0, 0, 0, 0, 0, 10564, 0 },
                { 1526, 523, 520, 0, 0, 0, 7800, 0 },
                { 195, 762, 340, 0, 0, 0, 8126, 8464 },
                { 195, 782, 360, 0, 0, 0, 8157, 10564 },
                { 7530, 404, 242, 1053, 89, 0, 17250, 0 },
                { 4800, 0, 0, 0, 0, 0, 450, 0 },
                { 5868, 103, 200, 69, 60, 0, 4553, 8700 },
                { 5868, 68, 0, 0, 0, 0, 494, 0 },
                { 1546, 146, 0, 0, 0, 0, 4800, 0 },
                { 1546, 802, 141, 140, 840, 0, 8302, 0 },
                { 2746, 841, 840, 140, 0, 0, 8302, 4950 },
                { 3496, 802, 0, 0, 0, 0, 811, 0 },
                { 3496, 841, 840, 140, 0, 0, 811, 8302 },
                { 7366, 822, 820, 240, 400, 0, 5305, 9300 },
                { 5861, 503, 0, 0, 0, 0, 2100, 0 },
                { 8411, 501, 500, 140, 0, 0, 5459, 7833 },
                { 192, 742, 400, 0, 0, 0, 8170, 9600 },
                { 201, 404, 88, 420, 240, 242, 8170, 8071 },
                { 201, 404, 89, 120, 0, 0, 8170, 9600 },
                { 7530, 404, 245, 0, 0, 0, 2737, 0 },
                { 7530, 404, 912, 0, 0, 0, 2738, 0 },
                { 7530, 404, 89, 80, 740, 420, 5908, 9300 },
                { 7530, 404, 88, 80, 740, 120, 5910, 9300 },
                { 7671, 0, 0, 0, 0, 0, 6000, 0 },
                { 4553, 903, 0, 0, 0, 0, 6300, 0 },
                { 1350, 0, 0, 0, 0, 0, 6000, 0 },
                { 1510, 62, 60, 0, 0, 0, 7800, 0 },
                { 5860, 63, 0, 0, 0, 0, 18000, 0 },
                { 201, 404, 88, 420, 0, 0, 8170, 9600 },
                { 186, 284, 360, 0, 0, 0, 8170, 9600 },
                { 1539, 482, 242, 0, 0, 0, 1800, 0 },
                { 1539, 482, 480, 0, 0, 0, 7904, 16800 },
                { 194, 682, 300, 0, 0, 0, 8170, 9600 },
                { 174, 149, 464, 140, 0, 0, 8751, 0 },
                { 174, 160, 0, 0, 0, 0, 8751, 0 },
                { 7800, 444, 940, 921, 952, 0, 10548, 8014 },
                { 7800, 124, 921, 0, 0, 0, 7350, 0 },
                { 7800, 424, 992, 980, 921, 0, 10553, 7264 },
                { 8250, 104, 0, 0, 0, 0, 10505, 9600 },
                { 7800, 464, 148, 1140, 921, 1152, 10553, 7264 },
                { 1541, 643, 640, 0, 0, 0, 7800, 0 },
                { 163, 104, 40, 0, 0, 0, 8170, 9600 },
                { 6300, 44, 0, 0, 0, 0, 15450, 0 },
                { 4534, 583, 0, 0, 0, 0, 4650, 0 },
                { 6187, 702, 541, 0, 0, 0, 2713, 16050 },
                { 5887, 702, 0, 0, 0, 0, 2713, 0 },
                { 5887, 0, 722, 0, 0, 0, 2100, 0 },
                { 198, 1022, 480, 0, 0, 0, 8170, 9600 },
                { 157, 2, 24, 40, 0, 0, 8170, 9600 },
                { 1510, 44, 60, 40, 80, 85, 7801, 10800 },
                { 1532, 302, 208, 300, 0, 0, 7800, 0 },
                { 1532, 302, 209, 0, 0, 0, 2813, 0 },
                { 1532, 305, 0, 0, 0, 0, 10518, 7564 },
                { 8411, 841, 840, 140, 0, 0, 8922, 0 },
                { 165, 1122, 500, 0, 0, 0, 8170, 9600 },
                { 1392, 0, 0, 0, 0, 0, 6000, 0 },
                { 6300, 284, 0, 0, 0, 0, 16350, 0 },
                { 8582, 0, 0, 0, 0, 0, 17700, 0 },
                { 7800, 921, 209, 302, 200, 0, 8700, 0 },
                { 7950, 0, 0, 0, 0, 0, 2700, 0 },
                { 5908, 621, 1143, 1000, 0, 0, 4553, 0 },
                { 5266, 0, 0, 0, 0, 0, 1800, 0 },
                { 6300, 224, 0, 0, 0, 0, 17517, 17850 },
                { 1200, 0, 0, 0, 0, 0, 17100, 0 },
                { 6300, 124, 0, 0, 0, 0, 16350, 0 },
                { 4350, 208, 1040, 1060, 0, 0, 10919, 0 },
                { 6300, 184, 242, 0, 0, 0, 3600, 0 },
                { 7800, 921, 160, 140, 0, 0, 7410, 9000 },
                { 6300, 0, 0, 0, 0, 0, 450, 0 }
            };

        private static string[,] NVS = new string[NL_wordCount + 1, 2] // NV$(NL,1)
            {
                { "AUT", "ANY" },    // 0
                { "GO", "NORTH" },   // 1
                { "*CLI", "SOUTH" }, // 2
                { "*WAL", "EAST" },  // 3
                { "*RUN", "WEST" },  // 4
                { "*ENT", "UP" },    // 5
                { "*PAC", "DOWN" },  // 6
                { "*FOL", "STA" },   // 7
                { "SAY", "PAS" },    // 8
                { "SAI", "HAL" },    // 9
                { "GET", "BOO" },    // 10
                { "*TAK", "BOT" },   // 11
                { "*CAT", "*RUM" },  // 12
                { "*PIC", "WIN" },   // 13
                { "*REM", "GAM" },   // 14
                { "*WEA", "MON" },   // 15
                { "*PUL", "PIR" },   // 16
                { "FLY", "ARO" },    // 17
                { "DRO", "BAG" },    // 18
                { "*REL", "*DUF" },  // 19
                { "*THR", "TOR" },   // 20
                { "*LEA", "OFF" },   // 21
                { "*GIV", "MAT" },   // 22
                { "DRI", "YOH" },    // 23
                { "*EAT", "30" },    // 24
                { "INV", "LUM" },    // 25
                { "SAI", "RUG" },    // 26
                { "LOO", "KEY" },    // 27
                { "*SHO", "INV" },   // 28
                { "WAI", "DUB" },    // 29
                { "REA", "SAI" },    // 30
                { ".", "FIS" },      // 31
                { "YOH", "ANC" },    // 32
                { "SCO", "SHA" },    // 33
                { "SAV", "PLA" },    // 34
                { "KIL", "CAV" },    // 35
                { "*ATT", "PAT" },   // 36
                { "LIG", "DOO" },    // 37
                { ".", "CHE" },      // 38
                { "OPE", "PAR" },    // 39
                { "*SMA", "HAM" },   // 40
                { "UNL", "NAI" },    // 41
                { "HEL", "BOA" },    // 42
                { "AWA", "*SHI" },   // 43
                { "*BUN", "SHE" },   // 44
                { "", "CRA" },       // 45
                { "QUI", "WAT" },    // 46
                { "BUI", "*SAL" },   // 47
                { "*MAK", "LAG" },   // 48
                { "WAK", "*TID" },   // 49
                { "SET", "PIT" },    // 50
                { "CAS", "SHO" },    // 51
                { "DIG", "*BEA" },   // 52
                { "BUR", "MAP" },    // 53
                { "FIN", "PAC" },    // 54
                { "JUM", "BON" },    // 55
                { "EMP", "HOL" },    // 56
                { "WEI", "SAN" },    // 57
                { "", "BOX" },       // 58
                { "", "SNE" },       // 59
            };

        private static int[,] RM = new int[RL_roomCount + 1, 6] // RM(RL,5)
            {
                { 0, 0, 0, 0, 0, 0 },  // 
                { 0, 0, 0, 0, 0, 0 },  // APARTMENT IN LONDON
                { 0, 0, 0, 0, 0, 1 },  // ALCOVE
                { 0, 0, 4, 2, 0, 0 },  // SECRET PASSAGEWAY
                { 0, 0, 0, 3, 0, 0 },  // MUSTY ATTIC
                { 0, 0, 0, 0, 0, 0 },  // * I'M OUTSIDE AN OPEN WINDOW ON A LEDGE ON THE SIDE OF AVERY TALL BUILDING
                { 0, 0, 8, 0, 0, 0 },  // SANDY BEACH ON A TROPICAL ISLE
                { 0, 12, 13, 14, 0, 11 },  // MAZE OF CAVES
                { 0, 0, 14, 6, 0, 0 },  // MEADOW
                { 0, 0, 0, 8, 0, 0 },  // GRASS SHACK
                { 10, 24, 10, 10, 0, 0 },  // *I'M IN THE OCEAN
                { 0, 0, 0, 0, 7, 0 },  // PIT
                { 7, 0, 14, 13, 0, 0 },  // MAZE OF CAVES
                { 7, 14, 12, 19, 0, 0 },  // MAZE OF CAVES
                { 0, 0, 0, 8, 0, 0 },  // *I'M AT THE FOOT OF A CAVE RIDDEN HILL.A PATH LEADS TO THE TOP
                { 17, 0, 0, 0, 0, 0 },  // TOOL SHED
                { 0, 0, 17, 0, 0, 0 },  // LONG HALLWAY
                { 0, 0, 0, 16, 0, 0 },  // LARGE CAVERN
                { 0, 0, 0, 0, 0, 14 },  // *I'M ON TOP OF A HILL. BELOW IS PIRATES ISLAND. ACROSS THE SEA OFF IN THE DISTANCE I SEE *TREASURE* ISLAND
                { 0, 14, 14, 13, 0, 0 },  // MAZE OF CAVES
                { 0, 0, 0, 0, 0, 0 },  // *I'M ABOARD PIRATE SHIP ANCHORED OFF SHORE
                { 0, 22, 0, 0, 0, 0 },  // * I'M ON THE BEACH AT TREASURE ISLAND
                { 21, 0, 23, 0, 0, 0 },  // SPOOKY OLD GRAVEYARD FILLED WITH PILESOF EMPTY AND BROKEN RUM BOTTLES
                { 0, 0, 0, 22, 0, 0 },  // LARGE BARREN FIELD
                { 10, 6, 6, 6, 0, 0 },  // SHALLOW LAGOON. TO THE NORTH IS THE OCEAN
                { 0, 0, 0, 23, 0, 0 },  // SACKED AND DESERTED MONASTARY
                { 0, 0, 0, 0, 0, 0 },  // 
                { 0, 0, 0, 0, 0, 0 },  // 
                { 0, 0, 0, 0, 0, 0 },  // 
                { 0, 0, 0, 0, 0, 0 },  // 
                { 0, 0, 0, 0, 0, 0 },  // 
                { 0, 0, 0, 0, 0, 0 },  // 
                { 0, 0, 0, 0, 0, 0 },  // .
                { 0, 0, 0, 0, 0, 0 },  // *WELCOME TO NEVER NEVER LAND
            };

        private static string[] RSS = new string[RL_roomCount + 1] // RS$(RL)
            {
                "",
                "APARTMENT IN LONDON/APARTMENT/",
                "ALCOVE",
                "SECRET PASSAGEWAY",
                "MUSTY ATTIC",
                "*I'M OUTSIDE AN OPEN WINDOW ON A LEDGE ON THE SIDE OF A VERY TALL BUILDING/LEDGE OF TALL BUILDING/",
                "SANDY BEACH ON A TROPICAL ISLE/SANDY BEACH/",
                "MAZE OF CAVES",
                "MEADOW",
                "GRASS SHACK",
                "*I'M IN THE OCEAN/OCEAN/",
                "PIT",
                "MAZE OF CAVES",
                "MAZE OF CAVES",
                "*I'M AT THE FOOT OF A CAVE RIDDEN HILL. A PATH LEADS TO THE TOP/FOOT OF HILL/",
                "TOOL SHED",
                "LONG HALLWAY",
                "LARGE CAVERN",
                "*I'M ON TOP OF A HILL. BELOW IS PIRATES ISLAND. ACROSS THE SEA OFF IN THE DISTANCE I SEE *TREASURE* ISLAND/TOP OF HILL/",
                "MAZE OF CAVES",
                "*I'M ABOARD PIRATE SHIP ANCHORED OFF SHORE/ON PIRATE SHIP/",
                "*I'M ON THE BEACH AT TREASURE ISLAND/BEACH AT TREASURE ISLAND/",
                "SPOOKY OLD GRAVEYARD FILLED WITH PILES OF EMPTY AND BROKEN RUM BOTTLES/GRAVEYARD/",
                "LARGE BARREN FIELD",
                "SHALLOW LAGOON. TO THE NORTH IS THE OCEAN/LAGOON/",
                "SACKED AND DESERTED MONASTARY/MONASTARY/",
                "",
                "",
                "",
                "",
                "",
                "",
                ".",
                "*WELCOME TO NEVER NEVER LAND/NEVER NEVER LAND/"
            };

        private static string[] MSS_messages = new string[ML_messageCount + 1] // MS$(ML)
            {
                "",
                "THERE'S A STRANGE SOUND",
                "THE NAME OF THE BOOK IS -TREASURE ISLAND- THERE'S A WORD ENGRAVED IN THE FLYLEAF -YOHO- AND A MESSAGE -LONG JOHN SILVER LEFT 2 TREASURES ON TREASURE ISLAND!-",
                "NOTHING HAPPENS",
                "THERE'S SOMETHING THERE ALRIGHT. MAYBE I SHOULD...",
                "THAT'S NOT VERY SAFE",
                "YOU MAY NEED MAGIC HERE",
                "EVERYTHING SPINS AROUND AND SUDDENLY YOU ARE ELSEWHERE...",
                "TORCH IS LIT",
                "I WAS WRONG. I GUESS ITS NOT A MONGOOSE CAUSE THE SNAKES BIT IT.",
                "I'M SNAKE BIT",
                "PARROT ATTACKS SNAKES AND DRIVES THEM OFF",
                "PIRATE WON'T LET ME",
                "ITS LOCKED",
                "ITS OPEN",
                "THERE ARE A SET OF PLANS IN IT",
                "NOT WHILE I'M CARRYING IT",
                "CROCS STOP ME",
                "SORRY I CAN'T",
                "WRONG GAME YOU SILLY GOOSE!",
                "I DON'T HAVE IT",
                "PIRATE GRABS RUM AND SCUTTLES OFF CHORTLING",
                "...I THINK ITS ME. HEE HEE.",
                "ITS NAILED TO THE FLOOR!",
                "-MAGIC WORD- HO AND A... (WORK ON IT. YOU'LL GET IT)",
                "NO. SOMETHING IS MISSING!",
                "IT WAS A TIGHT SQUEEZE!",
                "SOMETHING WON'T FIT",
                "SINCE NOTHING IS HAPPENING",
                "I SLIPPED AND FELL...",
                "SOMETHING FALLS OUT",
                "THEY'RE PLANS TO BUILD JOLLY ROGER (A PIRATE SHIP!). YOU'LL NEED HAMMER NAILS LUMBER ANCHOR SAILS AND KEEL.",
                "I'VE NO CONTAINER",
                "IT SOAKS INTO THE GROUND",
                "TOO DRY. FISH VANISH.",
                "PIRATE AWAKENS. SAYS -AYE MATEY WE BE CASTING OFF SOON- HE THEN VANISHES",
                "WHAT A WASTE...",
                "I'VE NO CREW",
                "PIRATE SAYS -AYE MATEY WE BE NEEDING A MAP FIRST-",
                "AFTER A MONTH AT SEA WE SET ANCHOR OFF OF A SANDY BEACH. ALL ASHORE WHO'S GOING ASHORE...",
                "TRY -WEIGH ANCHOR-",
                "THERE'S A MAP IN IT",
                "ITS A MAP TO TREASURE ISLAND. AT THE BOTTOM IT SAYS -30 PACES AND THEN DIG!-",
                "* WELCOME TO -PIRATES ADVENTURE- BY SCOTT & ALEXIS ADAMS *",
                "ITS EMPTY",
                "I'VE NO PLANS!",
                "OPEN IT?",
                "GO THERE?",
                "I FOUND SOMETHING!",
                "I DIDN'T FIND ANYTHING",
                "I DON'T SEE IT HERE",
                "OK I WALKED OFF 30 PACES.",
                "CONGRATULATIONS !!! BUT YOUR ADVENTURE IS NOT OVER YET...",
                "READING EXPANDS THE MIND",
                "THE PARROT CRYS",
                "-CHECK THE BAG MATEY-",
                "-CHECK THE CHEST MATEY-",
                "FROM THE OTHER SIDE!",
                "OPEN THE BOOK!",
                "THERE'S MULTIPLE EXITS HERE!",
                "CROCS EAT FISH AND LEAVE",
                "I'M UNDERWATER. I CAN'T SWIM. BLUB BLUB...",
                "-PIECES OF EIGHT-",
                "ITS STUCK IN THE SAND",
                "USE 1 WORD",
                "PIRATE SAYS -AYE MATEY WE BE WAITING FOR THE TIDE TO COME IN-",
                "THE TIDE IS OUT",
                "THE TIDE IS COMING IN",
                "ABOUT 20 POUNDS. TRY -SET SAIL-",
                "-TIDES A CHANGING MATEY-",
                "NOTE HERE -I BE LIKING PARROTS. THEY BE SMART MATEY-",
                "PIRATE FOLLOWS YOU ASHORE AS IF HE IS WAITING FOR SOMETHING."
            };

        private static string[] IAS_itemDescriptions = new string[IL_itemCount + 1] // IA$(IL)
            {
                "FLIGHT OF STAIRS",
                "OPEN WINDOW",
                "BOOKS IN A BOOKCASE",
                "LARGE LEATHER BOUND BOOK/BOO/",
                "BOOKCASE WITH A SECRET PASSAGE BEHIND IT",
                "PIRATE'S DUFFLE BAG/BAG/",
                "SIGN ON WALL -RETURN TREASURES HERE. SAY SCORE- SIGN BY STAIRS -ANTONYM OF LIGHT IS UNLIGHT-",
                "EMPTY BOTTLE/BOT/",
                "UNLIT TORCH/TOR/",
                "LIT TORCH/TOR/",
                "MATCHES/MAT/",
                "SMALL SHIP'S KEEL AND MAST",
                "WICKED LOOKING PIRATE",
                "TREASURE CHEST/CHE/",
                "MONGOOSE/MON/",
                "RUSTY ANCHOR/ANC/",
                "GRASS SHACK",
                "MEAN AND HUNGRY LOOKING CROCODILES",
                "LOCKED DOOR",
                "OPEN DOOR WITH HALL BEYOND",
                "PILE OF SAILS/SAI/",
                "FISH/FIS/",
                "*DUBLEONS*/DUB/",
                "DEADLY MAMBA SNAKES/SNA/",
                "PARROT/PAR/",
                "BOTTLE OF RUM/BOT/",
                "RUG/RUG/",
                "RING OF KEYS/KEY/",
                "OPEN TREASURE CHEST/CHE/",
                "SET OF PLANS/PLA/",
                "RUG",
                "CLAW HAMMER/HAM/",
                "NAILS/NAI/",
                "PILE OF PRECUT LUMBER/LUM/",
                "TOOL SHED",
                "LOCKED DOOR",
                "OPEN DOOR WITH PIT BEYOND",
                "PIRATE SHIP",
                "ROCK WALL WITH NARROW CRACK IN IT",
                "NARROW CRACK IN THE ROCK",
                "SALT WATER",
                "SLEEPING PIRATE",
                "BOTTLE OF SALT WATER/BOT/",
                "PIECES OF BROKEN RUM BOTTLES",
                "NON-SKID SNEAKERS/SNE/",
                "MAP/MAP/",
                "SHOVEL/SHO/",
                "MOULDY OLD BONES/BON/",
                "SAND/SAN/",
                "BOTTLES OF RUM/BOT/",
                "*RARE OLD PRICELESS STAMPS*/STA/",
                "LAGOON",
                "THE TIDE IS OUT",
                "THE TIDE IS COMING IN",
                "WATER WINGS/WIN/",
                "FLOTSAM AND JETSAM",
                "MONASTARY",
                "PLAIN WOODEN BOX/BOX/",
                "DEAD SQUIRREL",
                "",
                "",
            };

        private static int[] IA = new int[IL_itemCount + 1]; // IA(IL)

        private static int[] I2 = new int[IL_itemCount + 1] // I2(IL)
            {
                1,  // 00 FLIGHT OF STAIRS
                2,  // 01 OPEN WINDOW
                2,  // 02 BOOKS IN A BOOKCASE
                0,  // 03 LARGE LEATHER BOUND BOOK/BOO/
                0,  // 04 BOOKCASE WITH A SECRET PASSAGE BEHIND IT
                4,  // 05 PIRATE'S DUFFLE BAG/BAG/
                1,  // 06 SIGN ON WALL -RETURN TREASURES HERE. SAY SCORE- SIGN BY STAIRS -ANTONYM OF LIGHT IS UNLIGHT-
                0,  // 07 EMPTY BOTTLE/BOT/
                4,  // 08 UNLIT TORCH/TOR/
                0,  // 09 LIT TORCH/TOR/
                0,  // 10 MATCHES/MAT/
                6,  // 11 SMALL SHIP'S KEEL AND MAST
                9,  // 12 WICKED LOOKING PIRATE
                9,  // 13 TREASURE CHEST/CHE/
                8,  // 14 MONGOOSE/MON/
                24, // 15 RUSTY ANCHOR/ANC/
                8,  // 16 GRASS SHACK
                11, // 17 MEAN AND HUNGRY LOOKING CROCODILES
                11, // 18 LOCKED DOOR
                0,  // 19 OPEN DOOR WITH HALL BEYOND
                17, // 20 PILE OF SAILS/SAI/
                10, // 21 FISH/FIS/
                25, // 22 *DUBLEONS*/DUB/
                25, // 23 DEADLY MAMBA SNAKES/SNA/
                9,  // 24 PARROT/PAR/
                1,  // 25 BOTTLE OF RUM/BOT/
                0,  // 26 RUG/RUG/
                0,  // 27 RING OF KEYS/KEY/
                0,  // 28 OPEN TREASURE CHEST/CHE/
                0,  // 29 SET OF PLANS/PLA/
                1,  // 30 RUG
                15, // 31 CLAW HAMMER/HAM/
                0,  // 32 NAILS/NAI/
                17, // 33 PILE OF PRECUT LUMBER/LUM/
                17, // 34 TOOL SHED
                16, // 35 LOCKED DOOR
                0,  // 36 OPEN DOOR WITH PIT BEYOND
                0,  // 37 PIRATE SHIP
                18, // 38 ROCK WALL WITH NARROW CRACK IN IT
                17, // 39 NARROW CRACK IN THE ROCK
                10, // 40 SALT WATER
                0,  // 41 SLEEPING PIRATE
                0,  // 42 BOTTLE OF SALT WATER/BOT/
                4,  // 43 PIECES OF BROKEN RUM BOTTLES
                1,  // 44 NON-SKID SNEAKES/SNE/
                0,  // 45 MAP/MAP/
                15, // 46 SHOVEL/SHO/
                0,  // 47 MOULDY OLD BONES/BON/
                6,  // 48 SAND/SAN/
                0,  // 49 BOTTLES OF RUM/BOT/
                0,  // 50 *RARE OLD PRICELESS STAMPS*/STA/
                6,  // 51 LAGOON
                24, // 52 THE TIDE IS OUT
                0,  // 53 THE TIDE IS COMING IN
                15, // 54 WATER WINGS/WIN/
                0,  // 55 FLOTSAM AND JETSAM
                23, // 56 MONASTARY
                0,  // 57 PLAIN WOODEN BOX/BOX/
                0,  // 58 DEAD SQUIRREL
                0,  // 59
                0,  // 60
            };
    }
}
