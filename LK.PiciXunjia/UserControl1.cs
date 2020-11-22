﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using fuzhu;
using ADODB;
using MSXML2;
using UFIDA.U8.U8APIFramework;
using UFIDA.U8.U8MOMAPIFramework;
using UFIDA.U8.U8APIFramework.Parameter;
using System.Threading;
using System.Data.SqlClient;
using Process;

namespace LKU8.shoukuan
{
    public partial class UserControl1 : UserControl
    {


        DataTable dtXunjia;
        DataTable dtKuCun;
        DataTable dtXunjias;
        DataTable dtLishiXunjia;
        string sColname;
        

        public UserControl1()
        {
            InitializeComponent();

            ExtensionMethods.DoubleBuffered(dataGridView1, true);
            ExtensionMethods.DoubleBuffered(dataGridView2, true);
            ExtensionMethods.DoubleBuffered(dataGridView3, true);
        }

    

        #region 单元格显示按钮，参照档案
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridView1.Controls.Clear();//移除所有控件
            sColname = this.dataGridView1.Columns[e.ColumnIndex].Name.ToString();
            if (sColname == "cinvcode" || sColname == "xmbm" || sColname == "ccusname" || sColname == "zdxjr" || sColname == "cdefine1")
            //if (e.ColumnIndex.Equals(this.dataGridView1.Columns["dinvcode"].Index) || e.ColumnIndex.Equals(this.dataGridView1.Columns["Dopseq"].Index) || e.ColumnIndex.Equals(this.dataGridView1.Columns["Ddefine_23"].Index))
            {
                System.Windows.Forms.Button btn = new System.Windows.Forms.Button();//创建Buttonbtn   
                btn.Text = "...";//设置button文字   
                btn.Font = new System.Drawing.Font("Arial", 7);//设置文字格式   
                btn.Visible = true;//设置控件允许显示  
                btn.BackColor = dataGridView1.ColumnHeadersDefaultCellStyle.BackColor;


                btn.Width = this.dataGridView1.GetCellDisplayRectangle(e.ColumnIndex,
                                e.RowIndex, true).Height;//获取单元格高并设置为btn的宽   
                btn.Height = this.dataGridView1.GetCellDisplayRectangle(e.ColumnIndex,
                                e.RowIndex, true).Height;//获取单元格高并设置为btn的高   

                btn.Click += new EventHandler(btn_Click);//为btn添加单击事件   

                this.dataGridView1.Controls.Add(btn);//dataGridView1中添加控件btn   

                btn.Location = new System.Drawing.Point(((this.dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Right) -
                        (btn.Width)), this.dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Y);//设置btn显示位置   
            }
        }



        void btn_Click(object sender, EventArgs e)
        {
            if (sColname == "cinvcode")
            {
                try
                {

                    U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
                    obj.RefID = "Inventory_AA";
                    obj.Mode = U8RefService.RefModes.modeRefing;
                    obj.FilterSQL = " dedate is null";
                    obj.FillText = dataGridView1.CurrentCell.Value.ToString();
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
                            //dataGridView1.CurrentCell.Value = DbHelper.GetDbString(retRstGrid.Fields["cinvcode"].Value);
                            //dataGridView1.Rows[dataGridView1.CurrentCellAddress.Y].Cells["cinvname"].Value = DbHelper.GetDbString(retRstGrid.Fields["cinvname"].Value);
                            //dataGridView1.Rows[dataGridView1.CurrentCellAddress.Y].Cells["cinvstd"].Value = DbHelper.GetDbString(retRstGrid.Fields["cinvstd"].Value);
                            //this.textBox3.Text = DbHelper.GetDbString(retRstGrid.Fields["cdepcode"].Value);
                            dtXunjia.Rows[dataGridView1.CurrentCell.RowIndex]["cinvcode"] = DbHelper.GetDbString(retRstGrid.Fields["cinvcode"].Value);
                            dtXunjia.Rows[dataGridView1.CurrentCell.RowIndex]["cinvaddcode"] = DbHelper.GetDbString(retRstGrid.Fields["cinvaddcode"].Value);
                            dtXunjia.Rows[dataGridView1.CurrentCell.RowIndex]["cinvname"] = DbHelper.GetDbString(retRstGrid.Fields["cinvname"].Value);
                            dtXunjia.Rows[dataGridView1.CurrentCell.RowIndex]["cinvstd"] = DbHelper.GetDbString(retRstGrid.Fields["cinvstd"].Value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("参照失败，原因：" + ex.Message);
                }


             
            }
            else if (sColname == "ccusname")
            {
                try
                {

                    U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
                    obj.RefID = "customer_AA";
                    obj.Mode = U8RefService.RefModes.modeRefing;
                    //obj.FilterSQL = " bbomsub =1";
                    obj.FillText = dataGridView1.CurrentCell.Value.ToString();
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
                            //dataGridView1.CurrentCell.Value = DbHelper.GetDbString(retRstGrid.Fields["ccusname"].Value);
                            dtXunjia.Rows[dataGridView1.CurrentCell.RowIndex]["ccusname"] = DbHelper.GetDbString(retRstGrid.Fields["ccusname"].Value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("参照失败，原因：" + ex.Message);
                }
            }
            else if (sColname == "xmbm")
            {
                try
                {

                    U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
                    obj.RefID = "LK_XMLXS_AA";
                    obj.Mode = U8RefService.RefModes.modeRefing;
                    //obj.FilterSQL = " bbomsub =1";
                    obj.FillText = dataGridView1.CurrentCell.Value.ToString();
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
                            //dataGridView1.CurrentCell.Value = DbHelper.GetDbString(retRstGrid.Fields["cNo"].Value);
                            dtXunjia.Rows[dataGridView1.CurrentCell.RowIndex]["xmbm"] = DbHelper.GetDbString(retRstGrid.Fields["cNo"].Value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("参照失败，原因：" + ex.Message);
                }
            }
            else if (sColname == "zdxjr")
            {
                try
                {

                    U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
                    obj.RefID = "U8CUSTDEF_0016_AA";
                    obj.Mode = U8RefService.RefModes.modeRefing;
                    //obj.FilterSQL = " bbomsub =1";
                    obj.FillText = dataGridView1.CurrentCell.Value.ToString();
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
                            dataGridView1.CurrentCell.Value = DbHelper.GetDbString(retRstGrid.Fields["cNo"].Value);

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("参照失败，原因：" + ex.Message);
                }
            }
            else if (sColname == "cdefine1")
            {
                try
                {

                    U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
                    obj.RefID = "userdefine_aa";
                    obj.Mode = U8RefService.RefModes.modeRefing;
                    obj.FilterSQL = " cid =01";
                    obj.FillText = dataGridView1.CurrentCell.Value.ToString();
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
                            dataGridView1.CurrentCell.Value = DbHelper.GetDbString(retRstGrid.Fields["cvalue"].Value);

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("参照失败，原因：" + ex.Message);
                }
            }
        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.dataGridView1.Controls.Clear();//宽度调整时移除所有控件   
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            this.dataGridView1.Controls.Clear();//滚动条移动时移除所有控件   
        }
        #endregion


      
        #region 加载
      private void UserControl1_Load(object sender, EventArgs e)
        {
            Dgvfuzhu.BindReadDataGridViewStyle(this.Name, dataGridView1); // 初始化布局
            //Dgvfuzhu.BindReadDataGridViewStyle(this.Name, dataGridView2); // 初始化布局
            dataGridView2.AutoGenerateColumns = false;
            dataGridView1.AutoGenerateColumns = false;

            txtywy.Text = canshu.userName;

            if (canshu.userName.Contains("陆茜") || canshu.userName.Contains("demo") || canshu.userName.Contains("方美"))
            {
                dataGridView2.Columns["gys"].Visible = true;

            
            }
            string sql = "select 0 as xz,* from zdy_lk_xunjia where czt = '未提交' and cmaker = '"+canshu.userName+"'";
            dtXunjia = DbHelper.ExecuteTable(sql);
            dataGridView1.DataSource = dtXunjia;

            comboBox1.Text = "未提交";

        }
        #endregion


        #region 写序号
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv != null)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgv.RowHeadersWidth - 4, e.RowBounds.Height);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgv.RowHeadersDefaultCellStyle.Font, rect, dgv.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
            }
        }

        #endregion


        #region 布局设置
       public void SaveBuju()
        {
            Dgvfuzhu.SaveDataGridViewStyle(this.Name, dataGridView1);
            Dgvfuzhu.SaveDataGridViewStyle(this.Name, dataGridView2);
            Dgvfuzhu.SaveDataGridViewStyle(this.Name, dataGridView3);
            MessageBox.Show("布局保存成功！");
        }

       public void DelBuju()
        {
            Dgvfuzhu.deleteDataGridViewStyle(this.Name, dataGridView1);
            Dgvfuzhu.deleteDataGridViewStyle(this.Name, dataGridView2);
            Dgvfuzhu.deleteDataGridViewStyle(this.Name, dataGridView3);
            //Dgvfuzhu.BindReadDataGridViewStyle(this.Name, dataGridView1);
            MessageBox.Show("请关掉界面重新打开，恢复初始布局！");
        }
        #endregion
  

        #region 增加
       public void Add()
        {
            try
            {
                dataGridView1.EndEdit();
                dataGridView1.AllowUserToAddRows = true;
                DataRow dr = dtXunjia.NewRow();
                dr["ddate"] = DateTime.Now.ToString("yyyy-MM-dd");
                dr["czt"] = "未提交";
                dr["xz"] = "1";
                dr["cpersoncode"] = canshu.userName;
                if (dtXunjia.Rows.Count > 0)
                {
                    dr["ccusname"] = dtXunjia.Rows[dtXunjia.Rows.Count - 1]["ccusname"];
                    dr["xmbm"] = dtXunjia.Rows[dtXunjia.Rows.Count - 1]["xmbm"];
                    dr["zdxjr"] = dtXunjia.Rows[dtXunjia.Rows.Count - 1]["zdxjr"];
                    dr["cpersoncode"] = dtXunjia.Rows[dtXunjia.Rows.Count - 1]["cpersoncode"];
                }

                dtXunjia.Rows.Add(dr);
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.Columns["id"].ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

       #region 删除
       public void Del()
       {
           dataGridView1.EndEdit();
           int j = 0;
           DialogResult result = CommonHelper.MsgQuestion("确认要删除已勾选行吗？");
           if (result == DialogResult.Yes)
           {
               try
               {
                   for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
                   {
                       //是否选中
                       int ixz = DbHelper.GetDbInt(dataGridView1.Rows[i].Cells["xz"].Value);
                       if (ixz == 1)
                       {
                               //没保存的直接删除，保存的，删除数据库
                               string id = DbHelper.GetDbString(dataGridView1.Rows[i].Cells["id"].Value);

                               if (id == "" || string.IsNullOrEmpty(id))
                               {
                                   dtXunjia.Rows.RemoveAt(i);
                               }
                               else
                               {
                                   //如果是已进行中的询价，提示不能删除，需要先撤销
                                   string sql = " select 1  from zdy_lk_xunjias where id='"+id+"'";
                                   DataTable dt =  DbHelper.ExecuteTable(sql);
                                   if (dt.Rows.Count > 0)
                                   {
                                       MessageBox.Show("第" + (i + 1).ToString() + "行已经有询价，不能删除，请关闭");
                                       return;
                                   }


                                   //if (dataGridView1.Rows[i].Cells["czt"].Value.ToString() != "未提交")
                                   //{
                                   //    MessageBox.Show("第" + (i+1).ToString() + "行已经提交，先更改单据状态为未提交后再删除");
                                   //    return;

                                   //}
                                   string sId = DbHelper.GetDbString(dataGridView1.Rows[i].Cells["id"].Value);
                                   sql = " delete from zdy_lk_xunjia where id=@Id ";
                                   DbHelper.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@Id", sId) });

                               }
                               j++;
                           //dtXunjia.Rows.RemoveAt(i);
                       }


                   }
               }
               catch (Exception ex)
               {
                   //DbHelper.RollbackAndCloseConnection(tran);
                   CommonHelper.MsgError("删除失败！原因：" + ex.Message);
               }
               if (j > 0)
               {
                   CommonHelper.MsgInformation("删除完成！");
               }
           }
           Cx();
       }
       #endregion

       #region 保存
       public void Save()
       {
           dataGridView1.Update();
           dataGridView1.EndEdit();
           int j = 0;
           try
           {
              
               for (int i = 0; i < dataGridView1.Rows.Count; i++)
               {
                   //是否选中
                   int ixz = DbHelper.GetDbInt(dataGridView1.Rows[i].Cells["xz"].Value);
                   if (ixz == 1)
                   {

                       //没id，自动保存。有id，判断是否modifyed，如果更改了，update
                       string id = DbHelper.GetDbString(dataGridView1.Rows[i].Cells["id"].Value);
                       string cInvcode = DbHelper.GetDbString(dataGridView1.Rows[i].Cells["cinvcode"].Value);
                       //判断是否重复询价
                       string sqlpd = "select count(1) from zdy_lk_xunjia where ID<>'"+id+"' and cinvcode = '" + cInvcode + "' and datediff(day,ddate,getdate())<7";
                       object objpd = DbHelper.ExecuteScalar(sqlpd);
                       if (Convert.ToInt32(objpd)>0)
                       {

                           
                           DialogResult result= MessageBox.Show("第"+(i+1).ToString()+"行产品" + cInvcode + "最近7天已有询价，是否保存？","提示",MessageBoxButtons.OKCancel);
                           //DataTable dtmx 
                           if (result == DialogResult.Cancel)
                           //dataGridView2.DataSource = DbHelper.ExecuteTable("select ddate,gys,xunjia1,xunjia2,xunjia3,bz1,bz2,bz3 from zdy_lk_xunjias where id ='" + objpd.ToString() + "'");
                           {
                               continue;
                           }
                           

                       }

                       //MessageBox.Show(dataGridView1.Rows[0].Cells["bimportant"].Value.ToString());
                       if (string.IsNullOrEmpty(id))
                       {
                    
                           string sql = @" Insert Into zdy_lk_xunjia(ccusname,xmbm,cinvcode,cinvaddcode,cinvname,cinvstd,cqty1,cmemo1,czt,cmaker,ddate,dmaketime,zdxjr,baojia1,cpersoncode,lx) 
                    values(@ccusname,@xmbm,@cinvcode,@cinvaddcode,@cinvname,@cinvstd,@cqty1,@cmemo1,@czt,@cmaker,@ddate,getdate(),@zdxjr,@baojia1,@cpersoncode,2) select @@identity ";
                           object obj = DbHelper.GetSingle(sql, new SqlParameter[]{ 
                             new SqlParameter("@ccusname", dataGridView1.Rows[i].Cells["ccusname"].Value), 
                             new SqlParameter("@xmbm", dataGridView1.Rows[i].Cells["xmbm"].Value),
                             new SqlParameter("@cinvcode", dataGridView1.Rows[i].Cells["cinvcode"].Value),
                             new SqlParameter("@cinvaddcode", dataGridView1.Rows[i].Cells["cinvaddcode"].Value),
                             new SqlParameter("@cinvname", dataGridView1.Rows[i].Cells["cinvname"].Value),
                             new SqlParameter("@cinvstd", dataGridView1.Rows[i].Cells["cinvstd"].Value),
                             new SqlParameter("@cqty1", dataGridView1.Rows[i].Cells["cqty1"].Value),
                             new SqlParameter("@cmemo1",dataGridView1.Rows[i].Cells["cmemo1"].Value),
                             new SqlParameter("@czt", dataGridView1.Rows[i].Cells["czt"].Value),
                             new SqlParameter("@cmaker",canshu.userName),
                             new SqlParameter("@ddate",dataGridView1.Rows[i].Cells["ddate"].Value),
                             new SqlParameter("@zdxjr",dataGridView1.Rows[i].Cells["zdxjr"].Value),
                              new SqlParameter("@baojia1",dataGridView1.Rows[i].Cells["baojia1"].Value),
                                new SqlParameter("@cpersoncode",dataGridView1.Rows[i].Cells["cpersoncode"].Value)
                            });
                           //数据表赋值
                           dtXunjia.Rows[i]["id"] = Convert.ToInt32(obj);
                           // 设置为非更改状态
                          
                       }
                       else
                       {
                           //如果是已询价完成的，禁止更改
                           if (dataGridView1.Rows[i].Cells["czt"].Value.ToString() == "询价完成")
                           {
                               MessageBox.Show("第" + (i + 1).ToString() + "已询价完成，禁止更改");

                               continue;

                           }
                           else
                           {
                               string sql = @" update zdy_lk_xunjia
                        set ccusname=@ccusname,xmbm=@xmbm,cinvcode=@cinvcode,cinvname=@cinvname,cinvstd=@cinvstd,zdxjr = @zdxjr
                        ,cqty1=@cqty1,cmemo1=@cmemo1,czt=@czt,cmaker=@cmaker,ddate=@ddate,dmodifytime=getdate(),baojia1= @baojia1,
                            cpersoncode = @cpersoncode
                         where id = @id  ";
                               object obj = DbHelper.GetSingle(sql, new SqlParameter[]{ 
                             new SqlParameter("@ccusname", dataGridView1.Rows[i].Cells["ccusname"].Value), 
                             new SqlParameter("@xmbm", dataGridView1.Rows[i].Cells["xmbm"].Value),
                             new SqlParameter("@cinvcode", dataGridView1.Rows[i].Cells["cinvcode"].Value),
                             new SqlParameter("@cinvname", dataGridView1.Rows[i].Cells["cinvname"].Value),
                             new SqlParameter("@cinvstd", dataGridView1.Rows[i].Cells["cinvstd"].Value),
                             new SqlParameter("@cqty1", dataGridView1.Rows[i].Cells["cqty1"].Value),
                             new SqlParameter("@cmemo1",dataGridView1.Rows[i].Cells["cmemo1"].Value),
                             new SqlParameter("@czt", dataGridView1.Rows[i].Cells["czt"].Value),
                             new SqlParameter("@cmaker",canshu.userName),
                             new SqlParameter("@ddate",dataGridView1.Rows[i].Cells["ddate"].Value),
                              new SqlParameter("@id",dataGridView1.Rows[i].Cells["id"].Value),
                             new SqlParameter("@zdxjr",dataGridView1.Rows[i].Cells["zdxjr"].Value),
                              new SqlParameter("@baojia1",dataGridView1.Rows[i].Cells["baojia1"].Value),
                                 new SqlParameter("@cpersoncode",dataGridView1.Rows[i].Cells["cpersoncode"].Value)

                            });
                              
                           }
                       }
                       j++;
                   }
                   
               }
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
               return;
           }

           if (j > 0)
           {
               MessageBox.Show("保存完成！");
           }
       }
       #endregion

       #region 报价保存
       public void Save2()
       {
           dataGridView1.Update();
           dataGridView1.EndEdit();
           int j = 0;
           try
           {

               for (int i = 0; i < dataGridView1.Rows.Count; i++)
               {
                   //是否选中
                   int ixz = DbHelper.GetDbInt(dataGridView1.Rows[i].Cells["xz"].Value);
                   if (ixz == 1)
                   {

                       //没id，自动保存。有id，判断是否modifyed，如果更改了，update
                       string id = DbHelper.GetDbString(dataGridView1.Rows[i].Cells["id"].Value);
                      
                               string sql = @" update zdy_lk_xunjia
                        set baojia1= @baojia1
                         where id = @id  ";
                               object obj = DbHelper.GetSingle(sql, new SqlParameter[]{ 
                            
                              new SqlParameter("@baojia1",dataGridView1.Rows[i].Cells["baojia1"].Value),
                                   new SqlParameter("@id",id)

                            });

                           }
                       }
                       j++;
                   }

           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
               return;
           }

           if (j > 0)
           {
               MessageBox.Show("保存完成！");
           }
       }
       #endregion

       #region 关闭
       public void guanbi()
       {
             dataGridView1.Update();
           dataGridView1.EndEdit();

           try
           {
              
               for (int i = 0; i < dataGridView1.Rows.Count; i++)
               {
                   //是否选中
                   int ixz = DbHelper.GetDbInt(dataGridView1.Rows[i].Cells["xz"].Value);
                   if (ixz == 1)
                   {

                       //没id，自动保存。有id，判断是否modifyed，如果更改了，update
                       string id = DbHelper.GetDbString(dataGridView1.Rows[i].Cells["id"].Value);
                       if (string.IsNullOrEmpty(id))
                       {
                           MessageBox.Show("第"+i.ToString()+"没有保存，请先保存");
                           return;
                       }
                       if (string.IsNullOrEmpty(id)==false)
                       {
                           string cZt = DbHelper.GetDbString(dataGridView1.Rows[i].Cells["czt"].Value);
                           if (cZt != "关闭")
                           {
                               string sql = @" update zdy_lk_xunjia
                        set cztgbq = czt, czt='关闭',dclosetime = getdate()
                         where id = @id  ";
                               DbHelper.ExecuteNonQuery(sql, new SqlParameter[]{ 
                                                           new SqlParameter("@id",dataGridView1.Rows[i].Cells["id"].Value)
                            });
                           }
                           else
                           {
                               MessageBox.Show("第" + i.ToString() + "行已关闭");
                               continue;
                           
                           }
                       }
                   }
               }
               MessageBox.Show("关闭完成");
               Cx();
           }
               catch(Exception  ex)
           {
               MessageBox.Show(ex.Message);
               return;
               }

       }
       #endregion

       #region 打开
       public void DaKai()
       {
           dataGridView1.Update();
           dataGridView1.EndEdit();

           try
           {

               for (int i = 0; i < dataGridView1.Rows.Count; i++)
               {
                   //是否选中
                   int ixz = DbHelper.GetDbInt(dataGridView1.Rows[i].Cells["xz"].Value);
                   if (ixz == 1)
                   {

                       //没id，无法进行操作
                       string id = DbHelper.GetDbString(dataGridView1.Rows[i].Cells["id"].Value);
                       if (string.IsNullOrEmpty(id))
                       {
                           MessageBox.Show("第" + i.ToString() + "没有保存，请先保存");
                           return;
                       }
                       if (string.IsNullOrEmpty(id) == false)
                       {
                           string cZt = DbHelper.GetDbString(dataGridView1.Rows[i].Cells["czt"].Value);
                           if (cZt == "关闭")
                           {
                               string sql = @" update zdy_lk_xunjia
                        set czt = cztgbq, dclosetime = null
                         where id = @id  ";
                               DbHelper.ExecuteNonQuery(sql, new SqlParameter[]{ 
                                                           new SqlParameter("@id",dataGridView1.Rows[i].Cells["id"].Value)
                            });
                           }
                           else
                           {
                               MessageBox.Show("第" + (i+1).ToString() + "行不是关闭状态");
                               continue;

                           }
                       }
                   }
               }
               MessageBox.Show("打开完成");
               Cx();
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
               return;
           }

       }
       #endregion


       #region 提交
       public void Tijiao()
       {

           dataGridView1.Update();
           dataGridView1.EndEdit();

           try
           {
               
               for (int i = 0; i < dataGridView1.Rows.Count; i++)
               {
                   //没id，提示保存
                   string id = DbHelper.GetDbString(dataGridView1.Rows[i].Cells["id"].Value);
                   if (string.IsNullOrEmpty(id))
                   {
                       MessageBox.Show("第" + (i + 1).ToString() + "没有保存，请先保存");
                       return;
                   }

                   //是否选中
                   int ixz = DbHelper.GetDbInt(dataGridView1.Rows[i].Cells["xz"].Value);
                   if (ixz == 1)
                   {
                       //判断是否输入cas和数量，没输入的不允许进行提交
                       if (DbHelper.GetDbString(dataGridView1.Rows[i].Cells["cqty1"].Value) == "" && DbHelper.GetDbString(dataGridView1.Rows[i].Cells["cinvcode"].Value) == "")
                       {
                           MessageBox.Show("第" + (i + 1).ToString() + "行没有输入cas号和数量，无法提交");
                           continue;
                       }
                      //f分配采购员
                       //如果是已指定采购员的，直接到指定采购员
                       //如果是没指定的，进行分配，按行进行分配
                       //还是写触发器，同时写提醒语句。

                  

                      
                       if (string.IsNullOrEmpty(id)==false)
                       {
                           string cZt = DbHelper.GetDbString(dataGridView1.Rows[i].Cells["czt"].Value);
                           if (cZt == "未提交")
                           {
                               string sql = @" update zdy_lk_xunjia
                        set czt='已提交'
                         where id = @id  ";
                               DbHelper.ExecuteNonQuery(sql, new SqlParameter[]{ 
                                                           new SqlParameter("@id",dataGridView1.Rows[i].Cells["id"].Value)
                            });

                               dataGridView1.Rows[i].Cells["czt"].Value = "已提交";
                           }
                           else
                           {
                               MessageBox.Show("第" + (i+1).ToString() + "行已提交");
                               continue;
                           
                           }
                       }
                   }
               }
                       
              
               MessageBox.Show("提交完成");
               Cx();
           }
         
           catch (Exception ex)
           {
               CommonHelper.MsgInformation(ex.Message);
               return;
           }
           ////判断是否输入cas和数量
           //if (DbHelper.GetDbString(dataGridView1.Rows[e.RowIndex].Cells["cqty1"].Value) == "" && DbHelper.GetDbString(dataGridView1.Rows[e.RowIndex].Cells["cinvcode"].Value) == "")
           //{
           //    MessageBox.Show("当前行没有输入cas号和数量，无法保存");
           //    return;

           //}

           //UFLTMService connSrv = new UFLTMService();

           //connSrv.Start(canshu.u8Login.UFDataConnstringForNet);		//传递连接串初始化对象

           //connSrv.BeginTransaction();	//开始事务





           //AuditServiceProxy auditSvc = new AuditServiceProxy();

           ////构造Login的 CalledContext对象
           //CalledContext calledCtx = new CalledContext();
           //calledCtx.subId = "ST";
           //calledCtx.TaskID = canshu.u8Login.get_TaskId();
           //calledCtx.token = canshu.u8Login.userToken;
           ////业务对象标识
           //string bizObjectId = "UAPFORM.U8CUSTDEF_0018";
           ////业务事件标识  
           //string bizEventId = "U8CUSTDEF_0018.Commit";
           ////单据号
           //string voucherId = "10";
           //if (bizEventId == string.Empty || bizObjectId == string.Empty)
           //{
           //    MessageBox.Show("请选择选择业务对象或业务事件!");
           //    //return null;
           //}
           //string errMsg = "";
           ////bool a = auditSvc.IsFlowEnabled(bizObjectId, bizEventId, calledCtx, ref errMsg);
           ////if (a == true)
           ////    MessageBox.Show("提交成功");
           ////else
           ////    MessageBox.Show("提交失败，失败原因：" + errMsg);


           //errMsg = "";
           //bool bControled = true;

           //bool ret = auditSvc.SubmitApplicationMessage(bizObjectId, bizEventId, voucherId, calledCtx, ref bControled, ref errMsg);

           //if (ret == true && bControled)
           //    MessageBox.Show("提交成功");
           //else
           //    MessageBox.Show("提交失败，失败原因：" + errMsg);


           //connSrv.Commit();	//提交事务，回滚事务请根据自己代码的情况调用Rollback
           //connSrv.Finish();




           




       }
       #endregion

       #region 联查
       public void Liancha()
       {
          

           string cInvcode = DbHelper.GetDbString(dataGridView1.CurrentRow.Cells["cinvcode"].Value);

           string cInvaddcode = DbHelper.GetDbString(dataGridView1.CurrentRow.Cells["cinvaddcode"].Value);
           string cInvname = DbHelper.GetDbString(dataGridView1.CurrentRow.Cells["cinvname"].Value);
           string cInvstd = DbHelper.GetDbString(dataGridView1.CurrentRow.Cells["cinvstd"].Value);
           if (string.IsNullOrEmpty(cInvcode)==false)
           {

               FrmTree frm = new FrmTree(cInvcode, cInvname, cInvstd, cInvaddcode);
               frm.ShowDialog();

           }
           else
           {

               MessageBox.Show("选中行存货编码为空，不能联查！");
           }
       }
       #endregion

       #region 查询
       public void Cx()
       {
           SearchCondition searchObj = new SearchCondition();
           //searchObj.AddCondition("cinvcode", txtcinvcode.Text, SqlOperator.Like);
           searchObj.AddCondition("cmaker", txtywy.Text, SqlOperator.Equal);
           searchObj.AddCondition("xmbm", txtcCusname.Text, SqlOperator.Like);
           searchObj.AddCondition("ddate", dateTimePicker1.Value.ToString("yyyy-MM-dd"), SqlOperator.MoreThanOrEqual, dateTimePicker1.Checked == false);
           searchObj.AddCondition("ddate", dateTimePicker2.Value.ToString("yyyy-MM-dd"), SqlOperator.LessThanOrEqual, dateTimePicker2.Checked == false);
           searchObj.AddCondition("czt", comboBox1.Text, SqlOperator.Equal);


           string conditionSql = searchObj.BuildConditionSql(2);
           if (!string.IsNullOrEmpty(txtcinvcode.Text))
           {

               conditionSql += string.Format(" and (cinvcode like '%{0}%' or cinvaddcode like '%{0}%')", txtcinvcode.Text);
           }
           //StringBuilder strb = new StringBuilder(@"SELECT 0 as xz,* from zdy_lk_xunjia where cmaker = '"+canshu.userName+"'");
           StringBuilder strb = new StringBuilder(@"SELECT 0 as xz,* from zdy_lk_xunjia where isnull(lx,1)=2");
           strb.Append(conditionSql);

          
           dtXunjia= DbHelper.ExecuteTable(strb.ToString());
           dataGridView1.DataSource = dtXunjia;
           if (dtXunjia.Rows.Count > 0)
           {
               GetMx(0);
           }
           else
           {
               dataGridView2.DataSource = null;
               dataGridView3.DataSource = null;
               gridControl1.DataSource = null;
           }
       }
       #endregion

       #region 输入条件 参照
       private void button1_Click(object sender, EventArgs e)
       {
           try
           {

               U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
               obj.RefID = "Inventory_AA";
               obj.Mode = U8RefService.RefModes.modeRefing;
               //obj.FilterSQL = " bbomsub =1";
               obj.FillText = txtcinvcode.Text;
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

                       this.txtcinvcode.Text = DbHelper.GetDbString(retRstGrid.Fields["cinvcode"].Value);
                   }
               }
           }
           catch (Exception ex)
           {
               MessageBox.Show("参照失败，原因：" + ex.Message);
           }
       }

       private void button2_Click(object sender, EventArgs e)
       {
           try
           {

               U8RefService.IServiceClass obj = new U8RefService.IServiceClass();
               obj.RefID = "hr_hi_person_AA";
               obj.Mode = U8RefService.RefModes.modeRefing;
               //obj.FilterSQL = " cdepcode in ('01','04','07')  and rpersontype =10";
               obj.FillText = txtywy.Text;
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

                       this.txtywy.Text = DbHelper.GetDbString(retRstGrid.Fields["cpsn_name"].Value);
                   }
               }
           }
           catch (Exception ex)
           {
               MessageBox.Show("参照失败，原因：" + ex.Message);
           }
       }
       #endregion

       #region 换行自动保存
       private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
       {


     
//           try
//           {
//               //新增行，自动保存
//               //DataRow dr =dtXunjia.Rows[e.RowIndex];
//               //if (dr.RowState == DataRowState.Added)
//               //没id，自动保存。有id，判断是否modifyed，如果更改了，update



//               string id = DbHelper.GetDbString(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);

//               if (id == "" || string.IsNullOrEmpty(id))
//               {

//                   int i = e.RowIndex;
//                   string sql = @" Insert Into zdy_lk_xunjia(ccusname,xmbm,cinvcode,cinvname,cinvstd,cqty1,cqty2,cqty3,cmemo1,czt,cmaker,ddate,dmaketime,bimportant,burgent,zdxjr) 
//                    values(@ccusname,@xmbm,@cinvcode,@cinvname,@cinvstd,@cqty1,@cqty2,@cqty3,@cmemo1,@czt,@cmaker,@ddate,getdate(),@bimportant,@burgent,@zdxjr) select @@identity ";
//                   object obj = DbHelper.GetSingle(sql, new SqlParameter[]{ 
//                             new SqlParameter("@ccusname", dataGridView1.Rows[i].Cells["ccusname"].Value), 
//                             new SqlParameter("@xmbm", dataGridView1.Rows[i].Cells["xmbm"].Value),
//                             new SqlParameter("@cinvcode", dataGridView1.Rows[i].Cells["cinvcode"].Value),
//                             new SqlParameter("@cinvname", dataGridView1.Rows[i].Cells["cinvname"].Value),
//                             new SqlParameter("@cinvstd", dataGridView1.Rows[i].Cells["cinvstd"].Value),
//                             new SqlParameter("@cqty1", dataGridView1.Rows[i].Cells["cqty1"].Value),
//                             new SqlParameter("@cqty2", dataGridView1.Rows[i].Cells["cqty2"].Value),
//                             new SqlParameter("@cqty3", dataGridView1.Rows[i].Cells["cqty3"].Value),
//                             new SqlParameter("@cmemo1",dataGridView1.Rows[i].Cells["cmemo1"].Value),
//                             new SqlParameter("@czt", dataGridView1.Rows[i].Cells["czt"].Value),
//                             new SqlParameter("@cmaker",canshu.userName),
//                             new SqlParameter("@ddate",dataGridView1.Rows[i].Cells["ddate"].Value),
//                             new SqlParameter("@bimportant",dataGridView1.Rows[i].Cells["bimportant"].Value),
//                             new SqlParameter("@burgent",dataGridView1.Rows[i].Cells["burgent"].Value),
//                             new SqlParameter("@zdxjr",dataGridView1.Rows[i].Cells["zdxjr"].Value)
//                            });
//                   //数据表赋值
//                   dtXunjia.Rows[e.RowIndex]["id"] = Convert.ToInt32(obj);
//                   // 设置为非更改状态
//                   dtXunjia.AcceptChanges();
//               }



//           }
//           catch (Exception ex)
//           {
//               MessageBox.Show(ex.Message);
//               return;
//           }
       }
       #endregion

       //private void dataGridView1_Leave(object sender, EventArgs e)
       //{
       //    this.Validate();
       //    this.Update();
       //}

       //private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
       //{

       //}
      


       private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
       {
           //if (dataGridView1.Columns[e.ColumnIndex].Name == "cinvcode")
           //{
           //    string cInvcode = e.FormattedValue.ToString();

             
           //     string sRe =   pdChongfu(cInvcode);
           //     if (sRe=="error")
           //         e.Cancel = true;

               
           //}
       }

             

                private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
                {
                  
                }

                private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
                {
  //if (dataGridView1.IsCurrentCellDirty)
  //                  {

                        dataGridView1.CommitEdit((DataGridViewDataErrorContexts)123);
                        dataGridView1.BindingContext[dataGridView1.DataSource].EndCurrentEdit();

                        this.dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
                    //}
                }

                private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
                {
                    if (dataGridView1 != null)
                    {
                        string sv = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                        Clipboard.SetData(DataFormats.Text, sv);
                    }
                }

                private void dataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
                {
                    if (dataGridView2 != null)
                    {
                        string sv = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                        Clipboard.SetData(DataFormats.Text, sv);
                    }
                }

                #region
                private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
                {
                    dataGridView1.EndEdit();
                    string sColname2 = this.dataGridView1.Columns[e.ColumnIndex].Name.ToString();
                    if (sColname2 == "cinvaddcode")
                    {
                        string cInvocode = DbHelper.GetDbString(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

                        string sql = "select top  1 cinvcode,cinvname,cinvstd from inventory where dedate is null and cinvaddcode  ='" + cInvocode + "'";
                        SqlDataReader sdr = DbHelper.ExecuteReader(sql);
                        while (sdr.Read())
                        {
                            dataGridView1.Rows[e.RowIndex].Cells["cinvname"].Value = sdr[1].ToString();
                            dataGridView1.Rows[e.RowIndex].Cells["cinvstd"].Value = sdr[2].ToString();
                            dataGridView1.Rows[e.RowIndex].Cells["cinvcode"].Value = sdr[0].ToString();
                        }
                    }
                                        
                                            

                }
                #endregion

                private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
                {

                }
                private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
                {
                    this.dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
                }

                #region 点击显示明细，增加库存和历史询价
                //private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
                //{
                //    int iRow = e.RowIndex;
                //    //GetMx(iRow);
                //}

                private void GetMx(int iRow)
                {
                    if (iRow != -1)
                    {
                        string id = DbHelper.GetDbString(dataGridView1.Rows[iRow].Cells["id"].Value);
                        string cCAS = DbHelper.GetDbString(dataGridView1.Rows[iRow].Cells["cinvaddcode"].Value);
                        string sql = "";
                        //采购询价
                        dtXunjias = DbHelper.ExecuteTable(@"select ddate,gys,xunjia1,xunjia2,xunjia3,bz1,bz2,bz3,
               bYanfaQuery,cCost,cattch,cattch_fileid   from zdy_lk_xunjias where id ='" + id + "'");
                        dataGridView2.DataSource = dtXunjias;
                        //现存量
                        if (!string.IsNullOrEmpty(cCAS))
                        {
                            sql = string.Format(@"SELECT c.cwhname,  b.invaddcode,  b.InvCode,b.InvName ,b.InvStd,b.ComUnitName,a.cBatch,convert(real, a.iQuantity) as  iQuantity  FROM warehouse c,
dbo.CurrentStock  a,dbo.v_bas_inventory b WHERE a.cInvCode = b.InvCode  and a.cwhcode = c.cwhcode
AND a.iQuantity>0  and b.invaddcode like '%{0}%'  ", cCAS);
                            DataTable dtlk = DbHelper.ExecuteTable(sql);

                            //是否包含江西库存
                            if (canshu.bCxJiangXikc == "1")
                            {
                                //江西存量
                                sql = string.Format(@"SELECT c.cwhname,  b.invaddcode,  b.InvCode,b.InvName ,b.InvStd,b.ComUnitName,a.cBatch,convert(real, a.iQuantity) as  iQuantity  FROM warehouse c,
dbo.CurrentStock  a,dbo.v_bas_inventory b WHERE a.cInvCode = b.InvCode  and a.cwhcode = c.cwhcode
AND a.iQuantity>0  and b.invaddcode like '%{0}%'  ", cCAS);
                                DataTable dtyt = DbHelper2.Execute(sql, canshu.conStr2).Tables[0];

                                dtKuCun = dtlk.Copy();
                                //添加DataTable2的数据
                                foreach (DataRow dr in dtyt.Rows)
                                {
                                    dtKuCun.ImportRow(dr);
                                }
                                gridControl1.DataSource = dtKuCun;
                            }
                            else
                            {
                                dtKuCun = dtlk.Copy();
                                gridControl1.DataSource = dtKuCun;
                            }

                            //历史询价，查询最近3个月的历史询价
                            sql = string.Format(@" select a.id,a.ddate,a.cmaker,ccusname,a.fpxjrxm,a.cinvcode,cinvaddcode,a.cinvstd,a.cinvname,a.cqty1,b.xunjia1,b.xunjia2,b.xunjia3,b.bz1,b.bz2,b.bz3  from zdy_lk_xunjia a,zdy_lk_xunjias b 
                where a.id = b.id and A.ddate>DATEADD(M,-3,GETDATE())  and (cinvcode = '{0}' or cinvaddcode = '{0}')  ", cCAS);
                            dtLishiXunjia = DbHelper.ExecuteTable(sql);
                            dataGridView3.DataSource = dtLishiXunjia;
                        }
                        else
                        {
                            gridControl1.DataSource = null;
                            dataGridView3.DataSource = null;

                        }
                    }
                }
                #endregion


                private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
                {
                    int iRow = e.RowIndex;
                    GetMx(iRow);
                }

                private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
                {
                    int iRow = e.RowIndex;
                    //CAS号修改后，表体内容更改
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "cinvaddcode")
                    {
                        string cCAS = DbHelper.GetDbString(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);


                        string sql = @"select a.cinvcode, a.cinvname,a.cinvstd,a.cEnglishname from inventory a 
 where dedate is null and  a.cinvaddcode  ='" + cCAS + "'";
                        DataTable dt = DbHelper.ExecuteTable(sql);
                        if (dt.Rows.Count > 0)
                        {
                            dataGridView1.Rows[e.RowIndex].Cells["cinvcode"].Value = DbHelper.GetDbString(dt.Rows[0]["cinvcode"]);
                            dataGridView1.Rows[e.RowIndex].Cells["cinvname"].Value = DbHelper.GetDbString(dt.Rows[0]["cinvname"]);
                            dataGridView1.Rows[e.RowIndex].Cells["cinvstd"].Value = DbHelper.GetDbString(dt.Rows[0]["cinvstd"]);
                            dataGridView1.Rows[e.RowIndex].Cells["cEnglishname"].Value = DbHelper.GetDbString(dt.Rows[0]["cEnglishname"]);

                        }
                        else
                        {
                            sql = string.Format(@"select  cinvname, cEnglishname from zdy_lk_inventory a  where  cas  ='{0}'", cCAS);
                            dt = DbHelper.ExecuteTable(sql);
                            if (dt.Rows.Count > 0)
                            {
                                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["cinvname"].Value = DbHelper.GetDbString(dt.Rows[0]["cinvname"]);
                                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["cEnglishname"].Value = DbHelper.GetDbString(dt.Rows[0]["cEnglishname"]);

                            }
                        }


                        GetMx(iRow);
                    }
                }




    }
}
