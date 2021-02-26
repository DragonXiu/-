using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using System.Configuration;
using System.IO;

namespace JobLoadBoard
{

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            JcDeptDt();


        }
        public Bitmap pic = new Bitmap(1000, 600); //保存临时图片
        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer(); //定义计时器
        public int threadqty = 1;
        public DataTable cordinate = null; //定义坐标数组
        public string comboxtext1 = ""; //部门车间
        public string resourceid0 = ""; //临时选中的机台号
        System.Drawing.Point p_orignal; //捕获当前鼠标坐标
        public DataTable ArrayJob = generatetable();


        //public string sqlConnectionString = "Server=192.168.0.219;Database=Product;User ID=sa;Password=Rep@2014/p;Trusted_Connection=False;";

        /*与数据库建立连接 */
        public static readonly string sqlConnectionString = ConfigurationManager.ConnectionStrings["ClassRoomConnectionString"].ToString(); //引用配置文件的连接字符串

        public DataTable GetDataBySql(string SQLString, string sqlConnectionString)
        {
            if (sqlConnectionString == "") throw new Exception("连接字符串未初始化");
            DataTable dt = new DataTable("DefaultDataTable");
            DataSet ds = Query(SQLString, sqlConnectionString);
            dt = ds.Tables[0];
            return dt;
        }

        public DataSet Query(string SQLString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }

        public int ExecuteSql(string SQLString, string sqlConnectionString)
        {
            using (SqlConnection connection = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        /* 设置机台图标的坐标 */
        public DataTable GetCordinate()
        {
            DataTable cordinate = new DataTable();
            cordinate.Columns.Add("x", typeof(decimal));
            cordinate.Columns.Add("y", typeof(decimal));
            cordinate.Columns.Add("Length", typeof(decimal));
            cordinate.Columns.Add("Height", typeof(decimal));
            cordinate.Columns.Add("Name", typeof(string));
            cordinate.Columns.Add("Color", typeof(string));

            DataTable machine = MachineDt();
            for (int i = 0; i < machine.Rows.Count / 10 + 1; i++)
            {

                for (int j = 0; j < 10; j++)
                {

                    DataRow cordinateRow = cordinate.NewRow();

                    cordinateRow["Length"] = 60;
                    cordinateRow["Height"] = 20;
                    if (i * 10 + j > machine.Rows.Count - 1)
                        break;
                    cordinateRow["x"] = Convert.ToInt32(machine.Rows[i * 10 + j]["ResourceX_c"]);
                    cordinateRow["y"] = Convert.ToInt32(machine.Rows[i * 10 + j]["ResourceY_c"]);
                    cordinateRow["Name"] = machine.Rows[i * 10 + j]["ResourceID"].ToString();
                    cordinateRow["Color"] = machine.Rows[i * 10 + j]["Color"].ToString();

                    cordinate.Rows.InsertAt(cordinateRow, i * 10 + j);
                }

            }
            return cordinate;
        }


        /* 绘制机台图标 */
        private void panelPaint(DataTable cordinate, int a)
        {
            Graphics gc = Graphics.FromImage(pic);
            int qty = GetCordinate().Rows.Count;
            //DataTable cordinate = GetCordinate();
            float x = 0;
            float y = 0;
            float length = 0;
            float height = 0;
            Font myFont = new Font("宋体", 10, FontStyle.Regular);//定义写字笔字体
            Brush bush = new SolidBrush(Color.White);//字体的颜色
            SolidBrush mySolidBrush = null;
            for (int i = a; i < qty / threadqty; i++)
            {
                /// 设置绘图的颜色
                switch (cordinate.Rows[i]["Color"].ToString())
                {
                    case "Green":
                        mySolidBrush = new SolidBrush(Color.Green);//定义刷子的颜色
                        break;
                    case "Red":
                        mySolidBrush = new SolidBrush(Color.Red);//定义刷子的颜色
                        break;
                    case "Black":
                        mySolidBrush = new SolidBrush(Color.Black);//定义刷子的颜色
                        break;
                    case "Gray":
                        mySolidBrush = new SolidBrush(Color.Gray);//定义刷子的颜色
                        break;
                }
                x = (float)Math.Round(Convert.ToDecimal(cordinate.Rows[i]["x"]));
                y = (float)Math.Round(Convert.ToDecimal(cordinate.Rows[i]["y"]));
                length = (float)Math.Round(Convert.ToDecimal(cordinate.Rows[i]["length"]));
                height = (float)Math.Round(Convert.ToDecimal(cordinate.Rows[i]["height"]));
                gc.FillRectangle(mySolidBrush, x, y, length, height); //填充图标
                gc.DrawString(cordinate.Rows[i]["Name"].ToString(), myFont, bush, x, y + height / 4); //写字
                //Image image = global::TestGraphic.Properties.Resources.niang;
                //gc.DrawImage(image, new Rectangle(400, 20, 300, 300));
            }
            gc.Save();

        }

        /* 获取部门下所有机台 */
        private DataTable MachineDt()
        {
            DataTable dt0 = null;
            DataTable dt1 = null;
            DataTable dt2 = null;

            string sqlStr1 = String.Format(@"select r.ResourceID,r.resourcex_c,r.resourcey_c
            from dbo.Resource r 
			left join erp.Resourcegroup rg on r.company = rg.company and r.resourcegrpid = rg.resourcegrpid
            left join erp.Jcdept jc on rg.company = jc.company and rg.jcdept = jc.jcdept
            where jc.description in (N'{0}') order by r.ResourceID ", comboxtext1);
            dt0 = GetDataBySql(sqlStr1, sqlConnectionString);
            dt1 = WorkingMachineIDDt();
            dt2 = WorkingMachineDt();
            for (int i = 0; i < dt0.Rows.Count; i++)
            {
                int notice = 0;
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    if (dt0.Rows[i]["ResourceID"].ToString() != dt1.Rows[j]["ResourceID"].ToString())
                    {
                        notice = notice + 1;
                    }
                }
                if (notice == dt1.Rows.Count)
                {
                    DataRow DR2 = dt2.NewRow();
                    DR2["ResourceID"] = dt0.Rows[i]["ResourceID"];
                    DR2["Color"] = "Gray";
                    DR2["resourcex_c"] = dt0.Rows[i]["resourcex_c"];
                    DR2["resourcey_c"] = dt0.Rows[i]["resourcey_c"];
                    dt2.Rows.InsertAt(DR2, dt2.Rows.Count);
                }
            }
            return dt2;
        }

        /* 正在使用的机台排程信息 */
        private DataTable WorkingMachineDt()
        {
            DataTable dt0 = null;
            DataTable dt1 = null;
            string sqlStr1 = String.Format(@"select ld.jcdept,jc.Description,ld.resourceid, ld.JobNum,ld.OprSeq, case when ld.labortype = 'I' then N'Red' when ld.labortype = 'S' then N'Black' else N'Green' end 'Color',r.resourcex_c,r.resourcey_c
            from  erp.labordtl ld
			left join erp.Jcdept jc on ld.company = jc.company and ld.jcdept = jc.jcdept
            left join dbo.Resource r on ld.company = r.company and ld.resourceid = r.resourceid
            where jc.Description = N'{0}'  and ld.PayrollDate = convert(date,getdate()) and ld.ActiveTrans  = 1 and ld.resourceid <> ''
			order by ld.resourceid ", comboxtext1);
            dt0 = GetDataBySql(sqlStr1, sqlConnectionString);
            dt1 = dt0.Copy();
            dt1.Clear();
            if (dt0.Rows.Count > 0)
            {
                dt1.Rows.Add(dt0.Rows[0].ItemArray);

                for (int i = 1; i < dt0.Rows.Count; i++)
                {
                    if (dt0.Rows[i]["ResourceID"].ToString() != dt0.Rows[i - 1]["ResourceID"].ToString())
                    {
                        dt1.Rows.Add(dt0.Rows[i].ItemArray);
                    }

                }
            }
            return dt1;
        }

        /* 正在使用的机台型号 */
        private DataTable WorkingMachineIDDt()
        {
            DataTable dt0 = null;
            string sqlStr1 = String.Format(@"select distinct ld.resourceid
            from  erp.labordtl ld
			left join erp.Jcdept jc on ld.company = jc.company and ld.jcdept = jc.jcdept
            where jc.Description = N'{0}'  and ld.PayrollDate = convert(date,getdate()) and ld.ActiveTrans  = 1", comboxtext1);
            dt0 = GetDataBySql(sqlStr1, sqlConnectionString);
            //dataGridView1.DataSource = dt0;
            return dt0;
        }

        /* 获取部门代码 */
        private void JcDeptDt()
        {
            DataTable dt0 = null;
            string sqlStr1 = String.Format(@"select jc.Jcdept,jc.Description from erp.JcDept jc where jc.jcdept in ('P420','P520') ");
            dt0 = GetDataBySql(sqlStr1, sqlConnectionString);
            comboBox1.DataSource = dt0;
            comboBox1.DisplayMember = "Description";
            comboBox1.ValueMember = "Jcdept";
        }

        public void button1_Click(object sender, EventArgs e) //手动刷新当前部门的机台状态
        {
            backgroundpic();
            cordinate = GetCordinate();
            panelPaint(cordinate, 0);
            TaskFactory tf = new TaskFactory();
            tf.StartNew(thread1);
            Thread.Sleep(100);
            pictureBox1.Image = pic;
            pictureBox1.Show();
        }

        private void thread1()
        {
            cordinate = GetCordinate();
            panelPaint(cordinate, 0);

        }

        private void pictureBox1_MouseClick(object Sender, MouseEventArgs e) //点击机台图标查询对应报工明细
        {
            dataGridView1.DataSource = null;
            //p_orignal = e.Location;
            p_orignal = pictureBox1.PointToClient(Control.MousePosition);
            label1.Text = p_orignal.X.ToString();
            label2.Text = p_orignal.Y.ToString();
            TaskFactory tf = new TaskFactory();
            tf.StartNew(thread2);
            Thread.Sleep(200);
            if (resourceid0 != "")
            {
                dataGridView1.DataSource = Labordtl(resourceid0);
                if (dataGridView1.Rows.Count <= 0)
                {
                    dataGridView2.DataSource = null;
                    dataGridView3.DataSource = null;
                    return;
                }
                dataGridView1.Rows[0].Selected = true;
                ArrayJob.Clear();
                DataRow rowjob = ArrayJob.NewRow();
                rowjob["JobNum"] = dataGridView1.Rows[0].Cells["工单号"].Value.ToString();
                rowjob["OpCode"] = dataGridView1.Rows[0].Cells["工序"].Value.ToString();
                ArrayJob.Rows.InsertAt(rowjob, 0);
                for (int j = 0; j < ArrayJob.Rows.Count; j++)
                {
                    grid2data(ArrayJob);
                }
            }
        }
        private void thread2()
        {
            if (GetCordinate().Rows.Count > 0 && p_orignal.Y < 600 && p_orignal.Y > 0)
            {
                DataTable dt0 = GetCordinate();
                for (int i = 0; i < dt0.Rows.Count; i++)
                {
                    if (p_orignal.X >= (decimal)dt0.Rows[i]["x"] && p_orignal.X <= ((decimal)dt0.Rows[i]["x"] + (decimal)dt0.Rows[i]["Length"]) && p_orignal.Y >= (decimal)dt0.Rows[i]["y"] && p_orignal.Y <= ((decimal)dt0.Rows[i]["y"] + (decimal)dt0.Rows[i]["Height"]))
                    {
                        resourceid0 = (string)dt0.Rows[i]["Name"];
                    }
                }
            }

        }

        private DataTable Labordtl(string resourceID)  //查询报工明细
        {
            DataTable dt0 = null;
            string sqlStr1 = String.Format(@"select EmployeeNum '员工ID',Jobnum '工单号',OpCode '工序',PayrollDate '日期',ResourceID '资源ID'  from erp.labordtl where Resourceid  = '{0}' and PayrollDate = convert(date,getdate())  ", resourceID);
            dt0 = GetDataBySql(sqlStr1, sqlConnectionString);
            return dt0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //切换部门车间的机台
        {
            comboxtext1 = comboBox1.Text;
            backgroundpic();
            TaskFactory tf = new TaskFactory();
            tf.StartNew(thread1);
            Thread.Sleep(200);
            pictureBox1.Image = pic;
            pictureBox1.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            JcDeptDt();
            backgroundpic();
            TaskFactory tf = new TaskFactory();
            tf.StartNew(thread1);
            Thread.Sleep(100);
            pictureBox1.Image = pic;
            pictureBox1.Show();
            timer1.Interval = 1800000; //设置定时器自动刷新机台状态
            timer1.Start();
        }

        public static DataTable generatetable()
        {
            DataTable ArrayJob = new DataTable();
            ArrayJob.Columns.Add("JobNum", typeof(string));
            ArrayJob.Columns.Add("OpCode", typeof(string));
            DataRow RowJob = ArrayJob.NewRow();
            RowJob["JobNum"] = "";
            RowJob["OpCode"] = "";
            ArrayJob.Rows.InsertAt(RowJob, 0);
            return ArrayJob;
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            ArrayJob.Clear();
            int jobcount = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected == true)
                {
                    jobcount = ArrayJob.Rows.Count;
                    DataRow rowjob = ArrayJob.NewRow();
                    rowjob["JobNum"] = dataGridView1.Rows[i].Cells["工单号"].Value.ToString();
                    rowjob["OpCode"] = dataGridView1.Rows[i].Cells["工序"].Value.ToString();
                    ArrayJob.Rows.InsertAt(rowjob, jobcount);
                }
            }

            for (int j = 0; j < ArrayJob.Rows.Count; j++)
            {
                grid2data(ArrayJob);
            }

        }

        private void grid2data(DataTable ArrayJob)
        {
            DataTable dt0 = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            dt1.Columns.Add("报工日期", typeof(DateTime));
            dt1.Columns.Add("工单批次号", typeof(string));
            dt1.Columns.Add("员工", typeof(string));
            dt1.Columns.Add("报工数量", typeof(decimal));
            dt1.Columns.Add("报废数量", typeof(decimal));
            string jobnum = "";
            string opcode = "";

            for (int i = 0; i < ArrayJob.Rows.Count; i++)
            {
                jobnum = ArrayJob.Rows[i]["JobNum"].ToString();
                opcode = ArrayJob.Rows[i]["OpCode"].ToString();
                string sqlStrl1 = String.Format(@"select payrolldate '报工日期',shortchar06 as '工单批次号',employeeNum '员工',laborqty '报工数量',scrapqty '报废数量' from laborDtl where  jobNUm = '{0}' and opcode = '{1}' ", jobnum, opcode);
                dt0 = GetDataBySql(sqlStrl1, sqlConnectionString);
                string sqlStrl2 = String.Format(@"select sum(laborqty) '合计报工数量',sum(scrapqty) '合计报废数量' from laborDtl where  jobNUm = '{0}' and opcode = '{1}' ", jobnum, opcode);
                dt2 = GetDataBySql(sqlStrl2, sqlConnectionString);
                int k = 0;
                for (int j = dt1.Rows.Count; j < dt0.Rows.Count; j++)
                {
                    dt1.Rows.Add(dt0.Rows[k].ItemArray);
                    k = k + 1;
                }
            }
            dataGridView2.DataSource = dt1;
            dataGridView3.DataSource = dt2;
        }

        public void backgroundpic()
        {
            string path = "";
            if (comboxtext1.Trim() == "")
            {
                path = AppDomain.CurrentDomain.BaseDirectory + @"\\BackgroundPictures\\" + "P420-油封硫化.JPG";

            }
            else
            {
                //path = AppDomain.CurrentDomain.BaseDirectory + @"\\BackgroundPictures\\" + comboxtext1 + ".JPG";
                path = AppDomain.CurrentDomain.BaseDirectory + @"\\BackgroundPictures\\" + "P420-油封硫化.JPG";
            }
            FileStream fs = File.OpenRead(path); //OpenRead
            int filelength = 0;
            filelength = (int)fs.Length; //获得文件长度 
            Byte[] image = new Byte[filelength]; //建立一个字节数组 
            fs.Read(image, 0, filelength); //按字节流读取 
            Image result = System.Drawing.Image.FromStream(fs);
            pic = new Bitmap(result);
            fs.Close();
        }

    }



}
