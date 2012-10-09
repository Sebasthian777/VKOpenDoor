using System;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace vk
{
	public partial class form_vk: System.Windows.Forms.Form
	{
		private System.ComponentModel.Container componentes = null;

		public form_vk ()
		{
			IniComponentes();
		}

		protected override void Dispose (bool disposing)
		{
			if (disposing) {
				if (!(componentes == null))
				{
					componentes.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		void IniComponentes ()
		{
			System.Resources.ResourceManager recursos = new System.Resources.ResourceManager(typeof(form_vk));

			SuspendLayout();

			ButtonCerrar = new Button();
			configButtonCerrar();
			configForm();

			ResumeLayout(false);
		}

		void configForm ()
		{
			ClientSize = new System.Drawing.Size(250,250);
			Controls.Add(ButtonCerrar);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			Name = "";
			Text = "";
			AllowTransparency = true;
			Opacity = 100.0f;
			BackColor = Color.Lime;
			TransparencyKey = Color.Lime;
		}

		private void configButtonCerrar ()
		{
			ButtonCerrar.BackColor = Color.Aqua;
			ButtonCerrar.FlatStyle = FlatStyle.Flat;
			ButtonCerrar.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, ((Byte)(0)));
			ButtonCerrar.ForeColor = Color.White;
			ButtonCerrar.Location = new Point(0,0);
			ButtonCerrar.Name = "ButtonCerrar";
			ButtonCerrar.Size = new System.Drawing.Size(30,30);
			ButtonCerrar.Text = "X";
			ButtonCerrar.Click += new EventHandler(this.ButtonCerrarClick);
		}
		[STAThread]
		static void Main ()
		{
			Application.Run(new form_vk());
		}
		private System.Windows.Forms.Button ButtonCerrar;

		void ButtonCerrarClick (object sender, EventArgs e)
		{
			this.Close();
		}
	}
}

