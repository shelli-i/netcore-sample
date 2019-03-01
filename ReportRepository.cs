using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OfficeOpenXml;
using System.IO;
using System.Drawing;

namespace netcore_sample
{
    public class ReportRepository
    {
        private readonly string _reptpath;
        private readonly string _conn;
        private readonly string _fileOut;
        private readonly string _sp;

        public ReportRepository()
        { }
        public ReportRepository(string reptpath, string conn, string fileOut, string sp)
            : base()
        {
            var msg = $"{this.GetType().Name} expects ctor injection.";

            this._conn = conn;
            this._reptpath = reptpath;
            this._fileOut = fileOut;
            this._sp = sp;
        }
        public DataSet FindAllFor(string _con, string p)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection con = new SqlConnection(_con);
                cmd = new SqlCommand(p, con)
                {
                    CommandTimeout = 600
                };

                var ds = new DataSet();

                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                return ds;
            }
            catch (SqlException se)
            {
                throw se;
            }
            finally
            {
                if (cmd != null)
                    cmd.Connection.Close();
            }

        }
        private byte[] GetExcelSheet(string con, string p)
        {
            try
            {
                var ds = FindAllFor(con, p);

                using (var pkg = new ExcelPackage())
                {
                    // first worksheet summary
                    var lastws = pkg.Workbook.Worksheets.Add("Filed Clms Schs");
                    lastws.Cells["A1"].LoadFromDataTable(ds.Tables[0], true);
                    lastws.Cells.AutoFitColumns();
                    int srow = lastws.Dimension.End.Row,
                           scol = lastws.Dimension.End.Column;
                    //start row, start col, end row, end col to apply formats 
                    lastws.Cells[2, 6, srow, 6].Style.Numberformat.Format = "MM/dd/yyyy";
                    lastws.Cells[1, 1, 1, scol].Style.Font.Bold = true;

                    return pkg.GetAsByteArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void WriteBytes(string reptpath, string conn, string fileOut, string sp)
        {

            var FileContents = GetExcelSheet(conn, sp);
            File.WriteAllBytes(reptpath, FileContents);

            string fOut = fileOut;
            FileInfo fc = new FileInfo(reptpath);
            if (fc.Exists == true)
            {
                fc.CopyTo(fOut, true);
            }
        }
    }
}
