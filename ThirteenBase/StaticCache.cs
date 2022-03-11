namespace ThirteenBase;

/// <summary>
/// Решение с кэшированием вариантов чисел с заданной суммой цифр.
/// Кэш собирается статически перед началом основого расчета.
/// Приемлемое усложнение кода даёт серьезное преимущество по сравнение с другими решениями как по скорости, так и по памяти.
/// Преимущество достигается за счет кэш-локальности массива и меньшего количества уровненей косвенности при доступе к кэшу.
/// </summary>
public sealed class StaticCache : SolutionBase
{

    /// <summary>
    /// Helper класс для удобства обращения к кэшу
    /// </summary>
    private class Cache
    {
        /// <summary>
        /// Двумерный массив для кэширования результатов расчета.
        /// Первая размерность - (Length - 1), вторая - Sum.
        /// </summary>
        private readonly int[,] _cacheMultiLevel;

        public Cache(int[,] cacheMultiLevel)
        {
            _cacheMultiLevel = cacheMultiLevel;
        }

        public int this[int length, int sum]
        {
            get => _cacheMultiLevel[length - 1, sum];
            set => _cacheMultiLevel[length - 1, sum] = value;
        }
    }

    private readonly Cache _cache;

    public StaticCache()
    {
        _cache = new(new int[TaskParameters.LeftRightPartLength, TaskParameters.LeftRightPartLength * (TaskParameters.Base - 1) + 1]);
        // ↓ стартовый предрасчет значений и наполнение кэша
        for (var length = 1; length <= TaskParameters.LeftRightPartLength; length++)
        {
            var maxSum = length * (TaskParameters.Base - 1);
            for (var sum = 0; sum <= maxSum; sum++)
            {
                _cache[length, sum] = CountNumbersWithGivenSumOfDigitsCompute(length, sum);
            }
        }
    }

    /// <summary>
    /// Подсчитывает кол-во вариантов чисел заданной длины и с заданной суммой цифр
    /// </summary>
    /// <param name="length">Длина числа</param>
    /// <param name="sum">Сумма цифр</param>
    /// <returns>Кол-во вариантов</returns>
    /// <remarks>По существу, возвращает данные из кэша</remarks>
    internal override int CountNumbersWithGivenSumOfDigits(int length, int sum)
    {
        if (sum == 0)
        {
            return 1; // с суммой цифр = 0 только один вариант состоящий из всех нулей
        }

        if (length == 0)
        {
            return 0; // с нулевой длиной вариантов нет
        }

        return _cache[length, sum];
    }

    /// <summary>
    /// Подсчитывает кол-во вариантов чисел заданной длины и с заданной суммой цифр
    /// </summary>
    /// <param name="length">Длина числа</param>
    /// <param name="sum">Сумма цифр</param>
    /// <returns>Кол-во вариантов</returns>
    /// <remarks>По существу, производит расчет первого уровня, а для более низких уровней обращается к кэшу.
    /// NB! метод расчитан на "по-строчную" инициализацию кэша в конструкторе и не умеет делать расчет с произвольного места</remarks>
    private int CountNumbersWithGivenSumOfDigitsCompute(int length, int sum)
    {
        if (sum == 0)
        {
            return 1; // с суммой цифр = 0 только один вариант состоящий из всех нулей
        }

        if (length == 0)
        {
            return 0; // с нулевой длиной вариантов нет
        }

        var result = 0;

        for (var i = 0; i < TaskParameters.Base; i++)
        {
            if (sum >= i)
            {
                result += CountNumbersWithGivenSumOfDigits(length - 1, sum - i);
            }
            else
            {
                break;
            }
        }

        return result;
    }
}
