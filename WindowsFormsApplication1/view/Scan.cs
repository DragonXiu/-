using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using log4net;
using log4net.Layout;
using log4net.Config;
using System.Runtime.InteropServices;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Globalization;

namespace HILYCode
{
    public partial class Form1 : Form
    {
        public static ILog log;
        [DllImport("user32.dll")]
        static extern void BlockInput(bool Block);//禁用键盘鼠标
        //拖动无窗体的控件

        [DllImport("user32.dll")]//拖动无窗体的控件
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //拖动窗体
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);

        }
        BardCodeHooK BarCode = new BardCodeHooK();
        public Form1()
        {
            InitializeComponent();
            //  BarCode.BarCodeEvent += new BardCodeHooK.BardCodeDeletegate(BarCode_BarCodeEvent);

        }



        private void BarCode_BarCodeEvent(BardCodeHooK.BarCodes barcode)

        {
            ShowInfo(barcode);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // skinEngine1.SkinFile = Application.StartupPath + @"\Skins\office2007.ssk";
   
            orderTxt.Focus();
            orderTxt.SelectAll();
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            var logPattern = "%d{yyyy-MM-dd HH:mm:ss} --%-5p-- %m%n";
            //设置打印日志
            var list_logAppender = new ListViewLog()
            {
                listView = this.listView1,
                Layout = new PatternLayout(logPattern)

            };
            log4net.Config.BasicConfigurator.Configure(list_logAppender);
            // BlockInput(true);

        }

        private delegate void ShowInfoDelegate(BardCodeHooK.BarCodes barCode);
        private void ShowInfo(BardCodeHooK.BarCodes barCode)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ShowInfoDelegate(ShowInfo), new object[] { barCode });
            }
            else
            {
                //if (barCode.BarCode.Length > 12)
                //{
                codeTxt.Text = barCode.BarCode;

                //labCode.Text = barCode.BarCode;
                //}
                //else
                //{
                //    orderTxt.Text = barCode.BarCode;//是否为扫描枪输入，如果为true则是 否则为键盘输入
                //}
                //MessageBox.Show(barCode.BarCode.ToString());
            }
        }
        private void orderTxt_TextChanged(object sender, EventArgs e)
        {
            // 
            //orderTxt.Clear();
            if (orderTxt.Text.EndsWith("\r"))
            {
                codeTxt.Clear();
                codeTxt.Focus();
            }

        }
        private void codeTxt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (codeTxt.Text != "")
                {
                    if (codeTxt.Text.EndsWith("\r"))
                    {
                    }
                    // 
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = DateTime.Now.ToString();

            //将当前时间转换为字符串

            string t2 = string.Format("{0:hh:mm:ss}", DateTime.Now); //DateTime.Now.ToLongTimeString().ToString();

            //DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
            //dtFormat.ShortDatePattern = "HH: mm:ss";
            DateTime dt = DateTime.ParseExact("17:00:00", "HH:mm:ss", null);
            DateTime dt1 = DateTime.ParseExact(t2, "HH:mm:ss", null);
            DateTime dt2 = DateTime.ParseExact("17:00:02", "HH:mm:ss", null);

            try
            {


                if (dt1.TimeOfDay > dt.TimeOfDay && dt1 < dt2)
                {
                    DataSet dataset = new DataSet();
                    string strsql = "select [ID],[WorkOrder],[BarCode],[InsertTime] from BindingInfos where  DATEADD(DAY,-1,GETDATE())<InsertTime and InsertTime <GETDATE()  order by inserttime";
                    dataset = SqlData.GetDataSet(strsql, "table");
                    ExportExcel(dataset.Tables["table"]);
                }
            }
            catch (Exception ex)
            {

                log.Error(DateTime.Now.ToString() + "定时导出失败" + ex.ToString()); ;
            }

        }

        private void 数据查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            view.dataList datalist = new view.dataList();
            datalist.ShowDialog();
        }

        private void 关闭系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //BarCode.Stop();
            Close();
        }
        /// <summary>
        /// excel
        /// </summary>
        private void ExportExcel(DataTable dt)
        {
            try
            {
                //创建excel工作薄
                IWorkbook wb = new HSSFWorkbook();

                //创建一个sheet表
                ISheet sheet = wb.CreateSheet(dt.TableName);

                //创建一行
                IRow rowH = sheet.CreateRow(0);

                //创建一个单元格
                ICell cell = null;

                //创建Excel单元格样式
                ICellStyle cellStyle = wb.CreateCellStyle();
                //cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                //cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                //cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                //cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                //创建格式
                IDataFormat dataFormat = wb.CreateDataFormat();

                foreach (DataColumn col in dt.Columns)
                {
                    //创建单元格并设置单元格内容
                    rowH.CreateCell(col.Ordinal).SetCellValue(col.Caption);
                    //设置单元格格式
                    rowH.Cells[col.Ordinal].CellStyle = cellStyle;
                }
                //写入数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //跳过第一行，第一行为列名
                    IRow row = sheet.CreateRow(i + 1);

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        cell = row.CreateCell(j);
                        cell.SetCellValue(dt.Rows[i][j].ToString());
                        cell.CellStyle = cellStyle;
                    }
                }
                //设置导出文件路径
                string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\excel\\";
                //设置新建文件路径几名称
                string savePath = path + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒") + ".xls";
                //创建文件
                FileStream file = new FileStream(savePath, FileMode.CreateNew, FileAccess.Write);
                //创建一个IO流
                MemoryStream ms = new MemoryStream();
                //写入到流
                wb.Write(ms);
                //转换为字节数组
                byte[] bytes = ms.ToArray();
                file.Write(bytes, 0, bytes.Length);
                file.Flush();

                //还可以调用下面的方法。把流输出到浏览器下载
                //outputcc
                //释放资源
                bytes = null;
                ms.Close();
                ms.Dispose();
                file.Close();
                file.Dispose();
                wb.Close();
                sheet = null;
                wb = null;


                //水平对齐
                // cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                //垂直对齐
                //.VerticalAlignment = VerticalAlignment.Center;
                //设置字体
                // font = wb.CreateFont();
                //font.FontHeightInPoints = 18;
                // font.FontName = "微软雅黑";
                //cellStyle.SetFont(font);
            }
            catch (Exception ex)
            {


            }


        }

        private void 测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSet dataset = new DataSet();
            string strsql = "select [ID],[WorkOrder],[BarCode],[InsertTime] from [BindingInfos] where InsertTime>convert(varchar(100),GETDATE(),23)  order by inserttime";
            dataset = SqlData.GetDataSet(strsql, "table");
            ExportExcel(dataset.Tables["table"]);
        }

        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (StartToolItem.Text == "开始")
            {
                if (orderTxt.Text == "")
                {
                    MessageBox.Show("工单号不为空！");
                    return;
                }
                //安装钩子
                //BarCode.Start();
                timer2.Enabled = true;
                orderTxt.ReadOnly = true;
                StartToolItem.Text = "停止";
                //BlockInput(true);
                codeTxt.Focus();//文本
            }
            else if (StartToolItem.Text == "停止")
            {
                //卸载钩子
                //BarCode.Stop();
                timer2.Enabled = false;
                orderTxt.ReadOnly = false;
                StartToolItem.Text = "开始";
                orderTxt.Focus();
                orderTxt.Clear();
                codeTxt.Clear();
            }

        }

        private DateTime _dt = DateTime.Now;
        //禁止键盘输入
        private void codeTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.Handled = true;
            //DateTime tempDt = DateTime.Now;//保存按键时刻时间点
            //TimeSpan ts = tempDt.Subtract(_dt); //获取时间点
            //if (codeTxt.Text.Length>=1&&ts.Milliseconds>50)
            //{
            //    codeTxt.Text = "";
            //}
            //_dt = tempDt;

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            codeTxt.Focus();

        }

        private void button1_Click(object sender, EventArgs e)
        {
        //    codeTxt.Clear();
        }

        private void codeTxt_KeyPress_1(object sender, KeyPressEventArgs e)
        {
             if (e.KeyChar == (char)13)
            {
                if (orderTxt.Text != "" && codeTxt.Text != "")
                {
                    log.Info("工单：" + orderTxt.Text + "    " + "条码：" + codeTxt.Text + "绑定成功");

                    string sqlstr = "insert into [BindingInfos] (workorder,barcode) values('" + orderTxt.Text.Trim() + "','" + codeTxt.Text.Trim() + "')";
                    SqlData.ExecutSqlCommend(sqlstr);
                }
                codeTxt.SelectAll();
                codeTxt.Clear();
            }

        }

        private void but_Click(object sender, EventArgs e)
        {
            codeTxt.Clear();
        }
    }
}
