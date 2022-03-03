using System.ComponentModel.DataAnnotations;

namespace ToDoList.Common
{
    public enum TodoStatus
    {
        [Display(Name = "Pending")]
        Pending = 10,

        [Display(Name = "In Progress")]
        InProgress = 20,

        [Display(Name = "Done")]
        Done = 30
    }
}
