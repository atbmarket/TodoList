namespace TodoList
{
    public interface INotifier<T>
    {
        void Notify(T message);
    }
}