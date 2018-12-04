namespace TodoList
{
    public interface ITaskNotifier
    {
        void Notify(ITodoTask task);
    }
}