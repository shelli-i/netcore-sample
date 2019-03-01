using System;
using System.Collections.Generic;
using System.Text;

namespace netcore_sample
{
    public class ReportService
    {
        private readonly IReportRepository _rep;

        public ReportService() { }

        public ReportService(IReportRepository rep)
         :base()
        {
            var msg = $"{this.GetType().Name} expects ctor injection.";

            this._rep = rep ?? throw new ArgumentNullException(msg);
        }

        public void Write(string _path, string _con, string _out, string _sp)
        {
            _rep.WriteBytes(_path, _con, _out, _sp);
        }
    }
}
