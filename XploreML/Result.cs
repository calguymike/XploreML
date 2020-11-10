using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace XploreML
{
    public partial class Result : Form
    {
        public static float calVal1 = 12345;
        public static float calVal2 = 12345;
        public static float calVal3 = 12345;
        XmlDocument XMLDoc = new XmlDocument();
        public static string targetPath = @"C:\XploreML\";
        public static string TABpath = targetPath + "TAB";
        public static string XMLpath = targetPath + "XML";
        int[] cal1 = new int[27052];
        public static float gain1 = 1;
        public static int offset1 = 0;
        public static float gain2 = 1;
        public static int offset2 = 0;
        int ic = 1;

        public Result()
        {
            InitializeComponent();
            
        }

        private void Result_Load(object sender, EventArgs e)
        {
            populate();
            format_form();
        }

        void format_form()
        {
            //If Curve
            if (txtbx_Type.Text == "Curve")
            {
                txtbx_comp1.Visible = false;
                txtbx_comp2.Visible = false;
                txtbx_comp3.Visible = false;
                txtbx_name1.Visible = false;
                txtbx_name2.Visible = false;
                txtbx_name3.Visible = false;
                datagrid_Cur.Visible = true;
                this.AutoSize = true;
                               
            }
            //If Calibration
            else if (txtbx_Type.Text == "Calibration")
            {
                txtbx_comp1.Visible = false;
                txtbx_comp2.Visible = false;
                txtbx_comp3.Visible = false;
                txtbx_name1.Visible = false;
                txtbx_name2.Visible = false;
                txtbx_name3.Visible = false;
                datagrid_Cur.Visible = false;
                button1.Visible = true;
                this.AutoSize = true;
            }
            //If Measurement
            else if (txtbx_Type.Text == "Variable")
            {
                txtbx_comp1.Visible = false;
                txtbx_comp2.Visible = false;
                txtbx_comp3.Visible = false;
                txtbx_name1.Visible = false;
                txtbx_name2.Visible = false;
                txtbx_name3.Visible = false;
                datagrid_Cur.Visible = false;
                button1.Visible = false;
                this.AutoSize = true;
            }
        }


        public void populate()
        {
            txtbx_ResultName.Text = frm_search.FactoryName;
            txtbx_ResultDesc.Text = frm_search.DescL0;
            txtbx_ResultDesc2.Text = frm_search.DescL1;
            txtbx_ResultGroup.Text = frm_search.group;
            txtbx_ResultScaling.Text = frm_search.scaling;
            txtbx_resultMin.Text = frm_search.varMin;
            txtbx_resultMax.Text = frm_search.varMax;
            txtbx_Notes.Text = frm_search.notes;
            txtbx_Unit.Text = frm_search.unit;
            txtbx_Type.Text = frm_search.type;
            txtbx_Gain1.Text = frm_search.gain1.ToString();
            txtbx_Gain2.Text = frm_search.gain2.ToString();
            txtbx_Offs1.Text = frm_search.offset1.ToString();
            txtbx_Offs2.Text = frm_search.offset2.ToString();


            if (frm_search.type == "Calibration")
            {
                txtbx_ResultValue.Text = frm_search.calVal.ToString();
            }
            else if (frm_search.type == "Variable" || frm_search.type == "Curve")
            {
                txtbx_ResultValue.Text = "NA";
            }

            if (frm_search.type == "Curve")
            {
                Import_Curve();
            }


        }

        public void loadTAB()
        {
            
            string[] lines = System.IO.File.ReadAllLines(TABpath + ic + ".tab");
            int i = 0;
            foreach (string line in lines)
            {
                string[] words = line.Split(null);

                foreach (var word in words)
                {
                    if (word.Length >= 1)
                    {
                        cal1[i] = Int32.Parse(word);
                        i += 1;
                    }
                }
            }
        }

        private void Compare_Datasets_Cal()
        {
            string s = txtbx_ResultName.Text;

            while (ic <= 3)
            {
                if (File.Exists(XMLpath + ic + ".xml"))
                {
                    XMLDoc.Load(XMLpath + ic + ".xml");
                    
                    if (txtbx_Type.Text == "Calibration")
                    {
                        foreach (XmlElement xmlElement in XMLDoc.DocumentElement)
                        {
                            if (xmlElement.Name == "CalibrationValues")
                            {
                                string name = xmlElement.InnerText;
                                if (name.Contains(txtbx_ResultName.Text))
                                {
                                    loadTAB();
                                    int OffsetByte = Int32.Parse(xmlElement["OffsetByte"].InnerText);
                                    calVal1 = cal1[OffsetByte];
                                    scaleVal();

                                    if (!(calVal1 == 12345))
                                    {
                                        if (ic == 1)
                                        {
                                            (txtbx_comp1).Text = calVal1.ToString();
                                            txtbx_name1.Text = frm_FileSelection.ds1;
                                        }
                                        else if (ic == 2)
                                        {
                                            (txtbx_comp2).Text = calVal1.ToString();
                                            txtbx_name2.Text = frm_FileSelection.ds2;
                                        }
                                        else if (ic == 3)
                                        {
                                            (txtbx_comp3).Text = calVal1.ToString();
                                            txtbx_name3.Text = frm_FileSelection.ds3;
                                        }
                                        ic += 1;
                                        //this.Height = 500;
                                        txtbx_comp1.Visible = true;
                                        txtbx_comp2.Visible = true;
                                        txtbx_comp3.Visible = true;
                                        txtbx_name1.Visible = true;
                                        txtbx_name2.Visible = true;
                                        txtbx_name3.Visible = true;
                                        datagrid_Cur.Visible = false;
                                        this.AutoSize = true;

                                    }
                                    else
                                    {
                                        txtbx_comp1.Text = "NA";
                                        ic += 1;

                                    }
                                }
                            }
                            
                        }
                    }
                    else
                    {
                        break;
                    }


                    ic += 1;
                }

                else
                {
                    break;
                }

                //txtbx_comp1.Visible = true;
                //txtbx_comp2.Visible = true;
                //txtbx_comp3.Visible = true;
                //txtbx_name1.Visible = true;
                //txtbx_name2.Visible = true;
                //txtbx_name3.Visible = true;
                //datagrid_Cur.Visible = false;
                //this.AutoSize = true;



            }

        }

        private void Compare_Datasets_Cur()
        {

            MessageBox.Show("Tough look, this doesn't work yet");
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (txtbx_Type.Text == "Calibration")
            {
                ic = 1;
                Compare_Datasets_Cal();

                //if (!(txtbx_comp1.Text == txtbx_ResultValue.Text))
                //{
                //    txtbx_comp1.SelectionColor = Color.Red;
                //}

                //if (!(txtbx_comp2.Text == txtbx_ResultValue.Text))
                //{
                //    txtbx_comp2.SelectionColor = Color.Red;
                //}

                //if (!(txtbx_comp3.Text == txtbx_ResultValue.Text))
                //{
                //    txtbx_comp3.SelectionColor = Color.Red;
                //}
            }
            else if(txtbx_Type.Text == "Curve")
            {
                Compare_Datasets_Cur();
            }

        }


        public void scaleVal()
        {
            calVal1 = (calVal1 * frm_search.gain1 + frm_search.offset1) / (frm_search.gain2 + frm_search.offset2);

        }

        private void Import_Curve()
        {

            DataTable dt = new DataTable();


            datagrid_Cur.DataSource = frm_search._curList;
            datagrid_Cur.Columns[0].HeaderText = frm_search.BPName;
            datagrid_Cur.Columns[1].HeaderText = frm_search.CalAxisName;
            sizeDGV(datagrid_Cur);

            //MessageBox.Show("Hello");

        }


        void sizeDGV(DataGridView dgv)
        {
            DataGridViewElementStates states = DataGridViewElementStates.None;
            dgv.ScrollBars = ScrollBars.None;
            var totalHeight = dgv.Rows.GetRowsHeight(states) + dgv.ColumnHeadersHeight;
            totalHeight += dgv.Rows.Count * 4;  // a correction I need
            var totalWidth = dgv.Columns.GetColumnsWidth(states) + dgv.RowHeadersWidth;
            dgv.ClientSize = new Size(totalWidth, totalHeight);
        }

    }
}