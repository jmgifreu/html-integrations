using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

namespace pluginwiris
{
	public class createimage : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (this.Request.Form["mml"] != null && this.Request.Form["mml"].Length > 0) 
			{
                string toSave = this.Request.Form["mml"] + "\n";
				Hashtable config = Libwiris.loadConfig(this.MapPath(Libwiris.configFile));

                foreach (string serverParam in Libwiris.imageConfigProperties)
                {
                    if (this.Request.Form[serverParam] != null)
                    {
                        config[Libwiris.imageConfigProperties[serverParam]] = this.Request.Form[serverParam];
                    }

                    if (config[Libwiris.imageConfigProperties[serverParam]] != null)
                    {
                        toSave += config[Libwiris.imageConfigProperties[serverParam]].ToString() + "\n";
                    }
                }

                if (config["wirisimagefontranges"] != null)
                {
                    string[] fontRanges = config["wirisimagefontranges"].ToString().Split(',');

                    foreach (string fontRangeName in fontRanges)
                    {
                        string fontRangeNameParsed = fontRangeName.Trim();

                        if (config[fontRangeNameParsed] != null)
                        {
                            toSave += config[fontRangeNameParsed].ToString() + "\n";
                        }
                    }
                }

				string fileName = Libwiris.md5(toSave);
				string URL =  this.Page.ResolveUrl("showimage.aspx") + "?formula=" + fileName + ".png";
				string filePath = this.MapPath(Libwiris.FormulaDirectory + "/" + fileName + ".xml");

				if (!File.Exists(filePath)) 
				{
					TextWriter file = new StreamWriter(filePath);
					file.Write(toSave);
					file.Close();
				}
				
				this.Response.Write(URL);
			}
			else 
			{
				this.Response.Write("Error: no mathml has been sended");
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
