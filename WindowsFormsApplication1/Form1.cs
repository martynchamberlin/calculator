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
        private Timer timer = new Timer();
        private bool userIsPressingDelete = false;

        public Form1()
        {
            InitializeComponent();

            // Frustrating that VS is by default cutting off the bottom. Weird.
            this.Height = 544;
            // Give dropdowns some context
            updateDateDropdownsToProperDefaults();
        }

        private void btnWasClicked(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            // First we assume that they clicked a number
            string output = "";
            try
            {
                int num = Convert.ToInt16(btn.Text);
                output = btn.Text;
            }
            catch (FormatException error)
            {

            }
            if (this.is_operator(btn.Text))
            {
                output = " " + btn.Text + " ";
            }

            updateDisplay(output);

        }

        private void updateDisplay(string update, Boolean replace = false)
        {
            if (display.Text == "0" || replace)
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
            updateDisplay(Convert.ToString(numericUpDown1.Value));
        }

        public bool is_operator(string thing)
        {
            string[] operands = new String[] { "+", "-", "^", "*", "/" };
            return operands.Contains(thing);
        }

        private void calculateBtn(object sender, EventArgs e)
        {
            String input = display.Text;
            // if they're doing an exponent then we only let them have two operands.
            // any more than that, and they get an error message
            if (input.Contains('^'))
            {
                int index = input.IndexOf('^');
                try
                {
                    int num1 = Convert.ToInt32(input.Substring(0, index));
                    int num2 = Convert.ToInt32(input.Substring(index + 1));
                    display.Text = Convert.ToString(Math.Pow(num1, num2));
                }
                catch (Exception error)
                {
                    MessageBox.Show("An error occured! Please make sure that the input is valid");
                }
            }
            else
            {
                try
                {
                    var something = new DataTable().Compute(input, null);
                    display.Text = Convert.ToString(something);
                }
                catch (EvaluateException error)
                {
                    MessageBox.Show("An error occured! Please make sure that the input is valid");
                }
            }

        }


        public void deleteWasClicked(object sender, EventArgs e)
        {
            bool actually_deleted_something = false;
            // in case there's white space happening, we need recursion here
            while (display.Text.Length > 0)
            {
                string nextChar = display.Text.Substring(display.Text.Length - 1);
                if (nextChar != " ")
                {
                    if (actually_deleted_something)
                    {
                        break;
                    }
                    actually_deleted_something = true;
                }
                display.Text = display.Text.Substring(0, display.Text.Length - 1);
            }


            // this must not be an else statement, because both might be true 
            if (display.Text.Length == 0)
            {
                display.Text = "0";
            }
        }

        // When they press down the mouse, wait 3/4 of a second to see if they are
        // still holding down on it. If so, then execute a function 
        private void btnClear_MouseDown(object sender, MouseEventArgs e)
        {
            userIsPressingDelete = true;
            timer.Tick += new EventHandler(emptyOutput);
            timer.Interval = 750;
            timer.Enabled = true;
            timer.Start();
        }

        // When they press back up from the mouse, turn off the flag that's storing this
        // and then stop the timer
        private void btnClear_MouseUp(object sender, MouseEventArgs e)
        {
            timer.Stop();
        }

        // If the user is pressing down when this fires, clear the output
        void emptyOutput(object sender, EventArgs e)
        {
            if (userIsPressingDelete)
            {
                display.Text = "0";
            }
        }

        public String[] months()
        {
            String[] days = new String[12];
            for (int i = 1; i <= 12; i++)
            {
                days[i - 1] = i < 10 ? "0" + Convert.ToString(i) : Convert.ToString(i);
            }
            return days;
        }

        public String[] years()
        {
            return days();
        }

        public String[] days()
        {
            String[] days = new String[31];
            for ( int i = 1; i <= 31; i++)
            {
                days[i - 1] = i < 10 ? "0" + Convert.ToString(i) : Convert.ToString(i);
            }
            return days;
        }

        public String[] hours()
        {
            String[] hours = new String[12];
            for (int i = 1; i <= 12; i++)
            {
                hours[i - 1] = Convert.ToString(i);
                }
            return hours;
        }

        public String[] minutes()
        {
            String[] minutes = new String[60];
            for (int i = 0; i <= 59; i++)
            {
                minutes[i] = i < 10 ? "0" + Convert.ToString(i) : Convert.ToString(i);
            }
            return minutes;
        }

        private void updateDateDropdownsToProperDefaults()
        {

            // Programically populate the dropdowns for dates
            this.startMinute.Items.AddRange(minutes());
            this.finishMinute.Items.AddRange(minutes());
            this.startHour.Items.AddRange(hours());
            this.finishHour.Items.AddRange(hours());
            this.startDay.Items.AddRange(days());
            this.finishDay.Items.AddRange(days());
            this.startMonth.Items.AddRange(months());
            this.finishMonth.Items.AddRange(months());
            this.startYear.Items.AddRange(years());
            this.finishYear.Items.AddRange(years());

            // Now that they're autopopulated, let's predefine what the default selected 
            // option is for each dropdown
            DateTime current = DateTime.Now;
            int year = current.Year;
            int month = current.Month;
            int day = current.Day;
            int hour = current.Hour;
            int minute = current.Minute;

            // Frustrating that VS insists on 0-23 hours time. Must rememdy this with code...
            String ampm = hour >= 12 ? "P.M." : "A.M";
            hour = hour == 0 ? 12 : hour % 12;

            startYear.SelectedItem = (year < 10 ? "0" : "") + Convert.ToString(year).Substring(2);
            startMonth.SelectedItem = (month < 10 ? "0" : "") + Convert.ToString(month);
            startDay.SelectedItem = (day < 10 ? "0" : "" ) + Convert.ToString(day);
            startHour.SelectedItem = Convert.ToString(hour);
            startMinute.SelectedItem = (minute < 10 ? "0" : "" ) + Convert.ToString(minute);
            startTime.SelectedItem = ampm;

            TimeSpan span = new TimeSpan( 0, 5, 0, 0);
            DateTime future = DateTime.Now.Add(span);
            year = future.Year;
            month = future.Month;
            day = future.Day;
            hour = future.Hour;
            minute = future.Minute;

            ampm = hour >= 12 ? "P.M." : "A.M";
            hour = hour == 0 ? 12 : hour % 12;

            finishYear.SelectedItem = (year < 10 ? "0" : "") + Convert.ToString(year).Substring(2);
            finishMonth.SelectedItem = (month < 10 ? "0" : "") + Convert.ToString(month);
            finishDay.SelectedItem = (day < 10 ? "0" : "") + Convert.ToString(day);
            finishHour.SelectedItem = Convert.ToString(hour);
            finishMinute.SelectedItem = (minute < 10 ? "0" : "") + Convert.ToString(minute);
            finishTime.SelectedItem = ampm;
        }

        private void calculateTime_Click(object sender, EventArgs e)
        {
            //int startYear = 
            //DateTime start = new DateTime( )
        }

    }
}
