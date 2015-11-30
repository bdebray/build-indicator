using System;
using System.ComponentModel;
using System.Windows.Forms;
using BuildIndicatorSimulator.BuildIndicatorServiceLibaryReference;

namespace BuildIndicatorSimulator
{
    public partial class Form1 : Form
    {
        private readonly BuildIndicatorClient _serviceProxy = new BuildIndicatorClient();
        private readonly BackgroundWorker _statusUpdateWorker = new BackgroundWorker();

        public Form1()
        {
            InitializeComponent();
        }

        public void UpdateSourceDetails()
        {
            try
            {
                txtSource.Text = _serviceProxy.GetCurrentSource();
            }
            catch (Exception e)
            {
                txtSource.Text = @"Unable to get current Source: " + e.Message;
            }
        }

        public void UpdateServiceState()
        {
            try
            {
                txtServiceState.Text = _serviceProxy.GetBuildServiceState();
            }
            catch
            {
                txtServiceState.Text = @"Unable to get current Service State";
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateServiceState();
            UpdateSourceDetails();
            _statusUpdateWorker.DoWork += UpdateStateInBackground;
        }

        private void btnOn_Click(object sender, EventArgs e)
        {
            var shouldFlash = radioBuildingTrue.Checked;

            LightColor colorToChange;

            if (radioGreen.Checked)
            {
                colorToChange = LightColor.Green;
            }
            else if (radioYellow.Checked)
            {
                colorToChange = LightColor.Yellow;
            }
            else if (radioRed.Checked)
            {
                colorToChange = LightColor.Red;
            }
            else
            {
                MessageBox.Show(@"Invalid State Set.");
                return;
            }

            if (shouldFlash)
            {
                _serviceProxy.TurnOnFlash(colorToChange);
            }
            else
            {
                _serviceProxy.TurnOn(colorToChange); 
            }
        }

        private void btnOff_Click(object sender, EventArgs e)
        {
            _serviceProxy.TurnOff(LightColor.Green);
            _serviceProxy.TurnOff(LightColor.Yellow);
            _serviceProxy.TurnOff(LightColor.Red);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            _serviceProxy.ResetBuildSource();
        }

        private void btnUpdateState_Click(object sender, EventArgs e)
        {
            _statusUpdateWorker.RunWorkerAsync();
        }

        private void btnUrlSubmit_Click(object sender, EventArgs e)
        {
            var url = txtUrl.Text;

            if (!txtUrl.Text.EndsWith("/api/xml"))
            {
                url += "api/xml";
            }

            _serviceProxy.GetBuildState(url);
        }

        private void UpdateStateInBackground(object sender, EventArgs e)
        {
            UpdateSourceDetails();
            UpdateServiceState();
        }

    }
}
