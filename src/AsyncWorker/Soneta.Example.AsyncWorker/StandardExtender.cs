using Soneta.Business;
using Soneta.CRM;
using Soneta.Example.AsyncWorker;

[assembly: Worker(typeof(StandardExtender))]

namespace Soneta.Example.AsyncWorker
{
    public sealed class StandardExtender
    {
        private LongLoadingCalculator _calculator;
        private Kontrahent _kontrahent;
        private decimal? _value;

        [Context]
        public Kontrahent Kontrahent
        {
            get { return _kontrahent; }
            set
            {
                if (_kontrahent != value) _value = null;
                _kontrahent = value;
            }
        }

        public decimal Value => _value ?? (_value = Calculator.GetValue()).Value;

        private LongLoadingCalculator Calculator =>
            _calculator ?? (_calculator = new LongLoadingCalculator());
    }
}