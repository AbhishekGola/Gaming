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

/*
 CSGH: CS Glow hack
 CSRH: CS Radar hack
 CSFH: CS Flash hack
 CSTB: CS Trigger Bot
 */

namespace GameSupport
{
    public partial class GameSupport : Form
    {
        // Flash
        bool AimAssistActive = false;

        //Glow
        bool AimBotActive = false;

        // Flash
        bool AntiFlashActive = false;

        // Flash
        bool GlowActive = false;

        // Flash
        bool GodModeActive = false;

        // Flash
        bool RadarHackActive = false;

        // Flash
        bool TriggerBotActive = false;

        // Flash
        bool BunnyHopActive = false;

        public GameSupport()
        {
            InitializeComponent();
        }

        private void cbGameCSGO_CheckedChanged(object sender, EventArgs e)
        {
            if (cbGameCSGO.Checked)
            {
                pnlCSGO.Visible = true;
            }
            else
            {
                pnlCSGO.Visible = false;
                CloseAllApps();
            }
        }

        private void chbGlow_CheckedChanged(object sender, EventArgs e)
        {
            updateLabel();
            GlowActive = chbGlow != null && chbGlow.Checked;

            if (GlowActive)
            {
                StartProcess("CSGH", "Glow Hack");
            }
            else
            {
                StopProcess("CSGH", "Glow Hack");
            }
        }

        private void chbRadar_CheckedChanged(object sender, EventArgs e)
        {
            updateLabel();
            RadarHackActive = chbRadar != null && chbRadar.Checked;

            if (RadarHackActive)
            {
                StartProcess("CSRH", "Radar Hack");
            }
            else
            {
                StopProcess("CSRH", "Radar Hack");
            }
        }

        private void chbTriggerBot_CheckedChanged(object sender, EventArgs e)
        {
            updateLabel();
            TriggerBotActive = chbTriggerBot != null && chbTriggerBot.Checked;

            if (TriggerBotActive)
            {
                StartProcess("CSTB", "Trigger Bot");
            }
            else
            {
                StopProcess("CSTB", "Trigger Bot");
            }
        }

        private void chbAntiFlash_CheckedChanged(object sender, EventArgs e)
        {
            updateLabel();
            AntiFlashActive = chbAntiFlash != null && chbAntiFlash.Checked;

            if (AntiFlashActive)
            {
                StartProcess("CSFH", "Flash Hack");
            }
            else
            {
                StopProcess("CSFH", "Flash Hack");
            }

        }
        
        private void chbGodMode_CheckedChanged(object sender, EventArgs e)
        {
            updateLabel();
            GodModeActive = chbGodMode != null && chbGodMode.Checked;
            lblLastActionStatus.Text = "Not Implememted yet...";

            if (GodModeActive)
            {
            //    StartProcess("CSGM", "God mode");
            }
            else
            {
            //    StopProcess("CSGM", "God mode");
            }
        }

        private void chbAimAssist_CheckedChanged(object sender, EventArgs e)
        {
            updateLabel();
            AimAssistActive = chbAimAssist != null && chbAimAssist.Checked;

            if (AimAssistActive)
            {
                lblLastActionStatus.Text = "Not Implememted yet...";            
                //StartProcess("CSAA", "Aim Assist");
            }
            else
            {
                //StopProcess("CSAA", "Aim Assist");
            }
        }

        private void chbAimBot_CheckedChanged(object sender, EventArgs e)
        {
            updateLabel();
            AimAssistActive = chbAimAssist != null && chbAimAssist.Checked;

            if (AimBotActive)
            {
                lblLastActionStatus.Text = "Not Implememted yet...";            
                //StartProcess("CSAB", "Aim Assist");
            }
            else
            {
                //StopProcess("CSAB", "Aim Assist");
            }
        }

        private void chbBunnyHop_CheckedChanged(object sender, EventArgs e)
        {
            updateLabel();
            BunnyHopActive = chbBunnyHop != null && chbBunnyHop.Checked;

            if (BunnyHopActive)
            {
                StartProcess("CSBH", "Bunny Hop");
            }
            else
            {
                StopProcess("CSBH", "Bunny Hop");
            }
        }

        private void updateLabel()
        {
            this.lblAimAssist.Text = chbAimAssist != null && chbAimAssist.Checked ? "Enabled" : "Disabled";
            this.lblAimBot.Text = chbAimBot != null && chbAimBot.Checked ? "Enabled" : "Disabled";
            this.lblAntiFlash.Text = chbAntiFlash != null && chbAntiFlash.Checked ? "Enabled" : "Disabled";
            this.lblGodMode.Text = chbGodMode != null && chbGodMode.Checked ? "Enabled" : "Disabled";
            this.lblGLowStatus.Text = chbGlow != null && chbGlow.Checked ? "Enabled" : "Disabled";
            this.lblRadarStatus.Text = chbRadar != null && chbRadar.Checked ? "Enabled" : "Disabled";
            this.lblTriggerBot.Text = chbTriggerBot != null && chbTriggerBot.Checked ? "Enabled" : "Disabled";
        }

        private void StartProcess(string processName, string ActionName)
        {
            try
            {
                Process[] processes = null;
                try
                {
                    processes = Process.GetProcessesByName(processName);
                }
                catch
                {
                    lblLastActionStatus.Text = "No process found for action, starting Process";
                }
                if (processes == null || processes.Count() == 0)
                {
                    // Start RadarHack 
                    Process.Start(".\\" + processName + ".exe");
                }
                else
                {
                    lblLastActionStatus.Text = ActionName + " is already running.";
                }
            }
            catch (Exception ex)
            {
                lblLastActionStatus.Text = ActionName + " failed with error " + ex.Message;
            }
        }

        private void StopProcess(string processName, string ActionName)
        {
            try
            {
                Process[] processes = null;
                try
                {
                    processes = Process.GetProcessesByName(processName);
                }
                catch
                {
                    lblLastActionStatus.Text = "No process found for action, starting Process";
                }

                if (processes != null && processes.Count() > 0)
                {
                    // Start RadarHack 
                    foreach (Process process in processes)
                    {
                        process.Kill();
                    }
                }
                else
                {
                    lblLastActionStatus.Text = ActionName + " is not running or can't find background Application.";
                }
            }
            catch (Exception ex)
            {
                lblLastActionStatus.Text = ActionName + " failed with error " + ex.Message;
            }
        }

        private void GameSupport_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseAllApps();
        }

        private void CloseAllApps()
        {
            StopProcess("CSGH", "Glow Hack");
            StopProcess("CSRH", "Radar Hack");
            StopProcess("CSTB", "Trigger Bot");
            StopProcess("CSFH", "Flash Hack");
            StopProcess("CSBH", "Bunny Hop");
            // StopProcess("CSGM", "God mode");
            // StopProcess("CSAA", "Aim Assist");
            // StopProcess("CSAB", "Aim Bot");
        }
    }
}
