namespace SpaceAcademy;

public class MissionTask
{
    public int Id { get; }
    public string Title { get; }
    public bool IsCompleted { get; private set; }

    public MissionTask(int id, string title)
    {
        Id = id;
        Title = title;
    }

    public void Complete()
    {
        IsCompleted = true;
    }
}
