using System;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

using System.Data.SqlClient;
using System.Data;

namespace TemplateWeb.Common
{
    [Serializable]
    [DataContractAttribute]
    public class clsMLOV
    {
        [DataMember]
        public string txtColumn1 { get; set; }
        [DataMember]
        public string txtColumn2 { get; set; }
        [DataMember]
        public string txtColumn3 { get; set; }
        [DataMember]
        public string txtColumn4 { get; set; }
        [DataMember]
        public string txtColumn5 { get; set; }
        [DataMember]
        public string txtColumn6 { get; set; }
        [DataMember]
        public string txtColumn7 { get; set; }
        [DataMember]
        public string txtColumn8 { get; set; }
        [DataMember]
        public string txtColumn9 { get; set; }
        [DataMember]
        public string txtColumn10 { get; set; }
        [DataMember]
        public string txtColumnName1 { get; set; }
        [DataMember]
        public string txtColumnName2 { get; set; }
        [DataMember]
        public string txtColumnName3 { get; set; }
        [DataMember]
        public string txtColumnName4 { get; set; }
        [DataMember]
        public string txtColumnName5 { get; set; }
        [DataMember]
        public string txtColumnName6 { get; set; }
        [DataMember]
        public string txtColumnName7 { get; set; }
        [DataMember]
        public string txtColumnName8 { get; set; }
        [DataMember]
        public string txtColumnName9 { get; set; }
        [DataMember]
        public string txtColumnName10 { get; set; }
        [DataMember]
        public decimal decTotalRow { get; set; }

        public clsMLOV ()
        {
            txtColumn1 = string.Empty;
            txtColumn2 = string.Empty;
            txtColumn3 = string.Empty;
            txtColumn4 = string.Empty;
            txtColumn5 = string.Empty;
            txtColumn6 = string.Empty;
            txtColumn7 = string.Empty;
            txtColumn8 = string.Empty;
            txtColumn9 = string.Empty;
            txtColumn10 = string.Empty;
            txtColumnName1 = string.Empty;
            txtColumnName2 = string.Empty;
            txtColumnName3 = string.Empty;
            txtColumnName4 = string.Empty;
            txtColumnName5 = string.Empty;
            txtColumnName6 = string.Empty;
            txtColumnName7 = string.Empty;
            txtColumnName8 = string.Empty;
            txtColumnName9 = string.Empty;
            txtColumnName10 = string.Empty;
            decTotalRow = 0; 
        }


        public static clsMLOV ConvertFromDataReader(IDataReader dataReader)
        {
            clsMLOV retDat = new clsMLOV();
            retDat.txtColumn1 = clsGlobal.DataReaderGetString(dataReader["Column1"]);
            retDat.txtColumn2 = clsGlobal.DataReaderGetString(dataReader["Column2"]);
            retDat.txtColumn3 = clsGlobal.DataReaderGetString(dataReader["Column3"]);
            retDat.txtColumn4 = clsGlobal.DataReaderGetString(dataReader["Column4"]);
            retDat.txtColumn5 = clsGlobal.DataReaderGetString(dataReader["Column5"]);
            retDat.txtColumn6 = clsGlobal.DataReaderGetString(dataReader["Column6"]);
            retDat.txtColumn7 = clsGlobal.DataReaderGetString(dataReader["Column7"]);
            retDat.txtColumn8 = clsGlobal.DataReaderGetString(dataReader["Column8"]);
            retDat.txtColumn9 = clsGlobal.DataReaderGetString(dataReader["Column9"]);
            retDat.txtColumn10 = clsGlobal.DataReaderGetString(dataReader["Column10"]);
            retDat.txtColumnName1 = clsGlobal.DataReaderGetString(dataReader["ColumnName1"]);
            retDat.txtColumnName2 = clsGlobal.DataReaderGetString(dataReader["ColumnName2"]);
            retDat.txtColumnName3 = clsGlobal.DataReaderGetString(dataReader["ColumnName3"]);
            retDat.txtColumnName4 = clsGlobal.DataReaderGetString(dataReader["ColumnName4"]);
            retDat.txtColumnName5 = clsGlobal.DataReaderGetString(dataReader["ColumnName5"]);
            retDat.txtColumnName6 = clsGlobal.DataReaderGetString(dataReader["ColumnName6"]);
            retDat.txtColumnName7 = clsGlobal.DataReaderGetString(dataReader["ColumnName7"]);
            retDat.txtColumnName8 = clsGlobal.DataReaderGetString(dataReader["ColumnName8"]);
            retDat.txtColumnName9 = clsGlobal.DataReaderGetString(dataReader["ColumnName9"]);
            retDat.txtColumnName10 = clsGlobal.DataReaderGetString(dataReader["ColumnName10"]);
            retDat.decTotalRow = clsGlobal.DataReaderGetDecimal(dataReader["decTotalRow"]);
             
            return retDat;
        }



        public static clsMLOV ConvertFromDataRow(DataRow drow)
        {
            clsMLOV retDat = new clsMLOV();
            try
            {
                retDat.txtColumn1 = clsGlobal.DataReaderGetString(drow["Column1"]);
            }
            catch (Exception)
            {
                retDat.txtColumn1 = string.Empty;
            }
            try
            {
                retDat.txtColumn2 = clsGlobal.DataReaderGetString(drow["Column2"]);
            }
            catch (Exception)
            {
                retDat.txtColumn2 = string.Empty;
            }
            try
            {
                retDat.txtColumn3 = clsGlobal.DataReaderGetString(drow["Column3"]);
            }
            catch (Exception)
            {
                retDat.txtColumn3 = string.Empty;
            }
            try
            {
                retDat.txtColumn4 = clsGlobal.DataReaderGetString(drow["Column4"]);
            }
            catch (Exception)
            {
                retDat.txtColumn4 = string.Empty;
            }
            try
            {
                retDat.txtColumn5 = clsGlobal.DataReaderGetString(drow["Column5"]);
            }
            catch (Exception)
            {
                retDat.txtColumn5 = string.Empty;
            }
            try
            {
                retDat.txtColumn6 = clsGlobal.DataReaderGetString(drow["Column6"]);
            }
            catch (Exception)
            {
                retDat.txtColumn6 = string.Empty;
            }
            try
            {
                retDat.txtColumn7 = clsGlobal.DataReaderGetString(drow["Column7"]);
            }
            catch (Exception)
            {
                retDat.txtColumn7 = string.Empty;
            }
            try
            {
                retDat.txtColumn8 = clsGlobal.DataReaderGetString(drow["Column8"]);
            }
            catch (Exception)
            {
                retDat.txtColumn9 = string.Empty;
            }
            try
            {
                retDat.txtColumn9 = clsGlobal.DataReaderGetString(drow["Column9"]);
            }
            catch (Exception)
            {
                retDat.txtColumn9 = string.Empty;
            }
            try
            {
                retDat.txtColumn10 = clsGlobal.DataReaderGetString(drow["Column10"]);
            }
            catch (Exception)
            {
                retDat.txtColumn10 = string.Empty;
            }

            try
            {
                retDat.txtColumnName1 = clsGlobal.DataReaderGetString(drow["ColumnName1"]);
            }
            catch (Exception)
            {
                retDat.txtColumnName1 = string.Empty;
            }
            try
            {
                retDat.txtColumnName2 = clsGlobal.DataReaderGetString(drow["ColumnName2"]);
            }
            catch (Exception)
            {
                retDat.txtColumnName2 = string.Empty;
            }
            try
            {
                retDat.txtColumnName3 = clsGlobal.DataReaderGetString(drow["ColumnName3"]);
            }
            catch (Exception)
            {
                retDat.txtColumnName3 = string.Empty;
            }
            try
            {
                retDat.txtColumnName4 = clsGlobal.DataReaderGetString(drow["ColumnName4"]);
            }
            catch (Exception)
            {
                retDat.txtColumnName4 = string.Empty;
            }
            try
            {
                retDat.txtColumnName5 = clsGlobal.DataReaderGetString(drow["ColumnName5"]);
            }
            catch (Exception)
            {
                retDat.txtColumnName5 = string.Empty;
            }
            try
            {
                retDat.txtColumnName6 = clsGlobal.DataReaderGetString(drow["ColumnName6"]);
            }
            catch (Exception)
            {
                retDat.txtColumnName6 = string.Empty;
            }
            try
            {
                retDat.txtColumnName7 = clsGlobal.DataReaderGetString(drow["ColumnName7"]);
            }
            catch (Exception)
            {
                retDat.txtColumnName7 = string.Empty;
            }
            try
            {
                retDat.txtColumnName8 = clsGlobal.DataReaderGetString(drow["ColumnName8"]);
            }
            catch (Exception)
            {
                retDat.txtColumnName8 = string.Empty;
            }
            try
            {
                retDat.txtColumnName9 = clsGlobal.DataReaderGetString(drow["ColumnName9"]);
            }
            catch (Exception)
            {
                retDat.txtColumnName9 = string.Empty;
            }
            try
            {
                retDat.txtColumnName10 = clsGlobal.DataReaderGetString(drow["ColumnName10"]);
            }
            catch (Exception)
            {
                retDat.txtColumnName10 = string.Empty;
            }
            try
            {
                retDat.decTotalRow = clsGlobal.DataReaderGetDecimal(drow["decTotalRow"]);
            }
            catch (Exception)
            {
                retDat.decTotalRow = decimal.Zero;
            }
            return retDat;
        } 
    } 
}