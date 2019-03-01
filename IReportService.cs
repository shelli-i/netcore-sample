using System;
using System.Collections.Generic;
using System.Text;

namespace netcore_sample
{
    public interface IReportService
    {
        void Write(string rptpath, string con, string fileout, string sp);
    }
}
