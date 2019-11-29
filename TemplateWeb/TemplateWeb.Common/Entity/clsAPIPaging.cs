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
    public sealed class clsAPIPaging
    {
        [DataMember()]
        public int draw;
        [DataMember()]
        public int recordsTotal;
        [DataMember()]
        public int recordsFiltered;
        [DataMember()]

        public object data;
        public clsAPIPaging()
        {
        }

        public static clsAPIPaging CreateResultJSONPaging(int intDraw, int intRecordTotal, int intRecordFiltered, object objData)
        {
            clsAPIPaging retDat = new clsAPIPaging();
            retDat.draw = intDraw;
            retDat.recordsTotal = intRecordTotal;
            retDat.recordsFiltered = intRecordFiltered;
            retDat.data = objData;
            return retDat;
        }


    }
}
 