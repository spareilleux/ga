namespace GA.Core.Interfaces
{
    public interface IIndexer<in TKey, out TValue>
    {
        TValue this[TKey key] { get; }
    }
}
