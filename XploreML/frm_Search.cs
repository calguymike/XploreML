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
using System.Text.RegularExpressions;


namespace XploreML
{

    public partial class frm_search : Form
    {
        int[] cal = new int[65534];
        string[] calHEX = new string[65534];
        public static string FactoryName = "init";
        public static string DescL0 = "init";
        public static string DescL1 = "init";
        public static string scaling = "init";
        public static string group = "init";
        public static string format = "init";
        public static string unit = "init";
        public static string varMin = "init";
        public static string varMax = "init";
        public static string notes = "";
        public static string BPName = "";
        public static string CalAxisName = "";
        public static string targetPath = @"C:\XploreML\";
        public static string XMLDoc = targetPath + "LiveXML.xml";
        public static string TABDoc = targetPath + "LiveTAB.tab";
        public static string bpScaling = "init";
        XmlDocument LiveXML = new XmlDocument();
        public static float calVal;
        public static int OffsetByte;
        public static int NByte;
        public static int NByteSingle;
        public static int bpCount;
        public static float gain1 = 1;
        public static float offset1 = 0;
        public static float gain2 = 1;
        public static float offset2 = 0;
        public static string type = "init";
        public static int cb = 0;
        public static float curVal = 0;
        public static bool equiSpaced = false;
        //public static List<CurSpl> _curListVal = new List<CurSpl>();
        public static List<CurSpl> _curList = new List<CurSpl>();

        public class CurSpl
        {
            public float CurBP { get; set; }
            public float CurVal { get; set; }
        }


        public frm_search()
        {
            InitializeComponent();
            while (!(cb == 0))
            {
                txtbx_Search.Text = Clipboard.GetText();
                searchResults();
                System.Threading.Thread.Sleep(500);
                this.WindowState = FormWindowState.Minimized;
                this.Show();
                this.WindowState = FormWindowState.Normal;
                cb = 0;
            }
        }

        private void Frm_Search_Load(object sender, EventArgs e)
        {

        }

        public void Btn_Search_Click(object sender, EventArgs e)
        {
            searchResults();
            
        }

        public void loadTAB()
        {

            string[] lines = System.IO.File.ReadAllLines(TABDoc);
            int i = 0;
            foreach (string line in lines)
            {
                string[] words = line.Split(null);

                foreach (var word in words)
                {
                    
                    if (word.Length >= 1)
                    {
                        cal[i] = Int32.Parse(word);
                        i += 1;
                        //Console.Write(i + " ");
                    }
                }
            }

            //MessageBox.Show("hello, world!");
        }

        public void ListView_searchResults_DoubleClick(object sender, EventArgs e)
        {
            //Handles double clicking search result and opening result form with calibration/XML data

            //MessageBox.Show(listView_searchResults.SelectedItems[0].SubItems[0].Text);
            FactoryName = (listView_searchResults.SelectedItems[0].SubItems[0].Text);
            string name = "";
            foreach (XmlElement xmlElement in LiveXML.DocumentElement)
            {
                
                try
                {
                    name = xmlElement["FactoryName"].InnerText;
                }
                catch
                {

                }

                if (name == FactoryName)
                {
                    if (xmlElement.Name == "CalibrationValues")
                    {
                        FactoryName = xmlElement["FactoryName"].InnerText;
                        DescL0 = xmlElement["DescL0"].InnerText;
                        DescL1 = xmlElement["DescL1"].InnerText;
                        group = xmlElement["GroupID"].InnerText;
                        format = xmlElement["Var_Format"].InnerText;
                        unit = xmlElement["Var_Unit"].InnerText;
                        varMin = xmlElement["Var_Min"].InnerText;
                        varMax = xmlElement["Var_Max"].InnerText;
                        scaling = xmlElement["Var_ScalingID"].InnerText;
                        notes = xmlElement["Notes"].InnerText;
                        type = "Calibration";
                        NByte = Int32.Parse(xmlElement["NByte"].InnerText);
                        int OffsetByte = Int32.Parse(xmlElement["OffsetByte"].InnerText);

                        //if cal is one byte then directly add to variable
                        if (NByte == 1)
                        {
                            calVal = cal[OffsetByte];
                            scaleVal();
                        }

                        //if cal is 2 bytes then combine the bytes to single dec value
                        else if (NByte == 2)
                        {
                            int cal1 = cal[OffsetByte];
                            int cal2 = cal[OffsetByte + 1];

                            //if 2 bytes then calibration value is (byte 1 * 256) + byte 2
                            //calVal becomes unscaled decimal
                            calVal = ((cal1 * 256) + cal2);
                            //Then gets scaled
                            scaleVal();
                        }

                        //calVal = cal[OffsetByte];
                        //scaleVal();
                        Result resultForm = new Result();
                        resultForm.Show();
                    }


                    else if ((xmlElement.Name) == "Channels")
                    {
                        FactoryName = xmlElement["FactoryName"].InnerText;
                        DescL0 = xmlElement["DescL0"].InnerText;
                        DescL1 = xmlElement["DescL1"].InnerText;
                        group = "NA";
                        format = xmlElement["Format"].InnerText;
                        unit = xmlElement["Unit"].InnerText;
                        varMin = xmlElement["Min"].InnerText;
                        varMax = xmlElement["Max"].InnerText;
                        scaling = xmlElement["Channel_ScalingID"].InnerText;
                        notes = xmlElement["Notes"].InnerText;
                        type = "Variable";
                        calVal = 0;
                        Result resultForm = new Result();
                        resultForm.Show();
                    }

                    else if ((xmlElement.Name) == "CalibrationCurves")
                    {
                        FactoryName = xmlElement["FactoryName"].InnerText;
                        DescL0 = xmlElement["DescL0"].InnerText;
                        DescL1 = xmlElement["DescL1"].InnerText;
                        group = xmlElement["GroupID"].InnerText;
                        BPName = xmlElement["BreakPoint1_Label"].InnerText;
                        CalAxisName = xmlElement["Var_Label"].InnerText;
                        format = xmlElement["Var_Format"].InnerText;
                        unit = xmlElement["Var_Unit"].InnerText;
                        varMin = xmlElement["Var_Min"].InnerText;
                        varMax = xmlElement["Var_Max"].InnerText;
                        scaling = xmlElement["Var_ScalingID"].InnerText;
                        notes = xmlElement["Notes"].InnerText;
                        type = "Curve";
                        int OffsetByte = Int32.Parse(xmlElement["OffsetByte"].InnerText);
                        NByte = Int32.Parse(xmlElement["NByte"].InnerText);
                        bpScaling = xmlElement["BreakPoint1_ScalingID"].InnerText;
                        bpCount = Int32.Parse(xmlElement["BreakPoint1_Count"].InnerText);
                        NByteSingle = Int32.Parse(xmlElement["NByteSingleValue"].InnerText);
                        float BPMin = float.Parse(xmlElement["BreakPoint1_Min"].InnerText);
                        float BPMax = float.Parse(xmlElement["BreakPoint1_Max"].InnerText);
                        if (xmlElement["EquiSpaced"].InnerText == "true")
                        {
                            equiSpaced = true;
                        }else if (xmlElement["EquiSpaced"].InnerText == "false")
                        {
                            equiSpaced = false;
                        }


                        loadTAB();

                        _curList.Clear();
                        _curList.Clear();

                        List<float> LOCBPList = new List<float>();
                        List<float> LOCValList = new List<float>();

                        int i = OffsetByte;
                        int y = OffsetByte;

                        //if breakpoints are in the tab file, extract them
                        if (equiSpaced == false)
                        {
                            
                            //Create a list of breakpoints
                            while (y < OffsetByte + bpCount)
                            {
                                curVal = cal[i];

                                if (NByteSingle == 2)
                                {
                                    int cal1 = cal[i];
                                    int cal2 = cal[i + 1];

                                    curVal = ((cal1 * 256) + cal2);
                                    i += 2;
                                }
                                else if (NByteSingle == 1)
                                {
                                    i += 1;
                                }

                                y += 1;
                                scaleCurBP();
                                LOCBPList.Add(curVal);
                                //_curList.Add(new CurSpl { CurVal = curVal});

                            }

                            //Create a list of curve values
                            while (y < (OffsetByte + NByte))
                            {
                                curVal = cal[i];
                                if (NByteSingle == 2)
                                {
                                    int cal1 = cal[i];
                                    int cal2 = cal[i + 1];

                                    curVal = ((cal1 * 256) + cal2);
                                    i += 2;
                                }
                                else if (NByteSingle == 1)
                                {
                                    i += 1;
                                }
                                y += 1;
                                scaleCur();
                                LOCValList.Add(curVal);

                            }

                            //Merge the list of breakpoints and values into a single list with two elements
                            int n = 0;

                            while (n < bpCount)
                            {
                                _curList.Add(new CurSpl { CurBP = LOCBPList[n], CurVal = LOCValList[n] });
                                n += 1;
                            }
                        }


                        //if breakpoints are not in tab file, they must be equally spaced between min and max
                        //equispaced element of XML file shows if this is the case
                        else if (equiSpaced == true)
                        {
                            //Create a list of breakpoints

                            //define accuracy of breakpoints from scaling
                            scaleCurBP();
                            float minstep1 = (1 * gain1 + offset1) / (gain2 + offset2);
                            float minstep2 = (2 * gain1 + offset1) / (gain2 + offset2);
                            float minstep = (minstep2 - minstep1);
                            float stepSize = ((minstep * 256) / (bpCount - 1));
                            int x = 0;
                            float currBP = BPMin;
                            LOCBPList.Add(currBP);
                            while (x <= bpCount)
                            {
                                currBP = (currBP + stepSize);
                                //currBP = (x * + stepSize);
                                LOCBPList.Add(currBP);
                                x += 1;
                            }

                            //Create a list of curve values
                            while (i < (OffsetByte + NByte))
                            {
                                curVal = cal[i];
                                scaleCur();
                                LOCValList.Add(curVal);
                                //_curList.Add(new CurSpl { CurBP = curVal });
                                i += 1;
                            }

                            //Merge the list of breakpoints and values into a single list with two elements
                            int n = 0;

                            while (n < bpCount)
                            {
                                _curList.Add(new CurSpl { CurBP = LOCBPList[n], CurVal = LOCValList[n] });
                                n += 1;
                            }

                        }


                        //MessageBox.Show("break");
                        Result resultForm = new Result();
                        resultForm.Show();
                    }

                    else
                    {
                        break;
                    }

                }

            }

        }

        public void searchResults()
        {
            listView_searchResults.Items.Clear();

            if (btn_Search.Text != null && btn_Search.Text.Length >= 3)
            {
                LiveXML.Load(XMLDoc);
                loadTAB();


                foreach (XmlElement xmlElement in LiveXML.DocumentElement)
                {
                    string name = "";
                    if(chk_Group.Checked == true)
                    {
                        try
                        {
                            name = xmlElement["GroupID"].InnerText;
                        }
                        catch
                        {

                        }
                        
                    }
                    else
                    {
                        name = xmlElement.InnerText;
                    }

                    if (name.ToLower().Contains(txtbx_Search.Text.ToLower()))
                    {
                        //if (name.Contains("Calibration."))
                        if (xmlElement.Name == "CalibrationValues" && chk_Calibrations.Checked == true)
                        {
                            //if "CalibrationValues" in XML then set variables to XML data
                            FactoryName = xmlElement["FactoryName"].InnerText;
                            DescL0 = xmlElement["DescL0"].InnerText;
                            DescL1 = xmlElement["DescL1"].InnerText;
                            group = xmlElement["GroupID"].InnerText;
                            format = xmlElement["Var_Format"].InnerText;
                            unit = xmlElement["Var_Unit"].InnerText;
                            varMin = xmlElement["Var_Min"].InnerText;
                            varMax = xmlElement["Var_Max"].InnerText;
                            scaling = xmlElement["Var_ScalingID"].InnerText;
                            type = "Calibration";
                            OffsetByte = Int32.Parse(xmlElement["OffsetByte"].InnerText);
                            NByte = Int32.Parse(xmlElement["NByte"].InnerText);

                            //if cal is one byte then directly add to variable
                            if (NByte == 1)
                            {
                                calVal = cal[OffsetByte];
                                scaleVal();
                            }

                            //if cal is 2 bytes then combine the bytes to single dec value
                            else if (NByte == 2)
                            {
                                int cal1 = cal[OffsetByte];
                                int cal2 = cal[OffsetByte + 1];

                                calVal = ((cal1 * 256) + cal2);
                                scaleVal();
                            }



                        }
                        else if (xmlElement.Name == "Channels" && chk_Variables.Checked == true)
                        {
                            FactoryName = xmlElement["FactoryName"].InnerText;
                            DescL0 = xmlElement["DescL0"].InnerText;
                            DescL1 = xmlElement["DescL1"].InnerText;
                            group = "NA";
                            format = xmlElement["Format"].InnerText;
                            unit = xmlElement["Unit"].InnerText;
                            varMin = xmlElement["Min"].InnerText;
                            varMax = xmlElement["Max"].InnerText;
                            scaling = xmlElement["Channel_ScalingID"].InnerText;
                            type = "Variable";
                            calVal = 0;
                        }


                        else if (xmlElement.Name == "CalibrationCurves" && chk_Curves.Checked == true)
                        {
                            FactoryName = xmlElement["FactoryName"].InnerText;
                            DescL0 = xmlElement["DescL0"].InnerText;
                            DescL1 = xmlElement["DescL1"].InnerText;
                            group = xmlElement["GroupID"].InnerText;
                            format = xmlElement["Var_Format"].InnerText;
                            unit = xmlElement["Var_Unit"].InnerText;
                            varMin = xmlElement["Var_Min"].InnerText;
                            varMax = xmlElement["Var_Max"].InnerText;
                            scaling = xmlElement["Var_ScalingID"].InnerText;
                            type = "Curve";
                            OffsetByte = Int32.Parse(xmlElement["OffsetByte"].InnerText);
                            NByte = Int32.Parse(xmlElement["NByte"].InnerText);
                            NByteSingle = Int32.Parse(xmlElement["NByteSingleValue"].InnerText);

                        }

                        else if (xmlElement.Name == "CalibrationMaps" && chk_Curves.Checked == true)
                        {
                            FactoryName = xmlElement["FactoryName"].InnerText;
                            DescL0 = xmlElement["DescL0"].InnerText;
                            DescL1 = xmlElement["DescL1"].InnerText;
                            group = xmlElement["GroupID"].InnerText;
                            format = xmlElement["Var_Format"].InnerText;
                            unit = xmlElement["Var_Unit"].InnerText;
                            varMin = xmlElement["Var_Min"].InnerText;
                            varMax = xmlElement["Var_Max"].InnerText;
                            scaling = xmlElement["Var_ScalingID"].InnerText;
                            type = "Map";
                            OffsetByte = Int32.Parse(xmlElement["OffsetByte"].InnerText);
                            NByte = Int32.Parse(xmlElement["NByte"].InnerText);
                            NByteSingle = Int32.Parse(xmlElement["NByteSingleValue"].InnerText);

                        }

                        else
                        {
                        break;
                        }


                        ListViewItem row = new ListViewItem(FactoryName);
                        row.SubItems.Add(new ListViewItem.ListViewSubItem(row, type));
                        row.SubItems.Add(new ListViewItem.ListViewSubItem(row, group));
                        row.SubItems.Add(new ListViewItem.ListViewSubItem(row, DescL0));
                        

                        if (type == "Calibration")
                        {
                            row.SubItems.Add(new ListViewItem.ListViewSubItem(row, calVal.ToString()));
                        }
                        else if (type == "Variable")
                        {
                            row.SubItems.Add(new ListViewItem.ListViewSubItem(row, " "));
                        }

                        else if (type == "Curve")
                        {
                            row.SubItems.Add(new ListViewItem.ListViewSubItem(row, "..."));
                        }

                        else if (type == "Map")
                        {
                            row.SubItems.Add(new ListViewItem.ListViewSubItem(row, "..."));
                        }



                        if (!(row.Text.Contains("init")))
                        {
                            listView_searchResults.Items.Add(row);
                        }
                        
                    }
                }
            }
        }

        public void scaleVal()
        {
            //Calculates single 1 byte cal values using value from tab and scaling given

            //string[] scaleRaw = scaling.Split('_');
            foreach (XmlElement xmlElement in LiveXML.DocumentElement)
            {
                string name = xmlElement.InnerText;

                if (xmlElement.Name == "CalibrationScalings")
                {
                    if (xmlElement["ID"].InnerText == scaling)
                    {
                        gain1 = float.Parse(xmlElement["G1"].InnerText);
                        gain2 = float.Parse(xmlElement["G2"].InnerText);
                        offset1 = float.Parse(xmlElement["O1"].InnerText);
                        offset2 = float.Parse(xmlElement["O2"].InnerText);
                    }
                }
            }

         calVal = (calVal * gain1 + offset1) / (gain2 + offset2);
        }


        public void scaleCur()
        {
            //Calculates single 1 byte cal values using value from tab and scaling given

            //string[] scaleRaw = scaling.Split('_');
            foreach (XmlElement xmlElement in LiveXML.DocumentElement)
            {
                string name = xmlElement.InnerText;

                if (xmlElement.Name == "CalibrationScalings")
                {
                    if (xmlElement["ID"].InnerText == scaling)
                    {
                        gain1 = float.Parse(xmlElement["G1"].InnerText);
                        gain2 = float.Parse(xmlElement["G2"].InnerText);
                        offset1 = float.Parse(xmlElement["O1"].InnerText);
                        offset2 = float.Parse(xmlElement["O2"].InnerText);
                    }
                }
            }

            curVal = (curVal * gain1 + offset1) / (gain2 + offset2);
        }

        public void scaleCurBP()
        {
            //Calculates single 1 byte cal values using value from tab and scaling given

            //string[] scaleRaw = scaling.Split('_');
            foreach (XmlElement xmlElement in LiveXML.DocumentElement)
            {
                string name = xmlElement.InnerText;

                if (xmlElement.Name == "CalibrationScalings")
                {
                    if (xmlElement["ID"].InnerText == bpScaling)
                    {
                        gain1 = float.Parse(xmlElement["G1"].InnerText);
                        gain2 = float.Parse(xmlElement["G2"].InnerText);
                        offset1 = float.Parse(xmlElement["O1"].InnerText);
                        offset2 = float.Parse(xmlElement["O2"].InnerText);
                    }
                }
            }
            curVal = (curVal * gain1 + offset1) / (gain2 + offset2);
        }

        private void searchGroups()
        {
            string gp = "";
            listView_searchResults.Items.Clear();

            if (btn_Search.Text != null && btn_Search.Text.Length >= 3)
            {
                LiveXML.Load(XMLDoc);
                loadTAB();
                

                foreach (XmlElement xmlElement in LiveXML.DocumentElement)
                {
                    string name = xmlElement.InnerText;
                    
                    if (xmlElement.Name == "CalibrationValues")
                    {
                        try
                        {
                            gp = xmlElement["GroupID"].InnerText;
                        }

                        catch
                        {

                        }

                        if (gp.ToLower().Contains(txtbx_Search.Text.ToLower()))
                        {
                            //if (name.Contains("Calibration."))
                            if (xmlElement.Name == "CalibrationValues")
                            {
                                //if "CalibrationValues" in XML then set variables to XML data
                                FactoryName = xmlElement["FactoryName"].InnerText;
                                DescL0 = xmlElement["DescL0"].InnerText;
                                DescL1 = xmlElement["DescL1"].InnerText;
                                group = xmlElement["GroupID"].InnerText;
                                format = xmlElement["Var_Format"].InnerText;
                                unit = xmlElement["Var_Unit"].InnerText;
                                varMin = xmlElement["Var_Min"].InnerText;
                                varMax = xmlElement["Var_Max"].InnerText;
                                scaling = xmlElement["Var_ScalingID"].InnerText;
                                type = "Calibration";
                                OffsetByte = Int32.Parse(xmlElement["OffsetByte"].InnerText);
                                NByte = Int32.Parse(xmlElement["NByte"].InnerText);

                                //if cal is one byte then directly add to variable
                                if (NByte == 1)
                                {
                                    calVal = cal[OffsetByte];
                                    scaleVal();
                                }

                                //if cal is 2 bytes then combine the bytes to single dec value
                                else if (NByte == 2)
                                {
                                    int cal1 = cal[OffsetByte];
                                    int cal2 = cal[OffsetByte + 1];

                                    calVal = ((cal1 * 256) + cal2);
                                    scaleVal();
                                }



                            }
                            else if (xmlElement.Name == "Channels")
                            {
                                FactoryName = xmlElement["FactoryName"].InnerText;
                                DescL0 = xmlElement["DescL0"].InnerText;
                                DescL1 = xmlElement["DescL1"].InnerText;
                                group = "NA";
                                format = xmlElement["Format"].InnerText;
                                unit = xmlElement["Unit"].InnerText;
                                varMin = xmlElement["Min"].InnerText;
                                varMax = xmlElement["Max"].InnerText;
                                scaling = xmlElement["Channel_ScalingID"].InnerText;
                                type = "Variable";
                                calVal = 0;
                            }


                            else if (xmlElement.Name == "CalibrationCurves")
                            {
                                FactoryName = xmlElement["FactoryName"].InnerText;
                                DescL0 = xmlElement["DescL0"].InnerText;
                                DescL1 = xmlElement["DescL1"].InnerText;
                                group = xmlElement["GroupID"].InnerText;
                                format = xmlElement["Var_Format"].InnerText;
                                unit = xmlElement["Var_Unit"].InnerText;
                                varMin = xmlElement["Var_Min"].InnerText;
                                varMax = xmlElement["Var_Max"].InnerText;
                                scaling = xmlElement["Var_ScalingID"].InnerText;
                                type = "Curve";
                                OffsetByte = Int32.Parse(xmlElement["OffsetByte"].InnerText);
                                NByte = Int32.Parse(xmlElement["NByte"].InnerText);
                                NByteSingle = Int32.Parse(xmlElement["NByteSingleValue"].InnerText);

                            }

                            else
                            {
                                break;
                            }


                            ListViewItem row = new ListViewItem(FactoryName);
                            row.SubItems.Add(new ListViewItem.ListViewSubItem(row, type));
                            row.SubItems.Add(new ListViewItem.ListViewSubItem(row, group));
                            row.SubItems.Add(new ListViewItem.ListViewSubItem(row, DescL0));


                            if (type == "Calibration")
                            {
                                row.SubItems.Add(new ListViewItem.ListViewSubItem(row, calVal.ToString()));
                                //row.SubItems.Add(new ListViewItem.ListViewSubItem(row, group));
                            }
                            else if (type == "Variable")
                            {
                                row.SubItems.Add(new ListViewItem.ListViewSubItem(row, " "));
                            }

                            else if (type == "Curve")
                            {
                                row.SubItems.Add(new ListViewItem.ListViewSubItem(row, "..."));
                                //row.SubItems.Add(new ListViewItem.ListViewSubItem(row, group));
                            }



                            if (!(row.Text.Contains("init")))
                            {
                                listView_searchResults.Items.Add(row);
                            }

                        }

                    }
                }
            }
        }

        private void btn_D2H_Click(object sender, EventArgs e)
        {
            //TODO
            //  1. Read TAB into array (copy code from search function) **
            //  2. Convert each element of array to HEX
            //  3. Write array back to file in correct format (what is the correct format?)


            //Check number per line is filled out
            if(txtbox_noPerLine.TextLength <1)
            {
                MessageBox.Show("You must state how many hex values per line you want in output file");
                
            }

            else
            {
                //Read TAB to array
                loadTAB();

                // Convert each element of array to HEX
                for (int i = 0; i < 65534; i++)
                {
                    
                    calHEX[i] = cal[i].ToString("X").PadLeft(2, '0');
                }

                //Filepath to save to
                string pathHEX = @"C:\XploreML\cal.s19";
                //Number of HEX values on each line
                string noPerLineStr = txtbox_noPerLine.Text;
                //int noPerLine = int.Parse(txtbox_noPerLine.Text);
                int noPerLine = int.Parse(noPerLineStr);
                string noPerLineHEX = (noPerLine +5).ToString("X");
                float noLines = (65534 / noPerLine);
                int noLinesInt = (int)Math.Ceiling(noLines) + 1;

                //New array of lines to write to file
                string[] linesHEX = new string[noLinesInt + 1];
                //string[] chkSumLine = new string[noPerLine + 4];
                int ln = 0;
                int cntr = noLinesInt;

                //Set Start address
                //cal start address in dec
                int curAddress = 1073930240;


                //For each hex value, check if the data has reached data length (user entered), if not continue adding data
                //If data length reached then end line with checksum and start a new line
                int n = 0;
                for (int i = 0; i < 65534; i++)
                {

                    if (cntr >= noPerLine)
                    {

                        n = 0;
                        //AddChecksum to end of current line
                        if (ln > 0)
                        {
                            linesHEX[ln] +="FF";
                            //string currLine = linesHEX[ln];

                            //string input = linesHEX[ln];
                            //double partSize = 2;
                            //int k = 0;
                            //var output = input
                            //    .ToLookup(c => Math.Floor(k++ / partSize))
                            //    .Select(e => new String(e.ToArray()));
                        }

                        cntr = 0;
                        ln++;

                        //Add S19 Format to start of line
                        linesHEX[ln] = "S3";

                        //Add user configured number of bytes per line to S19 format
                        //Number of HEX values (in pairs, i.e 1E 2F FF = 3)
                        linesHEX[ln] += noPerLineHEX;
                        //chkSumLine[n] = noPerLineHEX;
                        n++;
                        //curAddress = curAddress + noPerLine + 1;


                        //Data address
                        string addressHEX = curAddress.ToString("X");
                        addressHEX = addressHEX.PadLeft(8, '0');
                        linesHEX[ln] += addressHEX;
                        //chkSumLine[n] = addressHEX;
                        n++;
                        curAddress = curAddress + noPerLine + 1;


                    }

                linesHEX[ln] += calHEX[i];
                //chkSumLine[n] = calHEX[i];
                n++;
                cntr += 1;

                }


                System.IO.File.WriteAllLines(pathHEX, linesHEX);


                //Confirmation
                MessageBox.Show("Done");
            }

               

        }
    }



    public sealed class HotkeyManager : NativeWindow, IDisposable
    {
        public HotkeyManager()
        {
            CreateHandle(new CreateParams());
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.WM_HOTKEY_MSG_ID)
            {
                if (m.WParam.ToInt32() == 123)
                {
                    frm_search.cb = 1;
                    frm_search searchForm = new frm_search();
                    searchForm.Show();
                }

                //if (m.WParam.ToInt32() == 234)
                //{
                //    MessageBox.Show("HotKey ID: 234 has been pressed");
                //}
            }
            base.WndProc(ref m);
        }

        public void Dispose()
        {
            DestroyHandle();
        }

        //interface IYourForm
        //{
        //    string clipboardTerm { get; set; }
        //}


    }





}


