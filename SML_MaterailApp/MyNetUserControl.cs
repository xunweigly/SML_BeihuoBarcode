using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UFIDA.U8.Portal.Proxy.editors;
using UFIDA.U8.Portal.Framework.MainFrames;
using UFIDA.U8.Portal.Framework.Actions;
using UFIDA.U8.Portal.Proxy.Actions;

namespace LKU8.shoukuan
{
    class MyNetUserControl : INetUserControl
    {
        #region INetUserControl 成员

        UserControl1 usercontrol = null;
        //private IEditorInput _editInput = null;
        //private IEditorPart _editPart = null;
        private string _title;
        public System.Windows.Forms.Control CreateControl(UFSoft.U8.Framework.Login.UI.clsLogin login, string MenuID, string Paramters)
        {
            usercontrol = new UserControl1();
            usercontrol.Name = "sddddddddd";
            return usercontrol;
            //throw new NotImplementedException();
        }

        public UFIDA.U8.Portal.Proxy.Actions.NetAction[] CreateToolbar(UFSoft.U8.Framework.Login.UI.clsLogin login)
        {
            //IActionDelegate nsd = new NetSampleDelegate();
            ////string skey = "mynewcontrol";
            //NetAction[] aclist;
            //aclist = new NetAction[4];

            //NetAction ac = new NetAction("add", nsd);
            //ac.Text = "增加";
            //ac.Style = 1;
            //ac.SetGroup = "edit";
            ////设置组行数
            //ac.SetGroupRow = 1;
            ////设置图标大小，3最大
            //ac.RowSpan = 3;
            ////用间隔线隔开
            //ac.Catalog = "234";
            ////ac.Image = "返回.ico";
            //ac.Tag = usercontrol;
            ////ac.ActionType = UFIDA.U8.Portal.Proxy.Actions.NetAction.NetActionType(1);
            //aclist[0] = ac;

            //ac = new NetAction("query", nsd);
            //ac.Text = "查询";
            //ac.Style = 3;
            //ac.SetGroup = "edit1";
            //ac.Tag = usercontrol;
            //aclist[1] = ac;

            //ac = new NetAction("query2", nsd);
            //ac.Text = "查询2";
            //ac.SetGroup = "edit2";
            //ac.Tag = usercontrol;
            //aclist[2] = ac;

            //ac = new NetAction("query3", nsd);
            //ac.Text = "查询3";
            //ac.SetGroup = "edit3";
            //ac.Tag = usercontrol;
            //aclist[3] = ac;
            //return aclist;
            return null;
            //throw new NotImplementedException();
        }
        public bool CloseEvent()
        {
            //throw new Exception("The method or operation is not implemented.");
            return true;
        }
        #endregion



        IEditorInput INetUserControl.EditorInput
        {
            get;
            set;
        }

        IEditorPart INetUserControl.EditorPart
        {
            get;set;

        }

        string INetUserControl.Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }
       


    }


}
