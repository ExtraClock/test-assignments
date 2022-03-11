namespace ThirteenBase;

/// <summary>
/// Решение "в лоб"
/// </summary>
public class PlainStupid : SolutionBase
{

    // наивное решение с рекурсией, сложность вроде и небольшая, но в комплекте со сложностью основной части делает решение бесполезным
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
