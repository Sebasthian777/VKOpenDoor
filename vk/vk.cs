using System;
using System.Windows;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using CustomMouseEvent;

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
			CampoTexto = new TextBox();
			configCampoTexto();

			configForm();

			ResumeLayout(false);
		}

		void configCampoTexto ()
		{
			CampoTexto.BackColor = Color.White;
			CampoTexto.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, ((Byte)(0)));
			CampoTexto.ForeColor = Color.Black;
			CampoTexto.Location = new Point(20,20);
			CampoTexto.Size = new System.Drawing.Size(120,30);
		}

		void configForm ()
		{
			ClientSize = new System.Drawing.Size(250,250);
			Controls.Add(ButtonCerrar);
			Controls.Add(CampoTexto);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			Name = "";
			Text = "";
			KeyPreview = true;

			/*AllowTransparency = true;
			Opacity = 100.0f;
			BackColor = Color.Lime;
			TransparencyKey = Color.Lime;*/

			//aca para capturar desde el formulario cuando se escribe por teclado
			//this.KeyUp += new KeyEventHandler(form_vk_KeyUp);

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
		private System.Windows.Forms.TextBox CampoTexto;

		void ButtonCerrarClick (object sender, EventArgs e)
		{
			this.Close();
		}

		void form_vk_KeyUp (object sender, KeyEventArgs e)
		{
			//y aca se lee cuando se levanta el teclado...
			CampoTexto.AppendText(e.KeyData.ToString());
		}
	}
}

