namespace BuildIndicatorSimulator
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
            this.btnOn = new System.Windows.Forms.Button();
            this.btnOff = new System.Windows.Forms.Button();
            this.radioGreen = new System.Windows.Forms.RadioButton();
            this.radioYellow = new System.Windows.Forms.RadioButton();
            this.radioRed = new System.Windows.Forms.RadioButton();
            this.radioBuildingTrue = new System.Windows.Forms.RadioButton();
            this.radioBuildingFalse = new System.Windows.Forms.RadioButton();
            this.grpBuilding = new System.Windows.Forms.GroupBox();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServiceState = new System.Windows.Forms.TextBox();
            this.btnUpdateState = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnUrlSubmit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grpBuilding.SuspendLayout();
            this.grpStatus.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOn
            // 
            this.btnOn.Location = new System.Drawing.Point(252, 135);
            this.btnOn.Name = "btnOn";
            this.btnOn.Size = new System.Drawing.Size(75, 23);
            this.btnOn.TabIndex = 0;
            this.btnOn.Text = "Submit";
            this.btnOn.UseVisualStyleBackColor = true;
            this.btnOn.Click += new System.EventHandler(this.btnOn_Click);
            // 
            // btnOff
            // 
            this.btnOff.Location = new System.Drawing.Point(204, 478);
            this.btnOff.Name = "btnOff";
            this.btnOff.Size = new System.Drawing.Size(75, 23);
            this.btnOff.TabIndex = 1;
            this.btnOff.Text = "Off";
            this.btnOff.UseVisualStyleBackColor = true;
            this.btnOff.Click += new System.EventHandler(this.btnOff_Click);
            // 
            // radioGreen
            // 
            this.radioGreen.AutoSize = true;
            this.radioGreen.Checked = true;
            this.radioGreen.Location = new System.Drawing.Point(6, 19);
            this.radioGreen.Name = "radioGreen";
            this.radioGreen.Size = new System.Drawing.Size(106, 17);
            this.radioGreen.TabIndex = 2;
            this.radioGreen.TabStop = true;
            this.radioGreen.Text = "Build Succeeded";
            this.radioGreen.UseVisualStyleBackColor = true;
            // 
            // radioYellow
            // 
            this.radioYellow.AutoSize = true;
            this.radioYellow.Location = new System.Drawing.Point(6, 42);
            this.radioYellow.Name = "radioYellow";
            this.radioYellow.Size = new System.Drawing.Size(93, 17);
            this.radioYellow.TabIndex = 3;
            this.radioYellow.Text = "Build Unstable";
            this.radioYellow.UseVisualStyleBackColor = true;
            // 
            // radioRed
            // 
            this.radioRed.AutoSize = true;
            this.radioRed.Location = new System.Drawing.Point(6, 65);
            this.radioRed.Name = "radioRed";
            this.radioRed.Size = new System.Drawing.Size(79, 17);
            this.radioRed.TabIndex = 4;
            this.radioRed.Text = "Build Failed";
            this.radioRed.UseVisualStyleBackColor = true;
            // 
            // radioBuildingTrue
            // 
            this.radioBuildingTrue.AutoSize = true;
            this.radioBuildingTrue.Location = new System.Drawing.Point(21, 23);
            this.radioBuildingTrue.Name = "radioBuildingTrue";
            this.radioBuildingTrue.Size = new System.Drawing.Size(43, 17);
            this.radioBuildingTrue.TabIndex = 5;
            this.radioBuildingTrue.Text = "Yes";
            this.radioBuildingTrue.UseVisualStyleBackColor = true;
            // 
            // radioBuildingFalse
            // 
            this.radioBuildingFalse.AutoSize = true;
            this.radioBuildingFalse.Checked = true;
            this.radioBuildingFalse.Location = new System.Drawing.Point(21, 46);
            this.radioBuildingFalse.Name = "radioBuildingFalse";
            this.radioBuildingFalse.Size = new System.Drawing.Size(39, 17);
            this.radioBuildingFalse.TabIndex = 6;
            this.radioBuildingFalse.TabStop = true;
            this.radioBuildingFalse.Text = "No";
            this.radioBuildingFalse.UseVisualStyleBackColor = true;
            // 
            // grpBuilding
            // 
            this.grpBuilding.Controls.Add(this.radioBuildingFalse);
            this.grpBuilding.Controls.Add(this.radioBuildingTrue);
            this.grpBuilding.Location = new System.Drawing.Point(168, 29);
            this.grpBuilding.Name = "grpBuilding";
            this.grpBuilding.Size = new System.Drawing.Size(159, 100);
            this.grpBuilding.TabIndex = 7;
            this.grpBuilding.TabStop = false;
            this.grpBuilding.Text = "Currently Building";
            // 
            // grpStatus
            // 
            this.grpStatus.Controls.Add(this.radioGreen);
            this.grpStatus.Controls.Add(this.radioYellow);
            this.grpStatus.Controls.Add(this.radioRed);
            this.grpStatus.Location = new System.Drawing.Point(6, 29);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(156, 100);
            this.grpStatus.TabIndex = 8;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "Build Status";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(288, 478);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 9;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "State:";
            // 
            // txtServiceState
            // 
            this.txtServiceState.Enabled = false;
            this.txtServiceState.Location = new System.Drawing.Point(49, 19);
            this.txtServiceState.Name = "txtServiceState";
            this.txtServiceState.Size = new System.Drawing.Size(278, 20);
            this.txtServiceState.TabIndex = 11;
            // 
            // btnUpdateState
            // 
            this.btnUpdateState.Location = new System.Drawing.Point(252, 139);
            this.btnUpdateState.Name = "btnUpdateState";
            this.btnUpdateState.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateState.TabIndex = 12;
            this.btnUpdateState.Text = "Update";
            this.btnUpdateState.UseVisualStyleBackColor = true;
            this.btnUpdateState.Click += new System.EventHandler(this.btnUpdateState_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grpStatus);
            this.groupBox1.Controls.Add(this.btnOn);
            this.groupBox1.Controls.Add(this.grpBuilding);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 172);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Test Example";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnUrlSubmit);
            this.groupBox2.Controls.Add(this.txtUrl);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(15, 190);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(348, 109);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Jenkins Build";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "URL:";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(49, 20);
            this.txtUrl.Multiline = true;
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(278, 54);
            this.txtUrl.TabIndex = 1;
            // 
            // btnUrlSubmit
            // 
            this.btnUrlSubmit.Location = new System.Drawing.Point(252, 80);
            this.btnUrlSubmit.Name = "btnUrlSubmit";
            this.btnUrlSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnUrlSubmit.TabIndex = 2;
            this.btnUrlSubmit.Text = "Submit";
            this.btnUrlSubmit.UseVisualStyleBackColor = true;
            this.btnUrlSubmit.Click += new System.EventHandler(this.btnUrlSubmit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Source:";
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(49, 64);
            this.txtSource.Multiline = true;
            this.txtSource.Name = "txtSource";
            this.txtSource.ReadOnly = true;
            this.txtSource.Size = new System.Drawing.Size(278, 69);
            this.txtSource.TabIndex = 16;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtSource);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtServiceState);
            this.groupBox3.Controls.Add(this.btnUpdateState);
            this.groupBox3.Location = new System.Drawing.Point(15, 305);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(348, 167);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Service Details";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 508);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnOff);
            this.Name = "Form1";
            this.Text = "Build Service Simulator";
            this.grpBuilding.ResumeLayout(false);
            this.grpBuilding.PerformLayout();
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOn;
        private System.Windows.Forms.Button btnOff;
        private System.Windows.Forms.RadioButton radioGreen;
        private System.Windows.Forms.RadioButton radioYellow;
        private System.Windows.Forms.RadioButton radioRed;
        private System.Windows.Forms.RadioButton radioBuildingTrue;
        private System.Windows.Forms.RadioButton radioBuildingFalse;
        private System.Windows.Forms.GroupBox grpBuilding;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServiceState;
        private System.Windows.Forms.Button btnUpdateState;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnUrlSubmit;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

