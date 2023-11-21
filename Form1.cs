using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Calculator : Form
    {

        private const string NumberErrorMessage = "Should be a number";

        public enum Operation
        {
            Addition = 0,
            Subtraction = 1,
            Multiplication = 2,
            Division = 3
        }
        public Calculator()
        {
            InitializeComponent();

            operationsList.DataSource = Enum.GetValues(typeof(Operation));
            EnableDisableCalculateButton();
        }

        private void operand1_TextChanged(object sender, EventArgs e)
        {

            errorProvider.SetError(operand1, null);
            EnableDisableCalculateButton();

        }

        private void operand2_TextChanged(object sender, EventArgs e)
        {

            errorProvider.SetError(operand2, null);
            EnableDisableCalculateButton();
        }

        private void operand1_Validating(object sender, CancelEventArgs e)
        {
            TextBox textbox = (TextBox)sender;

            if (!double.TryParse(textbox.Text, out _))
            {
                errorProvider.SetError(operand1, NumberErrorMessage);
            }
            else
            {
                errorProvider.SetError(operand1, null);
            }

            EnableDisableCalculateButton();
        }

        private void operand2_Validating(object sender, CancelEventArgs e)
        {
            TextBox textbox = (TextBox)sender;

            if (!double.TryParse(textbox.Text, out _))
            {
                errorProvider.SetError(operand2, NumberErrorMessage);
            }
            else
            {
                errorProvider.SetError(operand2, null);
            }

            EnableDisableCalculateButton();
        }

        private void operand2_Enter(object sender, EventArgs e)
        {
            errorProvider.SetError(operand2, null);
        }

        private void operand1_Enter(object sender, EventArgs e)
        {
            errorProvider.SetError(operand1, null);
        }

        private void operationsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableDisableCalculateButton();
        }

        private void EnableDisableCalculateButton()
        {
            string op1 = operand1.Text.Replace(",", ".");
            string op2 = operand2.Text.Replace(",", ".");

            bool isValidOperand1 = double.TryParse(op1, out _);
            bool isValidOperand2 = double.TryParse(op2, out _);
            bool isOperationSelected = operationsList.SelectedIndex != -1;

            calculate.Enabled = isValidOperand1 && isValidOperand2 && isOperationSelected;
            resultLabel.Text = string.Empty;

        }

        private void calculate_Click(object sender, EventArgs e)
        {
            if (operationsList.SelectedItem != null)
            {
                Operation selectedOperation = (Operation)operationsList.SelectedItem;

                string op1 = operand1.Text.Replace(",", ".");
                string op2 = operand2.Text.Replace(",", ".");

                double num1 = double.Parse(op1);
                double num2 = double.Parse(op2);

                double result = 0;

                switch (selectedOperation)
                {
                    case Operation.Addition:
                        result = num1 + num2;
                        break;

                    case Operation.Subtraction:
                        result = num1 - num2;
                        break;

                    case Operation.Multiplication:
                        result = num1 * num2;
                        break;

                    case Operation.Division:
                        if (num2 != 0)
                        {
                            result = num1 / num2;
                        }
                        else
                        {
                            MessageBox.Show("Cannot divide by zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;

                    default:
                        MessageBox.Show("Unknown operation selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                result = Math.Round(result, 2);

                resultLabel.Text = $"Result: {result}";
            }
        }
    }
}
