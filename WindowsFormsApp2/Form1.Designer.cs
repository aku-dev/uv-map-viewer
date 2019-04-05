namespace WindowsFormsApp2
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBoxShowVertex = new System.Windows.Forms.CheckBox();
            this.textBoxZoom = new System.Windows.Forms.TextBox();
            this.panelOutPictureBox = new System.Windows.Forms.Panel();
            this.checkBoxShowEdge = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelOutPictureBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.button1.Location = new System.Drawing.Point(530, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open file";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(92)))), ((int)(((byte)(92)))));
            this.pictureBox1.Location = new System.Drawing.Point(13, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 512);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.textBox1.Location = new System.Drawing.Point(12, 554);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(593, 99);
            this.textBox1.TabIndex = 2;
            // 
            // checkBoxShowVertex
            // 
            this.checkBoxShowVertex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxShowVertex.AutoSize = true;
            this.checkBoxShowVertex.Location = new System.Drawing.Point(58, 530);
            this.checkBoxShowVertex.Name = "checkBoxShowVertex";
            this.checkBoxShowVertex.Size = new System.Drawing.Size(56, 17);
            this.checkBoxShowVertex.TabIndex = 3;
            this.checkBoxShowVertex.Text = "Vertex";
            this.checkBoxShowVertex.UseVisualStyleBackColor = true;
            this.checkBoxShowVertex.CheckedChanged += new System.EventHandler(this.checkBoxShowVertex_CheckedChanged);
            // 
            // textBoxZoom
            // 
            this.textBoxZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxZoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.textBoxZoom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.textBoxZoom.Location = new System.Drawing.Point(12, 530);
            this.textBoxZoom.Name = "textBoxZoom";
            this.textBoxZoom.ReadOnly = true;
            this.textBoxZoom.Size = new System.Drawing.Size(40, 20);
            this.textBoxZoom.TabIndex = 4;
            this.textBoxZoom.Text = "100%";
            // 
            // panelOutPictureBox
            // 
            this.panelOutPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOutPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.panelOutPictureBox.Controls.Add(this.pictureBox1);
            this.panelOutPictureBox.Location = new System.Drawing.Point(12, 12);
            this.panelOutPictureBox.Name = "panelOutPictureBox";
            this.panelOutPictureBox.Size = new System.Drawing.Size(512, 512);
            this.panelOutPictureBox.TabIndex = 5;
            // 
            // checkBoxShowEdge
            // 
            this.checkBoxShowEdge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxShowEdge.AutoSize = true;
            this.checkBoxShowEdge.Checked = true;
            this.checkBoxShowEdge.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowEdge.Location = new System.Drawing.Point(120, 530);
            this.checkBoxShowEdge.Name = "checkBoxShowEdge";
            this.checkBoxShowEdge.Size = new System.Drawing.Size(51, 17);
            this.checkBoxShowEdge.TabIndex = 6;
            this.checkBoxShowEdge.Text = "Edge";
            this.checkBoxShowEdge.UseVisualStyleBackColor = true;
            this.checkBoxShowEdge.CheckedChanged += new System.EventHandler(this.checkBoxShowEdge_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(613, 663);
            this.Controls.Add(this.checkBoxShowEdge);
            this.Controls.Add(this.panelOutPictureBox);
            this.Controls.Add(this.textBoxZoom);
            this.Controls.Add(this.checkBoxShowVertex);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "UV Mapper";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelOutPictureBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBoxShowVertex;
        private System.Windows.Forms.TextBox textBoxZoom;
        private System.Windows.Forms.Panel panelOutPictureBox;
        private System.Windows.Forms.CheckBox checkBoxShowEdge;
        private System.Windows.Forms.Timer timer1;
    }
}

