
namespace TodoItems.Core.Model;

public class TodoItemDTO
{
    public string Id;
    public string Description { get; set; }
    public DateTime DueDay { get; set; }
    public string UserId { get; set; }
    public bool Done { get; set; }
    public bool Favorite { get; set; }
    public DateTime CreatedTime { get; set; }
    public List<Modification> ModificationList { get; set; }

    public TodoItemDTO(string description, DateTime dueDay, string userId)
    {
        Id = Guid.NewGuid().ToString();
        Description = description;
        DueDay = dueDay;
        UserId = userId;
        ModificationList = new List<Modification>();
    }
    public TodoItemDTO(string id,string description, DateTime dueDay, string userId)
    {
        Id = id;
        Description = description;
        DueDay = dueDay;
        UserId = userId;
        ModificationList = new List<Modification>();
    }
    public TodoItemDTO(string id,string description, DateTime dueDay, string userId,List<Modification> modificationList)
    {
        Id = id;
        Description = description;
        DueDay = dueDay;
        UserId = userId;
        ModificationList = modificationList;
    }

    public TodoItemDTO()
    {
    }

    public void Modify(string description)
    {
        int count = 0;
        ModificationList.ForEach(modification =>
        {
            if (DateTime.Now.Subtract(modification.TimeStamp).TotalDays <= 1)
            {
                count++;
            }
        });
        if (count >= Constants.MAX_MODIFY_TIME_ONE_DAY)
        {
            throw new MaximumModificationException("You have reached the maximum number of modifications for today. Please try agian tomorrow.");
        }

        if (!description.Equals(Description))
        {
            Description = description;
            ModificationList.Add(new Modification());
        }
    }
}
