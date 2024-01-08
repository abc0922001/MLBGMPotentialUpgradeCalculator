using System;

namespace MLBGMPotentialUpgradeCalculator
{
    class Program
    {
        // 定義靜態常量和變數
        private const int TEST_COUNT = 999999;
        private static readonly Double STANDARD_DEVIATION_FACTOR = 1.959964d;
        private static readonly Random random = new Random(); // 靜態隨機數生成器
        private static readonly int[] MergeOK = { 1, 0, 0, 0, 0, 0, 0 };

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("要合成幾個：");
                if (!int.TryParse(Console.ReadLine(), out int maxCount) || maxCount <= 0)
                {
                    Console.WriteLine("請輸入一個正整數。");
                    continue;
                }

                double[] arrData = new double[TEST_COUNT];
                double totalTestCount = 0;

                // 進行測試
                for (int i = 0; i < TEST_COUNT; i++)
                {
                    arrData[i] = PerformTest(maxCount);
                    totalTestCount += arrData[i];
                }

                double avgCount = totalTestCount / TEST_COUNT;
                Console.WriteLine($"平均合成到 {maxCount} 個，要 {Math.Round(avgCount, 2)} 個");
                Console.WriteLine($"安全合成到 {maxCount} 個，要 {Math.Round(avgCount + STANDARD_DEVIATION_FACTOR * StDev(arrData), 2)} 個");
            }
        }

        private static double PerformTest(int maxCount)
        {
            int nowCount = 0, needFalseCount = 0, falseCount = 0;

            while (nowCount != maxCount)
            {
                falseCount = (falseCount == 0) ? 3 : 2;
                needFalseCount += falseCount;

                int successNumber = MergeOK[random.Next(MergeOK.Length)];
                if (successNumber == 1) // 合成成功
                {
                    nowCount++;
                    falseCount = 0;
                }
                else
                {
                    falseCount = 1;
                }
            }

            return needFalseCount;
        }

        // 計算標準偏差
        public static double StDev(double[] arrData)
        {
            double xSum = 0, sSum = 0;
            int arrNum = arrData.Length;

            foreach (double x in arrData)
            {
                xSum += x;
            }
            double xAvg = xSum / arrNum;

            foreach (double x in arrData)
            {
                sSum += ((x - xAvg) * (x - xAvg));
            }
            return Math.Sqrt(sSum / (arrNum - 1));
        }
    }
}
