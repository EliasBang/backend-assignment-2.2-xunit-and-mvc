namespace SpaceAcademy;

public class CargoBay
{
    private List<string> _items = new List<string>();
    public int Count { get { return _items.Count; } }

    public bool TryAdd(string name)
    {
        if (_items.Count >= 2)
        {
            return false;
        }
        _items.Add(name);
        return true;
    }
}
