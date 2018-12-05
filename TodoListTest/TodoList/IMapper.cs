namespace TodoList
{
    public interface IMapper<T, T1>
    {
        T Map(T1 task);
    }
}