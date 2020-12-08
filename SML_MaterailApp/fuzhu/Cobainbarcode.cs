using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Cobainsoft.Windows.Forms;
using System.Drawing.Imaging;

namespace fuzhu
{
    class Cobainbarcode
    {
        public static byte[] GetBarCode(string txt,BarcodeType type,BarcodeTextPosition txtpos)
        {
            string path = Application.StartupPath + "\\log.bmp";
            string strtxm = txt;
            FileStream filestr = new FileStream(path, FileMode.Create);
            Cobainsoft.Windows.Forms.BarcodeControl control = new BarcodeControl();//实例化
            control.BarcodeType =type;                            //启用的编码
            control.Data = strtxm;//生成编码的字符串
            control.StretchText = false;
            control.CopyRight = "";//显示标题
            control.TextPosition = txtpos;//显示位置，Above，NotShown，Below
            filestr.Close();//关闭文件
            control.SaveImage(ImageFormat.Bmp, 1, 90, true, false, null, path);

            byte[] imgs = SetImageToByteArray(path);
            if (System.IO.File.Exists(path))
            {
                try
                {
                    System.IO.File.Delete(path);
                }
                catch
                {
                    MessageBox.Show("缺少log.bmp文件");

                }
            }
            return imgs;

        }
        private static byte[] SetImageToByteArray(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            int streamLength = (int)fs.Length;
            byte[] image = new byte[streamLength];
            fs.Read(image, 0, streamLength);
            fs.Close();
            return image;
        }

    }
}
