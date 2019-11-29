using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace TemplateWeb.Common.Entity
{
    [Serializable()]
    [DataContractAttribute()]
    public sealed class clsAPI
    {
        [DataMember()]
        private bool m_bitSuccess;
        [DataMember()]
        private object m_objData;
        [DataMember()]
        private string m_txtMessage;
        [DataMember()]

        private string m_txtStackTrace;
        public clsAPI()
        {
        }

        public static clsAPI CreateError(Exception ex)
        {
            clsAPI retDat = new clsAPI();
            retDat.bitSuccess = false;
            retDat.objData = null;
            retDat.txtMessage = ex.Message.ToString();
            retDat.txtStackTrace = ex.StackTrace.ToString();
            return retDat;
        }

        public static clsAPI CreateResult(bool bitSuccess, object objData, string txtMessage, string txtStackTrace)
        {
            clsAPI retDat = new clsAPI();
            retDat.bitSuccess = bitSuccess;
            retDat.objData = objData;
            retDat.txtMessage = txtMessage;
            retDat.txtStackTrace = txtStackTrace;
            return retDat;
        }

        [DataMember()]
        public bool bitSuccess
        {
            get { return m_bitSuccess; }
            set { m_bitSuccess = value; }
        }

        [DataMember()]
        public object objData
        {
            get { return m_objData; }
            set { m_objData = value; }
        }

        [DataMember()]
        public string txtMessage
        {
            get { return m_txtMessage; }
            set { m_txtMessage = value; }
        }

        [DataMember()]
        public string txtStackTrace
        {
            get { return m_txtStackTrace; }
            set { m_txtStackTrace = value; }
        }

    }
}
 