public class QuantityUnit
{
    public int QuantityUnitId { get; set; }
    public string Name { get; set; }

    public QuantityUnit(int quantityUnitId, string name)
    {
        QuantityUnitId = quantityUnitId;
        Name = name;
    }

    public QuantityUnit() {}
}