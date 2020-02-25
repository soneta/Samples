using Soneta.Business;
using Soneta.CRM;

namespace Soneta.Example.AsyncWorker
{
    public abstract class AsyncExtenderBase : IAsyncWorker, IAsyncIsReady
    {
        private LongLoadingCalculator _calculator;
        private bool _cancelled;
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

        public decimal Value
        {
            get
            {
                if (_value == null) throw new InProgressException();
                return _value.Value;
            }
        }

        public string ValueStatus =>
            _value == null
                ? "ładowanie ..."
                : _cancelled ? "przerwane" : "obliczone";

        private LongLoadingCalculator Calculator =>
            _calculator ?? (_calculator = new LongLoadingCalculator());

        public bool IsActionReady(IAsyncContext acx) => IsActionReady();

        public void Action(IAsyncContext acx)
        {
            _value = Calculator.GetValue(acx);
            _cancelled = acx.IsCancellationRequested;
        }

        public bool IsVisibleValue() => _value != null;

        public bool IsVisibleValueStatus() => IsActionReady();

        private bool IsActionReady() => _value == null || _cancelled;
    }
}