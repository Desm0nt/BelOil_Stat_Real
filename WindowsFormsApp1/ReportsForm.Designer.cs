namespace WindowsFormsApp1
{
    partial class ReportsForm
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.менюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сменитьПользователяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.закрытьПрограммуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сформировать1ПЭРToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.мастерВвода4Нормы1ТЭКToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьОтчетToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьОтчет1ТЭКToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.данныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокТопливныхРеусурсовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокПродуктовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокСотрудниковToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.списокОбъектовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.профильОрганизацииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.reoGridControl3 = new unvell.ReoGrid.ReoGridControl();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.button2 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.reoGridControl5 = new unvell.ReoGrid.ReoGridControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.button4 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.button1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.reoGridControl4 = new unvell.ReoGrid.ReoGridControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.reoGrid4 = new unvell.ReoGrid.ReoGridControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.reoGridControl2 = new unvell.ReoGrid.ReoGridControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.reoGridControl1 = new unvell.ReoGrid.ReoGridControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.CompanyBox = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.button3 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.RUPButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.POButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CompanyBox)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePicker1.Location = new System.Drawing.Point(412, 2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 23);
            this.dateTimePicker1.TabIndex = 2;
            this.dateTimePicker1.CloseUp += new System.EventHandler(this.dateTimePicker1_CloseUp);
            this.dateTimePicker1.DropDown += new System.EventHandler(this.dateTimePicker1_DropDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.менюToolStripMenuItem,
            this.отчетыToolStripMenuItem,
            this.данныеToolStripMenuItem,
            this.cToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1339, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // менюToolStripMenuItem
            // 
            this.менюToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сменитьПользователяToolStripMenuItem,
            this.закрытьПрограммуToolStripMenuItem});
            this.менюToolStripMenuItem.Name = "менюToolStripMenuItem";
            this.менюToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.менюToolStripMenuItem.Text = "&Меню";
            // 
            // сменитьПользователяToolStripMenuItem
            // 
            this.сменитьПользователяToolStripMenuItem.Name = "сменитьПользователяToolStripMenuItem";
            this.сменитьПользователяToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.сменитьПользователяToolStripMenuItem.Text = "Сменить пользователя";
            this.сменитьПользователяToolStripMenuItem.Click += new System.EventHandler(this.сменитьПользователяToolStripMenuItem_Click);
            // 
            // закрытьПрограммуToolStripMenuItem
            // 
            this.закрытьПрограммуToolStripMenuItem.Name = "закрытьПрограммуToolStripMenuItem";
            this.закрытьПрограммуToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.закрытьПрограммуToolStripMenuItem.Text = "Закрыть программу";
            this.закрытьПрограммуToolStripMenuItem.Click += new System.EventHandler(this.закрытьПрограммуToolStripMenuItem_Click);
            // 
            // отчетыToolStripMenuItem
            // 
            this.отчетыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сформировать1ПЭРToolStripMenuItem,
            this.мастерВвода4Нормы1ТЭКToolStripMenuItem,
            this.сохранитьОтчетToolStripMenuItem,
            this.сохранитьОтчет1ТЭКToolStripMenuItem});
            this.отчетыToolStripMenuItem.Image = global::WindowsFormsApp1.Properties.Resources.rep;
            this.отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            this.отчетыToolStripMenuItem.Size = new System.Drawing.Size(76, 24);
            this.отчетыToolStripMenuItem.Text = "&Отчеты";
            // 
            // сформировать1ПЭРToolStripMenuItem
            // 
            this.сформировать1ПЭРToolStripMenuItem.Name = "сформировать1ПЭРToolStripMenuItem";
            this.сформировать1ПЭРToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.сформировать1ПЭРToolStripMenuItem.Text = "Мастер данных (12-тэк + 1-пэр)";
            this.сформировать1ПЭРToolStripMenuItem.Click += new System.EventHandler(this.сформировать1ПЭРToolStripMenuItem_Click);
            // 
            // мастерВвода4Нормы1ТЭКToolStripMenuItem
            // 
            this.мастерВвода4Нормы1ТЭКToolStripMenuItem.Name = "мастерВвода4Нормы1ТЭКToolStripMenuItem";
            this.мастерВвода4Нормы1ТЭКToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.мастерВвода4Нормы1ТЭКToolStripMenuItem.Text = "Мастер ввода 4-Нормы/1-ТЭК";
            this.мастерВвода4Нормы1ТЭКToolStripMenuItem.Click += new System.EventHandler(this.мастерВвода4Нормы1ТЭКToolStripMenuItem_Click);
            // 
            // сохранитьОтчетToolStripMenuItem
            // 
            this.сохранитьОтчетToolStripMenuItem.Name = "сохранитьОтчетToolStripMenuItem";
            this.сохранитьОтчетToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.сохранитьОтчетToolStripMenuItem.Text = "Сохранить отчет 12-ТЭК";
            this.сохранитьОтчетToolStripMenuItem.Click += new System.EventHandler(this.сохранитьОтчетToolStripMenuItem_Click);
            // 
            // сохранитьОтчет1ТЭКToolStripMenuItem
            // 
            this.сохранитьОтчет1ТЭКToolStripMenuItem.Name = "сохранитьОтчет1ТЭКToolStripMenuItem";
            this.сохранитьОтчет1ТЭКToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.сохранитьОтчет1ТЭКToolStripMenuItem.Text = "Сохранить отчет 1-ТЭК";
            this.сохранитьОтчет1ТЭКToolStripMenuItem.Click += new System.EventHandler(this.сохранитьОтчет1ТЭКToolStripMenuItem_Click);
            // 
            // данныеToolStripMenuItem
            // 
            this.данныеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.списокТопливныхРеусурсовToolStripMenuItem,
            this.списокПродуктовToolStripMenuItem,
            this.списокСотрудниковToolStripMenuItem,
            this.toolStripSeparator1,
            this.списокОбъектовToolStripMenuItem});
            this.данныеToolStripMenuItem.Name = "данныеToolStripMenuItem";
            this.данныеToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.данныеToolStripMenuItem.Text = "&Данные";
            // 
            // списокТопливныхРеусурсовToolStripMenuItem
            // 
            this.списокТопливныхРеусурсовToolStripMenuItem.Image = global::WindowsFormsApp1.Properties.Resources.cb9pk_llwrs;
            this.списокТопливныхРеусурсовToolStripMenuItem.Name = "списокТопливныхРеусурсовToolStripMenuItem";
            this.списокТопливныхРеусурсовToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.списокТопливныхРеусурсовToolStripMenuItem.Size = new System.Drawing.Size(315, 22);
            this.списокТопливныхРеусурсовToolStripMenuItem.Text = "Список Топливо и отходы производства";
            // 
            // списокПродуктовToolStripMenuItem
            // 
            this.списокПродуктовToolStripMenuItem.Image = global::WindowsFormsApp1.Properties.Resources.cbl76_u0ijo;
            this.списокПродуктовToolStripMenuItem.Name = "списокПродуктовToolStripMenuItem";
            this.списокПродуктовToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.списокПродуктовToolStripMenuItem.Size = new System.Drawing.Size(315, 22);
            this.списокПродуктовToolStripMenuItem.Text = "Виды продукции (работ, услуг)";
            this.списокПродуктовToolStripMenuItem.Click += new System.EventHandler(this.списокПродуктовToolStripMenuItem_Click);
            // 
            // списокСотрудниковToolStripMenuItem
            // 
            this.списокСотрудниковToolStripMenuItem.Image = global::WindowsFormsApp1.Properties.Resources.cbx2p_h0eqv;
            this.списокСотрудниковToolStripMenuItem.Name = "списокСотрудниковToolStripMenuItem";
            this.списокСотрудниковToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.списокСотрудниковToolStripMenuItem.Size = new System.Drawing.Size(315, 22);
            this.списокСотрудниковToolStripMenuItem.Text = "Сотрудники";
            this.списокСотрудниковToolStripMenuItem.Click += new System.EventHandler(this.списокСотрудниковToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(312, 6);
            // 
            // списокОбъектовToolStripMenuItem
            // 
            this.списокОбъектовToolStripMenuItem.Image = global::WindowsFormsApp1.Properties.Resources.icoprod;
            this.списокОбъектовToolStripMenuItem.Name = "списокОбъектовToolStripMenuItem";
            this.списокОбъектовToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.списокОбъектовToolStripMenuItem.Size = new System.Drawing.Size(315, 22);
            this.списокОбъектовToolStripMenuItem.Text = "Единицы измерения (ОКЕИ)";
            // 
            // cToolStripMenuItem
            // 
            this.cToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.профильОрганизацииToolStripMenuItem});
            this.cToolStripMenuItem.Name = "cToolStripMenuItem";
            this.cToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.cToolStripMenuItem.Text = "&Сервис";
            // 
            // профильОрганизацииToolStripMenuItem
            // 
            this.профильОрганизацииToolStripMenuItem.Name = "профильОрганизацииToolStripMenuItem";
            this.профильОрганизацииToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.профильОрганизацииToolStripMenuItem.Text = "Профиль организации";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(65)))), ((int)(((byte)(135)))));
            this.label1.Location = new System.Drawing.Point(313, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Выбор периода:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.reoGridControl3);
            this.panel1.Location = new System.Drawing.Point(1120, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 5;
            this.panel1.Visible = false;
            // 
            // reoGridControl3
            // 
            this.reoGridControl3.BackColor = System.Drawing.Color.White;
            this.reoGridControl3.ColumnHeaderContextMenuStrip = null;
            this.reoGridControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControl3.LeadHeaderContextMenuStrip = null;
            this.reoGridControl3.Location = new System.Drawing.Point(0, 0);
            this.reoGridControl3.Name = "reoGridControl3";
            this.reoGridControl3.RowHeaderContextMenuStrip = null;
            this.reoGridControl3.Script = null;
            this.reoGridControl3.SheetTabContextMenuStrip = null;
            this.reoGridControl3.SheetTabNewButtonVisible = true;
            this.reoGridControl3.SheetTabVisible = true;
            this.reoGridControl3.SheetTabWidth = 60;
            this.reoGridControl3.ShowScrollEndSpacing = true;
            this.reoGridControl3.Size = new System.Drawing.Size(200, 100);
            this.reoGridControl3.TabIndex = 1;
            this.reoGridControl3.Text = "reoGridControl3";
            this.reoGridControl3.Visible = false;
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(254)))));
            this.tabPage6.Controls.Add(this.button2);
            this.tabPage6.Controls.Add(this.reoGridControl5);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(1307, 613);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "1-ТЭК";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 24);
            this.button2.TabIndex = 3;
            this.button2.Values.Text = "Сформировать";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // reoGridControl5
            // 
            this.reoGridControl5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reoGridControl5.BackColor = System.Drawing.Color.White;
            this.reoGridControl5.ColumnHeaderContextMenuStrip = null;
            this.reoGridControl5.LeadHeaderContextMenuStrip = null;
            this.reoGridControl5.Location = new System.Drawing.Point(3, 34);
            this.reoGridControl5.Name = "reoGridControl5";
            this.reoGridControl5.RowHeaderContextMenuStrip = null;
            this.reoGridControl5.Script = null;
            this.reoGridControl5.SheetTabContextMenuStrip = null;
            this.reoGridControl5.SheetTabNewButtonVisible = true;
            this.reoGridControl5.SheetTabVisible = true;
            this.reoGridControl5.SheetTabWidth = 60;
            this.reoGridControl5.ShowScrollEndSpacing = true;
            this.reoGridControl5.Size = new System.Drawing.Size(1304, 576);
            this.reoGridControl5.TabIndex = 2;
            this.reoGridControl5.Text = "reoGridControl5";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(254)))));
            this.tabPage4.Controls.Add(this.button4);
            this.tabPage4.Controls.Add(this.button1);
            this.tabPage4.Controls.Add(this.reoGridControl4);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1307, 613);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "4-норма";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(114, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(82, 24);
            this.button4.TabIndex = 4;
            this.button4.Values.Text = "Пересчитать";
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 24);
            this.button1.TabIndex = 3;
            this.button1.Values.Text = "Сформировать";
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // reoGridControl4
            // 
            this.reoGridControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reoGridControl4.BackColor = System.Drawing.Color.White;
            this.reoGridControl4.ColumnHeaderContextMenuStrip = null;
            this.reoGridControl4.LeadHeaderContextMenuStrip = null;
            this.reoGridControl4.Location = new System.Drawing.Point(3, 33);
            this.reoGridControl4.Name = "reoGridControl4";
            this.reoGridControl4.RowHeaderContextMenuStrip = null;
            this.reoGridControl4.Script = null;
            this.reoGridControl4.SheetTabContextMenuStrip = null;
            this.reoGridControl4.SheetTabNewButtonVisible = true;
            this.reoGridControl4.SheetTabVisible = true;
            this.reoGridControl4.SheetTabWidth = 60;
            this.reoGridControl4.ShowScrollEndSpacing = true;
            this.reoGridControl4.Size = new System.Drawing.Size(1304, 577);
            this.reoGridControl4.TabIndex = 2;
            this.reoGridControl4.Text = "reoGridControl4";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(254)))));
            this.tabPage3.Controls.Add(this.reoGrid4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1307, 613);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Приложение к 12-ТЭК";
            // 
            // reoGrid4
            // 
            this.reoGrid4.BackColor = System.Drawing.Color.White;
            this.reoGrid4.ColumnHeaderContextMenuStrip = null;
            this.reoGrid4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGrid4.LeadHeaderContextMenuStrip = null;
            this.reoGrid4.Location = new System.Drawing.Point(3, 3);
            this.reoGrid4.Name = "reoGrid4";
            this.reoGrid4.RowHeaderContextMenuStrip = null;
            this.reoGrid4.Script = null;
            this.reoGrid4.SheetTabContextMenuStrip = null;
            this.reoGrid4.SheetTabNewButtonVisible = true;
            this.reoGrid4.SheetTabVisible = true;
            this.reoGrid4.SheetTabWidth = 60;
            this.reoGrid4.ShowScrollEndSpacing = true;
            this.reoGrid4.Size = new System.Drawing.Size(1301, 607);
            this.reoGrid4.TabIndex = 1;
            this.reoGrid4.Text = "reoGridControl4";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(254)))));
            this.tabPage2.Controls.Add(this.reoGridControl2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1307, 613);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "12-ТЭК";
            // 
            // reoGridControl2
            // 
            this.reoGridControl2.BackColor = System.Drawing.Color.White;
            this.reoGridControl2.ColumnHeaderContextMenuStrip = null;
            this.reoGridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControl2.LeadHeaderContextMenuStrip = null;
            this.reoGridControl2.Location = new System.Drawing.Point(3, 3);
            this.reoGridControl2.Name = "reoGridControl2";
            this.reoGridControl2.RowHeaderContextMenuStrip = null;
            this.reoGridControl2.Script = null;
            this.reoGridControl2.SheetTabContextMenuStrip = null;
            this.reoGridControl2.SheetTabNewButtonVisible = true;
            this.reoGridControl2.SheetTabVisible = true;
            this.reoGridControl2.SheetTabWidth = 60;
            this.reoGridControl2.ShowScrollEndSpacing = true;
            this.reoGridControl2.Size = new System.Drawing.Size(1301, 607);
            this.reoGridControl2.TabIndex = 0;
            this.reoGridControl2.Text = "reoGridControl2";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(254)))));
            this.tabPage1.Controls.Add(this.reoGridControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1307, 613);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "1-ПЭР";
            // 
            // reoGridControl1
            // 
            this.reoGridControl1.BackColor = System.Drawing.Color.White;
            this.reoGridControl1.ColumnHeaderContextMenuStrip = null;
            this.reoGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControl1.LeadHeaderContextMenuStrip = null;
            this.reoGridControl1.Location = new System.Drawing.Point(3, 3);
            this.reoGridControl1.Name = "reoGridControl1";
            this.reoGridControl1.RowHeaderContextMenuStrip = null;
            this.reoGridControl1.Script = null;
            this.reoGridControl1.SheetTabContextMenuStrip = null;
            this.reoGridControl1.SheetTabNewButtonVisible = true;
            this.reoGridControl1.SheetTabVisible = true;
            this.reoGridControl1.SheetTabWidth = 60;
            this.reoGridControl1.ShowScrollEndSpacing = true;
            this.reoGridControl1.Size = new System.Drawing.Size(1301, 607);
            this.reoGridControl1.TabIndex = 0;
            this.reoGridControl1.Text = "reoGridControl1";
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
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControl1.Location = new System.Drawing.Point(12, 33);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1315, 639);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // CompanyBox
            // 
            this.CompanyBox.DropDownWidth = 185;
            this.CompanyBox.FormattingEnabled = true;
            this.CompanyBox.Location = new System.Drawing.Point(629, 3);
            this.CompanyBox.Name = "CompanyBox";
            this.CompanyBox.Size = new System.Drawing.Size(185, 21);
            this.CompanyBox.StateCommon.ComboBox.Content.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.CompanyBox.TabIndex = 6;
            this.CompanyBox.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(824, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 24);
            this.button3.TabIndex = 7;
            this.button3.Values.Text = "Выбрать";
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // RUPButton
            // 
            this.RUPButton.Enabled = false;
            this.RUPButton.Location = new System.Drawing.Point(905, 2);
            this.RUPButton.Name = "RUPButton";
            this.RUPButton.Size = new System.Drawing.Size(75, 24);
            this.RUPButton.TabIndex = 8;
            this.RUPButton.Values.Text = "РУП";
            this.RUPButton.Visible = false;
            this.RUPButton.Click += new System.EventHandler(this.RUPButton_Click);
            // 
            // POButton
            // 
            this.POButton.Enabled = false;
            this.POButton.Location = new System.Drawing.Point(986, 2);
            this.POButton.Name = "POButton";
            this.POButton.Size = new System.Drawing.Size(75, 24);
            this.POButton.TabIndex = 9;
            this.POButton.Values.Text = "ПО";
            this.POButton.Visible = false;
            this.POButton.Click += new System.EventHandler(this.POButton_Click);
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Office2007Blue;
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(1339, 684);
            this.Controls.Add(this.POButton);
            this.Controls.Add(this.RUPButton);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.CompanyBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ReportsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main1PerForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CompanyBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem менюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сменитьПользователяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem закрытьПрограммуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сформировать1ПЭРToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьОтчетToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem данныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокПродуктовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокОбъектовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокТопливныхРеусурсовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокСотрудниковToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private unvell.ReoGrid.ReoGridControl reoGridControl3;
        private System.Windows.Forms.TabPage tabPage6;
        private ComponentFactory.Krypton.Toolkit.KryptonButton button2;
        public unvell.ReoGrid.ReoGridControl reoGridControl5;
        private System.Windows.Forms.TabPage tabPage4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton button1;
        public unvell.ReoGrid.ReoGridControl reoGridControl4;
        private System.Windows.Forms.TabPage tabPage3;
        public unvell.ReoGrid.ReoGridControl reoGrid4;
        private System.Windows.Forms.TabPage tabPage2;
        private unvell.ReoGrid.ReoGridControl reoGridControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private unvell.ReoGrid.ReoGridControl reoGridControl1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripMenuItem сохранитьОтчет1ТЭКToolStripMenuItem;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox CompanyBox;
        private ComponentFactory.Krypton.Toolkit.KryptonButton button3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton RUPButton;
        private ComponentFactory.Krypton.Toolkit.KryptonButton POButton;
        private System.Windows.Forms.ToolStripMenuItem мастерВвода4Нормы1ТЭКToolStripMenuItem;
        private ComponentFactory.Krypton.Toolkit.KryptonButton button4;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem профильОрганизацииToolStripMenuItem;
    }
}