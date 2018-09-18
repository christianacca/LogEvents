using System;

namespace CcAcca.LogEvents
{
    /// <summary>
    /// A metric whose value can change
    /// </summary>
    public abstract class DynamicMetric
    {
        public abstract double Value { get; }

        public static DynamicMetric From(Func<double> valueSelector)
        {
            return new LambdaDynamicMetric(valueSelector);
        }

        public static DynamicMetric From<T>(Func<T, double> valueSelector, T state)
        {
            return new LambdaDynamicMetric(() => valueSelector(state));
        }

        private class LambdaDynamicMetric : DynamicMetric
        {
            public LambdaDynamicMetric(Func<double> valueSelector)
            {
                ValueSelector = valueSelector ?? throw new ArgumentNullException(nameof(valueSelector));
            }

            public override double Value => ValueSelector();

            private Func<double> ValueSelector { get; }
        }
    }
}