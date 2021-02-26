using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Layout;
using log4net.Core;
using log4net.Appender;
using System.Windows.Forms;

namespace HILYCode
{
    class ListViewLog:AppenderSkeleton
    {
        public ListView listView { get; set; }
  
        public ListViewLog()
        {

        }
        protected override void Append(LoggingEvent loggingEvent)
        {
            if (this.listView == null)
            {
                return;
            }
            if (!this.listView.IsHandleCreated)
            {
                return;
            }
            if (this.listView.IsDisposed)
            {
                return;
            }
            var patternLayout = this.Layout as PatternLayout;
            var str = string.Empty;
            if (patternLayout != null)
            {
                str = patternLayout.Format(loggingEvent);
                if (loggingEvent.ExceptionObject != null)
                {
                    str += loggingEvent.ExceptionObject.ToString() + Environment.NewLine;
                }
            }
            else
            {
                str = loggingEvent.LoggerName + "-" + loggingEvent.RenderedMessage + Environment.NewLine;
            }
            if (!this.listView.InvokeRequired)
            {
                printf(str);
            }
            else
            {
                this.listView.BeginInvoke((MethodInvoker)delegate
                {
                    if (!this.listView.IsHandleCreated)
                    {
                        return;
                    }
                    if (this.listView.IsDisposed)
                    {
                        return;
                    }
                    printf(str);
                });
            }
        }
        private void printf(string str)
        {
            if (listView.Items.Count > 20)
            {
                listView.Items.Clear();
            }
            ListViewItem item = new ListViewItem();
            item.Text = str.ToString();
            listView.BeginUpdate();
            listView.Items.Add(item);
            //滚到最后
            listView.Items[listView.Items.Count - 1].EnsureVisible();
            listView.EndUpdate();
        }
    }
}
