using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace v4l2_csharp_api_bindings;


public class BoundedQueue<T>
{
    private readonly BlockingCollection<T> _queue;
    private readonly int _maxSize;

    public BoundedQueue(int maxSize)
    {
        _maxSize = maxSize;
        _queue = new BlockingCollection<T>(new ConcurrentQueue<T>(), _maxSize);
    }

    public void Enqueue(T item)
    {
        // Add new item. If the queue is full, remove the oldest items first.
        while (_queue.Count >= _maxSize)
        {
            if (_queue.TryTake(out T? element))
            {
                if (element is IDisposable disposable) disposable.Dispose();
            }
        }
        _queue.Add(item);
    }

    public T Dequeue(CancellationToken cancellationToken)
    {
        // This will block until an item is available to be removed
        return _queue.Take(cancellationToken);
    }

    public void Consume(BoundedQueue<T> source, CancellationToken cancellationToken)
    {
        Task.Run(() =>
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var image = source.Dequeue(cancellationToken);
                    Enqueue(image);
                }
            }
            catch (OperationCanceledException)
            {
                // Ignore
            }
        }, cancellationToken);
    }

    public IEnumerable<T> GetConsumingEnumerable()
    {
        return _queue.GetConsumingEnumerable();
    }

    public void CompleteAdding()
    {
        _queue.CompleteAdding();
    }

    public bool IsCompleted => _queue.IsCompleted;

    public int Count => _queue.Count;

    public bool IsEmpty => _queue.Count == 0;
}
