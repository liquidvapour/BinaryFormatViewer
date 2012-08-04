/*
 * Created by SharpDevelop.
 * User: ra-el
 * Date: 19/08/2010
 * Time: 19:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace WinFormsBSV
{
    partial class MainForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        
        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
        	this.panel1 = new System.Windows.Forms.Panel();
        	this.button1 = new System.Windows.Forms.Button();
        	this.label1 = new System.Windows.Forms.Label();
        	this.textBox1 = new System.Windows.Forms.TextBox();
        	this.splitContainer1 = new System.Windows.Forms.SplitContainer();
        	this.treeView = new System.Windows.Forms.TreeView();
        	this.panel1.SuspendLayout();
        	this.splitContainer1.Panel1.SuspendLayout();
        	this.splitContainer1.SuspendLayout();
        	this.SuspendLayout();
        	// 
        	// panel1
        	// 
        	this.panel1.Controls.Add(this.button1);
        	this.panel1.Controls.Add(this.label1);
        	this.panel1.Controls.Add(this.textBox1);
        	this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
        	this.panel1.Location = new System.Drawing.Point(0, 0);
        	this.panel1.Name = "panel1";
        	this.panel1.Size = new System.Drawing.Size(392, 51);
        	this.panel1.TabIndex = 0;
        	// 
        	// button1
        	// 
        	this.button1.Location = new System.Drawing.Point(305, 10);
        	this.button1.Name = "button1";
        	this.button1.Size = new System.Drawing.Size(75, 23);
        	this.button1.TabIndex = 2;
        	this.button1.Text = "Open";
        	this.button1.UseVisualStyleBackColor = true;
        	this.button1.Click += new System.EventHandler(this.Button1Click);
        	// 
        	// label1
        	// 
        	this.label1.Location = new System.Drawing.Point(12, 15);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(42, 23);
        	this.label1.TabIndex = 1;
        	this.label1.Text = "File:";
        	// 
        	// textBox1
        	// 
        	this.textBox1.Location = new System.Drawing.Point(97, 12);
        	this.textBox1.Name = "textBox1";
        	this.textBox1.Size = new System.Drawing.Size(202, 20);
        	this.textBox1.TabIndex = 0;
        	// 
        	// splitContainer1
        	// 
        	this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.splitContainer1.Location = new System.Drawing.Point(0, 51);
        	this.splitContainer1.Name = "splitContainer1";
        	// 
        	// splitContainer1.Panel1
        	// 
        	this.splitContainer1.Panel1.Controls.Add(this.treeView);
        	this.splitContainer1.Size = new System.Drawing.Size(392, 211);
        	this.splitContainer1.SplitterDistance = 130;
        	this.splitContainer1.TabIndex = 1;
        	// 
        	// treeView
        	// 
        	this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.treeView.Location = new System.Drawing.Point(0, 0);
        	this.treeView.Name = "treeView";
        	this.treeView.Size = new System.Drawing.Size(130, 211);
        	this.treeView.TabIndex = 0;
        	// 
        	// MainForm
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(392, 262);
        	this.Controls.Add(this.splitContainer1);
        	this.Controls.Add(this.panel1);
        	this.Name = "MainForm";
        	this.Text = "WinFormsBSV";
        	this.panel1.ResumeLayout(false);
        	this.panel1.PerformLayout();
        	this.splitContainer1.Panel1.ResumeLayout(false);
        	this.splitContainer1.ResumeLayout(false);
        	this.ResumeLayout(false);
        }
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
    }
}
