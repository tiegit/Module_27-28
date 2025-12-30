public class Item
{
    public Item(string title, ItemType type, int weight)
    {
        Title = title;
        Type = type;
        Weight = weight;
    }

    public string Title { get; }
    public ItemType Type { get; }
    public int Weight { get; }

    public string GetInfo()
    {
        return Title + ", " + Type +", " + Weight;
    }
}
