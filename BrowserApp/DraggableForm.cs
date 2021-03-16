using System.Drawing;
using System.Windows.Forms;

namespace BrowserApp
{
    public class DraggableForm : Form
    {
        private bool drag = false; // determine if we should be moving the form
        private Point startPoint = new Point(0, 0); // also for the moving

        private Form form;

        

        public void init(Form form)
        {
            this.form = form;
            // form.FormBorderStyle = FormBorderStyle.None; // get rid of the standard title bar
            

            form.MouseDown += new MouseEventHandler(Title_MouseDown);
            form.MouseUp += new MouseEventHandler(Title_MouseUp);
            form.MouseMove += new MouseEventHandler(Title_MouseMove);     
        }
        
        
        void Title_MouseUp(object sender, MouseEventArgs e)
        {
            this.drag = false; 
        }
                
        void Title_MouseDown(object sender, MouseEventArgs e)
        {
            this.startPoint = e.Location;
            this.drag = true;
        }
                
        void Title_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.drag) 
            { 
                Point p1 = new Point(e.X, e.Y);
                Point p2 = form.PointToScreen(p1);
                Point p3 = new Point(p2.X - this.startPoint.X, 
                    p2.Y - this.startPoint.Y);
                form.Location = p3;
            }
        }
        
    }
}