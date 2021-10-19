using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Calculator1
{
    public partial class Form1 : Form
    {
        //Variablendefinition
        double firstNumber;
        bool clearTextboxOnNextNumber = false;
        bool calculationPerformed = false;
        bool operationPerformed = false;//
        string operatorsign = "";

        //Die Entleerung der Textboxes für die Eingabe der nächsten Zahl
        void emptyTextBox()
        {
            if (TextBox.Text == "0" || (clearTextboxOnNextNumber))
            {
                TextBox.Text = String.Empty;
                clearTextboxOnNextNumber = false;
            }
        }

        //=================================================================================================

        //Die Initialisierung des Forms
        public Form1()
        {
            InitializeComponent();
            TextBox.Text = "0";
        }

        //=================================================================================================

        //Funktion für die Eingabe der Zahlen
        void setNumber(string c_input)
        {
            emptyTextBox();
            TextBox.Text = TextBox.Text + c_input;
            displaySavedInput.Focus();
            calculationPerformed = false;
            operationPerformed = false;
        }

        //=================================================================================================

        void processCalculation(string c_operatorSign)
        {
            if (!calculationPerformed)
            {
                calculate();
            }
            if (!operationPerformed)//
            {
                firstNumber = Convert.ToDouble(TextBox.Text);
            }
            else
            {
                TextBox.Text = firstNumber.ToString();
                firstNumber = Convert.ToDouble(TextBox.Text);
            }

            operatorsign = c_operatorSign;
            displaySavedInput.Text = firstNumber + " " + operatorsign;
            clearTextboxOnNextNumber = true;
            operationPerformed = true;//
        }

        //=================================================================================================

        //Funktion fürs Dezimalzeichen
        void setDecimal()
        {
            emptyTextBox();
            if (TextBox.Text.Contains(","))
            {
                return;
            }

            if ((operatorsign == "+" || operatorsign == "-" || operatorsign == "✕" || operatorsign == "÷" && TextBox.Text == "") || TextBox.Text == "")
            {
                TextBox.Text = "0,";
            }
            else
            {
                TextBox.Text = TextBox.Text + ",";
            }
        }

        //=================================================================================================

        //Funktion für den Clear Button 
        void clear()
        {
            TextBox.Text = String.Empty;
            displaySavedInput.Text = String.Empty;
            firstNumber = 0;
            operatorsign = String.Empty;
            clearTextboxOnNextNumber = false;
            calculationPerformed = false;
            operationPerformed = false;
            TextBox.Text = "0";
        }

        //=================================================================================================

        //Funktion für den Backspace (Braucht Verbesserung)
        void setBackspace()
        { 
                if((displaySavedInput.Text.EndsWith("+") || displaySavedInput.Text.EndsWith("-") || displaySavedInput.Text.EndsWith("✕") ||  displaySavedInput.Text.EndsWith("÷")) && operationPerformed )
                {
                    return;
                }
                if (TextBox.Text.Length == 1 || ( TextBox.Text.Length == 2 && TextBox.Text.StartsWith("-") ) )
                {
                    TextBox.Text = "0";
                }
                else
                {
                    TextBox.Text = TextBox.Text.Remove(TextBox.Text.Length - 1);
                }

                if (displaySavedInput.Text == "" || (operatorsign == "+" || operatorsign == "-" || operatorsign == "✕" || operatorsign == "÷"))
                {
                    return;
                }
                else
                {
                    displaySavedInput.Text = displaySavedInput.Text.Remove(displaySavedInput.Text.Length - 1);
                }
        }

        //=================================================================================================

        //Funktion fürs Gleichheitszeichen 
        void calculate()
        {
            if (operatorsign != "")
            { 
                double firstNumberLocal = firstNumber;
                double secondNumberLocal = Convert.ToDouble(TextBox.Text);

                if (calculationPerformed)
                {
                    double tempNumber = secondNumberLocal;
                    secondNumberLocal = firstNumberLocal;
                    firstNumberLocal = tempNumber;
                }

                if (operatorsign == "+" && operationPerformed == false)//
                {
                    TextBox.Text = (firstNumberLocal + secondNumberLocal).ToString();
                }
                else if (operatorsign == "-" && operationPerformed == false)//
                {
                    TextBox.Text = (firstNumberLocal - secondNumberLocal).ToString();
                }
                else if (operatorsign == "÷" && operationPerformed == false)//
                {
                    TextBox.Text = (firstNumberLocal / secondNumberLocal).ToString();
                }
                else if (operatorsign == "✕" && operationPerformed == false)//
                {
                    TextBox.Text = (firstNumberLocal * secondNumberLocal).ToString();
                }

                if ( ! calculationPerformed)
                {
                    firstNumber = secondNumberLocal;
                }

                displaySavedInput.Text = "";
                calculationPerformed = true;
            }
        }

        //=================================================================================================

        //Funktion fürs Plus/Minus-Zeichen
        void setPositiveNegativeSign()
        {
            operationPerformed = false;
            TextBox.Text = (Convert.ToDouble(TextBox.Text) * (-1)).ToString();   
        }

        //=================================================================================================

        //Buttons von 0 bis 9
        private void setNumberClick(object sender, EventArgs e)
        {
            setNumber(((Button)sender).Text); //Button clickedButton = (Button)sender;
        }

        //=================================================================================================

        private void clearClick(object sender, EventArgs e)
        {
            clear();
        }

        //=================================================================================================

        private void calculateClick(object sender, EventArgs e)
        {
            calculate();
            clearTextboxOnNextNumber = true;
        }

        //=================================================================================================

        private void multiplyClick(object sender, EventArgs e)
        {
            processCalculation("✕");
        }

        //=================================================================================================

        private void addClick(object sender, EventArgs e)
        {
            processCalculation("+");
        }

        //=================================================================================================

        private void subtractClick(object sender, EventArgs e)
        {
            processCalculation("-");
        }

        //=================================================================================================

        private void divideClick(object sender, EventArgs e)
        {
            processCalculation("÷");
        }

        //=================================================================================================

        private void setDecimalClick(object sender, EventArgs e)
        {
            setDecimal();
        }

        //=================================================================================================

        //Interaction of the keyboard with the calculator    
        private void pressKeyDown(object sender, KeyEventArgs e)
        {
     
            if (e.KeyCode == Keys.Divide)
            {
                processCalculation("÷");
            }
            else if (e.KeyCode == Keys.Add || e.KeyCode==Keys.Oemplus)
            {
                processCalculation("+");
            }
            else if (e.KeyCode == Keys.Subtract || e.KeyCode==Keys.OemMinus)
            {
                processCalculation("-");
            }
            else if (e.KeyCode == Keys.Multiply)
            {
                processCalculation("✕");
            }

            if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
            {
                setNumber((e.KeyValue - 96).ToString());  
            }
            else if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
            {  
                setNumber((e.KeyCode - Keys.D0).ToString());   
            }
            
            if (e.KeyCode == Keys.Enter)
            {
                calculate();
                clearTextboxOnNextNumber = true;
            }

            if (e.KeyCode == Keys.Delete)
            {
                clear();
            }

            if (e.KeyCode == Keys.Decimal || e.KeyCode==Keys.Oemcomma)
            {
                setDecimal(); 
            }

            if (e.KeyCode == Keys.Back)
            {
                setBackspace();
            }
        }

        //=================================================================================================

        private void setPositiveNegativeSignClick(object sender, EventArgs e)
        {
            setPositiveNegativeSign();
        }

        //=================================================================================================

        private void clearEntryClick(object sender, EventArgs e)
        {
            TextBox.Text = String.Empty; 
            if (TextBox.Text == "0")
            {
                return;
            }
            else
            {
                TextBox.Text = "0";
            }
        }

        //=================================================================================================

        private void setBackspaceClick(object sender, EventArgs e)
        {
            setBackspace();
        }

        //=================================================================================================

        private void loadForm(object sender, EventArgs e)
        {
            displaySavedInput.Select(); //Defocus button when the program starts
        }

        //=================================================================================================
    }
}

