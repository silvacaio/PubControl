using System;

namespace DGPub.Domain.Core
{
    public class EventBase
    {

    }

    public class Event<T> : EventBase where T : EventBase
    {
        public T Value { get; set; }

        public bool Valid { get; set; }

        public string Error { get; set; }

        public static Event<T> CreateSuccess(T value)
        {
            return new Event<T>
            {
                Value = value,
                Valid = true,
                Error = string.Empty
            };
        }

        public static Event<T> CreateError(string error)
        {
            return new Event<T>
            {
                Value = default,
                Valid = false,
                Error = error
            };
        }

        //  public static implicit operator Event<T>(T value) => CreateSuccess(value);
    }

    public class None : EventBase
    {
        public static None Create()
        {
            return new None();
        }
    }
}
