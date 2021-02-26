using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace HILYCode
{

    /// <summary>
        /// 获取键盘输入或者USB扫描枪数据 可以是没有焦点 应为使用的是全局钩子
        /// USB扫描枪 是模拟键盘按下
        /// 这里主要处理扫描枪的值，手动输入的值不太好处理
        /// </summary>
    public class BardCodeHooK
    {
        //委托
        public delegate void BardCodeDeletegate(BarCodes barCode);
        public event BardCodeDeletegate BarCodeEvent;

    



        //定义成静态，这样不会抛出回收异常
        private static HookProc hookproc;


        public struct BarCodes
        {
            public int VirtKey;//虚拟码
            public int ScanCode;//扫描码
            public string KeyName;//键名
            public uint Ascll;//Ascll
            public char Chr;//字符
            public string OriginalChrs; //原始 字符
            public string OriginalAsciis;//原始 ASCII


            public string OriginalBarCode;  //原始数据条码

            public string BarCode;//条码信息 保存最终的条码
            public bool IsValid;//条码是否有效
            public DateTime Time;//扫描时间,
        }

        private struct EventMsg
        {
            public int message;
            public int paramL;
            public int paramH;
            public int Time;
            public int hwnd;
        }

        //设置钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        //卸载钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern bool UnhookWindowsHookEx(int idHook);
        //调用下一个钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);

        [DllImport("user32", EntryPoint = "GetKeyNameText")]
        private static extern int GetKeyNameText(int IParam, StringBuilder lpBuffer, int nSize);

        [DllImport("user32", EntryPoint = "GetKeyboardState")]
        private static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("user32", EntryPoint = "ToAscii")]
        private static extern bool ToAscii(int VirtualKey, int ScanCode, byte[] lpKeySate, ref uint lpChar, int uFlags);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string name);

        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();




        delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);
        BarCodes barCode = new BarCodes();
        int hKeyboardHook = 0;
        //禁用键盘
        //public const int WWH_KEYBOARD_LL = 13;
        ////LowLevel键盘截获，如果是WH_KEYBOARD＝2，并不能对系统键盘截取，Acrobat Reader会在你截取之前获得键盘。
        //HookProc KeyKeyBoardHookProcedure;
     
        

        string strBarCode = "";
        StringBuilder sbBarCode = new StringBuilder();

        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            #region 获取普通条码扫描


            if (nCode == 0)
            {
                EventMsg msg = (EventMsg)Marshal.PtrToStructure(lParam, typeof(EventMsg));
                if (wParam == 0x100)//WM_KEYDOWN=0x100 
                {
                    barCode.VirtKey = msg.message & 0xff;//虚拟码
                    barCode.ScanCode = msg.paramL & 0xff;//扫描码
                    StringBuilder strKeyName = new StringBuilder(225);
                    if (GetKeyNameText(barCode.ScanCode * 65536, strKeyName, 255) > 0)
                    {
                        barCode.KeyName = strKeyName.ToString().Trim(new char[] { ' ', '\0' });
                    }
                    else
                    {
                        barCode.KeyName = "";
                    }
                    byte[] kbArray = new byte[1024];
                    uint uKey = 0;
                    GetKeyboardState(kbArray);

                    if (ToAscii(barCode.VirtKey, barCode.ScanCode, kbArray, ref uKey, 0))
                    {
                        barCode.Ascll = uKey;

                        barCode.Chr = Convert.ToChar(uKey);
                        string ss = "";

                      //  MessageBox.Show(Convert.ToChar(uKey).ToString());
                        ss = barCode.Chr.ToString();
                    }

                    TimeSpan ts = DateTime.Now.Subtract(barCode.Time);

                    if (ts.TotalMilliseconds > 50)
                    {
                        //时间戳，大于50 毫秒表示手动输入
                        strBarCode = barCode.Chr.ToString();
                        sbBarCode.Remove(0, sbBarCode.Length);
                        sbBarCode.Append(barCode.Chr.ToString());
                        barCode.OriginalChrs = " ";
                        Convert.ToString(barCode.Chr);
                        barCode.OriginalAsciis = " ";
                        Convert.ToString(barCode.Ascll);
                        barCode.BarCode = sbBarCode.ToString();
                        barCode.OriginalBarCode = Convert.ToString(barCode.Chr);
                        barCode.IsValid = false;//判断是手动还是自动输入的
                        

                       // MessageBox.Show(strBarCode);

                    }
                    else
                    {
                        if (barCode.Chr.ToString() != " ")
                        {

                            sbBarCode.Append(barCode.Chr.ToString());

                            strBarCode += barCode.Chr.ToString();
                        }
                     //   MessageBox.Show(strBarCode);
                        if ((msg.message & 0xff) == 13 && sbBarCode.Length > 3)
                        {//回车
                            strBarCode = strBarCode.Remove(12, 1);
                            strBarCode= strBarCode.Remove(17, 1);
                            MessageBox.Show(strBarCode);
                            barCode.BarCode = strBarCode;
                            //barCode.BarCode = strBarCode.ToString();// barCode.OriginalBarCode;
                            barCode.IsValid = true;
                            strBarCode = "";
                            sbBarCode.Remove(0, sbBarCode.Length);
                        }
                        // strBarCode = barCode.Chr.ToString();
                    }
                    barCode.Time = DateTime.Now;
                    try
                    {
                        if (BarCodeEvent != null && barCode.IsValid)
                        {

                            AsyncCallback callback = new AsyncCallback(AsyncBack);
                            //object obj;
                            Delegate[] delArray = BarCodeEvent.GetInvocationList();
                            //foreach (Delegate del in delArray)
                            foreach (BardCodeDeletegate del in delArray)
                            {
                                try
                                {
                                    //方法1
                                    //obj = del.DynamicInvoke(barCode)
                                    //方法2
                                    del.BeginInvoke(barCode, callback, del);//异步调用防止界面卡死
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                            //BarCodeEvent(barCode);//触发事件
                            barCode.BarCode = "";
                            barCode.OriginalChrs = "";
                            barCode.OriginalAsciis = "";
                            barCode.OriginalBarCode = "";
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        barCode.IsValid = false; //最后一定要 设置barCode无效
                        barCode.Time = DateTime.Now;
                    }
                }
            }
            #endregion
# region 键盘


            //if (nCode >= 0)
            //{
            //    KeyBoardHookStruct kbh = (KeyBoardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyBoardHookStruct));
            //    // MessageBox.Show(kbh.vkCode.ToString());

            //    if (kbh.vkCode != 32)
            //    {
            //        return 1;
            //    }
            //}
#endregion
            return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
        }

        //异步返回方法
        public void AsyncBack(IAsyncResult ar)
        {
            BardCodeDeletegate del = ar.AsyncState as BardCodeDeletegate;
            del.EndInvoke(ar);
        }

        //安装钩子
        public bool Start()
        {
            if (hKeyboardHook == 0)
            {
                hookproc = new HookProc(KeyboardHookProc);


                //GetModuleHandle 函数 替代 Marshal.GetHINSTANCE
                //防止在 framework4.0中 注册钩子不成功
                IntPtr modulePtr = GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName);

                //WH_KEYBOARD_LL=13
                //全局钩子 WH_KEYBOARD_LL
                //  hKeyboardHook = SetWindowsHookEx(13, hookproc, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);

                //hKeyboardHook = SetWindowsHookEx(13, hookproc, modulePtr, 0);
                hKeyboardHook = SetWindowsHookEx(13, hookproc, modulePtr, 0);
            }
            return (hKeyboardHook != 0);
        }

        //卸载钩子
        public bool Stop()
        {
            if (hKeyboardHook != 0)
            {
                bool returnValue = UnhookWindowsHookEx(hKeyboardHook);
                hKeyboardHook = 0;
                return returnValue;
            }

            return true;
        }
    }
    //键盘Hoo结构函数
    [StructLayout(LayoutKind.Sequential)]
    public class KeyBoardHookStruct
    {
        public int vkCode;
        public int scanCode;
        public int flags;
        public int time;
        public int dwExtraInfo;
    }

}
