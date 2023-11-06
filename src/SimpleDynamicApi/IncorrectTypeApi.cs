using Soneta.Business;

namespace SimpleDynamicApi
{
    public class IncorrectTypeApi: IIncorrectTypeApi
    {
        public IRow GetRowById(int id) => null;

        public int SetRow(IRow row) => -1;

        public IGuidedRow GetGuidedRowById(int id) => null;

        public int SetGuidedRow(IGuidedRow row) => -1;
    }
}
