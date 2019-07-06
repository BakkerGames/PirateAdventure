// Program.Data.cs - 01/09/2019

namespace PirateAdventure
{
    partial class Program
    {
        private const string _gameName = "PirateAdventure";
        private const int _version = 20190109;

        private const int _itemCount = 61;
        private const int _commandCount = 155;
        private const int _commandValueCount = 8;
        private const int _wordCount = 60;
        private const int _roomCount = 34;
        private const int _exitDirections = 6;
        private const int _maxCarry = 5;
        private const int _startRoom = 1;
        private const int _totalTreasures = 2;
        private const int _wordSize = 3;
        private const int _lightTotal = 200;
        private const int _messageCount = 77;
        private const int _treasureRoom = 1;
        private const int _flagCount = 16;
        private const int _itemNowhere = 0;
        private const int _itemInventory = -1;

        // special item numbers for use in code
        private const int _unlitTorchItem = 8;
        private const int _litTorchItem = 9;

        private static bool[] _systemFlags = new bool[_flagCount + 1];
        /*
         System flags:
         0 = temp flag for command sequences
         1 = build boat sequence 1
         2 = build boat sequence 2 succeeded
         3 = matches found
         4 = at treasure island
         5 = set of plans found
         6 = showed intro message
         7 = walked 30 paces in barren field
         8 = walked 30 paces anywhere else
         9 =
         10 = dug up rusty anchor
         11 = rug dropped keys
         12 = map found
         13 =
         14 =
         15 =
         */

        private static int[,] _commandArray = new int[_commandCount, _commandValueCount]
            {
                // background events
                { 80, 422, 342, 420, 340, 0, 16559, 8850 },       // 0
                { 80, 462, 482, 460, 0, 0, 15712, 1705 },         // 1
                { 100, 521, 552, 540, 229, 220, 203, 8700 },      // 2
                { 3, 483, 0, 0, 0, 0, 15712, 0 },                 // 3
                { 100, 284, 0, 0, 0, 0, 8550, 0 },                // 4
                { 100, 28, 663, 403, 40, 0, 8700, 0 },            // 5
                { 100, 48, 20, 660, 740, 220, 9055, 10902 },      // 6
                { 100, 28, 20, 0, 0, 0, 3810, 0 },                // 7
                { 100, 8, 700, 720, 0, 0, 10868, 0 },             // 8
                { 100, 48, 40, 640, 400, 300, 9055, 8305 },       // 9
                { 25, 664, 0, 0, 0, 0, 4263, 0 },                 // 10
                { 40, 104, 886, 0, 0, 0, 4411, 0 },               // 11
                { 80, 242, 502, 820, 80, 240, 9321, 10109 },      // 12
                { 100, 8, 140, 80, 500, 0, 10262, 8850 },         // 13
                { 35, 421, 846, 420, 200, 0, 5162, 0 },           // 14
                // { 100, 129, 120, 0, 0, 0, 6508, 0 },           // 15 - had to remove intro message for proper rum handling
                // { 50, 242, 982, 820, 440, 240, 9321, 8850 },   // 16 - needed more rum handling
                { 50, 242, 982, 820, 440, 240, 9321, 10109 },     // 15
                { 100, 8, 980, 0, 0, 0, 10259, 0 },               // 16
                { 35, 483, 69, 0, 0, 0, 15705, 0 },               // 17
                { 10, 483, 249, 0, 0, 0, 15706, 0 },              // 18
                { 50, 484, 1073, 1086, 0, 0, 17661, 9150 },       // 19
                { 50, 204, 1086, 0, 0, 0, 16711, 0 },             // 20
                { 10, 209, 1040, 1060, 300, 1100, 10872, 10050 }, // 21
                { 10, 208, 1040, 1060, 89, 0, 10867, 0 },         // 22
                { 85, 483, 8, 0, 0, 0, 15719, 10200 },            // 23
                { 100, 8, 0, 0, 0, 0, 10200, 0 },                 // 24
                { 100, 104, 0, 0, 0, 0, 8550, 0 },                // 25
                { 80, 462, 282, 280, 1160, 0, 1422, 0 },          // 26
                // verb-noun events
                { 158, 82, 60, 0, 0, 0, 8170, 9600 },             // 27
                { 4510, 61, 0, 0, 0, 0, 300, 0 },                 // 28
                { 163, 22, 100, 0, 0, 0, 8170, 9600 },            // 29
                { 8100, 0, 0, 0, 0, 0, 16200, 0 },                // 30
                { 4800, 104, 120, 61, 0, 0, 10507, 8164 },        // 31
                { 4800, 107, 100, 61, 89, 0, 10507, 8164 },       // 32
                { 4063, 22, 0, 0, 0, 0, 647, 0 },                 // 33
                { 5570, 161, 203, 160, 180, 0, 10870, 1264 },     // 34
                // { 6170, 181, 180, 160, 0, 0, 8302, 0 },        // 35 - wrong
                { 6170, 181, 180, 160, 0, 0, 8302, 18900 },       // 35 - fixed
                { 6300, 104, 0, 0, 0, 0, 900, 0 },                // 36
                // { 1529, 442, 465, 440, 0, 0, 7800, 0 },        // 37 - wrong
                { 1529, 442, 465, 440, 0, 0, 7922, 0 },           // 37 - fixed
                { 1529, 442, 462, 0, 0, 0, 760, 9150 },           // 38
                { 183, 322, 180, 0, 0, 0, 8170, 9600 },           // 39
                { 1538, 262, 242, 0, 0, 0, 1800, 0 },             // 40
                // { 1538, 262, 245, 260, 0, 0, 7800, 0 },        // 41 - wrong
                { 1538, 262, 245, 260, 0, 0, 7922, 0 },           // 41 - fixed
                { 5888, 262, 242, 0, 0, 0, 1800, 0 },             // 42
                { 5888, 262, 245, 0, 0, 0, 1950, 0 },             // 43
                { 6188, 262, 245, 541, 260, 560, 2155, 7950 },    // 44
                { 5888, 261, 0, 0, 0, 0, 2400, 0 },               // 45
                { 4088, 561, 0, 0, 0, 0, 2400, 0 },               // 46
                { 4088, 263, 0, 0, 0, 0, 2713, 0 },               // 47
                { 4088, 562, 580, 109, 100, 249, 2303, 8700 },    // 48
                { 4088, 249, 562, 108, 900, 240, 6203, 8700 },    // 49
                { 4088, 248, 562, 0, 0, 0, 6600, 0 },             // 50
                { 4068, 103, 69, 0, 0, 0, 646, 0 },               // 51
                { 4068, 103, 68, 0, 0, 0, 6600, 0 },              // 52
                { 5887, 342, 0, 0, 0, 0, 2550, 0 },               // 53
                { 5887, 362, 0, 0, 0, 0, 2713, 0 },               // 54
                { 5887, 382, 0, 0, 0, 0, 2100, 0 },               // 55
                { 159, 382, 320, 0, 0, 0, 8170, 9600 },           // 56
                { 6187, 342, 362, 0, 0, 0, 2550, 0 },             // 57
                { 6187, 345, 362, 541, 360, 380, 8303, 10050 },   // 58
                { 3461, 503, 0, 0, 0, 0, 172, 0 },                // 59
                { 3750, 0, 0, 0, 0, 0, 9900, 0 },                 // 60
                { 1528, 0, 0, 0, 0, 0, 9900, 0 },                 // 61
                { 4108, 1143, 1012, 0, 0, 0, 646, 0 },            // 62
                { 6450, 0, 0, 0, 0, 0, 2853, 0 },                 // 63
                { 4510, 66, 0, 0, 0, 0, 2720, 0 },                // 64
                { 4950, 0, 0, 0, 0, 0, 9750, 0 },                 // 65
                { 5114, 0, 0, 0, 0, 0, 10650, 0 },                // 66
                { 7092, 592, 0, 0, 0, 0, 2745, 0 },               // 67
                { 185, 284, 140, 0, 0, 0, 8156, 10564 },          // 68
                { 4098, 1054, 0, 0, 0, 0, 647, 17550 },           // 69
                { 4098, 1053, 0, 0, 0, 0, 647, 17400 },           // 70
                { 4083, 322, 0, 0, 0, 0, 647, 0 },                // 71
                { 4095, 762, 0, 0, 0, 0, 647, 0 },                // 72
                { 195, 782, 921, 0, 0, 0, 2727, 0 },              // 73
                { 195, 762, 261, 0, 0, 0, 2727, 0 },              // 74
                { 6900, 0, 0, 0, 0, 0, 9450, 0 },                 // 75
                { 1526, 602, 0, 0, 0, 0, 2723, 0 },               // 76
                // { 1541, 621, 602, 640, 520, 600, 7853, 8250 }, // 77 - wrong
                { 1541, 621, 602, 640, 520, 600, 7853, 8372 },    // 77 - fixed
                { 195, 782, 661, 0, 0, 0, 2727, 0 },              // 78
                { 7092, 623, 583, 303, 643, 20, 8700, 0 },        // 79
                { 7092, 0, 0, 0, 0, 0, 3750, 0 },                 // 80
                { 200, 722, 220, 0, 0, 0, 10554, 9600 },          // 81
                { 195, 762, 61, 0, 0, 0, 2727, 0 },               // 82
                { 4050, 0, 0, 0, 0, 0, 10564, 0 },                // 83
                // { 1526, 523, 520, 0, 0, 0, 7800, 0 },          // 84 - wrong, unnecessary
                { 0, 0, 0, 0, 0, 0, 0, 0 },                       // 84 - fixed
                { 195, 762, 340, 0, 0, 0, 8126, 8464 },           // 85
                { 195, 782, 360, 0, 0, 0, 8157, 10564 },          // 86
                { 7530, 404, 242, 1053, 89, 0, 17250, 0 },        // 87
                { 4800, 0, 0, 0, 0, 0, 450, 0 },                  // 88
                { 5868, 103, 200, 69, 60, 0, 4553, 8700 },        // 89
                { 5868, 68, 0, 0, 0, 0, 494, 0 },                 // 90
                { 1546, 146, 0, 0, 0, 0, 4800, 0 },               // 91
                // { 1546, 802, 141, 140, 840, 0, 8302, 0 },      // 92 - wrong
                { 1546, 802, 141, 140, 840, 0, 8302, 18300 },     // 92 - fixed
                { 2746, 841, 840, 140, 0, 0, 8302, 4950 },        // 93
                { 3496, 802, 0, 0, 0, 0, 811, 0 },                // 94
                { 3496, 841, 840, 140, 0, 0, 811, 8302 },         // 95
                { 7366, 822, 820, 240, 400, 0, 5305, 9300 },      // 96
                { 5861, 503, 0, 0, 0, 0, 2100, 0 },               // 97
                { 8411, 501, 500, 140, 0, 0, 5459, 7833 },        // 98
                { 192, 742, 400, 0, 0, 0, 8170, 9600 },           // 99
                { 201, 404, 88, 420, 240, 242, 8170, 8071 },      // 100
                { 201, 404, 89, 120, 0, 0, 8170, 9600 },          // 101
                { 7530, 404, 245, 0, 0, 0, 2737, 0 },             // 102
                { 7530, 404, 912, 0, 0, 0, 2738, 0 },             // 103
                { 7530, 404, 89, 80, 740, 420, 5908, 9300 },      // 104
                { 7530, 404, 88, 80, 740, 120, 5910, 9300 },      // 105
                { 7671, 0, 0, 0, 0, 0, 6000, 0 },                 // 106
                { 4553, 903, 0, 0, 0, 0, 6300, 0 },               // 107
                { 1350, 0, 0, 0, 0, 0, 6000, 0 },                 // 108
                // { 1510, 62, 60, 0, 0, 0, 7800, 0 },            // 109 - wrong
                { 1510, 62, 60, 0, 0, 0, 7922, 0 },               // 109 - fixed
                { 5860, 63, 0, 0, 0, 0, 18000, 0 },               // 110
                { 201, 404, 88, 420, 0, 0, 8170, 9600 },          // 111
                { 186, 284, 360, 0, 0, 0, 8170, 9600 },           // 112
                { 1539, 482, 242, 0, 0, 0, 1800, 0 },             // 113
                { 1539, 482, 480, 0, 0, 0, 7904, 16800 },         // 114
                { 194, 682, 300, 0, 0, 0, 8170, 9600 },           // 115
                { 174, 149, 464, 140, 0, 0, 8751, 0 },            // 116
                { 174, 160, 0, 0, 0, 0, 8751, 0 },                // 117
                { 7800, 444, 940, 921, 952, 0, 10548, 8014 },     // 118
                { 7800, 124, 921, 0, 0, 0, 7350, 0 },             // 119
                { 7800, 424, 992, 980, 921, 0, 10553, 7264 },     // 120
                { 8250, 104, 0, 0, 0, 0, 10505, 9600 },           // 121
                { 7800, 464, 148, 1140, 921, 1152, 10553, 7264 }, // 122
                { 1541, 643, 640, 0, 0, 0, 7800, 0 },             // 123
                { 163, 104, 40, 0, 0, 0, 8170, 9600 },            // 124
                { 6300, 44, 0, 0, 0, 0, 15450, 0 },               // 125
                { 4534, 583, 0, 0, 0, 0, 4650, 0 },               // 126
                { 6187, 702, 541, 0, 0, 0, 2713, 16050 },         // 127
                { 5887, 702, 0, 0, 0, 0, 2713, 0 },               // 128
                { 5887, 0, 722, 0, 0, 0, 2100, 0 },               // 129
                { 198, 1022, 480, 0, 0, 0, 8170, 9600 },          // 130
                { 157, 2, 24, 40, 0, 0, 8170, 9600 },             // 131
                { 1510, 44, 60, 40, 80, 85, 7801, 10800 },        // 132
                // { 1532, 302, 208, 300, 0, 0, 7800, 0 },        // 133 - wrong
                { 1532, 302, 208, 300, 0, 0, 7922, 0 },           // 133 - fixed
                { 1532, 302, 209, 0, 0, 0, 2813, 0 },             // 134
                { 1532, 305, 0, 0, 0, 0, 10518, 7564 },           // 135
                // { 8411, 841, 840, 140, 0, 0, 8922, 0 },        // 136 - wrong
                { 8411, 841, 840, 140, 0, 0, 8902, 4950 },        // 136 - fixed
                { 165, 1122, 500, 0, 0, 0, 8170, 9600 },          // 137
                { 1392, 0, 0, 0, 0, 0, 6000, 0 },                 // 138
                { 6300, 284, 0, 0, 0, 0, 16350, 0 },              // 139
                { 8582, 0, 0, 0, 0, 0, 17700, 0 },                // 140
                // { 7800, 921, 209, 302, 200, 0, 8700, 0 },      // 141 - wrong
                { 7800, 921, 209, 302, 200, 0, 8823, 0 },         // 141 - fixed
                { 7950, 0, 0, 0, 0, 0, 2700, 0 },                 // 142
                { 5908, 621, 1143, 1000, 0, 0, 4553, 0 },         // 143
                { 5266, 0, 0, 0, 0, 0, 1800, 0 },                 // 144
                { 6300, 224, 0, 0, 0, 0, 17517, 17850 },          // 145
                { 1200, 0, 0, 0, 0, 0, 17100, 0 },                // 146
                { 6300, 124, 0, 0, 0, 0, 16350, 0 },              // 147
                { 4350, 208, 1040, 1060, 0, 0, 10919, 0 },        // 148
                { 6300, 184, 242, 0, 0, 0, 3600, 0 },             // 149
                { 7800, 921, 160, 140, 0, 0, 7410, 9000 },        // 150
                { 6300, 0, 0, 0, 0, 0, 450, 0 },                  // 151
                { 100, 129, 120, 0, 0, 0, 6508, 0 },              // 152 - intro message
                { 4350, 0, 0, 0, 0, 0, 18600, 0 },                // 153 - new command
                { 5908, 626, 1143, 0, 0, 0, 18750, 0 },           // 154 - new command
            };

        private static string[,] _verbNounList = new string[_wordCount, 2]
            {
                { "", "" },                // 0
                { "GO", "NORTH" },         // 1
                { "*CLIMB", "SOUTH" },     // 2
                { "*WALK", "EAST" },       // 3
                { "*RUN", "WEST" },        // 4
                { "*ENTER", "UP" },        // 5
                { "*PACE", "DOWN" },       // 6
                { "*FOLLOW", "STAIRS" },   // 7
                { "SAY", "PASSAGEWAY" },   // 8
                { "SAIL", "HALL" },        // 9
                { "GET", "BOOK" },         // 10
                { "*TAKE", "BOTTLE" },     // 11
                { "*CATCH", "*RUM" },      // 12
                { "*PICK", "WINDOW" },     // 13
                { "*REMOVE", "GAME" },     // 14
                { "*WEAR", "MONASTARY" },  // 15
                { "*PULL", "PIRATE" },     // 16
                { "FLY", "AROUND" },       // 17
                { "DROP", "BAG" },         // 18
                { "*RELEASE", "*DUFFLE" }, // 19
                { "*THROW", "TORCH" },     // 20
                { "*LEAVE", "OFF" },       // 21
                { "*GIVE", "MATCHES" },    // 22
                { "DRINK", "YOHO" },       // 23
                { "*EAT", "30" },          // 24
                { "INVENTORY", "LUMBER" }, // 25
                { "SAIL", "RUG" },         // 26
                { "LOOK", "KEY" },         // 27
                { "*SHOW", "INVENTORY" },  // 28
                { "WAIT", "DUBLOONS" },    // 29
                { "READ", "SAIL" },        // 30
                { "", "FISH" },            // 31
                { "YOHO", "ANCHOR" },      // 32
                { "SCORE", "SHACK" },      // 33
                { "SAVE", "PLANS" },       // 34
                { "KILL", "CAVE" },        // 35
                { "*ATTACK", "PATH" },     // 36
                { "LIGHT", "DOOR" },       // 37
                { "", "CHEST" },           // 38
                { "OPEN", "PARROT" },      // 39
                { "*SMASH", "HAMMER" },    // 40
                { "UNLOCK", "NAILS" },     // 41 - also verb UNLIGHT
                { "HELP", "BOAT" },        // 42
                { "AWA", "*SHIP" },        // 43 - not sure what verb AWA is
                { "*BUN", "SHED" },        // 44 - not sure what verb BUN is
                { "", "CRACK" },           // 45
                { "QUIT", "WATER" },       // 46
                { "BUILD", "*SALT" },      // 47
                { "*MAKE", "LAGOON" },     // 48
                { "WAKE", "*TIDE" },       // 49
                { "SET", "PIT" },          // 50
                { "CAST", "SHORE" },       // 51
                { "DIG", "*BEACH" },       // 52
                { "BURY", "MAP" },         // 53
                { "FIND", "PACE" },        // 54
                { "JUMP", "BONE" },        // 55
                { "EMPTY", "HOLE" },       // 56
                { "WEIGH", "SAND" },       // 57
                { "", "BOX" },             // 58
                { "", "SNEAKERS" },        // 59
            };

        private static int[,] _roomExitArray = new int[_roomCount, _exitDirections]
            {
                { 0, 0, 0, 0, 0, 0 },      // 00 NOWHERE
                { 0, 0, 0, 0, 0, 0 },      // 01 APARTMENT IN LONDON
                { 0, 0, 0, 0, 0, 1 },      // 02 ALCOVE
                { 0, 0, 4, 2, 0, 0 },      // 03 SECRET PASSAGEWAY
                { 0, 0, 0, 3, 0, 0 },      // 04 MUSTY ATTIC
                { 0, 0, 0, 0, 0, 0 },      // 05 *I'M OUTSIDE AN OPEN WINDOW ON A LEDGE ON THE SIDE OF AVERY TALL BUILDING
                { 0, 0, 8, 0, 0, 0 },      // 06 SANDY BEACH ON A TROPICAL ISLE
                { 0, 12, 13, 14, 0, 11 },  // 07 MAZE OF CAVES
                { 0, 0, 14, 6, 0, 0 },     // 08 MEADOW
                { 0, 0, 0, 8, 0, 0 },      // 09 GRASS SHACK
                { 10, 24, 10, 10, 0, 0 },  // 10 *I'M IN THE OCEAN
                { 0, 0, 0, 0, 7, 0 },      // 11 PIT
                { 7, 0, 14, 13, 0, 0 },    // 12 MAZE OF CAVES
                { 7, 14, 12, 19, 0, 0 },   // 13 MAZE OF CAVES
                { 0, 0, 0, 8, 0, 0 },      // 14 *I'M AT THE FOOT OF A CAVE RIDDEN HILL.A PATH LEADS TO THE TOP
                { 17, 0, 0, 0, 0, 0 },     // 15 TOOL SHED
                { 0, 0, 17, 0, 0, 0 },     // 16 LONG HALLWAY
                { 0, 0, 0, 16, 0, 0 },     // 17 LARGE CAVERN
                { 0, 0, 0, 0, 0, 14 },     // 18 *I'M ON TOP OF A HILL. BELOW IS PIRATES ISLAND. ACROSS THE SEA OFF IN THE DISTANCE I SEE *TREASURE* ISLAND
                { 0, 14, 14, 13, 0, 0 },   // 19 MAZE OF CAVES
                { 0, 0, 0, 0, 0, 0 },      // 20 *I'M ABOARD PIRATE SHIP ANCHORED OFF SHORE
                { 0, 22, 0, 0, 0, 0 },     // 21 *I'M ON THE BEACH AT TREASURE ISLAND
                { 21, 0, 23, 0, 0, 0 },    // 22 SPOOKY OLD GRAVEYARD FILLED WITH PILESOF EMPTY AND BROKEN RUM BOTTLES
                { 0, 0, 0, 22, 0, 0 },     // 23 LARGE BARREN FIELD
                { 10, 6, 6, 6, 0, 0 },     // 24 SHALLOW LAGOON. TO THE NORTH IS THE OCEAN
                { 0, 0, 0, 23, 0, 0 },     // 25 SACKED AND DESERTED MONASTARY
                { 0, 0, 0, 0, 0, 0 },      // 26
                { 0, 0, 0, 0, 0, 0 },      // 27
                { 0, 0, 0, 0, 0, 0 },      // 28
                { 0, 0, 0, 0, 0, 0 },      // 29
                { 0, 0, 0, 0, 0, 0 },      // 30
                { 0, 0, 0, 0, 0, 0 },      // 31
                { 0, 0, 0, 0, 0, 0 },      // 32
                { 0, 0, 0, 0, 0, 0 },      // 33 *WELCOME TO NEVER NEVER LAND
            };

        private static string[] _roomLongDesc = new string[_roomCount]
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
                "*WELCOME TO NEVER NEVER LAND/NEVER NEVER LAND/",
            };

        private static string[] _messages = new string[_messageCount]
            {
                /* 00 */ "",
                /* 01 */ "THERE'S A STRANGE SOUND",
                /* 02 */ "THE NAME OF THE BOOK IS -TREASURE ISLAND- THERE'S A WORD ENGRAVED IN THE FLYLEAF -YOHO- AND A MESSAGE -LONG JOHN SILVER LEFT 2 TREASURES ON TREASURE ISLAND!-",
                /* 03 */ "NOTHING HAPPENS",
                /* 04 */ "THERE'S SOMETHING THERE ALRIGHT. MAYBE I SHOULD...",
                /* 05 */ "THAT'S NOT VERY SAFE",
                /* 06 */ "YOU MAY NEED MAGIC HERE",
                /* 07 */ "EVERYTHING SPINS AROUND AND SUDDENLY YOU ARE ELSEWHERE...",
                /* 08 */ "TORCH IS LIT",
                /* 09 */ "I WAS WRONG. I GUESS ITS NOT A MONGOOSE CAUSE THE SNAKES BIT IT.",
                /* 10 */ "I'M SNAKE BIT",
                /* 11 */ "PARROT ATTACKS SNAKES AND DRIVES THEM OFF",
                /* 12 */ "PIRATE WON'T LET ME",
                /* 13 */ "ITS LOCKED",
                /* 14 */ "ITS OPEN",
                /* 15 */ "THERE ARE A SET OF PLANS IN IT",
                /* 16 */ "NOT WHILE I'M CARRYING IT",
                /* 17 */ "CROCS STOP ME",
                /* 18 */ "SORRY I CAN'T",
                /* 19 */ "WRONG GAME YOU SILLY GOOSE!",
                /* 20 */ "I DON'T HAVE IT",
                /* 21 */ "PIRATE GRABS RUM AND SCUTTLES OFF CHORTLING",
                /* 22 */ "...I THINK ITS ME. HEE HEE.",
                /* 23 */ "ITS NAILED TO THE FLOOR!",
                /* 24 */ "-MAGIC WORD- HO AND A... (WORK ON IT. YOU'LL GET IT)",
                /* 25 */ "NO. SOMETHING IS MISSING!",
                /* 26 */ "IT WAS A TIGHT SQUEEZE!",
                /* 27 */ "SOMETHING WON'T FIT",
                /* 28 */ "SINCE NOTHING IS HAPPENING",
                /* 29 */ "I SLIPPED AND FELL...",
                /* 30 */ "SOMETHING FALLS OUT",
                /* 31 */ "THEY'RE PLANS TO BUILD JOLLY ROGER (A PIRATE SHIP!). YOU'LL NEED HAMMER NAILS LUMBER ANCHOR SAILS AND KEEL.",
                /* 32 */ "I'VE NO CONTAINER",
                /* 33 */ "IT SOAKS INTO THE GROUND",
                /* 34 */ "TOO DRY. FISH VANISH.",
                /* 35 */ "PIRATE AWAKENS. SAYS -AYE MATEY WE BE CASTING OFF SOON- HE THEN VANISHES",
                /* 36 */ "WHAT A WASTE...",
                /* 37 */ "I'VE NO CREW",
                /* 38 */ "PIRATE SAYS -AYE MATEY WE BE NEEDING A MAP FIRST-",
                /* 39 */ "AFTER A MONTH AT SEA WE SET ANCHOR OFF OF A SANDY BEACH. ALL ASHORE WHO'S GOING ASHORE...",
                /* 40 */ "TRY -WEIGH ANCHOR-",
                /* 41 */ "THERE'S A MAP IN IT",
                /* 42 */ "ITS A MAP TO TREASURE ISLAND. AT THE BOTTOM IT SAYS -30 PACES AND THEN DIG!-",
                /* 43 */ "* WELCOME TO -PIRATES ADVENTURE- BY SCOTT & ALEXIS ADAMS *",
                /* 44 */ "ITS EMPTY",
                /* 45 */ "I'VE NO PLANS!",
                /* 46 */ "OPEN IT?",
                /* 47 */ "GO THERE?",
                /* 48 */ "I FOUND SOMETHING!",
                /* 49 */ "I DIDN'T FIND ANYTHING",
                /* 50 */ "I DON'T SEE IT HERE",
                /* 51 */ "OK I WALKED OFF 30 PACES.",
                /* 52 */ "CONGRATULATIONS !!! BUT YOUR ADVENTURE IS NOT OVER YET...",
                /* 53 */ "READING EXPANDS THE MIND",
                /* 54 */ "THE PARROT CRYS",
                /* 55 */ "-CHECK THE BAG MATEY-",
                /* 56 */ "-CHECK THE CHEST MATEY-",
                /* 57 */ "FROM THE OTHER SIDE!",
                /* 58 */ "OPEN THE BOOK!",
                /* 59 */ "THERE'S MULTIPLE EXITS HERE!",
                /* 60 */ "CROCS EAT FISH AND LEAVE",
                /* 61 */ "I'M UNDERWATER. I CAN'T SWIM. BLUB BLUB...",
                /* 62 */ "-PIECES OF EIGHT-",
                /* 63 */ "ITS STUCK IN THE SAND",
                /* 64 */ "USE 1 WORD",
                /* 65 */ "PIRATE SAYS -AYE MATEY WE BE WAITING FOR THE TIDE TO COME IN-",
                /* 66 */ "THE TIDE IS OUT",
                /* 67 */ "THE TIDE IS COMING IN",
                /* 68 */ "ABOUT 20 POUNDS. TRY -SET SAIL-",
                /* 69 */ "-TIDES A CHANGING MATEY-",
                /* 70 */ "NOTE HERE -I BE LIKING PARROTS. THEY BE SMART MATEY-",
                /* 71 */ "PIRATE FOLLOWS YOU ASHORE AS IF HE IS WAITING FOR SOMETHING.",
                /* 72 */ "TAKEN",
                /* 73 */ "THE ANCHOR COMES LOOSE",
                /* 74 */ "TIME PASSES...",
                /* 75 */ "ITS NAILED SHUT",
                /* 76 */ "THE TORCH IS OUT",
            };

        private static string[] _itemDescriptions = new string[_itemCount]
            {
                /* 00 */ "FLIGHT OF STAIRS",
                /* 01 */ "OPEN WINDOW",
                /* 02 */ "BOOKS IN A BOOKCASE",
                /* 03 */ "LARGE LEATHER BOUND BOOK/BOO/",
                /* 04 */ "BOOKCASE WITH A SECRET PASSAGE BEHIND IT",
                /* 05 */ "PIRATE'S DUFFLE BAG/BAG/",
                /* 06 */ "SIGN ON WALL -RETURN TREASURES HERE. SAY SCORE- SIGN BY STAIRS -ANTONYM OF LIGHT IS UNLIGHT-",
                /* 07 */ "EMPTY BOTTLE/BOT/",
                /* 08 */ "UNLIT TORCH/TOR/",
                /* 09 */ "LIT TORCH/TOR/",
                /* 10 */ "MATCHES/MAT/",
                /* 11 */ "SMALL SHIP'S KEEL AND MAST",
                /* 12 */ "WICKED LOOKING PIRATE",
                /* 13 */ "TREASURE CHEST/CHE/",
                /* 14 */ "MONGOOSE/MON/",
                /* 15 */ "RUSTY ANCHOR/ANC/",
                /* 16 */ "GRASS SHACK",
                /* 17 */ "MEAN AND HUNGRY LOOKING CROCODILES",
                /* 18 */ "LOCKED DOOR",
                /* 19 */ "OPEN DOOR WITH HALL BEYOND",
                /* 20 */ "PILE OF SAILS/SAI/",
                /* 21 */ "FISH/FIS/",
                /* 22 */ "*DUBLEONS*/DUB/",
                /* 23 */ "DEADLY MAMBA SNAKES/SNA/",
                /* 24 */ "PARROT/PAR/",
                /* 25 */ "BOTTLE OF RUM/BOT/",
                /* 26 */ "RUG/RUG/",
                /* 27 */ "RING OF KEYS/KEY/",
                /* 28 */ "OPEN TREASURE CHEST/CHE/",
                /* 29 */ "SET OF PLANS/PLA/",
                /* 30 */ "RUG",
                /* 31 */ "CLAW HAMMER/HAM/",
                /* 32 */ "NAILS/NAI/",
                /* 33 */ "PILE OF PRECUT LUMBER/LUM/",
                /* 34 */ "TOOL SHED",
                /* 35 */ "LOCKED DOOR",
                /* 36 */ "OPEN DOOR WITH PIT BEYOND",
                /* 37 */ "PIRATE SHIP",
                /* 38 */ "ROCK WALL WITH NARROW CRACK IN IT",
                /* 39 */ "NARROW CRACK IN THE ROCK",
                /* 40 */ "SALT WATER",
                /* 41 */ "SLEEPING PIRATE",
                /* 42 */ "BOTTLE OF SALT WATER/BOT/",
                /* 43 */ "PIECES OF BROKEN RUM BOTTLES",
                /* 44 */ "NON-SKID SNEAKERS/SNE/",
                /* 45 */ "MAP/MAP/",
                /* 46 */ "SHOVEL/SHO/",
                /* 47 */ "MOULDY OLD BONES/BON/",
                /* 48 */ "SAND/SAN/",
                /* 49 */ "BOTTLE OF RUM/BOT/",
                /* 50 */ "*RARE OLD PRICELESS STAMPS*/STA/",
                /* 51 */ "LAGOON",
                /* 52 */ "THE TIDE IS OUT",
                /* 53 */ "THE TIDE IS COMING IN",
                /* 54 */ "WATER WINGS/WIN/",
                /* 55 */ "FLOTSAM AND JETSAM",
                /* 56 */ "MONASTARY",
                /* 57 */ "PLAIN WOODEN BOX/BOX/",
                /* 58 */ "DEAD SQUIRREL",
                /* 59 */ "",
                /* 60 */ "",
            };

        private static int[] _itemLocation = new int[_itemCount];

        private static int[] _itemStartLocation = new int[_itemCount]
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
                0,  // 49 BOTTLE OF RUM/BOT/
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

        private static string _introMessage =
            "*** WELCOME TO PIRATE ADVENTURE ***\r\n" +
            "\r\n" +
            "UNLESS TOLD DIFFERENTLY YOU MUST FIND *TREASURES* AND RETURN THEM TO THEIR PROPER PLACE!\r\n" +
            "\r\n" +
            "ENTER ENGLISH COMMANDS THAT CONSIST OF A NOUN AND VERB. SOME EXAMPLES...\r\n" +
            "\r\n" +
            "TO FIND OUT WHAT YOU'RE CARRYING YOU MIGHT SAY: TAKE INVENTORY\r\n" +
            "TO GO INTO A HOLE YOU MIGHT SAY: GO HOLE\r\n" +
            "TO SAVE CURRENT GAME: SAVE GAME\r\n" +
            "\r\n" +
            "YOU WILL AT TIMES NEED SPECIAL ITEMS TO DO THINGS, BUT I'M SURE YOU'LL BE A GOOD ADVENTURER\r\n" +
            "AND FIGURE THESE THINGS OUT.\r\n" +
            "\r\n" +
            "HAPPY ADVENTURING... PRESS ENTER TO START";

        private static string _enterCommand = "ENTER COMMAND> ";
        private static string _endingMessage = "THANKS FOR PLAYING!!! PRESS ENTER...";
        private static string _cannotParseMessage = "I DON'T UNDERSTAND THAT!";
        private static string _cannotDoMessage = "I CAN'T DO THAT!";
        private static string _cannotGoThatWayMessage = "I CAN'T GO THAT WAY";
        private static string _inventoryFullMsg = "I'VE TOO MUCH TOO CARRY. TRY -TAKE INVENTORY-";

        private static string _numberOfMovesMessage()
        {
            string s = (numMoves == 1) ? "" : "S";
            return $"YOU HAVE PLAYED FOR {numMoves} MOVE{s}.";
        }
    }
}
