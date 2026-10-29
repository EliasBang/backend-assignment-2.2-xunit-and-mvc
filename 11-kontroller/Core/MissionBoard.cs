namespace SpaceAcademy;

public class MissionBoard
{
    private List<MissionTask> _tasks = new List<MissionTask>();
    private int _nextId = 1;
    public int Count { get { return _tasks.Count; } }

    public MissionTask Add(string title)
    {
        var task = new MissionTask(_nextId, title);
        _nextId++;
        _tasks.Add(task);
        return task;
    }

    public List<MissionTask> GetAll()
    {
        return new List<MissionTask>(_tasks);
    }

    public bool Complete(int id)
    {
        foreach (var task in _tasks)
        {
            if (task.Id == id)
            {
                task.Complete();
                return true;
            }
        }
        return false;
    }
}
