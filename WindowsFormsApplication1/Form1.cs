using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Frustrating that VS is by default cutting off the bottom. Weird.
            this.Height = 544;
        }

        private void btnWasClicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            // First we assume that they clicked a number
            string output = "";
            bool is_int = false;
            bool is_operator = false;
            try{
                int num = Convert.ToInt16( btn.Text );
                is_int = true;
                output = btn.Text;
            }
            catch (FormatException error)
            {

            }
            if (this.is_operator(btn.Text))
            {
                is_operator = true;
                output = " " + btn.Text + " ";
            }

            updateDisplay(output);

        }

        private void updateDisplay( string update, Boolean replace = false )
        {
            if ( display.Text == "0" || replace)
            {
                display.Text = update;
            }
            else
            {
                display.Text += update;
            }
            
        }
        private void addToScreenClicked(object sender, EventArgs e)
        {

        }

        public bool is_operator(string thing)
        {
            string[] operands = new String[] { "+", "-", "^", "X", "/" };
            return operands.Contains(thing);
        }

    }
}
