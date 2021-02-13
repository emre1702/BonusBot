using System.Threading.Tasks;

namespace BonusBot.Common.Extensions
{
    public static class TaskExtensions
    {
        public static async void IgnoreResult(this Task task)
        {
            try
            {
                await task;
            }
            catch { }
        }
    }
}