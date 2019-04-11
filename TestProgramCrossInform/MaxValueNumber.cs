using System;

namespace TestProgramCrossInform
{
    public class MaxValueNumber
    {
        public uint _maxNumber { get; }
        public uint[,] _table { get; }

        public MaxValueNumber(string firstString, string secondString, string thirdString)
        {
            _table = GenerateTable(firstString, secondString, thirdString);
            _maxNumber = GenerateNumber(_table);
        }

        /// <summary>
        /// String-based array generation function
        /// </summary>
        private uint[,] GenerateTable(string firstString, string secondString, string thirdString)
        {
            var tableInts = new uint[3, 3];

            try
            {
                for (int i = 0; i < 3; i++)
                {
                    tableInts[0, i] = Convert.ToUInt32(firstString.Split(' ')[i]);
                    tableInts[1, i] = Convert.ToUInt32(secondString.Split(' ')[i]);
                    tableInts[2, i] = Convert.ToUInt32(thirdString.Split(' ')[i]);
                }
            }
            catch (FormatException e)
            {
                throw new FormatException("Invalid input data!!!");
            }

            return tableInts;
        }

        /// <summary>
        /// The function of generating the largest number based on this table
        /// </summary>
        private uint GenerateNumber(uint[,] tableInts)
        {
            string result = "";
            uint maxValue = 0;
            uint maxValueTwo = 0;
            int overshoot = 0;

            foreach (var tInt in tableInts)
            {
                if (overshoot++ % 2 == 1)
                {
                    if (tInt > maxValueTwo)
                        maxValueTwo = tInt;
                }
                else if (tInt > maxValue)
                    maxValue = tInt;
            }

            //случай, когда наибольший элемент находится в центре одной из сторон не рассматривается
            //потому что, тогда нет вариантов пройти по всем числам, следовательно максимальное число мы не получим.

            if (maxValue == tableInts[1, 1]) //случай, когда наибольший элемент находится в центре
            {
                if (maxValueTwo == tableInts[1, 0])
                {
                    tableInts = RotateTo90(tableInts);
                }
                else if (maxValueTwo == tableInts[1, 2])
                {
                    tableInts = RotateTo90(RotateTo90(RotateTo90(tableInts)));
                }
                else if (maxValueTwo == tableInts[2, 1])
                {
                    tableInts = RotateTo90(RotateTo90(tableInts));
                }

                if (tableInts[0, 0] < tableInts[0, 2])
                    tableInts = RotateMirrorVertical(tableInts);

                result +=
                    $"{tableInts[1, 1]}{tableInts[0, 1]}{tableInts[0, 0]}" +
                    $"{tableInts[1, 0]}{tableInts[2, 0]}{tableInts[2, 1]}" +
                    $"{tableInts[2, 2]}{tableInts[1, 2]}{tableInts[0, 2]}";
            }
            else                    //Случай когда наибольший элемент находится в одном из углов
            {
                if (maxValue == tableInts[2, 0])
                {
                    tableInts = RotateTo90(tableInts);
                }
                else if (maxValue == tableInts[0, 2])
                {
                    tableInts = RotateTo90(RotateTo90(RotateTo90(tableInts)));
                }
                else if (maxValue == tableInts[2, 2])
                {
                    tableInts = RotateTo90(RotateTo90(tableInts));
                }

                result += $"{tableInts[0, 0]}";

                if (tableInts[1, 0] > tableInts[0, 1])
                    tableInts = RotateMirrorDiagonal(tableInts);

                result += $"{tableInts[0, 1]}";

                if (tableInts[0, 2] > tableInts[1, 1])
                {
                    result += $"{tableInts[0, 2]}";

                    if (tableInts[1, 1] > tableInts[2, 2])
                    {
                        result += $"{tableInts[1, 2]}{tableInts[1, 1]}{tableInts[1, 0]}" +
                                  $"{tableInts[2, 0]}{tableInts[2, 1]}{tableInts[2, 2]}";
                    }
                    else
                    {
                        result += $"{tableInts[1, 2]}{tableInts[2, 2]}{tableInts[2, 1]}";

                        if (tableInts[1, 1] > tableInts[2, 0])
                        {
                            result += $"{tableInts[1, 1]}{tableInts[1, 0]}{tableInts[2, 0]}";
                        }
                        else
                        {
                            result += $"{tableInts[2, 0]}{tableInts[1, 0]}{tableInts[1, 1]}";
                        }
                    }
                }
                else
                {
                    result +=
                        $"{tableInts[1, 1]}{tableInts[1, 0]}{tableInts[2, 0]}" +
                        $"{tableInts[2, 1]}{tableInts[2, 2]}{tableInts[1, 2]}" +
                        $"{tableInts[0, 2]}";
                }
            }

            try
            {
                return Convert.ToUInt32(result);
            }
            catch (OverflowException e)
            {
                throw new OverflowException("Overflow resulting from conversion of the result to UInt32");
            }
        }

        /// <summary>
        /// Table rotation function 90 degrees clockwise
        /// </summary>
        private uint[,] RotateTo90(uint[,] table)
        {
            var array = new uint[table.GetLength(0), table.GetLength(1)];

            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = table.GetLength(1) - 1; j >= 0; j--)
                {
                    array[i, j] = table[table.GetLength(1) - 1 - j, i];
                }
            }

            return array;
        }

        /// <summary>
        /// Vertical table mirroring function
        /// </summary>
        private uint[,] RotateMirrorVertical(uint[,] table)
        {
            var array = new uint[table.GetLength(0), table.GetLength(1)];

            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = table.GetLength(1) - 1; j >= 0; j--)
                {
                    array[i, j] = table[i, table.GetLength(1) - 1 - j];
                }
            }

            return array;
        }

        /// <summary>
        /// Diagonal table mirroring function
        /// </summary>
        private uint[,] RotateMirrorDiagonal(uint[,] table)
        {
            var array = new uint[table.GetLength(0), table.GetLength(1)];

            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    array[i, j] = table[j, i];
                }
            }

            return array;
        }
    }
}