using System;
using System.Collections.Generic;
using System.Text;

namespace netcore_sample
{
    public interface IReportRepository
    {
     
            void WriteBytes(string rptpath, string con, string fileout, string sp);
    }
}
