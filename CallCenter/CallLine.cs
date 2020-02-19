using System.Collections.Generic;

namespace CallCenter
{
    class CallLine
    {
        private readonly Queue<Call> _callLine = new Queue<Call>();

        public int Count { get { return _callLine.Count; } }
        public long TotalWaitingDurationInMilliseconds { get; private set; }

        public bool TryPeek(out Call call)
        {
            return _callLine.TryPeek(out call);
        }

        public void Dequeue()
        {
            var call = _callLine.Dequeue();

            TotalWaitingDurationInMilliseconds -= call.DurationInMilliseconds;
        }

        public void Enqueue(Call call)
        {
            _callLine.Enqueue(call);

            TotalWaitingDurationInMilliseconds += call.DurationInMilliseconds;
        }
    }
}
