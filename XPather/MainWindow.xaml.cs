using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace XPather
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClearXML_Click(object sender, RoutedEventArgs e)
        {
            txtxml.Clear();
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.PreserveWhitespace = true;

            string xPath = txtXPath.Text;

            if (string.IsNullOrWhiteSpace(xPath))
            {
                lblStatus.Text = "XPath cannot be blank";
                brdrStatus.ToolTip = "XPath cannot be blank";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtxml.Text))
            {
                lblStatus.Text = "XML cannot be blank";
                brdrStatus.ToolTip = "XML cannot be blank";
                return;
            }
            lblStatus.Text = "";
            brdrStatus.ToolTip = "";
            txtoutput.Text = "";

            try
            {
                xmlDocument.LoadXml(txtxml.Text);
            }
            catch (Exception ex)
            {
                string error = $"XML parsing error: {ex.Message}";
                lblStatus.Text = error;
                brdrStatus.ToolTip = error;
                return;
            }
            XmlNodeList outputNodes;

            try
            {
                outputNodes = xmlDocument.SelectNodes(xPath);
            }
            catch (Exception ex)
            {
                string error = $"XPath parsing error: {ex.Message}";
                lblStatus.Text = error;
                brdrStatus.ToolTip = error;
                return;
            }

            if(outputNodes.Count == 0)
            {
                txtoutput.Text = "No matching XML!";
                return;
            }
            // //strings[descendant::string[@name='OptimusTitle']]
            var xmlOut = new StringBuilder();

            foreach(XmlNode outputNode in outputNodes)
            {
                if(!string.IsNullOrWhiteSpace(outputNode.OuterXml))
                    xmlOut.Append(outputNode.OuterXml + Environment.NewLine);
                else
                    xmlOut.Append(outputNode.InnerXml + Environment.NewLine);
            }

            txtoutput.Text = xmlOut.ToString();
        }
    }
}
