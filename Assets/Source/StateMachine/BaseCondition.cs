using System;

namespace GlobalSource
{
    public class BaseCondition : IStateCondition
    {
        public byte State { get; }

        private readonly Func<bool> _condition;

        public BaseCondition(byte state, Func<bool> condition)
        {
            State = state;
            _condition = condition;
        }

        public bool Invoke()
        {
            return _condition();
        }
    }
}