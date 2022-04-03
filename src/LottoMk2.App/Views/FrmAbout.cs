using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LottoMk2
{
    public partial class FrmAbout : Form
    {
        public FrmAbout()
        {
            InitializeComponent();

            this.lnkTobbonkr.Click += (s, e) =>
            {
                string url = "http://bbon.kr/lottomk2/";
                this.GotoUrl(url);
            };
            this.lnkToTwitter.Click += (s, e) =>
            {
                string url = "http://twitter.com/bbonkr";
                this.GotoUrl(url);
            };
            this.lnkToFacebook.Click += (s, e) =>
            {
                string url = "http://facebook.com/bbonkr";
                this.GotoUrl(url);
            };
            this.lnkToGitHub.Click += (s, e) =>
            {
                string url = "http://github.com/bbonkr";
                this.GotoUrl(url);
            };

            this.btnClose.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; };

            var version = this.GetType().Assembly.GetName().Version?.ToString();
            if (!string.IsNullOrEmpty(version))
            {
                this.Text = $"정보 v{version}";
            }
        }

        private void GotoUrl(string url)
        {
            Process.Start(url);
        }
    }
}