using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MLBGMPotentialUpgradeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {

            //初始化機率
            int[] upgradeTo1 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }; //100%
            int[] upgradeTo2 = { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 }; //70%
            int[] upgradeTo3 = { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 }; //50%
            int[] upgradeTo4 = { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0 }; //40%
            int[] upgradeTo5 = { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0 }; //30%
            int[] upgradeTo6 = { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 }; //20%
            int[] upgradeTo7 = { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //10%

            const int COST = 40000; //花費           
            const int TEST_COUNT = 99999;//總測試次數

            start:

            Console.WriteLine("請輸入要達到的等級");

            int initLevel = 0; //初始等級
            int maxLevel = Convert.ToInt32(Console.ReadLine()); //最大等級
            int totalTestCount = 0;

            Double avgCount = 0d; //平均次數
            int nowLevel = initLevel; //目前等級
            Double sd = 2d; //幾倍標準差
            float[] arrData = new float[TEST_COUNT];

            Random rnd = new Random();
            //測試
            for (int i = 1; i < TEST_COUNT + 1; i++)
            {
                int upgradesCount = 0; //升級次數
                while (nowLevel != maxLevel)
                {
                    switch (nowLevel)
                    {
                        case 0:
                            nowLevel = GetUpgradedLevel(rnd, nowLevel, upgradeTo1);
                            break;
                        case 1:
                            nowLevel = GetUpgradedLevel(rnd, nowLevel, upgradeTo2);
                            break;
                        case 2:
                            nowLevel = GetUpgradedLevel(rnd, nowLevel, upgradeTo3);
                            break;
                        case 3:
                            nowLevel = GetUpgradedLevel(rnd, nowLevel, upgradeTo4);
                            break;
                        case 4:
                            nowLevel = GetUpgradedLevel(rnd, nowLevel, upgradeTo5);
                            break;
                        case 5:
                            nowLevel = GetUpgradedLevel(rnd, nowLevel, upgradeTo6);
                            break;
                        case 6:
                            nowLevel = GetUpgradedLevel(rnd, nowLevel, upgradeTo7);
                            break;
                    }
                    upgradesCount += 1;

                }
                //Console.WriteLine(string.Format("{0}. 升級 {1} 次", i, upgradesCount));
                //if (upgradesCount == MAX_LEVEL)
                //{
                //    Console.WriteLine(string.Format("第 {0} 次的時候，只需要 {1} 次就衝到 {2} 等", i, upgradesCount, MAX_LEVEL));
                //}
                arrData[i - 1] = (float)upgradesCount;
                totalTestCount += upgradesCount;
                nowLevel = initLevel;//初始化等級
            }
            avgCount = (double)totalTestCount / TEST_COUNT;
            //Console.WriteLine(string.Format("平均升級到 {0} 等，要 {1} 次，共花費 {3} 元", maxLevel, avgCount, StDev(arrData), avgCount * COST));

            Console.WriteLine(string.Format("平均升級到 {0} 等，要 {1} 次，共花費 {2} 萬元", maxLevel, Math.Round(avgCount, 2), Math.Round(avgCount * COST / 10000)));
            Console.WriteLine(string.Format("安全升級到 {0} 等，要 {1} 次，共花費 {2} 萬元", maxLevel, Math.Round(avgCount + sd * StDev(arrData), 2), Math.Round((avgCount + sd * StDev(arrData)) * COST / 10000)));
            goto start;

            //結束
            //Console.ReadKey();
        }

        private static int GetUpgradedLevel(Random rnd, int nowLevel, int[] upgradeToX)
        {
            int successNumber = upgradeToX[rnd.Next(upgradeToX.Length)];
            int level = nowLevel;
            if (successNumber == 1)
            {
                level += 1;
            }
            else
            {
                level = 0;
            }
            return level;
        }

        public static float StDev(float[] arrData) //计算标准偏差  
        {
            float xSum = 0F;
            float xAvg = 0F;
            float sSum = 0F;
            float tmpStDev = 0F;
            int arrNum = arrData.Length;
            for (int i = 0; i < arrNum; i++)
            {
                xSum += arrData[i];
            }
            xAvg = xSum / arrNum;
            for (int j = 0; j < arrNum; j++)
            {
                sSum += ((arrData[j] - xAvg) * (arrData[j] - xAvg));
            }
            tmpStDev = Convert.ToSingle(Math.Sqrt((sSum / (arrNum - 1))).ToString());
            return tmpStDev;
        }
    }
}
