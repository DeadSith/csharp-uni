namespace Lab1
{
    public interface IRateAndCopy<T>
    {
        double Rating { get; }

        T DeepCopy();
    }
}