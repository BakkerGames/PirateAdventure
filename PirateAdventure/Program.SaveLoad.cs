// SaveLoad.cs - 12/03/2017

namespace PirateAdventure
{
    partial class Program
    {
        public static void Save()
        {
            // 710 {PRINT}"SAVING GAME"
            //     :{IF}D=-1{THEN}{INPUT}"READY OUTPUT TAPE";K$
            //     :{PRINT}{INT}(IL*5/60)+1;"MINUTES"{ELSE}{OPEN}"O",D,SV$
            // 720 {PRINT}#D,SF,LX,DF,R
            //     :{FOR}W=0{TO}IL
            //     :{PRINT}#D,IA(W)
            //     :{NEXT}
            //     :{IF}D<>-1{CLOSE}
            // 730 {GOTO}960

        }

        private static void Load()
        {
            // 110 {IF}D<>-1{THEN}{CLOSE}
            //     :{OPEN}"I",D,SV${ELSE}{INPUT}"READY SAVED TAPE";K$
            //     :{PRINT}{INT}(IL*5/60)+1;"MINUTES"
            // 120 {INPUT}#D,SF,LX,DF,R
            //     :{FOR}X=0{TO}IL
            //     :{INPUT}#D,IA(X)
            //     :{NEXT}
            //     :{IF}D<>-1{CLOSE}

        }
    }
}
