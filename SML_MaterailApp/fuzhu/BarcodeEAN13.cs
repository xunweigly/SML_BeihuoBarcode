  using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;

namespace LKU8.shoukuan.fuzhu
{
    
  
        public class EAN13
        {
            private DataTable m_EAN13 = new DataTable();
            public EAN13()
            {
                m_EAN13.Columns.Add("ID");
                m_EAN13.Columns.Add("Type");
                m_EAN13.Columns.Add("A");
                m_EAN13.Columns.Add("B");
                m_EAN13.Columns.Add("C");
                m_EAN13.Rows.Add("0", "AAAAAA", "0001101", "0100111", "1110010");
                m_EAN13.Rows.Add("1", "AABABB", "0011001", "0110011", "1100110");
                m_EAN13.Rows.Add("2", "AABBAB", "0010011", "0011011", "1101100");
                m_EAN13.Rows.Add("3", "AABBBA", "0111101", "0100001", "1000010");
                m_EAN13.Rows.Add("4", "ABAABB", "0100011", "0011101", "1011100");
                m_EAN13.Rows.Add("5", "ABBAAB", "0110001", "0111001", "1001110");
                m_EAN13.Rows.Add("6", "ABBBAA", "0101111", "0000101", "1010000");
                m_EAN13.Rows.Add("7", "ABABAB", "0111011", "0010001", "1000100");
                m_EAN13.Rows.Add("8", "ABABBA", "0110111", "0001001", "1001000");
                m_EAN13.Rows.Add("9", "ABBABA", "0001011", "0010111", "1110100");
            }
            private uint m_Height = 40;
            /// <summary>
            /// 绘制高
            /// </summary>
            public uint Heigth { get { return m_Height; } set { m_Height = value; } }
            private byte m_FontSize = 0;
            /// <summary>
            /// 字体大小（宋体）
            /// </summary>
            public byte FontSize { get { return m_FontSize; } set { m_FontSize = value; } } 
          
            private byte m_Magnify = 0;
            /// <summary>     
            /// 放大系数    
            /// </summary>
            public byte Magnify { get { return m_Magnify; } set { m_Magnify = value; } }
            public Bitmap GetCodeImage(string p_Text)
            {
                if (p_Text.Length != 13) throw new Exception("数字不是13位!");
                string _CodeText = p_Text.Remove(0, 1);
                string _CodeIndex = "101";
                char[] _LeftType = GetValue(p_Text.Substring(0, 1), "Type").ToCharArray();
                for (int i = 0; i != 6; i++)
                {
                    _CodeIndex += GetValue(_CodeText.Substring(0, 1), _LeftType[i].ToString());
                    _CodeText = _CodeText.Remove(0, 1);
                }
                
                _CodeIndex += "01010";
                
                for (int i = 0; i != 6; i++)
                {
                    _CodeIndex += GetValue(_CodeText.Substring(0, 1), "C");
                    _CodeText = _CodeText.Remove(0, 1);
                }          
                _CodeIndex += "101";
                return GetImage(_CodeIndex, p_Text);
            }
            /// <summary>
            /// 获取目标对应的数据
            /// </summary>
            /// <param name="p_Code">编码</param>
            /// <param name="p_Value">类型</param>        
            /// <returns>编码</returns>
            private string GetValue(string p_Value, string p_Type)
            {
                if (m_EAN13 == null) return "";
                DataRow[] _Row = m_EAN13.Select("ID='" + p_Value + "'");
                if (_Row.Length != 1) throw new Exception("错误的编码" + p_Value.ToString());
                return _Row[0][p_Type].ToString();
            }
            /// <summary>
            /// 绘制编码图形
            /// </summary>
            /// <param name="p_Text">编码</param>
            /// <returns>图形</returns>
            private Bitmap GetImage(string p_Text,string p_ViewText)
            {
                char[] _Value = p_Text.ToCharArray();
                int _FontWidth = 0;
                Font _MyFont = null;
                if (m_FontSize != 0)
                {
                    #region 获取符合大小的字体
                    _MyFont = new Font("Arial", m_FontSize);
                    Bitmap _MyFontBmp = new Bitmap(m_FontSize, m_FontSize);
                    Graphics _FontGraphics = Graphics.FromImage(_MyFontBmp);


                    SizeF _DrawSize = _FontGraphics.MeasureString(p_ViewText.Substring(0, 1), _MyFont);
                    _FontWidth = (int)_DrawSize.Width;
                    //修改font

                    //_MyFont = new Font("Arial", 16);
                    //for (byte i = m_FontSize; i != 0; i--)
                    //{
                    //    SizeF _DrawSize = _FontGraphics.MeasureString(p_ViewText.Substring(0, 1), _MyFont);
                    //    if (_DrawSize.Height > m_FontSize)
                    //    {
                    //        _MyFont = new Font("宋体", i);
                            
                    //    }
                    //    else
                    //    {
                    //        _FontWidth = (int)_DrawSize.Width;
                    //        break;
                    //    }
                    //}
                    #endregion
                }
                //if (ScanDrawText(_MyFont, p_Text, _FontWidth) == false)
                //{
                //    _FontWidth = 0;
                //    m_FontSize = 0;
                //}
                //宽 == 需要绘制的数量*放大倍数 + 两个字的宽   ,高+ 一个字符
                Bitmap _CodeImage = new Bitmap(_Value.Length * ((int)m_Magnify + 1) + (_FontWidth * 2), (int)m_Height );
                Graphics _Garphics = Graphics.FromImage(_CodeImage);
                _Garphics.FillRectangle(Brushes.White, new Rectangle(0, 0, _CodeImage.Width, _CodeImage.Height));                
              
                int _Height = 0;
                int _LenEx = _FontWidth;
                for (int i = 0; i != _Value.Length; i++)
                {
                    int _DrawWidth = m_Magnify + 1;
                    if (i == 0 || i == 2 || i==46 || i==48 || i==92 || i==94)
                    {
                        _Height = (int)m_Height;                  
                    }
                    else
                    {
                        _Height = (int)m_Height - _MyFont.Height;
                    }
                    if (_Value[i] == '1')
                    {
                        _Garphics.FillRectangle(Brushes.Black, new Rectangle(_LenEx, 0, _DrawWidth, _Height));
                      
                    }
                    else
                    {
                        _Garphics.FillRectangle(Brushes.White, new Rectangle(_LenEx, 0, _DrawWidth, _Height));
                    }
                    _LenEx += _DrawWidth;              
                }
                //绘制文字
                if (_FontWidth != 0 && m_FontSize != 0)
                {
                    int _StarX = 0;
                    int _StarY=(int)m_Height - _MyFont.Height;

                    //_Garphics.DrawString(p_ViewText.Substring(0, 1), new Font("Arial", 16), Brushes.Black, 0, _StarY);
                    _Garphics.DrawString(p_ViewText.Substring(0, 1), _MyFont, Brushes.Black, 0, _StarY);
                    _StarX = _FontWidth + (3 * (m_Magnify + 1));
                    //_Garphics.DrawString(p_ViewText.Substring(1, 6), new Font("Arial", 16), Brushes.Black, _StarX, _StarY);
                    _Garphics.DrawString(p_ViewText.Substring(1, 6), _MyFont, Brushes.Black, _StarX, _StarY);
                    _StarX = _FontWidth + (50 * (m_Magnify + 1));
                    //_Garphics.DrawString(p_ViewText.Substring(7, 6), new Font("Arial", 16), Brushes.Black, _StarX, _StarY);
                    _Garphics.DrawString(p_ViewText.Substring(7, 6), _MyFont, Brushes.Black, _StarX, _StarY);
                }
                _Garphics.Dispose();
                return _CodeImage;
            }
            /// <summary>
            /// 判断字体是否大与绘制图形
            /// </summary>
            /// <param name="_MyFont">字体</param>
            /// <param name="p_Text">文字</param>
            /// <param name="p_Width">字体的宽</param>
            /// <returns>true可以绘制 False不可以绘制</returns>
            private bool ScanDrawText(Font _MyFont,string p_Text,int p_Width)
            {
                if(_MyFont==null)return false;
                int _Width = (p_Text.Length - 6 - 5) * ((int)m_Magnify + 1);
                if ((p_Width*12) > _Width) return false;
                return true;
            }
            /// <summary>
            /// 获得条码的最后一位（验证位）
            /// </summary>
            /// <param name="ANumbers">条码</param>
            /// <returns></returns>
            public static char EAN13ISBN(string _Numb)
            {
                int _Sum = 0; 
                int _i = 1;   //权值
                foreach (char _Char in _Numb)
                {
                    if ("0123456789".IndexOf(_Char) < 0) continue; // 非数字
                    _Sum += (_Char - '0') * _i;
                    _i = _i == 1 ? 3 : 1; 
                }
                return "01234567890"[10 - _Sum % 10];
            }
         
        }
    }

   

