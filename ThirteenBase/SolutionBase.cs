namespace ThirteenBase;

/// <summary>
/// Базовый класс для демонстрации различных подходов к решению.
/// </summary>
public abstract class SolutionBase
{
    /// <summary>
    /// Параметры из условия задачи
    /// </summary>
    public TaskParameters TaskParameters { get; init; } = TaskParameters.Default;

    /// <summary>
    /// Даёт ответ на поставленный в задаче вопрос.
    /// Сложность = O (Base ^ (LeftRightPartLength - 1)) * O (CountNumbersWithGivenSumOfDigits)
    /// Сложность O (CountNumbersWithGivenSumOfDigits) следующая в зависимости от варианта решения:
    ///     * PlainStupid - O (Base * LeftRightPartLength)
    ///     * для остальных решений сложность O(1), однако добавляется небольшой startup cost на прогрев кэша
    /// </summary>
    /// <returns>Количество "красивых" чисел с лидирующим нулём</returns>
    public long Solve()
    {
        // так как левый разряд должен быть 0, фиксируем его и используем длину перебираемых чисел уменьшенную на единицу
        return Solve(TaskParameters.LeftRightPartLength - 1, 0);
    }

    internal long Solve(int lengthCountDown, int sumOfDigits)
    {
        if (lengthCountDown == 0)
        {
            return TaskParameters.MiddlePartMultiplier * CountNumbersWithGivenSumOfDigits(TaskParameters.LeftRightPartLength, sumOfDigits);
        }

        var result = 0L;
        for (var i = 0; i < TaskParameters.Base; i++)
        {
            result += Solve(lengthCountDown - 1, sumOfDigits + i);
        }

        return result;
    }

    /// <summary>
    /// Подсчитывает кол-во вариантов чисел заданной длины и с заданной суммой цифр
    /// </summary>
    /// <param name="length">Длина числа</param>
    /// <param name="sum">Сумма цифр</param>
    /// <returns>Кол-во вариантов</returns>
    internal abstract int CountNumbersWithGivenSumOfDigits(int length, int sum);
}
