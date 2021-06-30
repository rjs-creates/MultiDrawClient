
namespace MultiDrawClient
{
    partial class ClientForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonConnection = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonColor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelThickness = new System.Windows.Forms.ToolStripLabel();
            this.toolStripAlpha = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelFramesReceived = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelFragments = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDestackAvg = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBytesReceived = new System.Windows.Forms.ToolStripLabel();
            this.timertick = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Info;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonConnection,
            this.toolStripSeparator1,
            this.toolStripButtonColor,
            this.toolStripSeparator2,
            this.toolStripLabelThickness,
            this.toolStripAlpha,
            this.toolStripSeparator3,
            this.toolStripLabelFramesReceived,
            this.toolStripSeparator4,
            this.toolStripLabelFragments,
            this.toolStripSeparator5,
            this.toolStripDestackAvg,
            this.toolStripSeparator6,
            this.toolStripBytesReceived});
            this.toolStrip1.Location = new System.Drawing.Point(0, 780);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1126, 32);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonConnection
            // 
            this.toolStripButtonConnection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonConnection.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonConnection.Image")));
            this.toolStripButtonConnection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonConnection.Name = "toolStripButtonConnection";
            this.toolStripButtonConnection.Size = new System.Drawing.Size(86, 29);
            this.toolStripButtonConnection.Text = "Connect";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // toolStripButtonColor
            // 
            this.toolStripButtonColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonColor.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonColor.Image")));
            this.toolStripButtonColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonColor.Name = "toolStripButtonColor";
            this.toolStripButtonColor.Size = new System.Drawing.Size(62, 29);
            this.toolStripButtonColor.Text = "Color";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // toolStripLabelThickness
            // 
            this.toolStripLabelThickness.Name = "toolStripLabelThickness";
            this.toolStripLabelThickness.Size = new System.Drawing.Size(107, 29);
            this.toolStripLabelThickness.Text = "Thickness:1";
            // 
            // toolStripAlpha
            // 
            this.toolStripAlpha.Name = "toolStripAlpha";
            this.toolStripAlpha.Size = new System.Drawing.Size(95, 29);
            this.toolStripAlpha.Text = "Alpha:255";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
            // 
            // toolStripLabelFramesReceived
            // 
            this.toolStripLabelFramesReceived.Name = "toolStripLabelFramesReceived";
            this.toolStripLabelFramesReceived.Size = new System.Drawing.Size(138, 29);
            this.toolStripLabelFramesReceived.Text = "Frames RX\'ed:0";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 32);
            // 
            // toolStripLabelFragments
            // 
            this.toolStripLabelFragments.Name = "toolStripLabelFragments";
            this.toolStripLabelFragments.Size = new System.Drawing.Size(114, 29);
            this.toolStripLabelFragments.Text = "Fragments:0";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 32);
            // 
            // toolStripDestackAvg
            // 
            this.toolStripDestackAvg.Name = "toolStripDestackAvg";
            this.toolStripDestackAvg.Size = new System.Drawing.Size(134, 29);
            this.toolStripDestackAvg.Text = "DeStack Avg: 0";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 32);
            // 
            // toolStripBytesReceived
            // 
            this.toolStripBytesReceived.Name = "toolStripBytesReceived";
            this.toolStripBytesReceived.Size = new System.Drawing.Size(122, 29);
            this.toolStripBytesReceived.Text = "Bytes RX\'ed:0";
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1126, 812);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ClientForm";
            this.Text = "MultiDraw Client";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonConnection;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonColor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabelThickness;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabelFramesReceived;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabelFragments;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripDestackAvg;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripLabel toolStripBytesReceived;
        private System.Windows.Forms.ToolStripLabel toolStripAlpha;
        private System.Windows.Forms.Timer timertick;
    }
}

