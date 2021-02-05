﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Analogy.DataTypes;
using Analogy.Managers;

namespace Analogy.ApplicationSettings
{
    public partial class UpdateSettingsUC : DevExpress.XtraEditors.XtraUserControl
    {
        private UserSettingsManager Settings { get; } = UserSettingsManager.UserSettings;

        public UpdateSettingsUC()
        {
            InitializeComponent();

        }

        private void UpdateSettingsUC_Load(object sender, EventArgs e)
        {
            LoadSettings();
            SetupEventsHandlers();
        }

        private void SetupEventsHandlers()
        {
            cbUpdates.SelectedIndexChanged += (s, e) =>
            {
                var options = typeof(UpdateMode).GetDisplayValues();
                UpdateManager.Instance.UpdateMode = (UpdateMode) Enum.Parse(typeof(UpdateMode),
                    options.Single(k => k.Value == cbUpdates.SelectedItem.ToString()).Key, true);
            };
        }

        private void LoadSettings()
        {
            cbUpdates.Properties.Items.AddRange(typeof(UpdateMode).GetDisplayValues().Values);
            cbUpdates.SelectedItem = UpdateManager.Instance.UpdateMode.GetDisplay();
            if (AnalogyNonPersistSettings.Instance.DisableUpdatesByDataProvidersOverrides)
            {
                lblDisableUpdates.Visible = true;
                cbUpdates.Enabled = false;
            }
        }
    }
}
