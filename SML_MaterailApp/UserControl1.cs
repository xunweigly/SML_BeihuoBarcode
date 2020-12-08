using System;
using System.Data;
using System.Windows.Forms;
using fuzhu;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using LKU8.shoukuan.fuzhu;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using BarcodeLib;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;




namespace LKU8.shoukuan
{
    public partial class UserControl1 : UserControl
    {



        DataTable dt = new DataTable();
        DataTable dtPrint = new DataTable();
        bool m_checkStatus; //表头复选框用
        string ls = Environment.CurrentDirectory;
        public UserControl1()
        {
            InitializeComponent();

        }
        #region 初始化
        private void UserControl1_Load(object sender, EventArgs e)
        {

            DevExpress.Accessibility.AccLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressUtilsLocalizationCHS();
            DevExpress.XtraEditors.Controls.Localizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraEditorsLocalizationCHS();
            DevExpress.XtraGrid.Localization.GridLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraGridLocalizationCHS();
            DevExpress.XtraLayout.Localization.LayoutLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraLayoutLocalizationCHS();
            //DevExpress.XtraNavBar.NavBarLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraNavBarLocalizationCHS();
            DevExpress.XtraPrinting.Localization.PreviewLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraPrintingLocalizationCHS();
            //DevExpress.XtraReports.Localization.ReportLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraReportsLocalizationCHS();
     
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            //reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);



            //string[] fileInfo = Directory.GetFiles(ls + "\\标签格式", "*.rdlc");
            //foreach (string s in fileInfo)
            //{
            //    cmbmb.Items.Add(s.ToString());
            //}

           
            
        }





       
#endregion
        #region gridview1表头复选框
        private void gridView1_Click(object sender, EventArgs e)
        {
            if (DevControlHelper.ClickGridCheckBox(this.gridView1, "chk", m_checkStatus))
            {
                m_checkStatus = !m_checkStatus;
            }

        }

        private void gridView1_CustomDrawColumnHeader
  (object sender, DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column != null && (e.Column.FieldName == "chk"))
            {
                e.Info.InnerElements.Clear();
                e.Painter.DrawObject(e.Info);
                DevControlHelper.DrawCheckBox(e, m_checkStatus);
                e.Handled = true;
            }
        }
        void gridView1_DataSourceChanged(object sender, EventArgs e)
        {
            GridColumn column = this.gridView1.Columns.ColumnByFieldName("chk");
            if (column != null)
            {
                column.Width = 80;
                column.OptionsColumn.ShowCaption = false;
                column.ColumnEdit = new RepositoryItemCheckEdit();
            }


        }
        #endregion

        #region 数据校验
    
        private string txtJYxiang()
        {
            if (string.IsNullOrEmpty(txtMocode.Text))
            {
                CommonHelper.MsgInformation("生产订单号不能为空");
                return "error";
            }

            if (string.IsNullOrEmpty(txtcInvcode.Text))
            {
                CommonHelper.MsgInformation("存货编码不能为空");
                return "error";
            }

            

      
            //if (string.IsNullOrEmpty(txtqty.Text) || Convert.ToInt16(txtqty.Text) == 0)
            //{
            //    CommonHelper.MsgInformation("数量不能为0、空");
            //    return "error";
            //}

            //if (string.IsNullOrEmpty(txtpage.Text) || Convert.ToInt16(txtpage.Text) == 0)
            //{
            //    CommonHelper.MsgInformation("打印数量不能为0或空");
            //    return "error";
            //}




            return "ok";
        }
        #endregion


        #region 生成条形码
        public static byte[] GetBarcode2(int height, int width, TYPE type, string code, out Image image)
        {
            LKU8.shoukuan.fuzhu.BarCode.Code128 b = new LKU8.shoukuan.fuzhu.BarCode.Code128();
            //b.ValueFont = new Font("宋体", 10);
            b.ValueFont = null;
            //b.Height = 30;
            
           image = b.GetCodeImage(code, LKU8.shoukuan.fuzhu.BarCode.Code128.Encode.Code128A);
            MemoryStream ms = new MemoryStream();
            byte[] buffer = null;
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            buffer = ms.GetBuffer();
            
            return buffer;
        }
        public static byte[] GetBarcodeEAN13(int height, int font, TYPE type, string code, out Image image)
        {
            //获取验证位
            //char _ISBN = LKU8.shoukuan.fuzhu.EAN13.EAN13ISBN(code);
            //MessageBox.Show(_ISBN.ToString());
            LKU8.shoukuan.fuzhu.EAN13 _EAN13Code = new LKU8.shoukuan.fuzhu.EAN13();
            _EAN13Code.Magnify = 1;
            _EAN13Code.Heigth = 120;
            _EAN13Code.FontSize = 16;
            image = _EAN13Code.GetCodeImage(code);
            MemoryStream ms = new MemoryStream();
            byte[] buffer = null;
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            buffer = ms.GetBuffer();

            return buffer;



            //pictureBox1.Image = _EAN13Code.GetCodeImage("6901028036955");
            //pictureBox1.Image.Save(@"C:/1.bmp");
        }
        //, BarcodeType type, BarcodeTextPosition txtpos

      
        #endregion

        private string Condition()
        {
            string conditionSql = "";
            SearchCondition searchObj = new SearchCondition();

            searchObj.AddCondition("ddate", dateTimePicker1.Text, SqlOperator.MoreThanOrEqual, dateTimePicker1.Checked == false);
            searchObj.AddCondition("ddate", dateTimePicker2.Text, SqlOperator.LessThanOrEqual, dateTimePicker2.Checked == false);
            searchObj.AddCondition("b.cmocode", txtMocode.Text, SqlOperator.Equal);
            searchObj.AddCondition("b.cInvCode", txtcInvcode.Text, SqlOperator.Equal);
            //searchObj.AddCondition("a.ccode", txtcCode.Text, SqlOperator.Equal);
            searchObj.AddCondition("a.crdcode", txtcRdcode.Text, SqlOperator.Equal);
            searchObj.AddCondition("b.cWhCode", txtcWhcode.Text, SqlOperator.Equal);
            searchObj.AddCondition("i.cinvdefine4", txtcPoscode.Text, SqlOperator.Like);

            conditionSql = searchObj.BuildConditionSql(2);
            if (cmbShow.Text == "否")
            {
                conditionSql += " and isnull(b.cdefine35,'')='' ";
            }

            if (checkBox1.Checked)
            {

                conditionSql += " and (b.cwhcode<>'0104' and  i.cinvname not like '%组件%'  AND i.cinvname not like '%模组%' )";
            
            }

            //领料单号多选
               string sCode = txtcCode.Text.Trim();
               if (!string.IsNullOrEmpty(sCode))
               {
                   conditionSql += " and (";
                   int k = 0;
                   string sCode1;// 取/前面的数据，sCode剩余的数据
                   while (sCode.IndexOf("/") > 0)
                   {
                       sCode1 = sCode.Substring(0, sCode.IndexOf("/"));
                       sCode = sCode.Substring(sCode.IndexOf("/") + 1);
                       if (k == 0)
                       {
                           conditionSql += string.Format(" a.ccode like '%{0}' ",sCode1);
                       }
                       else
                       {
                         conditionSql += string.Format("  or a.ccode like '%{0}' ",sCode1);
                       }
                       k++;
                   }

                   if (k == 0)
                   {
                       conditionSql += string.Format(" a.ccode like '%{0}' ", sCode);
                   }
                   else
                   {
                       conditionSql += string.Format("  or a.ccode like '%{0}' ", sCode);
                   }
                   conditionSql += ")";
               }


            return conditionSql;
        }
       

     

        #region 打印
        private void btnPrintbarcode_Click(object sender, EventArgs e)
        {
            if (dtPrint.Rows.Count == 0)
            {
                CommonHelper.MsgError("没有生成标签!");
                return;
            }

            int ire = (int)reportViewer1.PrintDialog();
            {
                dtPrint.Rows.Clear();
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.Clear();
                 
            }
        }

        #endregion



        #region 选择
        private void btnall_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["chk"] = 1;
                }
            }
            //dataGridView2.EndEdit();
        }

        private void btnabn_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["chk"] = 0;
                }
            }
            //dataGridView2.EndEdit();
        }

        private void btnshow_Click(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "1 =1";
        }
        #endregion 

            
        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.Validate();
        }

        private void btnCx_Click(object sender, EventArgs e)
        {


            string sql= @"
          SELECT a.cCode,b.irowno,a.dDate,b.cInvCode,i.cInvName,i.cInvStd,i.cinvdefine4,convert(real, b.iQuantity) as iQuantity,convert(real, b.iQuantity-isnull(b.fOutQuantity,0)) wcksl,b.cmocode,b.imoseq,
A.cMemo,A.cRdCode,B.cWhCode,d.cWhName,c.cRdName,b.AutoID,b.cdefine35,b.InvCode FROM dbo.MaterialAppVouch a
INNER JOIN dbo.MaterialAppVouchs b ON b.ID = a.ID
INNER JOIN inventory i  ON i.cInvCode = b.cInvCode
LEFT JOIN Rd_Style c on a.cRdCode = c.cRdCode
left join Warehouse d on b.cWhCode = d.cWhCode
WHERE b.iQuantity>ISNULL(b.fOutQuantity,0)
AND ISNULL(B.cBCloser,'')='' AND ISNULL(A.cHandler,'')<>'' ";

            sql += Condition();
            dt = DbHelper.ExecuteTable(sql);
            dt.Columns.Add("chk", typeof(Boolean));

            gridControl1.DataSource = dt;
            
        }

    

        #region 参照
        private void btnRdcode_Click(object sender, EventArgs e)
        {
            try
            {

                U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
                obj.RefID = "Rd_Style_AA";
                obj.Mode = U8RefService.RefModes.modeRefing;
                //obj.FilterSQL = "  Status='3' ";
                obj.FillText = txtcRdcode.Text;
                obj.Web = false;
                obj.MetaXML = "<Ref><RefSet   bMultiSel='0'  /></Ref>";
                obj.RememberLastRst = false;
                ADODB.Recordset retRstGrid = null, retRstClass = null;
                string sErrMsg = "";
                obj.GetPortalHwnd((int)this.Handle);

                Object objLogin = canshu.u8Login;
                if (obj.ShowRefSecond(ref objLogin, ref retRstClass, ref retRstGrid, ref sErrMsg) == false)
                {
                    MessageBox.Show(sErrMsg);
                }
                else
                {
                    if (retRstGrid != null)
                    {
                        this.txtcRdcode.Text = DbHelper.GetDbString(retRstGrid.Fields["crdcode"].Value);
                       


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("参照失败，原因：" + ex.Message);
            }
        }

        private void btnmo_Click(object sender, EventArgs e)
        {
            try
            {

                U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
                obj.RefID = "MoOrderseq_mm";
                obj.Mode = U8RefService.RefModes.modeRefing;
                //obj.FilterSQL = "  Status='3' ";
                obj.FillText = txtMocode.Text;
                obj.Web = false;
                obj.MetaXML = "<Ref><RefSet   bMultiSel='0'  /></Ref>";
                obj.RememberLastRst = false;
                ADODB.Recordset retRstGrid = null, retRstClass = null;
                string sErrMsg = "";
                obj.GetPortalHwnd((int)this.Handle);

                Object objLogin = canshu.u8Login;
                if (obj.ShowRefSecond(ref objLogin, ref retRstClass, ref retRstGrid, ref sErrMsg) == false)
                {
                    MessageBox.Show(sErrMsg);
                }
                else
                {
                    if (retRstGrid != null)
                    {
                        this.txtMocode.Text = DbHelper.GetDbString(retRstGrid.Fields["mocode"].Value);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("参照失败，原因：" + ex.Message);
            }
        }
  

        private void btnInvocode_Click(object sender, EventArgs e)
        {
            try
            {

                U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
                obj.RefID = "Inventory_AA";
                obj.Mode = U8RefService.RefModes.modeRefing;
                //obj.FilterSQL = "  Status='3' ";
                obj.FillText = txtcInvcode.Text;
                obj.Web = false;
                obj.MetaXML = "<Ref><RefSet   bMultiSel='0'  /></Ref>";
                obj.RememberLastRst = false;
                ADODB.Recordset retRstGrid = null, retRstClass = null;
                string sErrMsg = "";
                obj.GetPortalHwnd((int)this.Handle);

                Object objLogin = canshu.u8Login;
                if (obj.ShowRefSecond(ref objLogin, ref retRstClass, ref retRstGrid, ref sErrMsg) == false)
                {
                    MessageBox.Show(sErrMsg);
                }
                else
                {
                    if (retRstGrid != null)
                    {
                        this.txtcInvcode.Text = DbHelper.GetDbString(retRstGrid.Fields["cinvcode"].Value);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("参照失败，原因：" + ex.Message);
            }
        }

        private void btnWhcode_Click(object sender, EventArgs e)
        {
            try
            {

                U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
                obj.RefID = "Warehouse_AA";
                obj.Mode = U8RefService.RefModes.modeRefing;
                //obj.FilterSQL = "  Status='3' ";
                obj.FillText = txtcWhcode.Text;
                obj.Web = false;
                obj.MetaXML = "<Ref><RefSet   bMultiSel='0'  /></Ref>";
                obj.RememberLastRst = false;
                ADODB.Recordset retRstGrid = null, retRstClass = null;
                string sErrMsg = "";
                obj.GetPortalHwnd((int)this.Handle);

                Object objLogin = canshu.u8Login;
                if (obj.ShowRefSecond(ref objLogin, ref retRstClass, ref retRstGrid, ref sErrMsg) == false)
                {
                    MessageBox.Show(sErrMsg);
                }
                else
                {
                    if (retRstGrid != null)
                    {
                        this.txtcWhcode.Text = DbHelper.GetDbString(retRstGrid.Fields["cwhcode"].Value);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("参照失败，原因：" + ex.Message);
            }
        }

        private void btnPoscode_Click(object sender, EventArgs e)
        {
            try
            {

                U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
                obj.RefID = "Position_AA";
                obj.Mode = U8RefService.RefModes.modeRefing;
                //obj.FilterSQL = "  Status='3' ";
                obj.FillText = txtcPoscode.Text;
                obj.Web = false;
                obj.MetaXML = "<Ref><RefSet   bMultiSel='0'  /></Ref>";
                obj.RememberLastRst = false;
                ADODB.Recordset retRstGrid = null, retRstClass = null;
                string sErrMsg = "";
                obj.GetPortalHwnd((int)this.Handle);

                Object objLogin = canshu.u8Login;
                if (obj.ShowRefSecond(ref objLogin, ref retRstClass, ref retRstGrid, ref sErrMsg) == false)
                {
                    MessageBox.Show(sErrMsg);
                }
                else
                {
                    if (retRstGrid != null)
                    {
                        this.txtcPoscode.Text = DbHelper.GetDbString(retRstGrid.Fields["cposcode"].Value);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("参照失败，原因：" + ex.Message);
            }
        }
        #endregion

        #region 生成标签
        private void button1_Click(object sender, EventArgs e)
        {
            int j = 0;
            try
            {
                string sql = string.Format(@" INSERT INTO zdy_sml_beiliao_main(dmaketime,cmaker) 
                    values(getdate(),'{0}') select @@identity ", canshu.userName);
                object iId = DbHelper.ExecuteScalar(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dt.Rows[i]["chk"] == DBNull.Value ? false : dt.Rows[i]["chk"]))
                    {
                        int iQty = DbHelper.GetDbInt(dt.Rows[i]["wcksl"]);
                        int iAppids = DbHelper.GetDbInt(dt.Rows[i]["AutoID"]);
                        sql = string.Format(@" INSERT INTO zdy_sml_beiliao(id,iappids,iquantity) 
                    values('{0}','{1}','{2}')", iId, iAppids, iQty);
                        DbHelper.ExecuteNonQuery(sql);
                        j++;

                    }

                }

                if (j > 0)
                {
                    try
                    {
                        if (rbBig.Checked)
                        {
                            //生成标签格式
                            DbHelper.ExecuteNonQuery("zdy_sml_sp_beiliao", new SqlParameter[] { new SqlParameter("@id", iId) }
                                                 , CommandType.StoredProcedure);
                            //
                            string sqllabel = string.Format(@"SELECT autoid,id,cinvcode,cinvname,cposcode,iquantity,cmocode1,cmocode2,cmocode3,cmocode4,
cmocode5,cmocode6 FROM zdy_sml_beiliaos  where id = '{0}'  order by cposcode", iId.ToString());
                            dtPrint = DbHelper.ExecuteTable(sqllabel);

                            this.reportViewer1.ProcessingMode = ProcessingMode.Local;

                            // DataSetFirst_DataTableFirst 必须与 RDLC 报表中为表格配置的数据源名称相同
                            reportViewer1.LocalReport.DataSources.Clear();
                            reportViewer1.LocalReport.ReportPath = ls + "\\uap\\runtime\\Beiliao.rdlc";
                            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "SML_MaterailApp.Beiliao.rdlc";
                            ReportDataSource rds = new ReportDataSource("DataSet1", dtPrint);
                            
                            reportViewer1.LocalReport.DataSources.Add(rds);
                            //设置纵向
                            var pageSettings = this.reportViewer1.GetPageSettings();
                            pageSettings.Landscape = false;

                            //高长宽短，方向设置成反的。
                            pageSettings.PaperSize = new PaperSize()
                            {
                                Height = Convert.ToInt32(600 * 0.3937m),
                                Width = Convert.ToInt32(400 * 0.3937m)
                            };

                            this.reportViewer1.SetPageSettings(pageSettings);
                            this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                            this.reportViewer1.RefreshReport();
                        }
                        else
                        {
                            //生成标签格式
                            DbHelper.ExecuteNonQuery("zdy_sml_sp_beiliao2", new SqlParameter[] { new SqlParameter("@id", iId) }
                                                 , CommandType.StoredProcedure);
                            //
                            string sqllabel = string.Format(@"SELECT autoid,id,cinvcode,cinvname,cposcode,iquantity,cmocode1,cmocode2,cmocode3,cmocode4,
cmocode5,cmocode6 FROM zdy_sml_beiliaos  where id = '{0}' order by cposcode", iId.ToString());
                            dtPrint = DbHelper.ExecuteTable(sqllabel);

                            this.reportViewer1.ProcessingMode = ProcessingMode.Local;

                            // DataSetFirst_DataTableFirst 必须与 RDLC 报表中为表格配置的数据源名称相同
                            reportViewer1.LocalReport.DataSources.Clear();
                            reportViewer1.LocalReport.ReportPath = ls + "\\uap\\runtime\\Beiliao2.rdlc";
                            ReportDataSource rds = new ReportDataSource("DataSet1", dtPrint);
                            
                            reportViewer1.LocalReport.DataSources.Add(rds);
                            //设置纵向
                            var pageSettings = this.reportViewer1.GetPageSettings();
                            pageSettings.Landscape = false;

                            pageSettings.PaperSize = new PaperSize()
                            {
                                Height = Convert.ToInt32(300 * 0.3937m),
                                Width = Convert.ToInt32(400 * 0.3937m)
                            };

                            this.reportViewer1.SetPageSettings(pageSettings);
                            this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                            this.reportViewer1.RefreshReport();
                        }


                    }
                    catch (Exception ex)
                    {
                        CommonHelper.MsgError(ex.Message);
                        return;
                    }


                    MessageBox.Show("生成完成");

                }
                else
                {
                    MessageBox.Show("没有选择数据，无法生成！");
                    sql = string.Format(@"delete from zdy_sml_beiliao_main where id= '{0}'", iId);
                    DbHelper.ExecuteNonQuery(sql);
                    return;
                
                }
            }

            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(ex.Message, "生成失败");
                return;
            }
        }
        #endregion

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }



    }




}
