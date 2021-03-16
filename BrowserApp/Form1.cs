using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrowserApp
{
    public partial class Form1 : DraggableForm
    {
        public int tabIndex = 0;
        public List<string> bookmarks = new List<string>();
        public Form1()
        {
            InitializeComponent();
            base.init(this);
            

        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            tabControllBar1.setForm(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WebBrowser webBrowser = new WebBrowser();
            webBrowser.Visible = true;
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.Dock = DockStyle.Fill;
            webBrowser.DocumentCompleted += WebBrowser_DocumentCompleted;
            webBrowser.Navigate("google.com");

            //Adding new tab
            tabControl1.TabPages.Add("New Page");
            tabControl1.SelectTab(tabIndex);
            tabControl1.SelectedTab.Controls.Add(webBrowser);
            tabControl1.SelectedTab.ToolTipText = "Google";
            tabIndex++;
        }

        public void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            tabControl1.SelectedTab.Text = ((WebBrowser) tabControl1.SelectedTab.Controls[0]).DocumentTitle;
        }
        

        //Close
        private void toolStripButton3_Click(object sender, EventArgs e)
        {   
            Application.Exit();
        }

        //Minimalize/Maximalize
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
