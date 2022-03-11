namespace ThirteenBase;

/// <summary>
/// Параметры из условия задачи
/// </summary>
/// <param name="Base">Основание системы счисления</param>
/// <param name="NumberLength">Длина числа</param>
/// <param name="LeftRightPartLength">Длина сравниваемой части (левой/правой)</param>
public record TaskParameters(
    int Base,
    int NumberLength,
    int LeftRightPartLength)
{

    /// <summary>
    /// Множитель вариантов за счет серединной части: Base ^ (NumberLength - LeftRightPartLength * 2)
    /// </summary>
    public int MiddlePartMultiplier
    {
        get
        {
            if (_middlePartMultiplier is null)
            {
                var middlePartMultiplier = 1;
                var middlePartLength = NumberLength - LeftRightPartLength * 2;
                for (var i = 0; i < middlePartLength; i++)
                {
                    middlePartMultiplier *= Base;
                }
                _middlePartMultiplier = middlePartMultiplier;
            }
            return _middlePartMultiplier.Value;
        }
    }
    private int? _middlePartMultiplier;

    public static readonly TaskParameters Default = new(Base: 13, NumberLength: 13, LeftRightPartLength: 6);
}
