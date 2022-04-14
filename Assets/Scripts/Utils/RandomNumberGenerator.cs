using System;
public class RandomNumberGenerator
{
    private static Random random = new Random();

    public struct PartitionsParameters
    {
        public void InitPartitions(int _currentPartitions, int _maxPartitions)
        {
            currentPartitions = _currentPartitions;
            maxPartitions = _maxPartitions;
        }

        public void InitValues(int _currentValues, int _maxValues)
        {
            currentValues = _currentValues;
            maxValues = _maxValues;
        }

        public void InitPartitionsValuesLimits(int _minValue, int _maxValue)
        {
            minValueInPartition = _minValue;
            maxValueInPartition = _maxValue;
        }

        public int maxPartitions;
        public int maxValues;
        public int currentPartitions;
        public int currentValues;
        public int maxValueInPartition;
        public int minValueInPartition;
    }

    public static int Generate(int maxValue, bool includeMax = false)
    {
        maxValue = includeMax ? maxValue + 1 : maxValue;
        return random.Next(maxValue);
    }

    public static int Generate(int minValue, int maxValue, bool includeMax = false)
    {
        maxValue = includeMax ? maxValue + 1 : maxValue;
        return random.Next(minValue, maxValue);
    }

    public static int GenerateForPartitions(PartitionsParameters parameters)
    {
        int number = Generate(parameters.minValueInPartition, parameters.maxValueInPartition, true);
        float remainingValuesCount;
        float remainingPartitionsCount;

        do
        {
            remainingValuesCount = (parameters.maxValues - (parameters.currentValues + number));
            remainingPartitionsCount = (parameters.maxPartitions - (parameters.currentPartitions + 1));
            if (remainingValuesCount > (remainingPartitionsCount * parameters.maxValueInPartition))
            {
                number++;
            }
            else if ((remainingValuesCount / remainingPartitionsCount) < 1)
            {
                number--;
            }
        } while (
            (remainingValuesCount > (remainingPartitionsCount * parameters.maxValueInPartition))
            || ((remainingPartitionsCount / remainingPartitionsCount) < 1)
        );

        return number;
    }
}
