
namespace RimworldApparelBodyTex_V1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_xml = new System.Windows.Forms.TextBox();
            this.btn_listModLoadOrder = new System.Windows.Forms.Button();
            this.textBox_log = new System.Windows.Forms.TextBox();
            this.btn_getModConfig = new System.Windows.Forms.Button();
            this.textBox_modConfig = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label_FolderActiveMod = new System.Windows.Forms.Label();
            this.label_XMLFile = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox_TexDestinationPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_UpdateBodyType = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBox_BodyTypeDestination = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBox_BodyTypeSource = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.checkBox_IncludeBodyTex = new System.Windows.Forms.CheckBox();
            this.checkBox_overwrite = new System.Windows.Forms.CheckBox();
            this.Btn_MulaiCek = new System.Windows.Forms.Button();
            this.btn_remModFolder = new System.Windows.Forms.Button();
            this.btn_addModFolder = new System.Windows.Forms.Button();
            this.checkedListBox_modFolder = new System.Windows.Forms.CheckedListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.checkBox_sameAsSource = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_BodyTextDestination = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_BodyTextSource = new System.Windows.Forms.TextBox();
            this.btn_genHarBodyTexFromVanillaTex = new System.Windows.Forms.Button();
            this.textBox_rootModFolder = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 635);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 609);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Step 1 - Get Active Mod";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.textBox_xml);
            this.panel1.Controls.Add(this.btn_listModLoadOrder);
            this.panel1.Controls.Add(this.textBox_log);
            this.panel1.Controls.Add(this.btn_getModConfig);
            this.panel1.Controls.Add(this.textBox_modConfig);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(762, 603);
            this.panel1.TabIndex = 8;
            // 
            // textBox_xml
            // 
            this.textBox_xml.Location = new System.Drawing.Point(3, 60);
            this.textBox_xml.Multiline = true;
            this.textBox_xml.Name = "textBox_xml";
            this.textBox_xml.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_xml.Size = new System.Drawing.Size(737, 196);
            this.textBox_xml.TabIndex = 10;
            this.textBox_xml.WordWrap = false;
            // 
            // btn_listModLoadOrder
            // 
            this.btn_listModLoadOrder.Location = new System.Drawing.Point(3, 262);
            this.btn_listModLoadOrder.Name = "btn_listModLoadOrder";
            this.btn_listModLoadOrder.Size = new System.Drawing.Size(175, 23);
            this.btn_listModLoadOrder.TabIndex = 9;
            this.btn_listModLoadOrder.Text = "B - Get Mod Load Order";
            this.btn_listModLoadOrder.UseVisualStyleBackColor = true;
            this.btn_listModLoadOrder.Click += new System.EventHandler(this.btn_listModLoadOrder_Click);
            // 
            // textBox_log
            // 
            this.textBox_log.Location = new System.Drawing.Point(3, 313);
            this.textBox_log.Multiline = true;
            this.textBox_log.Name = "textBox_log";
            this.textBox_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_log.Size = new System.Drawing.Size(737, 275);
            this.textBox_log.TabIndex = 8;
            // 
            // btn_getModConfig
            // 
            this.btn_getModConfig.Location = new System.Drawing.Point(609, 3);
            this.btn_getModConfig.Name = "btn_getModConfig";
            this.btn_getModConfig.Size = new System.Drawing.Size(131, 23);
            this.btn_getModConfig.TabIndex = 7;
            this.btn_getModConfig.Text = "A - Get ModConfig";
            this.btn_getModConfig.UseVisualStyleBackColor = true;
            this.btn_getModConfig.Click += new System.EventHandler(this.btn_getModConfig_Click);
            // 
            // textBox_modConfig
            // 
            this.textBox_modConfig.Location = new System.Drawing.Point(150, 5);
            this.textBox_modConfig.Name = "textBox_modConfig";
            this.textBox_modConfig.Size = new System.Drawing.Size(448, 20);
            this.textBox_modConfig.TabIndex = 6;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 609);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Step 2 - Gen Apparel Tex";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.textBox_rootModFolder);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.Btn_MulaiCek);
            this.panel2.Controls.Add(this.btn_remModFolder);
            this.panel2.Controls.Add(this.btn_addModFolder);
            this.panel2.Controls.Add(this.checkedListBox_modFolder);
            this.panel2.Location = new System.Drawing.Point(6, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(756, 530);
            this.panel2.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.IndianRed;
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label_FolderActiveMod);
            this.panel4.Controls.Add(this.label_XMLFile);
            this.panel4.Controls.Add(this.textBox1);
            this.panel4.Controls.Add(this.textBox2);
            this.panel4.Location = new System.Drawing.Point(24, 168);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(609, 71);
            this.panel4.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Folder Active Mod";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "XML In Active Mod";
            // 
            // label_FolderActiveMod
            // 
            this.label_FolderActiveMod.AutoSize = true;
            this.label_FolderActiveMod.Location = new System.Drawing.Point(216, 32);
            this.label_FolderActiveMod.Name = "label_FolderActiveMod";
            this.label_FolderActiveMod.Size = new System.Drawing.Size(16, 13);
            this.label_FolderActiveMod.TabIndex = 3;
            this.label_FolderActiveMod.Text = "...";
            // 
            // label_XMLFile
            // 
            this.label_XMLFile.AutoSize = true;
            this.label_XMLFile.Location = new System.Drawing.Point(219, 6);
            this.label_XMLFile.Name = "label_XMLFile";
            this.label_XMLFile.Size = new System.Drawing.Size(16, 13);
            this.label_XMLFile.TabIndex = 2;
            this.label_XMLFile.Text = "...";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(116, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(116, 29);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.Controls.Add(this.tableLayoutPanel4);
            this.panel3.Controls.Add(this.btn_UpdateBodyType);
            this.panel3.Controls.Add(this.tableLayoutPanel2);
            this.panel3.Controls.Add(this.tableLayoutPanel1);
            this.panel3.Controls.Add(this.checkedListBox1);
            this.panel3.Controls.Add(this.checkBox_IncludeBodyTex);
            this.panel3.Controls.Add(this.checkBox_overwrite);
            this.panel3.Location = new System.Drawing.Point(24, 267);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(609, 248);
            this.panel3.TabIndex = 6;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.textBox_TexDestinationPath, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(20, 149);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(586, 29);
            this.tableLayoutPanel4.TabIndex = 8;
            // 
            // textBox_TexDestinationPath
            // 
            this.textBox_TexDestinationPath.Location = new System.Drawing.Point(133, 3);
            this.textBox_TexDestinationPath.Name = "textBox_TexDestinationPath";
            this.textBox_TexDestinationPath.Size = new System.Drawing.Size(450, 20);
            this.textBox_TexDestinationPath.TabIndex = 8;
            this.textBox_TexDestinationPath.TextChanged += new System.EventHandler(this.textBox_TexDestinationPath_TextChanged);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "TexDestinationPath";
            // 
            // btn_UpdateBodyType
            // 
            this.btn_UpdateBodyType.Location = new System.Drawing.Point(281, 82);
            this.btn_UpdateBodyType.Name = "btn_UpdateBodyType";
            this.btn_UpdateBodyType.Size = new System.Drawing.Size(127, 23);
            this.btn_UpdateBodyType.TabIndex = 6;
            this.btn_UpdateBodyType.Text = "Update BodyType";
            this.btn_UpdateBodyType.UseVisualStyleBackColor = true;
            this.btn_UpdateBodyType.Click += new System.EventHandler(this.btn_UpdateBodyType_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.comboBox_BodyTypeDestination, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(20, 114);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(256, 29);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // comboBox_BodyTypeDestination
            // 
            this.comboBox_BodyTypeDestination.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBox_BodyTypeDestination.FormattingEnabled = true;
            this.comboBox_BodyTypeDestination.Items.AddRange(new object[] {
            "Fat",
            "Female",
            "Hulk",
            "Male",
            "Thin"});
            this.comboBox_BodyTypeDestination.Location = new System.Drawing.Point(131, 4);
            this.comboBox_BodyTypeDestination.Name = "comboBox_BodyTypeDestination";
            this.comboBox_BodyTypeDestination.Size = new System.Drawing.Size(121, 21);
            this.comboBox_BodyTypeDestination.TabIndex = 6;
            this.comboBox_BodyTypeDestination.TextChanged += new System.EventHandler(this.comboBox_BodyTypeDestination_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "BodyTypeDestination";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.comboBox_BodyTypeSource, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 79);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(256, 29);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // comboBox_BodyTypeSource
            // 
            this.comboBox_BodyTypeSource.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBox_BodyTypeSource.FormattingEnabled = true;
            this.comboBox_BodyTypeSource.Items.AddRange(new object[] {
            "Fat",
            "Female",
            "Hulk",
            "Male",
            "Thin"});
            this.comboBox_BodyTypeSource.Location = new System.Drawing.Point(131, 4);
            this.comboBox_BodyTypeSource.Name = "comboBox_BodyTypeSource";
            this.comboBox_BodyTypeSource.Size = new System.Drawing.Size(121, 21);
            this.comboBox_BodyTypeSource.TabIndex = 6;
            this.comboBox_BodyTypeSource.TextChanged += new System.EventHandler(this.comboBox_BodyTypeSource_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "BodyTypeSource";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Genderless Mixed BodyTex (Vanilla)",
            "Gender BodyTex (Humanoid Alien Race)"});
            this.checkedListBox1.Location = new System.Drawing.Point(150, 39);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(227, 34);
            this.checkedListBox1.TabIndex = 2;
            // 
            // checkBox_IncludeBodyTex
            // 
            this.checkBox_IncludeBodyTex.AutoSize = true;
            this.checkBox_IncludeBodyTex.Location = new System.Drawing.Point(20, 39);
            this.checkBox_IncludeBodyTex.Name = "checkBox_IncludeBodyTex";
            this.checkBox_IncludeBodyTex.Size = new System.Drawing.Size(127, 17);
            this.checkBox_IncludeBodyTex.TabIndex = 1;
            this.checkBox_IncludeBodyTex.Text = "Include Body Texture";
            this.checkBox_IncludeBodyTex.UseVisualStyleBackColor = true;
            this.checkBox_IncludeBodyTex.CheckedChanged += new System.EventHandler(this.checkBox_IncludeBodyTex_CheckedChanged);
            // 
            // checkBox_overwrite
            // 
            this.checkBox_overwrite.AutoSize = true;
            this.checkBox_overwrite.Location = new System.Drawing.Point(20, 16);
            this.checkBox_overwrite.Name = "checkBox_overwrite";
            this.checkBox_overwrite.Size = new System.Drawing.Size(71, 17);
            this.checkBox_overwrite.TabIndex = 0;
            this.checkBox_overwrite.Text = "Overwrite";
            this.checkBox_overwrite.UseVisualStyleBackColor = true;
            this.checkBox_overwrite.CheckedChanged += new System.EventHandler(this.checkBox_overwrite_CheckedChanged);
            // 
            // Btn_MulaiCek
            // 
            this.Btn_MulaiCek.Location = new System.Drawing.Point(639, 492);
            this.Btn_MulaiCek.Name = "Btn_MulaiCek";
            this.Btn_MulaiCek.Size = new System.Drawing.Size(75, 23);
            this.Btn_MulaiCek.TabIndex = 5;
            this.Btn_MulaiCek.Text = "Mulai cek";
            this.Btn_MulaiCek.UseVisualStyleBackColor = true;
            this.Btn_MulaiCek.Click += new System.EventHandler(this.Btn_MulaiCek_Click);
            // 
            // btn_remModFolder
            // 
            this.btn_remModFolder.Location = new System.Drawing.Point(639, 135);
            this.btn_remModFolder.Name = "btn_remModFolder";
            this.btn_remModFolder.Size = new System.Drawing.Size(75, 23);
            this.btn_remModFolder.TabIndex = 4;
            this.btn_remModFolder.Text = "Remove";
            this.btn_remModFolder.UseVisualStyleBackColor = true;
            this.btn_remModFolder.Click += new System.EventHandler(this.btn_remModFolder_Click);
            // 
            // btn_addModFolder
            // 
            this.btn_addModFolder.Location = new System.Drawing.Point(639, 23);
            this.btn_addModFolder.Name = "btn_addModFolder";
            this.btn_addModFolder.Size = new System.Drawing.Size(114, 23);
            this.btn_addModFolder.TabIndex = 3;
            this.btn_addModFolder.Text = "Add Mod Folder";
            this.btn_addModFolder.UseVisualStyleBackColor = true;
            this.btn_addModFolder.Click += new System.EventHandler(this.btn_addModFolder_Click);
            // 
            // checkedListBox_modFolder
            // 
            this.checkedListBox_modFolder.FormattingEnabled = true;
            this.checkedListBox_modFolder.Location = new System.Drawing.Point(24, 49);
            this.checkedListBox_modFolder.Name = "checkedListBox_modFolder";
            this.checkedListBox_modFolder.Size = new System.Drawing.Size(609, 109);
            this.checkedListBox_modFolder.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(768, 609);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Template";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.comboBox3, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(19, 15);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(256, 29);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // comboBox3
            // 
            this.comboBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(131, 4);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(121, 21);
            this.comboBox3.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "label3";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.checkBox_sameAsSource);
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Controls.Add(this.textBox_BodyTextDestination);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.textBox_BodyTextSource);
            this.tabPage4.Controls.Add(this.btn_genHarBodyTexFromVanillaTex);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(768, 609);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Gen Humanoid Alien Race Body Tex";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // checkBox_sameAsSource
            // 
            this.checkBox_sameAsSource.AutoSize = true;
            this.checkBox_sameAsSource.Location = new System.Drawing.Point(178, 77);
            this.checkBox_sameAsSource.Name = "checkBox_sameAsSource";
            this.checkBox_sameAsSource.Size = new System.Drawing.Size(104, 17);
            this.checkBox_sameAsSource.TabIndex = 5;
            this.checkBox_sameAsSource.Text = "Same as Source";
            this.checkBox_sameAsSource.UseVisualStyleBackColor = true;
            this.checkBox_sameAsSource.CheckedChanged += new System.EventHandler(this.checkBox_sameAsSource_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(157, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Destination, HAR Tex Location:";
            // 
            // textBox_BodyTextDestination
            // 
            this.textBox_BodyTextDestination.Location = new System.Drawing.Point(18, 94);
            this.textBox_BodyTextDestination.Name = "textBox_BodyTextDestination";
            this.textBox_BodyTextDestination.Size = new System.Drawing.Size(744, 20);
            this.textBox_BodyTextDestination.TabIndex = 3;
            this.textBox_BodyTextDestination.TextChanged += new System.EventHandler(this.textBox_BodyTextDestination_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Source, Vanilla Tex Location:";
            // 
            // textBox_BodyTextSource
            // 
            this.textBox_BodyTextSource.Location = new System.Drawing.Point(18, 35);
            this.textBox_BodyTextSource.Name = "textBox_BodyTextSource";
            this.textBox_BodyTextSource.Size = new System.Drawing.Size(744, 20);
            this.textBox_BodyTextSource.TabIndex = 1;
            this.textBox_BodyTextSource.TextChanged += new System.EventHandler(this.textBox_BodyTextSource_TextChanged);
            // 
            // btn_genHarBodyTexFromVanillaTex
            // 
            this.btn_genHarBodyTexFromVanillaTex.Location = new System.Drawing.Point(18, 120);
            this.btn_genHarBodyTexFromVanillaTex.Name = "btn_genHarBodyTexFromVanillaTex";
            this.btn_genHarBodyTexFromVanillaTex.Size = new System.Drawing.Size(744, 23);
            this.btn_genHarBodyTexFromVanillaTex.TabIndex = 0;
            this.btn_genHarBodyTexFromVanillaTex.Text = "Gen HAR Tex From Vanilla Tex";
            this.btn_genHarBodyTexFromVanillaTex.UseVisualStyleBackColor = true;
            this.btn_genHarBodyTexFromVanillaTex.Click += new System.EventHandler(this.btn_genHarBodyTexFromVanillaTex_Click);
            // 
            // textBox_rootModFolder
            // 
            this.textBox_rootModFolder.Location = new System.Drawing.Point(122, 23);
            this.textBox_rootModFolder.Name = "textBox_rootModFolder";
            this.textBox_rootModFolder.Size = new System.Drawing.Size(511, 20);
            this.textBox_rootModFolder.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(30, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Root Mod Folder";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 297);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "LOG:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(19, 8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(125, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "ModsConfig.xml Location";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(124, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "ModsConfig.xml preview:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 659);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Rimworld Apparel Body Texture Gene";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_xml;
        private System.Windows.Forms.Button btn_listModLoadOrder;
        private System.Windows.Forms.TextBox textBox_log;
        private System.Windows.Forms.Button btn_getModConfig;
        private System.Windows.Forms.TextBox textBox_modConfig;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_remModFolder;
        private System.Windows.Forms.Button btn_addModFolder;
        private System.Windows.Forms.CheckedListBox checkedListBox_modFolder;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Btn_MulaiCek;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox checkBox_overwrite;
        private System.Windows.Forms.CheckBox checkBox_IncludeBodyTex;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox comboBox_BodyTypeDestination;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox comboBox_BodyTypeSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_UpdateBodyType;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TextBox textBox_TexDestinationPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckBox checkBox_sameAsSource;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_BodyTextDestination;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_BodyTextSource;
        private System.Windows.Forms.Button btn_genHarBodyTexFromVanillaTex;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label_FolderActiveMod;
        private System.Windows.Forms.Label label_XMLFile;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_rootModFolder;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
    }
}

