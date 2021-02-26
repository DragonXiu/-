using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HILYCode.view
{
    public partial class dataList : Form
    {
        string ls_sup_prd_no = "";
        public dataList()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                CheckData();
                DataSet dataset = new DataSet();
                if (orderTxt.Text == "")
                {

                    if (ls_sup_prd_no == "")
                    {
                        string strsqldate = String.Format("select [ID] ,[WorkOrder] ,[BarCode] ,[InsertTime]  from [BindingInfos] where CONVERT(varchar(120),InsertTime,23)>='{0} ' " +
                            "and CONVERT(varchar(120),InsertTime,23)<='{1}'", dateTimePic1.Text ,dateTimePic2.Text);
                        dataset = SqlData.GetDataSet(strsqldate, "table");
                        dataGridView1.DataSource = dataset.Tables["table"];
                    }
                    else
                    {
                        string strsqldate =String.Format( "select [ID] as 序号 ,[WorkOrder] as 工单号,[BarCode] as  条码,[InsertTime] as 时间 from [BindingInfos] where BarCode like '{0}%'  " +
                            "and CONVERT(varchar(120),InsertTime,23)>='{1}' and CONVERT(varchar(120),InsertTime,23)<='{3}'", ls_sup_prd_no, dateTimePic1.Text, dateTimePic2.Text);
                        dataset = SqlData.GetDataSet(strsqldate, "table");
                        dataGridView1.DataSource = dataset.Tables["table"];
                    }


                }
                else if (orderTxt.Text != "")
                {
                    string strsql = "select [ID] as 序号 ,[WorkOrder] as 工单号,[BarCode] as 条码,[InsertTime] as 时间 from [BindingInfos] where [WorkOrder]='" + orderTxt.Text.Trim() + "' and " +
                        " CONVERT(varchar(120),InsertTime,23)>='" + dateTimePic1.Text + "' and CONVERT(varchar(120)," +
                        "InsertTime,23)<='" + dateTimePic2.Text + "' order by inserttime";
                    dataset = SqlData.GetDataSet(strsql, "table");
                    dataGridView1.DataSource = dataset.Tables["table"];
                }
            }
            catch (Exception ex)
            {


            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataList_Load(object sender, EventArgs e)
        {

        }
        private void CheckData()
        {
            if (radio03304.Checked)
            {
                ls_sup_prd_no = "61500010037";
            }
            else if (radio03305.Checked)
            {
                ls_sup_prd_no = "61500010100";
            }
            else if (radio04559.Checked)
            {
                ls_sup_prd_no = "612630010106";
            }
            else if (radio04560.Checked)
            {
                ls_sup_prd_no = "612630030009";
            }
            else if (radio02916.Checked)
            {
                ls_sup_prd_no = "610800010037";
            }
            else if (radio02917.Checked)
            {
                ls_sup_prd_no = "610800010027";
            }
        }

        private void butLo_Click(object sender, EventArgs e)
        {
            try
            {
                CheckData();

                string sqlstr = "select " + ls_sup_prd_no + " as 条码前缀,a.number as 流水号 from master..spt_values a  left join bindinginfos b on a.number = RIGHT(b.barcode, 5)" +
                      "where type = 'P'" +
                      "and number>= (select MIN(right(barcode, 5)) from bindinginfos where CONVERT(varchar(120),InsertTime,23)>='" + dateTimePic1.Text + "'and CONVERT(varchar(120),InsertTime,23)<='" + dateTimePic2.Text + "' and BarCode like '" + ls_sup_prd_no + "%')" +
                      "and number<= (select MAX(RIGHT(barcode, 5))from BindingInfos where CONVERT(varchar(120),InsertTime,23)>='" + dateTimePic1.Text + "'and CONVERT(varchar(120),InsertTime,23)<='" + dateTimePic2.Text + "' and BarCode like '" + ls_sup_prd_no + "%')" +
                      "and RIGHT(b.barcode,5) is null";
                DataSet dataset = new DataSet();
                dataset = SqlData.GetDataSet(sqlstr, "table");
                dataGridView1.DataSource = dataset.Tables["table"];
            }
            catch (Exception)
            {

              
            }

        }

        private void radio03304_CheckedChanged(object sender, EventArgs e)
        {
            if (radio02916.Checked || radio02917.Checked || radio03304.Checked || radio03305.Checked || radio04559.Checked || radio04560.Checked)
            {
                butLo.Enabled = true;
                butCho.Enabled = true;
            }
            else
            {
                butLo.Enabled = false;
                butCho.Enabled = false;
            }
        }

        private void butCho_Click(object sender, EventArgs e)
        {
            try
            {          
            CheckData();
            string sql = " select [ID] as 序号 ,[WorkOrder] as 工单号,[BarCode] as  条码,[InsertTime] as 时间" +
                "  FROM[HILWILL].[dbo].[BindingInfos] where barcode in " +
                "(select barcode from[BindingInfos] group by barcode having COUNT(barcode) > 1) " +
                " and  CONVERT(varchar(120),InsertTime,23)>='" + dateTimePic1.Text + "' and CONVERT(varchar(120),"
                + "InsertTime,23)<='" + dateTimePic2.Text + "' and BarCode like '" + ls_sup_prd_no + "%'  ";
            DataSet dataset = new DataSet();
            dataset = SqlData.GetDataSet(sql, "table");
            dataGridView1.DataSource = dataset.Tables["table"];
            }
            catch (Exception)
            {

              
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length>=12)
            {
                butLo.Enabled = true;
                butCho.Enabled = true;
                ls_sup_prd_no = textBox1.Text.Trim();
            }
            else if (textBox1.Text.Length<=12)
            {
                butLo.Enabled = false;
                butCho.Enabled = false;
            }
        }
    }
}
