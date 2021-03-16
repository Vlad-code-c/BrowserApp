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
    public partial class TabControllBar : UserControl
    {
        Form1 form;

        public TabControllBar()
        {
            InitializeComponent();

            toolStripTextBox2.KeyDown += new KeyEventHandler(toolStripTextBox2_Edited);
        }

        public void setForm(Form1 form)
        {
            this.form = form;
            addBookmarksFromDb(DatabaseControll.getBookmarks());
        }

        //Plus
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            addTab();
        }
        
        //Back
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ((WebBrowser)form.tabControl1.SelectedTab.Controls[0]).GoBack();
            canGo();
        }
        
        //Forward
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            ((WebBrowser)form.tabControl1.SelectedTab.Controls[0]).GoForward();
            canGo();
        }

        //Refresh
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            ((WebBrowser)form.tabControl1.SelectedTab.Controls[0]).Refresh();
        }

        //Bookmarks
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            string url = ((WebBrowser) form.tabControl1.SelectedTab.Controls[0]).Url.ToString();
            
            form.bookmarks.Add(((WebBrowser) form.tabControl1.SelectedTab.Controls[0]).DocumentTitle);
            bookmarksToolStripMenuItem.DropDown.Items.Add(url);
            
            bookmarksToolStripMenuItem.DropDown.Items[bookmarksToolStripMenuItem.DropDown.Items.Count - 1].Click +=
                (o, args) => { addCustomTab(url); };
            
            DatabaseControll.addBookmark(url);
        }

        //Url textBox
        private void toolStripTextBox2_Edited(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                addTab();    
            }
        }
        
        //Minus
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            removeTab();
        }




        private void addTab()
        {
            string url = "google.com";
            string title = "Google";
            
            if (!toolStripTextBox2.Text.Equals(""))
            {
                if (toolStripTextBox2.Text.Contains("."))
                {
                    url = toolStripTextBox2.Text;
                    title = url.Split('.')[0];
                }
                else
                {
                    url = "google.com/search?q=" + toolStripTextBox2.Text.Replace(" ", "+");
                    title = toolStripTextBox2.Text;
                }
            }
            
            WebBrowser webBrowser = new WebBrowser();
            webBrowser.Visible = true;
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.Dock = DockStyle.Fill;
            webBrowser.DocumentCompleted += form.WebBrowser_DocumentCompleted;
            webBrowser.Navigate(url);

            //Adding new tab
            form.tabControl1.TabPages.Add(title);
            form.tabControl1.SelectTab(form.tabIndex);
            form.tabControl1.SelectedTab.Controls.Add(webBrowser);
            form.tabIndex++;
            
            
            canGo();
        }

        private void addCustomTab(string url)
        {
            WebBrowser webBrowser = new WebBrowser();
            webBrowser.Visible = true;
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.Dock = DockStyle.Fill;
            webBrowser.DocumentCompleted += form.WebBrowser_DocumentCompleted;
            webBrowser.Navigate(url);

            //Adding new tab
            form.tabControl1.TabPages.Add("New Page");
            form.tabControl1.SelectTab(form.tabIndex);
            form.tabControl1.SelectedTab.Controls.Add(webBrowser);
            form.tabIndex++;
            
            
            canGo();
        }

        private void removeTab()
        {
            if (form.tabControl1.TabPages.Count > 1)
            {
                form.tabControl1.TabPages.RemoveAt(form.tabControl1.SelectedIndex);
                form.tabIndex--;
                form.tabControl1.SelectTab(form.tabControl1.TabPages.Count - 1);
            }
            else
            {
                Application.Exit();
            }
        }

        private void canGo()
        {
            //TODO: Finish
            return;
            
            //if can't go forward
            if (!((WebBrowser) form.tabControl1.SelectedTab.Controls[0]).CanGoForward)
            {
                toolStripButton6.Enabled = false;
            }
            else
            {
                toolStripButton6.Enabled = true;
            }
            
            //if can't go back
            if (!((WebBrowser) form.tabControl1.SelectedTab.Controls[0]).CanGoBack)
            {
                toolStripButton5.Enabled = false;
            }
            else
            {
                toolStripButton6.Enabled = true;
            }
        }


        private void addBookmarksFromDb(List<string> bookmarks)
        {
            foreach (string url in bookmarks)
            {
                form.bookmarks.Add(((WebBrowser) form.tabControl1.SelectedTab.Controls[0]).DocumentTitle);
                bookmarksToolStripMenuItem.DropDown.Items.Add(url);
                
                bookmarksToolStripMenuItem.DropDown.Items[bookmarksToolStripMenuItem.DropDown.Items.Count - 1].Click +=
                    (o, args) => { addCustomTab(url); };
            }
        }
        
        
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
