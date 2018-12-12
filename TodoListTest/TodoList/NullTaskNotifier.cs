namespace TodoList
{
    public class NullTaskNotifier : INotifier<ITodoTask>
    {
        public void Notify(ITodoTask message)
        {
            System.Console.WriteLine(message.Content);
        }
    }
}