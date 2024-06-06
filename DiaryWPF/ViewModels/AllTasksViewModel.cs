using Diary.Models;
using Diary.ViewModels;
using DiaryWPF.Models;
using System.Collections.ObjectModel;

namespace DiaryWPF.ViewModels
{
    /// <summary>
    /// View model for the AllTasksView.
    /// </summary>
    public class AllTasksViewModel : ViewModelBase
    {
        private readonly ObservableCollection<DiaryTask> userTasks = UserManager.CurrentUser.Tasks;

        public ObservableCollection<DiaryTask> UserTasks { get { return userTasks; } }
    }
}
