using Diary.Models;
using DiaryWPF.Models;
using System.Collections.ObjectModel;

namespace DiaryWPF.ViewModels
{
    /// <summary>
    /// View model for the InvboxView.
    /// It stores the filtered tasks.
    /// </summary>
    public class InboxViewModel
    {
        private readonly ObservableCollection<DiaryTask> userTasks = UserManager.CurrentUser.Tasks;

        public ObservableCollection<DiaryTask> FilteredTasks
        {
            get 
            { 
                return DiaryTask.FilterTasks(userTasks); 
            }
        }
    }
}
