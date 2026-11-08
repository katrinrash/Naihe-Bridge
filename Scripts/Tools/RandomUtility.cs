using System;

/// <summary>
/// A utility class for generating random numbers and values, including integers, floats, boolean and enum values.
/// </summary>

public static class RandomUtility
{
    private static Random _random;

    // Default random range
    private static readonly int _minInt = 0;
    private static readonly int _maxInt = 100;

    private static int _cache = -1;

    #region Management

    public static void Init(int currentSeed = 3)
    {
        _random = new Random(currentSeed);
    }

    public static void CleanCache()
    {
        _cache = -1;
    }

    #endregion

    #region Random Int Generation

    // Returns a random integer between default min and max values (0 to 100)
    public static int GetRandomInt()
    {
        return _random.Next(_minInt, _maxInt + 1);
    }

    // Returns a random integer between specified min and max values
    public static int GetRandomInt(int mix, int max)
    {
        return _random.Next(mix, max);
    }

    // Returns a random integer between specified min and max values, ensuring it's unique from the last generated value
    public static int GetRandomUniqueInt(int min, int max)
    {
        int rand;

        do
        {
            rand = GetRandomInt(min, max);
        }
        while (rand == _cache);

        _cache = rand;
        return rand;

    }

    #endregion

    #region Random Float Generation

    // Returns a random float between 0.0 and 1.0
    public static float GetRandomFloat()
    {
        return (float)_random.NextDouble();
    }

    // Returns a random float between specified min and max values
    public static float GetRandomFloat(float min, float max)
    {
        return min + (float)_random.NextDouble() * (max - min);
    }

    #endregion

    #region Random Boolean Generation

    // Returns a random default boolean value (true or false)
    public static bool GetRandomBool()
    {
        return _random.Next(0, 2) == 0;
    }

    // Returns a random boolean value based on the specified probability (0 to 100), int based
    public static bool GetRandomBool(int probability)
    {
        return _random.Next() < probability;
    }

    // Returns a random boolean value based on the specified probability (0.0 to 1.0), float based
    public static bool GetRandomBool(float probability)
    {
        return _random.NextDouble() < probability;
    }

    #endregion

    #region Random Enum Generation

    // Returns a random value from the specified enum type
    public static T GetRandomEnumValue<T>() where T : Enum
    {
        T[] values = EnumHelperUtility.GetEnumArray<T>();
        int index = _random.Next(0, values.Length);
        return values[index];
    }

    #endregion
}
