
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TemplateWeb.Common;
//
//   History.
//
//   20-September-2013                    Initial version.            (nosa) 
//
using DTG.Packaging;
using DTG.Spreadsheet;
using System.Text;

public class clsExcelAdapter
{

	public class ExcelSubHeader
	{
		public string txtColumnName;
		public int intColSpan;

		public int intRowSpan;
		public ExcelSubHeader()
		{
			txtColumnName = string.Empty;
			intColSpan = 0;
			intRowSpan = 0;
		}

		public ExcelSubHeader(string txtColumnName, int intColSpan, int intRowSpan)
		{
			this.txtColumnName = txtColumnName;
			this.intColSpan = intColSpan;
			this.intRowSpan = intRowSpan;
		}
	}

	public class ExcelSubFooter
	{
		public string txtColumnName;
		public int intColSpan;

		public int intRowSpan;
		public ExcelSubFooter()
		{
			txtColumnName = string.Empty;
			intColSpan = 0;
			intRowSpan = 0;
		}

		public ExcelSubFooter(string txtColumnName, int intColSpan, int intRowSpan)
		{
			this.txtColumnName = txtColumnName;
			this.intColSpan = intColSpan;
			this.intRowSpan = intRowSpan;
		}
	}

	private ExcelWorkbook Wbook;
	private int intLoop;
	int intStartRow;

	DataTable m_dtReport;
	public clsExcelAdapter()
	{
		Wbook = new ExcelWorkbook();
		intLoop = 65;
		//A 
		intStartRow = 0;
	}

	public void CreateNewWorksheet(string txtName, DataTable dtReport)
	{
		Wbook.Worksheets.Add(txtName);
		m_dtReport = dtReport;
	}

	public void PrintHeader(string[] txtHeader)
	{
		foreach (string txtHeaderItem in txtHeader) {
            string txtColumnMerge = ((char)intLoop).ToString() + (intStartRow.ToString()) + ":" + (((char)intLoop).ToString() + m_dtReport.Columns.Count).ToString() + (intStartRow).ToString();
			Wbook.Worksheets[0].Cells[txtColumnMerge].Value = txtHeaderItem.ToString();
			Wbook.Worksheets[0].Cells[txtColumnMerge].IsMerged = true;
			intStartRow += 1;
		}
	}

	public void PrintHeaderContent()
	{
		PrintHeaderContent(null);
	}

	public void PrintHeaderContent(ArrayList AliasList)
	{
		// intStartRow += 1
		int intTempLoop = intLoop;
		int j = 0;
		string txtColumn = ((char) (intTempLoop)).ToString();
		foreach (DataColumn dcol in m_dtReport.Columns) {
			if (intTempLoop - 65 > 25) {
				j = (intTempLoop - 65) / 25;
				txtColumn = ((char) (65 + (j - 1))).ToString() + ((char) (intTempLoop - ((25 * j) + 1))).ToString();
			} else {
				txtColumn = ((char) (intTempLoop)).ToString();
			}

			Wbook.Worksheets[0].Cells[txtColumn + intStartRow].Value = p_GetAliasColumn(dcol.ColumnName, AliasList);
			intTempLoop += 1;
		}
		intStartRow += 1;
	}

	public void PrintHeaderContentWithHeaderList(ArrayList subHeaderList)
	{
		intStartRow += 1;
		int intTempLoop = intLoop;
		int intColumnSpan = 0;
		int intRowSpan = 0;

		foreach (List<ExcelSubHeader> ExcelSubHeaderList in subHeaderList) {
			intTempLoop = intLoop;
			foreach (ExcelSubHeader dat in ExcelSubHeaderList) {
				while (Wbook.Worksheets[0].Cells[((char) (intTempLoop)).ToString() + intStartRow.ToString()].IsMerged) {
					intTempLoop += 1;
				}
				string txtColumnMerge = ((char) (intTempLoop)).ToString() + intStartRow.ToString();
				if (dat.intColSpan > 0) {
                    txtColumnMerge =((char)  (intTempLoop)).ToString() + intStartRow.ToString() + ":" + ((char) (intTempLoop + (dat.intColSpan - 1))).ToString() + (intStartRow).ToString();
					intColumnSpan += dat.intColSpan;
				} else {
					intColumnSpan = 0;
				}
				if (dat.intRowSpan > 0) {
					txtColumnMerge = ((char) (intTempLoop)).ToString() + (intStartRow).ToString() + ":" + ((char) (intTempLoop)).ToString() + (intStartRow + dat.intRowSpan).ToString();
					intRowSpan += dat.intRowSpan;
				} else {
					//intRowSpan = 0
				}
				Wbook.Worksheets[0].Cells[txtColumnMerge].Value = dat.txtColumnName;

				if (dat.intColSpan > 0 | dat.intRowSpan > 0) {
					Wbook.Worksheets[0].Cells[txtColumnMerge].IsMerged = true;
				}

				intTempLoop += 1;
			}
			intStartRow += 1;
		}
	}

	private string p_GetAliasColumn(string txtColumName, ArrayList AliasList)
	{

		if (AliasList == null) {
			return txtColumName;
		}
		if (AliasList.Count != 2) {
			return txtColumName;
		}

		ArrayList columnList = new ArrayList(0);
        ArrayList aliascolumnList = new ArrayList(1);

		for (int i = 0; i <= columnList.Count - 1; i++) {
			string columnName = columnList[i].ToString();
			if (txtColumName.ToLower().Trim().Equals(columnName.ToLower().Trim())) {
				return aliascolumnList[i].ToString(); 
			}
		}

		//Jika ga ketemu langsung balikin columnName
		return txtColumName;
	}

	public void PrintContent()
	{
		int intTempLoop = intLoop;
		int j = 0;
		string txtColumn = ((char) (intTempLoop)).ToString();
		foreach (DataColumn dcol in m_dtReport.Columns) {
			if (intTempLoop - 65 > 25) {
				j = (intTempLoop - 65) / 25;
				txtColumn = ((char) (65 + (j - 1))).ToString() + ((char) (intTempLoop - ((25 * j) + 1))).ToString();
			} else {
                txtColumn = ((char) intTempLoop).ToString();
			} 

			int intLoopCol = intStartRow;
			foreach (DataRow drow in m_dtReport.Rows) {
                if (Information.IsNumeric(drow[dcol.ColumnName]))
                {
					Wbook.Worksheets[0].Cells[txtColumn + intLoopCol].Value = drow[dcol.ColumnName];
					Wbook.Worksheets[0].Cells[txtColumn + intLoopCol].Style.StringFormat = "#,###";
                }
                else
                {
                    Wbook.Worksheets[0].Cells[txtColumn + intLoopCol].Value = drow[dcol.ColumnName];
                }
				intLoopCol += 1;
			}
			intTempLoop += 1;
			j += 1;
		}

		//Set row
		intStartRow += m_dtReport.Rows.Count;
	}

	public void PrintContentWithValue(List<string> ValueList)
	{
		int intTempLoop = intLoop;
		int intLoopCol = intStartRow;
		int j = 0;
		string txtColumn = ((char) (intTempLoop)).ToString();

		foreach (string value in ValueList) {
			if (intTempLoop - 65 > 25) {
				j = (intTempLoop - 65) / 25;
                txtColumn = ((char)(65 + (j - 1))).ToString() + ((char)intTempLoop - ((25 * j) + 1)).ToString();
			} else {
				txtColumn = ((char) (intTempLoop)).ToString();
			}

			Wbook.Worksheets[0].Cells[txtColumn + intLoopCol].Value = value;
			intTempLoop += 1;
			j += 1;
		}
		intLoopCol += 1;

		//Set row
		intStartRow += 1;
	}


	public void PrintSubFooterWithFooterList(ArrayList subFooterList)
	{
		int intTempLoop = intLoop;
		int intColumnSpan = 0;
		int intRowSpan = 0;

		foreach (List<ExcelSubFooter> ExcelSubFooterList in subFooterList) {
			intTempLoop = intLoop;
			foreach (ExcelSubFooter dat in ExcelSubFooterList) {
				while (Wbook.Worksheets[0].Cells[((char) (intTempLoop)).ToString() + intStartRow.ToString()].IsMerged) {
					intTempLoop += 1;
				}
				string txtColumnMerge = ((char) (intTempLoop)).ToString() + intStartRow.ToString();
				if (dat.intColSpan > 0) {
					txtColumnMerge = ((char) (intTempLoop)).ToString() + intStartRow.ToString() + ":" + ((char) (intTempLoop + dat.intColSpan - 1)).ToString() + (intStartRow).ToString();
					intColumnSpan += dat.intColSpan;
				} else {
					intColumnSpan = 0;
				}
				if (dat.intRowSpan > 0) {
					txtColumnMerge = ((char) (intTempLoop)).ToString() + (intStartRow).ToString() + ":" + ((char) (intTempLoop)).ToString() + (intStartRow + dat.intRowSpan).ToString();
					intRowSpan += dat.intRowSpan;
				} else {
					//intRowSpan = 0
				}
				Wbook.Worksheets[0].Cells[txtColumnMerge].Value = dat.txtColumnName;

				if (dat.intColSpan > 0 | dat.intRowSpan > 0) {
					Wbook.Worksheets[0].Cells[txtColumnMerge].IsMerged = true;
				}

				intTempLoop += 1;
			}
			intStartRow += 1;
		}
	}

	public void SaveExcel(string txtFilePath)
	{
		Wbook.WriteXLS(txtFilePath);
	}

	public void CloseExcel()
	{
		Wbook = null;
	}


}
 